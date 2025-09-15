using BusinessLayer.Abstract;
using BusinessLayer.FluentValidation;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;   
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;

namespace AdminPetshop.Controllers
{
    [Authorize] 
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly UserManager<ApplicationUser> _userManager; 

        public CustomerController(ICustomerService customerService,
                                  UserManager<ApplicationUser> userManager) 
        {
            _customerService = customerService;
            _userManager = userManager; 
        }

     

       

        [HttpGet]
        public IActionResult Index()
        {
            var users = _userManager.Users.ToList(); 
            return View(users); 
        }

        [HttpGet]
        public async Task<IActionResult> EditCustomer(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();
            return View(user); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCustomer([Bind("Id,UserName,Email,Address,IsAdmin,PhoneNumber")] ApplicationUser input)
        {
            if (!ModelState.IsValid) return View(input);

            var user = await _userManager.FindByIdAsync(input.Id);
            if (user == null) return NotFound();

            // izin verilen alanları güncelle
            user.UserName = input.UserName;
            user.Email = input.Email;
            user.PhoneNumber = input.PhoneNumber;
            user.Address = input.Address;
            user.IsAdmin = input.IsAdmin;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var e in result.Errors)
                ModelState.AddModelError(string.Empty, e.Description);

            return View(input);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                TempData["Error"] = string.Join(" | ", result.Errors.Select(e => e.Description));
            }
            return RedirectToAction(nameof(Index));
        }
    }
}







