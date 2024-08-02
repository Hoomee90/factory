using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;

namespace Factory.Controllers
{
	public class HomeController : Controller
	{
		private readonly FactoryContext _db;
		
		public HomeController(FactoryContext db)
		{
			_db = db;
		}

		[HttpGet("/")]
		public ActionResult Index()
		{
			ViewBag.categories = _db.Machines.ToList();
			ViewBag.items = _db.Engineers.ToList();
			return View();
		}
		
	}
}