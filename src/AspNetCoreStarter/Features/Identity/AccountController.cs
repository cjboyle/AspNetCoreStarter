using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreStarter.Features.Identity
{
    [Authorize, AutoValidateAntiforgeryToken]
    public partial class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl != null)
                ViewData[nameof(returnUrl)] = returnUrl;

            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(Login.Command request, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var login = await _mediator.Send(request);

            if (login.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            else if (login.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(SendCode), new { ReturnUrl = returnUrl, RememberMe = request.RememberMe });
            }
            else if (login.IsLockedOut)
            {
                ModelState.AddModelError("", "This account has been locked. Please try again in 1 minute.");
            }
            else if (login.IsNotAllowed)
            {
                ModelState.AddModelError("", "This account has been disabled. If this is an error, please contact support.");
            }
            else
            {
                ModelState.AddModelError("", "The username or password was incorrect.");
            }
            return View();
        }

        [HttpGet, AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(Register.Command request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var result = await _mediator.Send(request);

            return View();
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> SendCode(Send2FA.Query request)//string provider, string returnUrl, bool rememberMe)
        {
            throw new NotImplementedException();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> SendCode(Send2FA.Command request)
        {
            throw new NotImplementedException();
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> VerifyCode(Verify2FA.Query request)//string provider, string returnUrl, bool rememberMe)
        {
            throw new NotImplementedException();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> VerifyCode(Verify2FA.Command request)
        {
            //var result = _mediator.Send(request);
            throw new NotImplementedException();
        }

        [HttpGet, AllowAnonymous]
        public async Task<JsonResult> VerifyUniqueUsername(string Username)
        {
            var parsedRequest = new VerifyUniqueAccount.UsernameQuery { Username = Username };
            return Json(false);
            return Json(await _mediator.Send(parsedRequest));
        }

        [HttpGet, AllowAnonymous]
        public async Task<JsonResult> VerifyUniqueEmail(VerifyUniqueAccount.EmailQuery request)
        {
            return Json(await _mediator.Send(request));
        }
    }
}
