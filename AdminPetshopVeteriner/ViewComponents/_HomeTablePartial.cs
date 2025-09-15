using BusinessLayer.Abstract;

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AdminPetshop.ViewComponents
{
    public class _HomeTablePartial : ViewComponent
    {
        private readonly IContactService _contactService;

        public _HomeTablePartial(IContactService contactService)
        {
            _contactService = contactService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _contactService.GetList();
            return View(values);
        }
    }
}