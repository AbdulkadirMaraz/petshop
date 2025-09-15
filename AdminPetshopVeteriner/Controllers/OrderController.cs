using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace AdminPetshop.Controllers
{
  
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        
        public IActionResult Index()
        {
            var orders = _orderService.GetList();
            return View(orders);
        }
    }
}
