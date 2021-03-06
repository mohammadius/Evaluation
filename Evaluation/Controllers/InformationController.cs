using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
	public class InformationController : Controller
	{
		public IActionResult Index()
		{
			return RedirectToAction("Center");
		}
		
		public IActionResult Center()
		{
			return View();
		}
		
		public IActionResult Section()
		{
			return View();
		}
		
		public IActionResult Position()
		{
			return View();
		}
		
		public IActionResult Staff()
		{
			return View();
		}
	}
}
