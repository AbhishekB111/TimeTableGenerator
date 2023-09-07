using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using TimeTableGenerator.ViewModels;

namespace TimeTableGenerator.Controllers
{
	public class TimeTableController : Controller
	{
		#region"Initial Requirements"
		public IActionResult InitialRequirements()
		{
			return View();
		}

		[HttpPost]
		public IActionResult InitialRequirements(InitialRequirementsViewModel Model)
		{
			if (ModelState.IsValid)
			{
				HttpContext.Session.SetInt32("NoOfWorkingDays", Model.NoOfWorkingDays);
				HttpContext.Session.SetInt32("NoOfSubPerDay", Model.NoOfSubjectsPerDay);
				return RedirectToAction("SubjectDetails", "TimeTable", Model);
			}
			return View();
		}
		#endregion
				
		#region"Subject Details"
		[HttpGet]
		public IActionResult SubjectDetails(InitialRequirementsViewModel Model)
		{
			if (Model != null)
			{
				List<SubjectDetailsViewModel> models = new List<SubjectDetailsViewModel>();

				for (int i = 1; i <= Model.TotalSubjects; i++)
				{
					SubjectDetailsViewModel model = new SubjectDetailsViewModel();
					models.Add(model);
				}

				ViewBag.NoOfSubjects = Model.TotalSubjects;
				HttpContext.Session.SetInt32("NoOfHrs", (int)Model.TotalHrsOfWeek);
				models[0].Subjects = (int)Model.TotalHrsOfWeek;
				ViewBag.IsVarified = false;
				return View(models);
			}
			return View();
		}

		[HttpPost]
		public IActionResult SubjectDetails(List<SubjectDetailsViewModel> Model)
		{

			if (ModelState.IsValid)
			{
				int NoOfHrsInWeek = (int)HttpContext.Session.GetInt32("NoOfHrs");
				int UserEntredHrs = 0;

				foreach (var item in Model)
				{
					UserEntredHrs = UserEntredHrs + item.SubjectHours;
				}

				if (NoOfHrsInWeek > UserEntredHrs)
				{
					ViewBag.Message = $"You have entered hours less than actual hours (i.e {NoOfHrsInWeek}) please enter valid input";
					ViewBag.NoOfSubjects = Model.Count;
					return View();
				}
				else if (NoOfHrsInWeek < UserEntredHrs)
				{
					ViewBag.Message = $"You have entered hours more than actual hours (i.e {NoOfHrsInWeek}) please enter valid input";
					ViewBag.NoOfSubjects = Model.Count;
					return View();
				}

				ViewBag.NoOfSubjects = Model.Count;
				ViewBag.IsVarified = true;
				return View();
			}
			ViewBag.NoOfSubjects = Model.Count;
			ViewBag.IsVarified = false;
			return View();
		}
		#endregion

		#region"Generate Table"
		[HttpPost]
		public IActionResult GenerateTable(List<SubjectDetailsViewModel> Model)
		{
			ViewBag.NoOfWorkingDays = HttpContext.Session.GetInt32("NoOfWorkingDays");
			ViewBag.NoOfSubPerDay = HttpContext.Session.GetInt32("NoOfSubPerDay");
			
			if (ModelState.IsValid)
			{
				var list = RandomDistributer(Model);
				return View(list);
			}

			ViewBag.NoOfSubjects = Model.Count;
			ViewBag.IsVarified = false;
			return View("SubjectDetails", Model);
		}
		#endregion

		#region"Methods"
		private List<GenerateTableViewModel> RandomDistributer(List<SubjectDetailsViewModel> list)
		{
			int TotalHrs = (int)HttpContext.Session.GetInt32("NoOfHrs");
			List<GenerateTableViewModel> model = new List<GenerateTableViewModel>();
			foreach (var item in list)
			{
				for(int i = 1; i <= item.SubjectHours; i++)
				{
					GenerateTableViewModel subjects = new GenerateTableViewModel();
					subjects.SubjectName = item.SubjectName;
					model.Add(subjects);
				}
			}


			List<GenerateTableViewModel> items = new List<GenerateTableViewModel>();
			Random random = new Random();
			int count = model.Count;
			while (items.Count < count)
			{
				GenerateTableViewModel subject = model.ElementAtOrDefault(random.Next(0, count - 1));
				if (subject != null)
				{
					items.Add(subject);
					model.Remove(subject);
				}
			}
			return items;
		}
		#endregion
	}
}
