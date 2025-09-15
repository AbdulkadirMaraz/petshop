using CustomerPetshop.Models;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using CustomerPetshop.Extensions;
using CustomerPetshop.Models.ViewModels;



namespace CustomerPetshop.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly PetShopContext _context;

        public CheckoutController(PetShopContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemDto>>("Cart") ?? new List<CartItemDto>();
            ViewBag.Cart = cart;
            return View(new CustomerPetshop.Models.ViewModels.CheckoutViewModel());
        }


        [HttpPost]
        public IActionResult Index(CustomerPetshop.Models.ViewModels.CheckoutViewModel model)

        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            
            var cart = HttpContext.Session.GetObjectFromJson<List<CartItemDto>>("Cart");
            if (cart == null || !cart.Any())
            {
                ModelState.AddModelError("", "Sepetiniz boş.");
                return View(model);
            }

           
            var order = new Order
            {
                OrderDate = DateTime.Now,
                CustomerId = 1, // giriş yapan kullanıcıdan alman lazım
                TotalAmount = cart.Sum(x => x.Price * x.Quantity)
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

           
            foreach (var item in cart)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = item.Price
                };
                _context.OrderItems.Add(orderItem);
            }

            _context.SaveChanges();

            
            HttpContext.Session.Remove("Cart");

            
            return RedirectToAction("Pay", "Payment",new { orderId = order.Id });
        }
       
    }
}