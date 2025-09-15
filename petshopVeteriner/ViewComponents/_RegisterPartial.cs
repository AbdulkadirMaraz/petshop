using Microsoft.AspNetCore.Mvc;

namespace CustomerPetshop.ViewComponents
{
    public class _RegisterPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
