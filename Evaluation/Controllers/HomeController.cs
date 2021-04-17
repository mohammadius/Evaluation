using System.Threading;
using System.Threading.Tasks;
using DNTCaptcha.Core;
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
		private readonly IDNTCaptchaValidatorService _validatorService;
		private readonly IValidator<LoginViewModel> _loginViewModelValidator;

		public HomeController(IDNTCaptchaValidatorService validatorService, IValidator<LoginViewModel> loginViewModelValidator)
		{
			_validatorService = validatorService;
			_loginViewModelValidator = loginViewModelValidator;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(LoginViewModel loginData, CancellationToken cancellationToken)
		{
			if (!_validatorService.HasRequestValidCaptchaEntry(Language.Persian, DisplayMode.SumOfTwoNumbers))
			{
				ViewBag.ErrorMessage = "کد امنیتی نادرست است.";
				return View("Index");
			}

			var validationResult = await _loginViewModelValidator.ValidateAsync(loginData, cancellationToken);
			if (!validationResult.IsValid)
			{
				ViewBag.ErrorMessage = validationResult.Errors[0].ErrorMessage;
				return View("Index");
			}

			//TODO: login user
			await HttpContext.SignInAsync(loginData.Username, "مدیر مرکز");

			return RedirectToAction("Center", "Information");
		}

		[Authorize]
		public async Task<IActionResult> SignOut()
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