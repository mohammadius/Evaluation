using System.Collections.Generic;

namespace Evaluation.Data
{
	public static class OptionsData
	{
		public static readonly Dictionary<string, List<Option>> AdminOptions = new Dictionary<string, List<Option>>
		{
			{
				"BasicInformation",
				new List<Option>
				{
					new Option {Href = "/information/employee", DisplayName = "کارمند"},
					new Option {Href = "/information/position", DisplayName = "سمت"},
					new Option {Href = "/information/manager", DisplayName = "مدیر"},
					new Option {Href = "/information/question", DisplayName = "پرسش"}
				}
			},
			{
				"UserSetting",
				new List<Option>
				{
					new Option {Href = "/user/changePassword", DisplayName = "تغییر رمز عبور"}
				}
			},
			{
				"Report",
				new List<Option>
				{
					new Option {Href = "/report/center", DisplayName = "مرکز"},
					new Option {Href = "/report/section", DisplayName = "بخش"},
					new Option {Href = "/report/position", DisplayName = "سمت"},
					new Option {Href = "/report/staff", DisplayName = "کارمند"}
				}
			}
		};
	}

	public class Option
	{
		public string Href { get; set; }
		public string DisplayName { get; set; }
	}
}