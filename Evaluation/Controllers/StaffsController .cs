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
	public class StaffsController : Controller
	{
		private readonly EvaluationContext _dbContext;
		private readonly EvaluationContextProcedures _uspContext;

		public StaffsController(EvaluationContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions, CancellationToken cancellationToken)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwStaff, loadOptions, cancellationToken);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values, CancellationToken cancellationToken)
		{
			var newStaff = new VwStaff();
			JsonConvert.PopulateObject(values, newStaff);

			//TODO: validate newStaff

			await _uspContext.uspStaffInsertAsync(
				newStaff.Id,
				newStaff.SectionId,
				newStaff.FirstName,
				newStaff.LastName,
				PersianDateTime.Parse(newStaff.BirthDate).ToDateTime(),
				PersianDateTime.Parse(newStaff.EmploymentDate).ToDateTime(),
				null, cancellationToken);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(string key, string values, CancellationToken cancellationToken)
		{
			var staff = new VwStaff();
			JsonConvert.PopulateObject(values, staff);

			//TODO: validate staff

			await _uspContext.uspStaffUpdateAsync(key,
				staff.SectionId,
				staff.FirstName,
				staff.LastName,
				PersianDateTime.Parse(staff.BirthDate).ToDateTime(),
				PersianDateTime.Parse(staff.EmploymentDate).ToDateTime(), null, cancellationToken);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(string key, CancellationToken cancellationToken)
		{
			await _uspContext.uspStaffDeleteAsync(key, null, cancellationToken);
		}
	}
}