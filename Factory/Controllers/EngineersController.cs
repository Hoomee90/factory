using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Factory.Models;
using System.Linq;

namespace Factory.Controllers
{
	public class EngineersController : Controller
	{
		private readonly FactoryContext _db;
		
		public EngineersController(FactoryContext db)
		{
			_db = db;
		}
		
		public ActionResult Index()
		{
			return View(_db.Engineers.ToList());
		}
		
		public ActionResult Create()
		{
			return View();
		}
		
		[HttpPost]
		public ActionResult Create(Engineer engineer)
		{
			if (!ModelState.IsValid)
			{
				return View(engineer);
			}
			else
			{
				_db.Engineers.Add(engineer);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
		}
		
		public ActionResult Details(int id)
		{
			Engineer thisEngineer = _db.Engineers
				.Include(engineer => engineer.JoinEntities)
				.ThenInclude(join => join.Machine)
				.FirstOrDefault(engineer => engineer.EngineerId == id);
			return View(thisEngineer);
		}
		
		public ActionResult Edit(int id)
		{
			Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
			return View(thisEngineer);
		}
		
		[HttpPost]
		public ActionResult Edit(Engineer engineer)
		{
			if (!ModelState.IsValid)
			{
				return View(engineer);
			}
			else
			{
				_db.Engineers.Update(engineer);
				_db.SaveChanges();
				return RedirectToAction("Index");
			}
		}
		
		public ActionResult AddMachine(int id)
		{
			Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
			ViewBag.MachineId = new SelectList(_db.Machines, "MachineId", "Name");
			return View(thisEngineer);
		}
		
		[HttpPost]
		public ActionResult AddMachine(Engineer engineer, int MachineId)
		{
			bool joinEntityExists = _db.EngineerMachines.Any(join => join.MachineId == MachineId && join.EngineerId == engineer.EngineerId);
			if (!joinEntityExists && MachineId != 0)
			{
				_db.EngineerMachines.Add(new EngineerMachine() { MachineId = MachineId, EngineerId = engineer.EngineerId });
				_db.SaveChanges();
			}
			return RedirectToAction("Details", new { id = engineer.EngineerId});
		}
		
		public ActionResult Delete(int id)
		{
			Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
			return View(thisEngineer);
		}
		
		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id)
		{
				Engineer thisEngineer = _db.Engineers.FirstOrDefault(engineer => engineer.EngineerId == id);
				_db.Engineers.Remove(thisEngineer);
				_db.SaveChanges();
				return RedirectToAction("Index");
		}
		
		[HttpPost]
		public ActionResult DeleteJoin(int joinId)
		{
			EngineerMachine joinEntry = _db.EngineerMachines.FirstOrDefault(entry => entry.EngineerMachineId == joinId);
			_db.EngineerMachines.Remove(joinEntry);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}