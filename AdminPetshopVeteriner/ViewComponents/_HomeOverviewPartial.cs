using DataAccessLayer.Concrete;


using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPetshop.ViewComponents
{
    public class _HomeOverviewPartial : ViewComponent
    {
        PetShopContext c = new PetShopContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.orders = c.Orders.Count();
            ViewBag.orderItems = c.OrderItems.Count();
            ViewBag.cartItems = c.CartItems.Count();
            ViewBag.currentMonthMessage = c.Contacts.Count();

            
            return View();
        }
    }
}