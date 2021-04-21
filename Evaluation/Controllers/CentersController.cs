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
	public class CentersController : Controller
	{
		private readonly EvaluationContext _dbContext;
		private readonly EvaluationContextProcedures _uspContext;

		public CentersController(EvaluationContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions, CancellationToken cancellationToken)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwCenter, loadOptions, cancellationToken);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values, CancellationToken cancellationToken)
		{
			var newCenter = new VwCenter();
			JsonConvert.PopulateObject(values, newCenter);

			//TODO: validate newCenter

			await _uspContext.uspCenterInsertAsync(newCenter.Title, newCenter.Address, null, cancellationToken);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(int key, string values, CancellationToken cancellationToken)
		{
			var center = new VwCenter();
			JsonConvert.PopulateObject(values, center);

			//TODO: validate center

			await _uspContext.uspCenterUpdateAsync(key, center.Title, center.Address, null, cancellationToken);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key, CancellationToken cancellationToken)
		{
			await _uspContext.uspCenterDeleteAsync(key, null, cancellationToken);
		}
	}
}