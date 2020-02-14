using Belatrix.Client.Common;
using Belatrix.Client.Proxies;
using Belatrix.Client.Proxies.Commands;
using Belatrix.Client.Proxies.Responses;
using Belatrix.Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Belatrix.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IIdentityProxy _identityProxy;

        public AccountController(
            ILogger<AccountController> logger,
            IIdentityProxy identityProxy)
        {
            _logger = logger;
            _identityProxy = identityProxy;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid) 
            {
                // Try to authenticate
                try
                {
                    var result = await _identityProxy.Authenticate(new UserLoginCommand
                    {
                        Email = model.Email,
                        Password = model.Password
                    });

                    if (result != null && result.Succeeded)
                    {
                        // Sign in current user
                        await SignIn(result);

                        // Redirect to home
                        return Redirect("~/");
                    }
                }
                catch (Exception)
                {
                    model.HasError = true;
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("~/");
        }

        private async Task SignIn(IdentityAccess identity) 
        {
            var token = identity.AccessToken.Split('.');
            var base64Content = Convert.FromBase64String(
                token[1].Replace('-', '+').Replace('_', '/').PadRight(4 * ((token[1].Length + 3) / 4), '=')
            );

            var user = JsonSerializer.Deserialize<AccessTokenUser>(base64Content);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.nameid),
                new Claim(ClaimTypes.Name, user.unique_name),
                new Claim(ClaimTypes.Email, user.email),
                new Claim("access_token", identity.AccessToken)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.UtcNow.AddHours(1)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}
