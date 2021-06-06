using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
	[Authorize]
	public class InformationController : Controller
	{
		public IActionResult Index()
		{
			return RedirectToAction("Employee");
		}
		
		public IActionResult Employee()
		{
			return View();
		}
		
		public IActionResult Position()
		{
			return View();
		}
		
		public IActionResult Manager()
		{
			return View();
		}
		
		public IActionResult Question()
		{
			return View();
		}
	}
}
