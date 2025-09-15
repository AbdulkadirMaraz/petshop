using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using AdminPetshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FluentValidation.Results;
using BusinessLayer.FluentValidation;
using AdminPetshop.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;


public class ProductController : Controller
{
   
    private readonly IProductService _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService = productService;
        _categoryService = categoryService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var products = _productService.GetList();
        var categories = _categoryService.GetList();

        var model = products.Select(p => new ProductViewModel
        {
            Id = p.Id,
            Name = p.Name,
            ImageUrl = p.ImageUrl,
            ImageUrl2 = p.ImageUrl2,
            ImageUrl3 = p.ImageUrl3,
            ImageUrl4 = p.ImageUrl4,
            Price = p.Price,
            CategoryId = p.CategoryId,
            CategoryName = categories.FirstOrDefault(c => c.Id == p.CategoryId)?.Name ?? "Kategori Yok"
        }).ToList();

        return View(model);
    }

    
    [HttpGet]
    public IActionResult AddProduct()
    {
        var categories = _categoryService.GetList();
        var model = new ProductViewModel
        {
            CategoryOptions = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList()
        };

        return View(model);
    }

   
    [HttpPost]
    public IActionResult AddProduct(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Name = model.Name,
                Description=model.Description,
                ImageUrl = model.ImageUrl,
                ImageUrl2 = model.ImageUrl2,
                ImageUrl3 = model.ImageUrl3,
                ImageUrl4 = model.ImageUrl4,
                Price = model.Price,
                Tags=model.Tags,
                CategoryId = model.CategoryId,
                DateOfAddition =model.DateOfAddition
            };

            ProductValidator validator = new();
            ValidationResult result = validator.Validate(product);

            if (result.IsValid)
            {
                _productService.Insert(product);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
        }

       
        var categories = _categoryService.GetList();
        model.CategoryOptions = categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();

        return View(model);
    }

    
    [HttpGet]
    public IActionResult EditProduct(int id)
    {
        var product = _productService.GetById(id);
        if (product == null)
            return NotFound();

        var categories = _categoryService.GetList();

        var model = new ProductViewModel
        {
            Id = product.Id,
            Name = product.Name,
            Description=product.Description,
            ImageUrl = product.ImageUrl,
            ImageUrl2 = product.ImageUrl2,
            ImageUrl3 = product.ImageUrl3,
            ImageUrl4 = product.ImageUrl4,
            Price = product.Price,
            Tags=product.Tags,
            CategoryId = product.CategoryId,
            CategoryOptions = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList()
        };

        return View(model);
    }

   
    [HttpPost]
    public IActionResult EditProduct(ProductViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = new Product
            {
                Id = model.Id,
                Name = model.Name,
                Description=model.Description,
                ImageUrl = model.ImageUrl,
                ImageUrl2 = model.ImageUrl2,
                ImageUrl3 = model.ImageUrl3,
                ImageUrl4 = model.ImageUrl4,
                Price = model.Price,
                Tags=model.Tags,
                CategoryId = model.CategoryId
            };

            ProductValidator validator = new();
            ValidationResult result = validator.Validate(product);

            if (result.IsValid)
            {
                _productService.Update(product);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
        }

       
        var categories = _categoryService.GetList();
        model.CategoryOptions = categories.Select(c => new SelectListItem
        {
            Value = c.Id.ToString(),
            Text = c.Name
        }).ToList();

        return View(model);
    }

    
    public IActionResult DeleteProduct(int id)
    {
        var product = _productService.GetById(id);
        if (product != null)
        {
            _productService.Delete(product);
        }
        return RedirectToAction("Index");
    }
}
