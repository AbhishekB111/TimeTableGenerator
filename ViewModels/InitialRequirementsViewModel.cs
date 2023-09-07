using System.ComponentModel.DataAnnotations;

namespace TimeTableGenerator.ViewModels
{
	public class InitialRequirementsViewModel
	{
		[Display(Name = "No Of Working Days")]
		[Required(ErrorMessage ="This is Required")]
		[Range(1,7)]
		public Int32 NoOfWorkingDays { get; set; }

		[Display(Name = "No Of Subjects Per Day")]
		[Required]
		[Range(1,8)]
		public Int32 NoOfSubjectsPerDay { get; set; }

		[Required]
		[Display(Name = "Total Subjects")]
		[Range(1,int.MaxValue,ErrorMessage = "Enter only positive number.")]
		public Int32 TotalSubjects { get; set; }

		public Int32? TotalHrsOfWeek { get; set; }
	}
}
