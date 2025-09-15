using Microsoft.AspNetCore.Mvc;

namespace CustomerPetshop.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
