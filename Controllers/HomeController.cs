using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TimeTableGenerator.ViewModels;

namespace TimeTableGenerator.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		
	}
}