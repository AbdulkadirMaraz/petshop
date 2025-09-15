using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CustomerPetshop.Models; 
using BusinessLayer.Abstract; 

using CustomerPetshop.Extensions;


namespace CustomerPetshop.Controllers
{
    public class CustomerCartController : Controller
    {
        private const string CartSessionKey = "Cart";
        private readonly IProductService _productService;

        public CustomerCartController(IProductService productService)
        {
            _productService = productService;
        }

        
        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey) ?? new List<CartItemDto>();
            return View(cart);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
           
            if (quantity < 1) quantity = 1;

            var product = _productService.GetById(productId);
            if (product == null) return NotFound();

            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey) ?? new List<CartItemDto>();

            var existingItem = cart.FirstOrDefault(c => c.ProductId == productId);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItemDto
                {
                    ProductId = product.Id,
                    Name = product.Name,   // DTO ile eşleştir
                    Price = product.Price,
                    Quantity = quantity,
                    ImageUrl = product.ImageUrl
                });
            }

            HttpContext.Session.SetObject(CartSessionKey, cart);

           
            var referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer))
                return Redirect(referer);

            return RedirectToAction("Index");
        }

        
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey) ?? new List<CartItemDto>();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.SetObject(CartSessionKey, cart);
            }

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateQuantity(int productId, int quantity)
        {
            var cart = HttpContext.Session.GetObject<List<CartItemDto>>(CartSessionKey) ?? new List<CartItemDto>();
            var item = cart.FirstOrDefault(c => c.ProductId == productId);
            if (item != null)
            {
                if (quantity <= 0) cart.Remove(item);
                else item.Quantity = quantity;

                HttpContext.Session.SetObject(CartSessionKey, cart);
            }

            return RedirectToAction("Index");
        }

       
        public IActionResult ClearCart()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction("Index");
        }
    }
}
