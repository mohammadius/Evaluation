using System.Threading;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Evaluation.Data;
using Evaluation.Models;
using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Evaluation.Controllers
{
	[Authorize]
	[Route("api/[controller]/[action]")]
	public class QuestionSectionsController  : Controller
	{
		private readonly EvaluationContext _dbContext;
		private readonly EvaluationContextProcedures _uspContext;

		public QuestionSectionsController(EvaluationContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions, CancellationToken cancellationToken)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwSectionQuestion, loadOptions, cancellationToken);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values, CancellationToken cancellationToken)
		{
			var newSectionQuestion = new VwSectionQuestion();
			JsonConvert.PopulateObject(values, newSectionQuestion);

			//TODO: validate newSectionQuestion

			await _uspContext.uspSectionQuestionInsertAsync(
				newSectionQuestion.SectionId,
				newSectionQuestion.QuestionId,
				PersianDateTime.Parse(newSectionQuestion.StartDate).ToDateTime(),
				null, cancellationToken);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(int key, string values, CancellationToken cancellationToken)
		{
			var sectionQuestion = new VwSectionQuestion();
			JsonConvert.PopulateObject(values, sectionQuestion);

			//TODO: validate sectionQuestion

			await _uspContext.uspSectionQuestionUpdateAsync(
				key,
				sectionQuestion.SectionId,
				sectionQuestion.QuestionId,
				PersianDateTime.Parse(sectionQuestion.StartDate).ToDateTime(),
				null, cancellationToken);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key, CancellationToken cancellationToken)
		{
			await _uspContext.uspSectionQuestionDeleteAsync(key, null, cancellationToken);
		}
	}
}