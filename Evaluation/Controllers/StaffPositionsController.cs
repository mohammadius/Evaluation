using System.Threading;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Evaluation.Data;
using Evaluation.Models;
using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Evaluation.Controllers
{
	[Route("api/[controller]/[action]")]
	public class StaffPositionsController : Controller
	{
		private readonly EvaluationContext _dbContext;
		private readonly EvaluationContextProcedures _uspContext;

		public StaffPositionsController(EvaluationContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions, CancellationToken cancellationToken)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwStaffPosition, loadOptions, cancellationToken);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values, CancellationToken cancellationToken)
		{
			var newStaffPosition = new VwStaffPosition();
			JsonConvert.PopulateObject(values, newStaffPosition);

			//TODO: validate newStaffPosition

			await _uspContext.uspStaffPositionInsertAsync(
				newStaffPosition.StaffId,
				newStaffPosition.PositionId,
				PersianDateTime.Parse(newStaffPosition.StartDate).ToDateTime(),
				null, cancellationToken);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(string key, string values, CancellationToken cancellationToken)
		{
			var staffPosition = new VwStaffPosition();
			JsonConvert.PopulateObject(values, staffPosition);

			//TODO: validate staffPosition

			await _uspContext.uspStaffPositionUpdateAsync(key,
				staffPosition.StaffId,
				staffPosition.PositionId,
				PersianDateTime.Parse(staffPosition.StartDate).ToDateTime(),
				null, cancellationToken);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(string key, CancellationToken cancellationToken)
		{
			await _uspContext.uspStaffPositionDeleteAsync(key, null, cancellationToken);
		}
	}
}