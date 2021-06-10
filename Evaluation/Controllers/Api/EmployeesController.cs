using System;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Evaluation.Data;
using Evaluation.Models;
using MD.PersianDateTime.Standard;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Evaluation.Controllers.Api
{
	[Authorize]
	[Route("api/[controller]/[action]")]
	public class EmployeesController : Controller
	{
		private readonly ScoreContext _dbContext;
		private readonly ScoreContextProcedures _uspContext;

		public EmployeesController(ScoreContext dbContext)
		{
			_dbContext = dbContext;
			_uspContext = _dbContext.GetProcedures();
		}

		[HttpGet]
		public async Task<object> Get(DataSourceLoadOptions loadOptions)
		{
			return await DataSourceLoader.LoadAsync(_dbContext.VwEmployee, loadOptions);
		}

		[HttpPost]
		public async Task<IActionResult> Insert(string values)
		{
			var newEmployee = new VwEmployee();
			JsonConvert.PopulateObject(values, newEmployee);

			await _uspContext.uspEmployeeInsertAsync(
				newEmployee.Nid,
				newEmployee.FirstName,
				newEmployee.LastName,
				newEmployee.PositionId,
				string.IsNullOrWhiteSpace(newEmployee.HireDate) ? (DateTime?) null : PersianDateTime.Parse(newEmployee.HireDate).ToDateTime(),
				newEmployee.ManagerLevel0,
				newEmployee.ManagerLevel1,
				newEmployee.ManagerLevel2);

			//await _uspContext.uspStaffPasswordInsertAsync(
			//	newStaff.Id,
			//	PasswordUtility.HashPassword("test123"),
			//	null, cancellationToken);

			return Ok();
		}

		[HttpPut]
		public async Task<IActionResult> Update(int key, string values)
		{
			var employee = new VwEmployee();
			JsonConvert.PopulateObject(values, employee);

			await _uspContext.uspEmployeeUpdateAsync(key,
				employee.Nid,
				employee.FirstName,
				employee.LastName,
				employee.PositionId,
				string.IsNullOrWhiteSpace(employee.HireDate) ? (DateTime?) null : PersianDateTime.Parse(employee.HireDate).ToDateTime(),
				employee.ManagerLevel0,
				employee.ManagerLevel1,
				employee.ManagerLevel2);

			return Ok();
		}

		[HttpDelete]
		public async Task Delete(int key)
		{
			await _uspContext.uspEmployeeDeleteAsync(key);
		}
	}
}