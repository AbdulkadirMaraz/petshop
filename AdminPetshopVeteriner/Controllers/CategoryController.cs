using BusinessLayer.Abstract;
using BusinessLayer.FluentValidation;
using EntityLayer.Concrete;

using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPetshop.Controllers
{
   
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var values = _categoryService.GetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            Console.WriteLine(category);

            CategoryValidator validationRules = new();
            ValidationResult result = validationRules.Validate(category);
            if (result.IsValid)
            {
                _categoryService.Insert(category);
            return RedirectToAction("Index");
            }

            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public IActionResult DeleteCategory(int id)
        {
            var value = _categoryService.GetById(id);
            _categoryService.Delete(value);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult EditCategory(int id)
        {
            
            var value = _categoryService.GetById(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult EditCategory(Category category)
        {

            CategoryValidator validationRules = new();
            ValidationResult result = validationRules.Validate(category);
            if (result.IsValid)
            {
                _categoryService.Update(category);
                return RedirectToAction("Index");
            }

            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
    }
}

