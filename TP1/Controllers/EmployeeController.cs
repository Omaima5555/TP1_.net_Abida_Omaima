using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TP1.Models;
using TP1.Models.Repositories;

namespace TP1.Controllers
{
    public class EmployeeController : Controller
    {
        // Injection de dépendance
        readonly IRepository<Employee> employeeRepository;

        public EmployeeController(IRepository<Employee> empRepository)
        {
            employeeRepository = empRepository;
        }

        // GET: EmployeeController
        public ActionResult Index()
        {
            var employees = employeeRepository.GetAll();
            ViewData["EmployeesCount"] = employees.Count();
            ViewData["SalaryAverage"] = employeeRepository.SalaryAverage();
            ViewData["MaxSalary"] = employeeRepository.MaxSalary();
            ViewData["HREmployeesCount"] = employeeRepository.HrEmployeesCount();
            return View(employees);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var employee = employeeRepository.FindByID(id);
            return View(employee);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View();
        }

        // post: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employeeRepository.Add(employee);
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }


        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = employeeRepository.FindByID(id);
            return View(employee);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Employee newemployee)
        {
            try
            {
                employeeRepository.Update(id, newemployee);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(newemployee);
            }
        }

        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var employee = employeeRepository.FindByID(id);
            return View(employee);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Employee employee)
        {
            try
            {
                employeeRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(employee);
            }
        }

        public ActionResult Search(string term)
        {
            var result = employeeRepository.Search(term);
            return View("Index", result);
        }
    }
}
