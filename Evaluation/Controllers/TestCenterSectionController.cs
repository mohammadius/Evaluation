using System.Linq;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Evaluation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
	[Route("api/[controller]/[action]")]
	public class TestCenterSectionController : Controller
	{
		[HttpGet]
		public object Centers(DataSourceLoadOptions loadOptions)
		{
			return DataSourceLoader.Load(TestData.Centers, loadOptions);
		}
		
		[HttpGet]
		public object Sections(int centerId, DataSourceLoadOptions loadOptions)
		{
			var sections = TestData.Sections.Where(s => s.CenterId == centerId);
			return DataSourceLoader.Load(sections, loadOptions);
		}
		
		[HttpGet]
		public object Options(int centerId, DataSourceLoadOptions loadOptions)
		{
			return DataSourceLoader.Load(TestData.Options, loadOptions);
		}
	}
}