using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AdminPetshop.Controllers
{
  
        public class AdminContactController : Controller
        {
            private readonly IContactService _contactService;

            public AdminContactController(IContactService contactService)
            {
                _contactService = contactService;
            }

            public IActionResult Index()
            {
                var values = _contactService.GetList();
                return View(values);
            }

            public IActionResult DeleteMessage(int id)
            {
                var value = _contactService.GetById(id);
                _contactService.Delete(value);
                return RedirectToAction("Index");
            }
            public IActionResult MessageDetails(int id)
            {
                var values = _contactService.GetById(id);
                return View(values);
            }
        }
    }

