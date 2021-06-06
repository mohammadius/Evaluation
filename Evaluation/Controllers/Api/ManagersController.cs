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
	public class ManagersController : Controller
	{
		private readonly ScoreContext _dbContext;
		private readonly ScoreContextProcedures _uspContext;

		public ManagersController(ScoreContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwManager, loadOptions);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values)
		{
			var newManager = new VwManager();
			JsonConvert.PopulateObject(values, newManager);

			await _uspContext.uspManagerInsertAsync(newManager.EmployeeId);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key)
		{
			await _uspContext.uspManagerDeleteAsync(key);
		}
	}
}