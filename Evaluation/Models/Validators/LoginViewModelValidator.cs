using Evaluation.Models.ViewModels;
using FluentValidation;

namespace Evaluation.Models.Validators
{
	public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
	{
		public LoginViewModelValidator()
		{
			RuleFor(lvm => lvm.Username)
				.NotEmpty()
				.Length(10)
				.Matches("^[1-9][\\d]{9}$")
				.WithMessage("نام کاربری باید کد ملی باشد.");
			
			RuleFor(lvm => lvm.Password)
				.NotEmpty()
				.MinimumLength(6)
				.Matches("^(?=.*[A-Za-z])(?=.*\\d)[A-Za-z\\d]{6,}$")
				.WithMessage("رمز عبور باید حداقل 6 کاراکتر و شامل حرف و عدد باشد.");
		}
	}
}
