using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;
using AdminPetshop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace AdminPetshop.Controllers
{
 
    public class CartController : Controller
    {
        private readonly ICartItemService _cartItemService;
        private readonly IProductService _productService;

        public CartController(ICartItemService cartItemService, IProductService productService)
        {
            _cartItemService = cartItemService;
            _productService = productService;
        }

        public IActionResult Index()
        {
            var cartItems = _cartItemService.GetList();
            var products = _productService.GetList();

            var viewModel = cartItems.Select(cart => new CartItemViewModel
            {
                Id = cart.Id,
                CustomerId = cart.CustomerId,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                ProductName = products.FirstOrDefault(p => p.Id == cart.ProductId)?.Name ?? "Bilinmiyor"
            }).ToList();

            return View(viewModel);
        }
    }
}
