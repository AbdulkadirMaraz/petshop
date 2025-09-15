using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace AdminPetshop.Controllers
{
  
    public class OrderItemController : Controller
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        public IActionResult Index(int id) // orderId
        {
            var items = _orderItemService.GetList().Where(x => x.OrderId == id).ToList();
            return View(items);
        }
    }
}
