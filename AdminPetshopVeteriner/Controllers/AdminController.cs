using System.Threading.Tasks;
using AdminPetshop.Models;
using EntityLayer.Concrete;                  
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AdminPetshop.Controllers
{
    public class AdminController : Controller
    {
        
            private readonly UserManager<ApplicationUser> _userManager;
            private readonly SignInManager<ApplicationUser> _signInManager;

            public AdminController(UserManager<ApplicationUser> userManager,
                                     SignInManager<ApplicationUser> signInManager)
            {
                _userManager = userManager;
                _signInManager = signInManager;
            }

            [HttpGet]
            public async Task<IActionResult> Index()
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Index", "AdminLogin");

                var model = new RegisterViewModel
                {
                    UserName = user.UserName,
                    Mail = user.Email,
                    Address = user.Address,
                    PhoneNumber = user.PhoneNumber
                    
                };

                ViewBag.Success = TempData["Success"];
                return View(model);
            }

            
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Index(RegisterViewModel vm)
            {
                
                ModelState.Remove(nameof(RegisterViewModel.Password));
                ModelState.Remove(nameof(RegisterViewModel.PasswordConfirm));

                if (!ModelState.IsValid) return View(vm);

                var user = await _userManager.GetUserAsync(User);
                if (user == null) return RedirectToAction("Index", "AdminLogin");

               
                user.UserName = vm.UserName;
                user.Email = vm.Mail;
                user.Address = vm.Address;
                user.PhoneNumber = vm.PhoneNumber;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    foreach (var e in updateResult.Errors)
                        ModelState.AddModelError(string.Empty, e.Description);
                    return View(vm);
                }

                
                if (!string.IsNullOrWhiteSpace(vm.Password) || !string.IsNullOrWhiteSpace(vm.PasswordConfirm))
                {
                    if (vm.Password != vm.PasswordConfirm)
                    {
                        ModelState.AddModelError(nameof(vm.PasswordConfirm), "Şifreler uyuşmuyor.");
                        return View(vm);
                    }

                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passResult = await _userManager.ResetPasswordAsync(user, token, vm.Password);
                    if (!passResult.Succeeded)
                    {
                        foreach (var e in passResult.Errors)
                            ModelState.AddModelError(string.Empty, e.Description);
                        return View(vm);
                    }
                }

                
                await _signInManager.RefreshSignInAsync(user);

                TempData["Success"] = "Profil başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
        }
    }

