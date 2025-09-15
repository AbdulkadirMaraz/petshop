using Microsoft.AspNetCore.Mvc;

namespace AdminPetshop.ViewComponents
{
    public class _HeadPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
