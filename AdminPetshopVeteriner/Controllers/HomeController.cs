
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPetshop.Areas.Admin.Controllers
{
   
    [Authorize] 
    public class HomeController : Controller
    {
        public IActionResult Index() => View();
    }
}
