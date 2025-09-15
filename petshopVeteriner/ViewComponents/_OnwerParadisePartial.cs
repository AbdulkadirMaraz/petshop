using Microsoft.AspNetCore.Mvc;

namespace AgricultureUI.ViewComponents
{
    public class _OnwerParadisePartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
