using Cobblestone.Models;
using Data;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Cobblestone.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Add()
        {
            return View();
        }

        public ActionResult ListView()
        {
            return View();
        }

        [HttpGet, Route("list")]
        public ActionResult List()
        {
            var repository = new EmployeeData();
            var employess = repository.GetList().Select(x => new Employee
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Birthday= x.Birthday,
                Age = x.Age
            });
            if (employess == null)
                return View("Error");
            
            return PartialView(employess);
        }

        [HttpPost]
        public ActionResult SaveEmployee(Employee employee)
        {
            if (!ModelState.IsValid)
                return View("Add", employee);

            var repository = new EmployeeData();
            var employeeId = repository.SaveEmployee(employee.Id, employee.FirstName, employee.LastName, employee.Birthday, out string error);
            if (employeeId != null)
                return RedirectToAction("Details", new { id = employeeId });

            return View("Error");
        }

        public ActionResult Details(int id)
        {
            var repository = new EmployeeData();
            var employee = repository.GetEmployee(id, out string error);
            var model = new Employee
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Age = employee.Age,
                Birthday = employee.Birthday
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}