using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Evaluation.Utilities
{
	public static class SignInUtility
	{
		public static async Task SignInAsync(this HttpContext httpContext, string id, string role, string section)
		{
			var identity = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Role, role),
					new Claim(MyClaimTypes.Id, id),
					new Claim(MyClaimTypes.Section, section),
					new Claim(MyClaimTypes.DisplayRole, $"{role} {section}")
				}, CookieAuthenticationDefaults.AuthenticationScheme);

			var principal = new ClaimsPrincipal(identity);

			await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}
	}
}