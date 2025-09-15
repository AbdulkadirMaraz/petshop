using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAccessLayer.Concrete;                 
using CustomerPetshop.Models;        

namespace CustomerPetshop.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IHttpClientFactory _httpFactory;
        private readonly PetShopContext _context;

        public PaymentController(IHttpClientFactory httpFactory, PetShopContext context)
        {
            _httpFactory = httpFactory;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Pay(int orderId)
        {
            
            var order = await _context.Orders
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
                return NotFound("Sipariş bulunamadı.");

            
            var lines = await _context.OrderItems
                .AsNoTracking()
                .Where(oi => oi.OrderId == orderId)
                .Select(oi => new { oi.ProductId, oi.Quantity, oi.UnitPrice })
                .ToListAsync();

            if (lines.Count == 0)
                return BadRequest("Sipariş kalemi bulunamadı.");

           
            var productIds = lines.Select(l => l.ProductId).Distinct().ToList();
            var productNames = await _context.Products
                .AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .Select(p => new { p.Id, p.Name })
                .ToListAsync();

           
            var items = lines.Select(oi => new CheckoutItemDto
            {
                Id = oi.ProductId.ToString(),
                Name = productNames.FirstOrDefault(p => p.Id == oi.ProductId)?.Name ?? $"Ürün #{oi.ProductId}",
                Category1 = "PetShop",
                ItemType = "PHYSICAL",
                Price = Math.Round(oi.UnitPrice * oi.Quantity, 2) // 2 hane
            }).ToList();

            
            var request = new CheckoutInitRequestDto
            {
                BasketId = order.Id.ToString(),
                Items = items,
                CustomerId = "U1",
                Email = "test@test.com",
                GsmNumber = "+905350000000",
                Name = "John",
                Surname = "Doe",
                IdentityNumber = "74300864791", // sandbox için geçerli örnek TCKN
                Billing = new AddressDto { ContactName = "John Doe", City = "Istanbul", Country = "Turkey", Description = "Adres", ZipCode = "34000" },
                Shipping = new AddressDto { ContactName = "John Doe", City = "Istanbul", Country = "Turkey", Description = "Adres", ZipCode = "34000" }
            };

            var client = _httpFactory.CreateClient("ServerApi");

            
            var resp = await client.PostAsJsonAsync("/api/payments/checkout/init", request);
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                return Content($"API hata: {(int)resp.StatusCode}\n\n{body}", "text/plain; charset=utf-8");

           
            var data = JsonSerializer.Deserialize<CheckoutInitResponseDto>(
                body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            
            if (string.IsNullOrWhiteSpace(data?.CheckoutFormContent))
            {
                var status = data?.Status ?? "(null)";
                var code = data?.ErrorCode ?? "(null)";
                var msg = data?.ErrorMessage ?? "(null)";
                return Content(
                    $"INIT FAILED\nStatus={status}\nErrorCode={code}\nErrorMessage={msg}\n\nRAW RESPONSE:\n{body}",
                    "text/plain; charset=utf-8"
                );
            }

           
            return View("Checkout", new CheckoutVm
            {
                CheckoutFormContent = data.CheckoutFormContent,
                Error = string.IsNullOrWhiteSpace(data.ErrorMessage) ? null : data.ErrorMessage
            });
        }

        
        [HttpGet("/payment/result")]
        public async Task<IActionResult> Result([FromQuery] string token)
        {
            var client = _httpFactory.CreateClient("ServerApi");
            var resp = await client.GetAsync($"/api/payments/result?token={Uri.EscapeDataString(token ?? "")}");
            var body = await resp.Content.ReadAsStringAsync();

            if (!resp.IsSuccessStatusCode)
                return Content($"API hata: {(int)resp.StatusCode}\n\n{body}", "text/plain; charset=utf-8");

            var data = JsonSerializer.Deserialize<CheckoutResultResponseDto>(
                body,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return View(new ResultVm { Data = data, Error = data?.ErrorMessage });
        }
    }
}









