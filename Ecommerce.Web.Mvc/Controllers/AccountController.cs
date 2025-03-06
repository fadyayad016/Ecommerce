using Ecommerce.Application.Dto;
using Ecommerce.Application.Identity;
using Ecommerce.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Mvc.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _accountService;
    private readonly IUserService _userService;
    private readonly IEmailService _emailService;
    public AccountController(IAccountService accountService, IUserService userService, IEmailService emailService)
    {
        _accountService = accountService;
        _userService = userService;
        _emailService = emailService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        if (!ModelState.IsValid) return View(registerUserDto);
        var rs = await _accountService.RegisterUserAsync(registerUserDto);
        if (rs.Succeeded)
            return RedirectToAction("Login", new { succeeded = rs.Succeeded, message = rs.Message });
        ModelState.AddModelError(string.Empty, rs.Message);
        return View(registerUserDto);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginUserDto());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto, string returnUrl)
    {
        if (!ModelState.IsValid) return View(loginUserDto);
        var rs = await _accountService.SignInAsync(loginUserDto);
        if (rs.Succeeded)
        {

            TempData["notification"] = "<script>swal(`" + "Welcome Back!" + "`, `" + "Hello " + loginUserDto.UserName + ", Welcome back!" + "`,`" + "success" + "`)" + "</script>";
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
            //return Redirect("/");
        }
        else if (rs.Data.RequiresTwoFactor)
        {
            var sendCode = true;
            return RedirectToAction("LoginTwoStep", new { loginUserDto.UserName, loginUserDto.RememberMe, returnUrl, sendCode });
        }
        else if (rs.Data.IsLockedOut)
        {

            ModelState.AddModelError(string.Empty, "Account locked");
        }
        else
        {
            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
        }

        return View(loginUserDto);
    }

   

    [HttpGet]
    [Route("logout")]
    [Route("Account/Logout")]
    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        await _accountService.SignOutAsync();
        return Redirect("/");
    }


    [Route("accessdenied")]
    [Route("Account/AccessDenied")]
    [HttpGet]
    [AllowAnonymous]
    public IActionResult AccessDenied()
    {
        try
        {
            var returnUrl = Request.Headers["Referer"].ToString();
            var msg = "<script>swal(`" + "Access Denied!" + "`, `" + "You are not allowed to view this resource." + "`,`" + "warning" + "`)" + "</script>";
            TempData["notification"] = msg;
            if (returnUrl != "")
            {
                return Redirect(returnUrl);


            }
            return Redirect("/");
        }
        catch { return View(); }
    }




}

