using System.Threading;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Evaluation.Data;
using Evaluation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Evaluation.Controllers
{
	[Authorize]
	[Route("api/[controller]/[action]")]
	public class QuestionDetailsController  : Controller
	{
		private readonly EvaluationContext _dbContext;
		private readonly EvaluationContextProcedures _uspContext;

		public QuestionDetailsController(EvaluationContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions, CancellationToken cancellationToken)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwQuestion, loadOptions, cancellationToken);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values, CancellationToken cancellationToken)
		{
			var newQuestion = new VwQuestion();
			JsonConvert.PopulateObject(values, newQuestion);

			//TODO: validate newQuestion

			await _uspContext.uspQuestionInsertAsync(newQuestion.Title,newQuestion.Options,newQuestion.Coefficiency, null, cancellationToken);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(int key, string values, CancellationToken cancellationToken)
		{
			var question = new VwQuestion();
			JsonConvert.PopulateObject(values, question);

			//TODO: validate question

			await _uspContext.uspQuestionUpdateAsync(key, question.Title,question.Options,question.Coefficiency, null, cancellationToken);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key, CancellationToken cancellationToken)
		{
			await _uspContext.uspQuestionDeleteAsync(key, null, cancellationToken);
		}
	}
}