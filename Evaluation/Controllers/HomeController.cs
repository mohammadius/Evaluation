using System.Security.Claims;
using System.Threading.Tasks;
using DNTCaptcha.Core;
using Evaluation.Data;
using Evaluation.Models.ViewModels;
using Evaluation.Utilities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
	public class HomeController : Controller
	{
		private readonly ScoreContext _dbContext;
		private readonly IDNTCaptchaValidatorService _validatorService;
		private readonly IValidator<LoginViewModel> _loginViewModelValidator;

		public HomeController(ScoreContext dbContext, IDNTCaptchaValidatorService validatorService, IValidator<LoginViewModel> loginViewModelValidator)
		{
			_dbContext = dbContext;
			_validatorService = validatorService;
			_loginViewModelValidator = loginViewModelValidator;
		}

		[HttpGet("/")]
		public IActionResult Index()
		{
			var userRole = User.FindFirst(ClaimTypes.Role);

			if (userRole is null)
			{
				return View();
			}

			return userRole.Value switch
			{
				"admin" => RedirectToAction("Index", "Information"),
				_ => RedirectToAction("LogOut")
			};
		}

		[HttpPost("signin"), ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(LoginViewModel loginData)
		{
			// Validating Captcha
			if (!_validatorService.HasRequestValidCaptchaEntry(Language.Persian, DisplayMode.SumOfTwoNumbers))
			{
				ViewBag.ErrorMessage = "کد امنیتی نادرست است.";
				return View("Index");
			}

			// Validating LoginViewModel
			var validationResult = await _loginViewModelValidator.ValidateAsync(loginData);
			if (!validationResult.IsValid)
			{
				ViewBag.ErrorMessage = validationResult.Errors[0].ErrorMessage;
				return View("Index");
			}

			// Temporary Admin login.
			// ---TO BE DELETED---
			await HttpContext.SignInAsync("admin", "admin", "");
			return RedirectToAction("Index", "Information");
		}

		[HttpGet("accessdenied"), Authorize]
		public IActionResult AccessDenied()
		{
			return View();
		}

		[HttpGet("logout"), Authorize]
		public async Task<IActionResult> LogOut()
		{
			await HttpContext.SignOutAsync();

			return RedirectToAction("Index");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View();
		}
	}
}