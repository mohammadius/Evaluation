using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Evaluation.Data;
using Evaluation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Evaluation.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]/[action]")]
	public class QuestionsController : Controller
	{
		private readonly ScoreContext _dbContext;
		private readonly ScoreContextProcedures _uspContext;

		public QuestionsController(ScoreContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwQuestion, loadOptions);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values)
		{
			var newQuestion = new VwQuestion();
			JsonConvert.PopulateObject(values, newQuestion);

			await _uspContext.uspQuestionInsertAsync(newQuestion.Title, newQuestion.Coef, newQuestion.Options);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(int key, string values)
		{
			var question = new VwQuestion();
			JsonConvert.PopulateObject(values, question);

			await _uspContext.uspQuestionUpdateAsync(key, question.Title, question.Coef, question.Options);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key)
		{
			await _uspContext.uspQuestionDeleteAsync(key);
		}
	}
}