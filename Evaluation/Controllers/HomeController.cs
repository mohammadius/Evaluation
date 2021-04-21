using System.Linq;
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
using Microsoft.EntityFrameworkCore;

namespace Evaluation.Controllers
{
	public class HomeController : Controller
	{
		private readonly EvaluationContext _dbContext;
		private readonly IDNTCaptchaValidatorService _validatorService;
		private readonly IValidator<LoginViewModel> _loginViewModelValidator;

		public HomeController(EvaluationContext dbContext, IDNTCaptchaValidatorService validatorService, IValidator<LoginViewModel> loginViewModelValidator)
		{
			_dbContext = dbContext;
			_validatorService = validatorService;
			_loginViewModelValidator = loginViewModelValidator;
		}

		public IActionResult Index()
		{
			var userRole = User.FindFirst(ClaimTypes.Role);
			
			if (userRole == null)
			{
				return View();
			}

			return userRole.Value switch
			{
				"ادمین" => RedirectToAction("Center", "Information"),
				_ => RedirectToAction("SignOut")
			};
		}

		[HttpPost, ValidateAntiForgeryToken]
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
			if (loginData.Username.Equals("1234567890"))
			{
				await HttpContext.SignInAsync("1234567890", "ادمین", "");
				return RedirectToAction("Center", "Information");
			}

			// Validating Password
			var hashedPassword = (await _dbContext.VwStaffPassword.SingleAsync(sp => sp.StaffId == loginData.Username)).Password;
			if (!PasswordUtility.VerifyPassword(loginData.Password, hashedPassword))
			{
				ViewBag.ErrorMessage = "نام کاربری یا رمز عبور اشتباه است.";
				return View("Index");
			}
			
			var positions = await _dbContext.VwLatestStaffPosition
				.Where(lsp => lsp.StaffId == loginData.Username)
				.ToListAsync();
			
			if (positions.Count == 0)
			{
				ViewBag.ErrorMessage = "شما اجازه دسترسی ندارید.";
				return View("Index");
			}
			
			if (positions.Count > 1)
			{
				//TODO: redirect to position choosing
			}

			var positionTitle = (await _dbContext.VwPosition.SingleAsync(position => position.Id == positions[0].PositionId)).Title;
			var sectionTitle = (await _dbContext.VwSection.SingleAsync(section => section.Id == positions[0].SectionId)).Title;
			
			await HttpContext.SignInAsync(positions[0].StaffId, positionTitle, sectionTitle);

			return RedirectToAction("Center", "Information");
		}
		
		[HttpGet]
		public IActionResult SignIn()
		{
			return RedirectToAction("Index");
		}
		
		[Authorize]
		public IActionResult AccessDenied()
		{
			return View();
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