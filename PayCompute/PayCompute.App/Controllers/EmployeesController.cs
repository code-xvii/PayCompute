using Microsoft.AspNetCore.Mvc;
using PayCompute.App.Models;
using PayCompute.Services;
using System.Linq;

namespace PayCompute.App.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        public IActionResult Index()
        {
            var employees = _employeeService.GetEmployees().Select(e => new EmployeeIndexViewModel
            {
                Id = e.Id,
                EmployeeNo = e.EmployeeNo,
                FullName = e.FullName,
                Gender = e.Gender,
                ImageUrl = e.ImageUrl,
                City = e.City,
                Designation = e.Designation,
                DateJoined = e.DateJoined
            });
            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
