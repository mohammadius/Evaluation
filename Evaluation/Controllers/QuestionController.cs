using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
	[Authorize(Roles = "ادمین")]
	public class QuestionController  : Controller
	{
		public IActionResult Index()
		{
			return RedirectToAction("Detail");
		}
		
		public IActionResult Detail()
		{
			return View();
		}
		
		public IActionResult Section()
		{
			return View();
		}
	}
}