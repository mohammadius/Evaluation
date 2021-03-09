using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Evaluation.Models;
using Microsoft.AspNetCore.Mvc;

namespace Evaluation.Controllers
{
	[Route("api/[controller]/[action]")]
	public class QuestionDetailsController  : Controller
	{
		[HttpGet]
		public object Get(DataSourceLoadOptions loadOptions)
		{
			return DataSourceLoader.Load(TestData.QuestionDetails, loadOptions);
		}
	}
}