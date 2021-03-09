using System;
using System.Collections.Generic;
using Evaluation.Models.ViewModels;

namespace Evaluation.Models
{
	public static class TestData
	{
		public static List<CenterViewModel> Centers = new List<CenterViewModel>
		{
			new CenterViewModel {Id = 1, Title = "مرکز 1", Address = "آدرس 1"},
			new CenterViewModel {Id = 2, Title = "مرکز 2", Address = "آدرس 2"},
			new CenterViewModel {Id = 3, Title = "مرکز 3", Address = "آدرس 3"}
		};

		public static List<SectionViewModel> Sections = new List<SectionViewModel>
		{
			new SectionViewModel {Id = 1, Title = "بخش 1، مرکز 1", CenterId = 1},
			new SectionViewModel {Id = 2, Title = "بخش 2، مرکز 1", CenterId = 1},
			new SectionViewModel {Id = 3, Title = "بخش 3، مرکز 1", CenterId = 1},
			new SectionViewModel {Id = 4, Title = "بخش 1، مرکز 2", CenterId = 2},
			new SectionViewModel {Id = 5, Title = "بخش 2، مرکز 2", CenterId = 2},
			new SectionViewModel {Id = 6, Title = "بخش 3، مرکز 2", CenterId = 2},
			new SectionViewModel {Id = 7, Title = "بخش 1، مرکز 3", CenterId = 3},
			new SectionViewModel {Id = 8, Title = "بخش 2، مرکز 3", CenterId = 3},
			new SectionViewModel {Id = 9, Title = "بخش 3، مرکز 3", CenterId = 3}
		};

		public static List<StaffViewModel> Staffs = new List<StaffViewModel>
		{
			new StaffViewModel
			{
				Id = "1",
				FirstName = "محمد",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 1,
				SectionId = 1
			},
			new StaffViewModel
			{
				Id = "2",
				FirstName = "علی",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 1,
				SectionId = 2
			},
			new StaffViewModel
			{
				Id = "3",
				FirstName = "رضا",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 1,
				SectionId = 3
			},

			new StaffViewModel
			{
				Id = "4",
				FirstName = "محمد",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 2,
				SectionId = 4
			},
			new StaffViewModel
			{
				Id = "5",
				FirstName = "علی",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 2,
				SectionId = 5
			},
			new StaffViewModel
			{
				Id = "6",
				FirstName = "رضا",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 2,
				SectionId = 6
			},

			new StaffViewModel
			{
				Id = "7",
				FirstName = "محمد",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 3,
				SectionId = 7
			},
			new StaffViewModel
			{
				Id = "8",
				FirstName = "علی",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 3,
				SectionId = 8
			},
			new StaffViewModel
			{
				Id = "9",
				FirstName = "رضا",
				PersianBirthDate = "1399-11-25",
				PersianEmploymentDate = "1399-11-25",
				CenterId = 3,
				SectionId = 9
			}
		};

		public static List<QuestionDetailViewModel> QuestionDetails = new List<QuestionDetailViewModel>
		{
			new QuestionDetailViewModel
			{
				Id = 1,
				Title = "پرسش شماره یک",
				Coefficient = 1,
				Options = "گزینه 1=1;گزینه 2=2;گزینه 3=3;گزینه 4=4;گزینه 5=5"
			},
			new QuestionDetailViewModel
			{
				Id = 2,
				Title = "پرسش شماره دو",
				Coefficient = 1,
				Options = "گزینه 1=1;گزینه 2=2;گزینه 3=3;گزینه 4=4;گزینه 5=5"
			},
			new QuestionDetailViewModel
			{
				Id = 3,
				Title = "پرسش شماره سه",
				Coefficient = 1,
				Options = "گزینه 1=1;گزینه 2=2;گزینه 3=3;گزینه 4=4;گزینه 5=5"
			}
		};

		public static List<QuestionSectionViewModel> QuestionSections = new List<QuestionSectionViewModel>
		{
			new QuestionSectionViewModel {Id = 1, QuestionDetailId = 1, CenterId = 1, SectionId = 2},
			new QuestionSectionViewModel {Id = 2, QuestionDetailId = 2, CenterId = 1, SectionId = 1},
			new QuestionSectionViewModel {Id = 3, QuestionDetailId = 3, CenterId = 2, SectionId = 2},
			new QuestionSectionViewModel {Id = 4, QuestionDetailId = 2, CenterId = 3, SectionId = 3},
			new QuestionSectionViewModel {Id = 5, QuestionDetailId = 3, CenterId = 3, SectionId = 1}
		};

		public static Dictionary<string, List<Option>> Options = new Dictionary<string, List<Option>>
		{
			{
				"BasicInformation",
				new List<Option>
				{
					new Option {Href = "/information/center", DisplayName = "مرکز"},
					new Option {Href = "/information/section", DisplayName = "بخش"},
					new Option {Href = "/information/position", DisplayName = "سمت"},
					new Option {Href = "/information/staff", DisplayName = "کارمند"}
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