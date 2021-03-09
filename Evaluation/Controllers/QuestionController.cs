using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
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