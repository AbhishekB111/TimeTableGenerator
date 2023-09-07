using System.ComponentModel.DataAnnotations;

namespace TimeTableGenerator.ViewModels
{
	public class SubjectDetailsViewModel
	{
		[Required]
		public String SubjectName { get; set; }
		[Required]
		[Range(1,int.MaxValue,ErrorMessage = "Please enter at least one hour")]
		public int SubjectHours { get; set; }
		public int Subjects { get; set; }
	}
}
