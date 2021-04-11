using System.Collections.Generic;

namespace Evaluation.Data
{
	public static class OptionsData
	{
		public static Dictionary<string, List<Option>> AdminOptions = new Dictionary<string, List<Option>>
		{
			{
				"BasicInformation",
				new List<Option>
				{
					new Option {Href = "/information/center", DisplayName = "مرکز"},
					new Option {Href = "/information/section", DisplayName = "بخش"},
					new Option {Href = "/information/position", DisplayName = "سمت"},
					new Option {Href = "/information/staff", DisplayName = "کارمند"},
					new Option {Href = "/information/staffPosition", DisplayName = "سمت کارمند"}
			}
			},
			{
				"Question",
				new List<Option>
				{
					new Option {Href = "/question/detail", DisplayName = "پرسش"},
					new Option {Href = "/question/section", DisplayName = "پرسش مرکز"}
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