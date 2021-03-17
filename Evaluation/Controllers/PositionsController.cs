using System.Threading;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Evaluation.Data;
using Evaluation.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Evaluation.Controllers
{
	[Route("api/[controller]/[action]")]
	public class PositionsController : Controller
	{
		private readonly EvaluationContext _dbContext;
		private readonly EvaluationContextProcedures _uspContext;

		public PositionsController(EvaluationContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions, CancellationToken cancellationToken)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwPosition, loadOptions, cancellationToken);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values, CancellationToken cancellationToken)
		{
			var newPosition = new VwPosition();
			JsonConvert.PopulateObject(values, newPosition);

			//TODO: validate newPosition

			await _uspContext.uspPositionInsertAsync(newPosition.Title, null, cancellationToken);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(int key, string values, CancellationToken cancellationToken)
		{
			var position = new VwPosition();
			JsonConvert.PopulateObject(values, position);

			//TODO: validate position

			await _uspContext.uspPositionUpdateAsync(key, position.Title, null, cancellationToken);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key, CancellationToken cancellationToken)
		{
			await _uspContext.uspPositionDeleteAsync(key, null, cancellationToken);
		}
	}
}