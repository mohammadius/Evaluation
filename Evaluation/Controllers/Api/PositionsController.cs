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
	public class PositionsController : Controller
	{
		private readonly ScoreContext _dbContext;
		private readonly ScoreContextProcedures _uspContext;

		public PositionsController(ScoreContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwPosition, loadOptions);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values)
		{
			var newPosition = new VwPosition();
			JsonConvert.PopulateObject(values, newPosition);

			await _uspContext.uspPositionInsertAsync(newPosition.Title);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(int key, string values)
		{
			var position = new VwPosition();
			JsonConvert.PopulateObject(values, position);

			await _uspContext.uspPositionUpdateAsync(key, position.Title);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key)
		{
			await _uspContext.uspPositionDeleteAsync(key);
		}
	}
}