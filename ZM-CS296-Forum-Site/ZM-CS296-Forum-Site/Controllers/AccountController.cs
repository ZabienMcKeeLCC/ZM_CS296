using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZM_CS296_Forum_Site.Models;

namespace ZM_CS296_Forum_Site.Controllers
{
    public class AccountController : Controller
    {
            private UserManager<AppUser> userManager;
            private SignInManager<AppUser> signInManager;

            public AccountController(UserManager<AppUser> userMngr,
                SignInManager<AppUser> signInMngr)
            {
                userManager = userMngr;
                signInManager = signInMngr;
            }

            public IActionResult Registration()
            {
                return View();
            }

            [HttpPost]
            public async Task<IActionResult> Registration(RegistrationVM model)
            {
                if (ModelState.IsValid)
                {
                    var user = new AppUser { UserName = model.Username };
                    var result = await userManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        await signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                return View(model);
            }

            [HttpGet]
            public IActionResult LogIn(string returnURL = "")
            {
                var model = new LoginVM
                {
                    ReturnUrl = returnURL
                };
                return View(model);
            }

            [HttpPost]
            public async Task<IActionResult> LogIn(LoginVM model)
            {
                if (ModelState.IsValid)
                {
                    var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, isPersistent: model.RememberMe, lockoutOnFailure: false);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                        {
                            return Redirect(model.ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("Home", "Home");
                        }
                    }
                }
                ModelState.AddModelError("", "Invalid username/password.");
                return View(model);
            }


            public async Task<IActionResult> LogOut()
            {
                await signInManager.SignOutAsync();
                return RedirectToAction("Home", "Home");
            }
        }
    }
