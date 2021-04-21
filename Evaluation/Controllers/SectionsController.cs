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
	public class SectionsController : Controller
	{
		private readonly EvaluationContext _dbContext;
		private readonly EvaluationContextProcedures _uspContext;

		public SectionsController(EvaluationContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions, CancellationToken cancellationToken)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwSection, loadOptions, cancellationToken);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values, CancellationToken cancellationToken)
		{
			var newSection = new VwSection();
			JsonConvert.PopulateObject(values, newSection);

			//TODO: validate newSection

			await _uspContext.uspSectionInsertAsync(newSection.CenterId, newSection.Title, null, cancellationToken);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(int key, string values, CancellationToken cancellationToken)
		{
			var section = new VwSection();
			JsonConvert.PopulateObject(values, section);

			//TODO: validate section

			await _uspContext.uspSectionUpdateAsync(key, section.CenterId, section.Title, null, cancellationToken);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key, CancellationToken cancellationToken)
		{
			await _uspContext.uspSectionDeleteAsync(key, null, cancellationToken);
		}
	}
}