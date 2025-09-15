using Microsoft.AspNetCore.Mvc;

namespace AdminPetshop.ViewComponents
{
    public class _NavbarPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
