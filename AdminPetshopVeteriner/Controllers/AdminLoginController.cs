using System.Threading.Tasks;
using AdminPetshop.Models;
using EntityLayer.Concrete;                 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPetshop.Controllers
{
    [AllowAnonymous]
    public class AdminLoginController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminLoginController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Error = TempData["Error"];
            return View();
        }

        [HttpPost]
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

            
            if (!user.IsAdmin)
            {
                TempData["Error"] = "Bu panele sadece yönetici (IsAdmin = true) kullanıcılar giriş yapabilir.";
                return RedirectToAction(nameof(AccessDenied));
            }

            // Şifreyi doğrula ve giriş yap
            var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.password, false, false);
            if (result.Succeeded)
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            ModelState.AddModelError(string.Empty, "Giriş başarısız.");
            return View(loginViewModel);
        }

        [HttpGet, AllowAnonymous]
        public IActionResult SingUp() => View();

        [HttpPost, AllowAnonymous]
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
                IsAdmin = registerViewModel.IsAdmin 
            };

            var createResult = await _userManager.CreateAsync(user, registerViewModel.Password);
            if (!createResult.Succeeded)
            {
                foreach (var e in createResult.Errors)
                    ModelState.AddModelError(string.Empty, e.Description);
                return View(registerViewModel);
            }

           
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet, AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }
    }
}
