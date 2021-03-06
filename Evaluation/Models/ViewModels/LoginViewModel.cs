using System.ComponentModel.DataAnnotations;

namespace Evaluation.Models.ViewModels
{
	public class LoginViewModel
	{
		[Required(AllowEmptyStrings = false, ErrorMessage = "نام کاربری الزامی است")]
		public string Username { get; set; }

		[Required(AllowEmptyStrings = false, ErrorMessage = "رمز عبور الزامی است")]
		public string Password { get; set; }
	}
}
