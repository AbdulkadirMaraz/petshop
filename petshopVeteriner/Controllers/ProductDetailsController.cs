using BusinessLayer.Abstract;
using CustomerPetshop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CustomerPetshop.Controllers
{
    public class ProductDetailsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductDetailsController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        public IActionResult Index(int id)
        {
            var product = _productService.GetById(id);
            if (product == null)
                return NotFound();

            var categories = _categoryService.GetList();
            var relatedProducts = _productService.GetList().Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id).Take(4).ToList();

            ViewBag.RelatedProducts = relatedProducts;
            var model = new ProductViewModel
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                ImageUrl2=product.ImageUrl2,
                ImageUrl3 = product.ImageUrl3,
                ImageUrl4 = product.ImageUrl4,

                Price = product.Price,
                Tags = product.Tags,
                CategoryId = product.CategoryId,
                CategoryOptions = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList()
            };

            return View(model);
        }
    }
}
