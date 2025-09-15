using BusinessLayer.Abstract;
using CustomerPetshop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPetshop.Controllers
{
    public class HomeController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]


        public IActionResult Index(int? categoryId)
        {
            var categories = _categoryService.GetList();
            var products = _productService.GetList();


            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
                categories = categories.Where(c => c.Id == categoryId.Value).ToList();
            }

            var model = categories.Select(category => new CategoryWithProductsViewModel
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                CategoryImageUrl = category.CategoryImageUrl,
                Products = products
                    .Where(p => p.CategoryId == category.Id)
                    .Select(p => new ProductViewModel
                    {
                        Id = p.Id,
                        Name = p.Name,
                        ImageUrl = p.ImageUrl,
                        Description = p.Description,
                        Price = p.Price,
                        Tags = p.Tags,
                        CategoryId = p.CategoryId,
                        CategoryName = category.Name
                    }).ToList()
            }).ToList();

            var latestProducts = _productService.GetList()
    .OrderByDescending(p => p.DateOfAddition)
    .Take(12)
    .ToList();

            ViewBag.LatestProducts = latestProducts;
            


            var lastProducts = _productService.GetList()
   .OrderByDescending(p => p.DateOfAddition)
   .Take(3)
   .ToList();

            ViewBag.LastProducts = lastProducts;
            return View(model);
        }



    }
}
