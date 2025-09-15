using BusinessLayer.Abstract;
using CustomerPetshop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CustomerPetshop.Controllers
{
    public class CustomerProductController : Controller
    {

        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public CustomerProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index(int? categoryId)
        {
            var products = _productService.GetList();

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == categoryId.Value).ToList();
            }

            var model = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                ImageUrl = p.ImageUrl,
                Description = p.Description,
                Price = p.Price,
                Tags = p.Tags,
                CategoryId = p.CategoryId
            }).ToList();

            return View(model);
        }
       

    }
}
