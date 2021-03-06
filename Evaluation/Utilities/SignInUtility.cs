using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Evaluation.Utilities
{
	public static class SignInUtility
	{
		public static async Task SignInAsync(this HttpContext httpContext, string userId, string userRole)
		{
			var identity = new ClaimsIdentity(new[]
			{
				new Claim(ClaimTypes.Name, userId),
				new Claim(ClaimTypes.Role, userRole)
			}, CookieAuthenticationDefaults.AuthenticationScheme);

			var principal = new ClaimsPrincipal(identity);

			await httpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}
	}
}