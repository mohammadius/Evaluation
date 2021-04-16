using System.Threading.Tasks;
using DNTCaptcha.Core;
using Evaluation.Models.ViewModels;
using Evaluation.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
	public class HomeController : Controller
	{
		private readonly IDNTCaptchaValidatorService _validatorService;

		public HomeController(IDNTCaptchaValidatorService validatorService)
		{
			_validatorService = validatorService;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> SignIn(LoginViewModel loginData)
		{
			if (!_validatorService.HasRequestValidCaptchaEntry(Language.Persian, DisplayMode.SumOfTwoNumbers))
			{
				ViewBag.ErrorMessage = "کد امنیتی نادرست است.";
				return View("Index");
			}
			
			//TODO: validate loginData

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