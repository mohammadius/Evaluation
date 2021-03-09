namespace Evaluation.Models.ViewModels
{
	public class QuestionDetailViewModel
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public byte Coefficient { get; set; }
		
		public string Options { get; set; }

		//public List<KeyValuePair<string, int>> Answers { get; set; }
	}
}
