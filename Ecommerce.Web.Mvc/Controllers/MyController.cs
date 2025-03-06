using AspNetCore.ReportingServices.ReportProcessing.ReportObjectModel;
using AutoMapper;
using Ecommerce.Application.Dto;
using Ecommerce.Application.Handlers.CustomerAccount.Commands;
using Ecommerce.Application.Handlers.Customers.Commands;
using Ecommerce.Application.Handlers.Customers.Queries;
using Ecommerce.Application.Identity;
using Ecommerce.Application.Interfaces;
using Ecommerce.Web.Mvc.Extension;
using Ecommerce.Web.Mvc.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json;

namespace Ecommerce.Web.Mvc.Controllers;

[Authorize]
public class MyController : Controller
{
    #region Basic Config
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly IKeyAccessor _keyAccessor;
    private readonly IAccountService _accountService;
    private readonly IUserService _userService;
    private readonly ICurrentUser _currentUser;
    private readonly IProfileService _profileService;
    private readonly IEmailService _emailService;
    public MyController(IMediator mediator,
        IMapper mapper,
        IKeyAccessor keyAccessor,
        IAccountService accountService,
        IProfileService profileService,
        IUserService userService,
        ICurrentUser currentUser,
        IEmailService emailService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _keyAccessor = keyAccessor;
        _accountService = accountService;
        _profileService = profileService;
        _userService = userService;
        _currentUser = currentUser;
        _emailService = emailService;
    }
    #endregion

    public async Task<IActionResult> Index()
    {
        var customerInfo = await _mediator.Send(new GetCustomerInfoByLoginUserQuery());
        var currentUser = await _userService.GetCurrentUsersAsync();
        ViewBag.CustomerInfo = customerInfo;
        ViewBag.CurrentUser = currentUser;
        return View();
    }




    #region Customer Login
    [HttpGet]
    [Route("customerlogin")]
    [Route("My/Login")]
    [AllowAnonymous]
    public ActionResult Login(string? returnUrl)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View(new LoginUserDto());
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginUserDto loginUserDto, string? returnUrl)
    {
        var conAdv = _keyAccessor?["AdvancedConfiguration"] != null ? JsonSerializer.Deserialize<AdvancedConfigurationDto>(_keyAccessor["AdvancedConfiguration"]) : new AdvancedConfigurationDto();
        if (!ModelState.IsValid) return View(loginUserDto);
        var rs = await _accountService.SignInAsync(loginUserDto);
        if (rs.Succeeded)
        {
            var user = await _userService.GetUserByUserNameAsync(loginUserDto.UserName);
            //TempData["notification"] = "<script>swal(`" + "Welcome Back!" + "`, `" + "Hello " + user?.Data?.FullName?? loginUserDto.UserName + ", Welcome back!" + "`,`" + "success" + "`)" + "</script>";
            TempData["notification"] = $"<script>swal('Welcome Back!', 'Hello {user?.Data?.FullName ?? loginUserDto.UserName}, Welcome back!', 'success')</script>";
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
            return RedirectToAction("LoginTwoStep", "Account", new { loginUserDto.UserName, loginUserDto.RememberMe, returnUrl, sendCode });
        }
        else if (rs.Data.IsLockedOut) ModelState.AddModelError(string.Empty, "Account locked");
        else if (conAdv.EnableEmailConfirmation && !rs.Data.IsEmailConfirmed) ModelState.AddModelError(string.Empty, "Email are Not Confirmed!");
        else ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

        return View(loginUserDto);
    }
    #endregion

    #region Customer Register
    [AllowAnonymous]
    public ActionResult Register() => View();

    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Register(CustomerRegisterDto customerRegister)
    {
        if (!ModelState.IsValid) return View(customerRegister);
        SecurityConfigurationDto conSec = _keyAccessor?["SecurityConfiguration"] != null ? JsonSerializer.Deserialize<SecurityConfigurationDto>(_keyAccessor["SecurityConfiguration"])! : new SecurityConfigurationDto();
        AdvancedConfigurationDto conAdv = _keyAccessor?["AdvancedConfiguration"] != null ? JsonSerializer.Deserialize<AdvancedConfigurationDto>(_keyAccessor["AdvancedConfiguration"])! : new AdvancedConfigurationDto();

        if (customerRegister?.Password.Length < conSec?.PasswordRequiredLength)
        {
            ModelState.AddModelError(customerRegister.Password, $"The Password must be at least {conSec?.PasswordRequiredLength} characters long.");
        }

        if (ModelState.IsValid)
        {
            var response = await _mediator.Send(new RegisterCustomerCommand { CustomerRegister = customerRegister });
            if (response.Succeeded)
            {
                if (conAdv.EnableEmailConfirmation)
                {
                    try
                    {
                        var url = $"{Request.Scheme}://{Request.Host.Value}/emailconfirmation";
                        var url2 = $"emailconfirmation";
                        await _emailService.SendEmailConfirmationAsync(customerRegister.UserName, "Email Confirmation", url);
                        TempData["notification"] = $"<script>swal('Success!', 'A confirmation email send to your email.', 'success')</script>";
                        return Redirect("/my/login");
                    }
                    catch
                    {
                        TempData["notification"] = "<script>swal(`" + "Error Occurred!" + "`, `" + "Can't send email confirmation. please contact support." + "`,`" + "error" + "`)" + "</script>";
                        return Redirect("/my/login");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "My", new { succeeded = response.Succeeded, message = response.Message });
                }

            }
            ModelState.AddModelError(string.Empty, response.Message);
        }

        return View(customerRegister);
    }
    #endregion

    #region Update Password
    public IActionResult Password()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Password(EditPasswordDto editPassword)
    {
        if (ModelState.IsValid)
        {
            var response = await _profileService.UpdatePasswordAsync(editPassword);
            if (response.Succeeded)
            {
                await _accountService.SignOutAsync();
                TempData["notification"] = "<script>swal(`" + "Your Password Changed!" + "`, `" + "Please login to continue." + "`,`" + "success" + "`)" + "</script>";
                return Redirect("/my/login");
            }
            ModelState.AddModelError("", response.Message);
        }

        return View(editPassword);
    }
    #endregion

}
