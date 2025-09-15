using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

using Server.DTOs;
using Server.Options;
using IyzipayOptions = Iyzipay.Options;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/payments")] 
    public class PaymentsController : ControllerBase
    {
        private readonly IyzicoOptions _opts;
        public PaymentsController(IOptions<IyzicoOptions> opts) => _opts = opts.Value;

        [HttpPost("checkout/init")]
        public async Task<ActionResult<CheckoutInitResponseDto>> InitializeCheckout([FromBody] CheckoutInitRequestDto dto)
        {
            var items = dto.Items ?? new List<CheckoutItemDto>();
            var price = items.Sum(x => x.Price);
            var paidPrice = price;

            var iyzOpts = new IyzipayOptions
            {
                ApiKey = (_opts.ApiKey ?? string.Empty).Trim(),
                SecretKey = (_opts.SecretKey ?? string.Empty).Trim(),
                BaseUrl = (_opts.BaseUrl ?? string.Empty).Trim()
            };

           
            Console.WriteLine($"[IYZI CONF] BaseUrl={iyzOpts.BaseUrl}, ApiKeyLen={iyzOpts.ApiKey.Length}, SecretKeyLen={iyzOpts.SecretKey.Length}");


            var req = new Iyzipay.Request.CreateCheckoutFormInitializeRequest
            {
                Locale = Iyzipay.Model.Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Price = price.ToString("0.00", CultureInfo.InvariantCulture),
                PaidPrice = paidPrice.ToString("0.00", CultureInfo.InvariantCulture),
                Currency = Iyzipay.Model.Currency.TRY.ToString(),
                BasketId = string.IsNullOrWhiteSpace(dto.BasketId) ? "BASKET-1" : dto.BasketId,
                PaymentGroup = Iyzipay.Model.PaymentGroup.PRODUCT.ToString(),
                CallbackUrl = _opts.CallbackUrl,
                EnabledInstallments = new List<int> { 1, 2, 3, 6, 9 }
            };

            var buyerName = string.IsNullOrWhiteSpace(dto.Name) ? "John" : dto.Name;
            var buyerSurname = string.IsNullOrWhiteSpace(dto.Surname) ? "Doe" : dto.Surname;

            req.Buyer = new Iyzipay.Model.Buyer
            {
                Id = string.IsNullOrWhiteSpace(dto.CustomerId) ? "CUST1" : dto.CustomerId,
                Name = buyerName,
                Surname = buyerSurname,
                GsmNumber = string.IsNullOrWhiteSpace(dto.GsmNumber) ? "+905350000000" : dto.GsmNumber,
                Email = string.IsNullOrWhiteSpace(dto.Email) ? "test@test.com" : dto.Email,
                IdentityNumber = string.IsNullOrWhiteSpace(dto.IdentityNumber) ? "74300864791" : dto.IdentityNumber,
                LastLoginDate = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationDate = DateTime.UtcNow.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"),
                RegistrationAddress = dto.Billing?.Description ?? "Adres",
                Ip = HttpContext.Connection.RemoteIpAddress?.ToString() ?? "127.0.0.1",
                City = dto.Billing?.City ?? "Istanbul",
                Country = dto.Billing?.Country ?? "Turkey",
                ZipCode = dto.Billing?.ZipCode ?? "34000"
            };

            req.ShippingAddress = new Iyzipay.Model.Address
            {
                ContactName = dto.Shipping?.ContactName ?? $"{buyerName} {buyerSurname}",
                City = dto.Shipping?.City ?? "Istanbul",
                Country = dto.Shipping?.Country ?? "Turkey",
                Description = dto.Shipping?.Description ?? "Adres",
                ZipCode = dto.Shipping?.ZipCode ?? "34000"
            };

            req.BillingAddress = new Iyzipay.Model.Address
            {
                ContactName = dto.Billing?.ContactName ?? $"{buyerName} {buyerSurname}",
                City = dto.Billing?.City ?? "Istanbul",
                Country = dto.Billing?.Country ?? "Turkey",
                Description = dto.Billing?.Description ?? "Adres",
                ZipCode = dto.Billing?.ZipCode ?? "34000"
            };

            var basket = new List<Iyzipay.Model.BasketItem>();
            foreach (var i in items)
            {
                basket.Add(new Iyzipay.Model.BasketItem
                {
                    Id = string.IsNullOrWhiteSpace(i.Id) ? "ITEM1" : i.Id,
                    Name = string.IsNullOrWhiteSpace(i.Name) ? "Ürün" : i.Name,
                    Category1 = string.IsNullOrWhiteSpace(i.Category1) ? "PetShop" : i.Category1,
                    ItemType = string.IsNullOrWhiteSpace(i.ItemType)
                        ? Iyzipay.Model.BasketItemType.PHYSICAL.ToString()
                        : i.ItemType,
                    Price = i.Price.ToString("0.00", CultureInfo.InvariantCulture)
                });
            }
            req.BasketItems = basket;

            var init = await Iyzipay.Model.CheckoutFormInitialize.Create(req, iyzOpts);

            return Ok(new CheckoutInitResponseDto
            {
                Status = init.Status,
                ErrorCode = init.ErrorCode,
                ErrorMessage = string.IsNullOrWhiteSpace(init.ErrorMessage) ? null : init.ErrorMessage,
                ConversationId = init.ConversationId,
                Token = init.Token,
                CheckoutFormContent = init.CheckoutFormContent
            });
        }

        [HttpPost("callback")]
        public IActionResult Callback([FromForm] string token)
        {
            var redirectUrl = $"{_opts.CustomerUiBaseUrl}/payment/result?token={Uri.EscapeDataString(token ?? "")}";
            return Redirect(redirectUrl);
        }

        [HttpGet("result")]
        public async Task<ActionResult<CheckoutResultResponseDto>> Result([FromQuery] string token)
        {
            var iyzOpts = new IyzipayOptions
            {
                ApiKey = _opts.ApiKey,
                SecretKey = _opts.SecretKey,
                BaseUrl = _opts.BaseUrl
            };

            var req = new Iyzipay.Request.RetrieveCheckoutFormRequest
            {
                Locale = Iyzipay.Model.Locale.TR.ToString(),
                ConversationId = Guid.NewGuid().ToString(),
                Token = token
            };

            var form = await Iyzipay.Model.CheckoutForm.Retrieve(req, iyzOpts);

            var dto = new CheckoutResultResponseDto
            {
                Status = form.Status,
                ErrorCode = form.ErrorCode,
                ErrorMessage = form.ErrorMessage,
                PaymentStatus = form.PaymentStatus,
                PaymentId = form.PaymentId,
                BasketId = form.BasketId,
                Price = form.Price,
                PaidPrice = form.PaidPrice,
                Installment = form.Installment?.ToString(),
                CardAssociation = form.CardAssociation,
                CardFamily = form.CardFamily
            };

            return Ok(dto);
        }
        [HttpGet("_diag")]
        public IActionResult Diag([FromServices] Microsoft.Extensions.Options.IOptions<Server.Options.IyzicoOptions> oo)
        {
            var v = oo.Value;
           
            return Ok(new
            {
                baseUrl = (v.BaseUrl ?? "").Trim(),
                apiKeyLen = (v.ApiKey ?? "").Trim().Length,
                secretKeyLen = (v.SecretKey ?? "").Trim().Length
            });
        }
    }
    

    }
