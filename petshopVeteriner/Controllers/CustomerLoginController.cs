using System.Threading.Tasks;
using CustomerPetshop.Models;
using EntityLayer.Concrete;                
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CustomerPetshop.Controllers
{
    [AllowAnonymous]
    public class CustomerLoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;          
        private readonly SignInManager<ApplicationUser> _signInManager;     

        public CustomerLoginController(
            UserManager<ApplicationUser> userManager,                        
            SignInManager<ApplicationUser> signInManager)                   
        {
            _userManager = userManager;                                   
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            
            var user = await _userManager.FindByNameAsync(loginViewModel.userName)
                       ?? await _userManager.FindByEmailAsync(loginViewModel.userName);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Kullanıcı bulunamadı.");
                return View(loginViewModel);
            }

           

            var result = await _signInManager.PasswordSignInAsync(
                user, loginViewModel.password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError(string.Empty, "Giriş başarısız.");
            return View(loginViewModel);
        }

        [HttpGet]
        public IActionResult SingUp() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SingUp(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            if (registerViewModel.Password != registerViewModel.PasswordConfirm)
            {
                ModelState.AddModelError(string.Empty, "Şifreler uyuşmuyor.");
                return View(registerViewModel);
            }

            var user = new ApplicationUser
            {
                UserName = registerViewModel.UserName,
                Email = registerViewModel.Mail,
                Address = registerViewModel.Address,
                PhoneNumber = registerViewModel.PhoneNumber,
                IsAdmin = false                        
            };

            var createResult = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (createResult.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var e in createResult.Errors)
                ModelState.AddModelError(string.Empty, e.Description);

            return View(registerViewModel);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View(returnUrl);
        }
    }
}
