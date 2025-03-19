﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Models;
using Shop.ViewModels;
using System.Security.Claims;

namespace Shop.Controllers
{
    public class AccountController : Controller {
        private readonly ShopContext _shopDb;
        public AccountController( ShopContext shopDb ) {
            _shopDb = shopDb;
        }
        public IActionResult Login(AccountViewModel model) {
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> LoginPost(AccountViewModel model) {
            if (string.IsNullOrWhiteSpace(model.Account) || string.IsNullOrWhiteSpace(model.Password)) {
                return View("Login", model);
            }

            var user = _shopDb.Users.SingleOrDefault(x => x.Account == model.Account && x.Password == model.Password);
            if (user == null) return View("Login", model);

            var claims = new List<Claim> {
                new(ClaimTypes.Name, user.Account),
                new(ClaimTypes.Role, user.Role),
                new(ClaimTypes.Email, user.Account),
                new("nickname", user.Nickname)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties {
                IsPersistent = false,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            });

            return RedirectToAction("Fun");
        }

        [Authorize]
        public IActionResult Fun() {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        
        public IActionResult AccessDenied() {
            return View();
        }
    }
}
