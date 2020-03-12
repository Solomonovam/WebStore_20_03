using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("users")]
    public class EmployeesController : Controller
    {
        private static readonly List<Employee> _Employee = new List<Employee> {
            new Employee{
                id = 1,
                Patronymic = "Иванов",
                SurName = "Иван",
                FirstName = "Иванович",
                Age = 39
            },
            new Employee{
                id = 2,
                Patronymic = "Петров",
                SurName = "Пётр",
                FirstName = "Петрович",
                Age = 18
            },
            new Employee{
                id = 3,
                Patronymic = "Сидоров",
                SurName = "Сидор",
                FirstName = "Сидорович",
                Age = 27
            },
        };

        //[Route("employees")]
        public IActionResult Index() => View(_Employee);

        //[Route("employee/{id}")]
        public IActionResult Details(int id) {
            var employee = _Employee.FirstOrDefault(e => e.id == id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

    }
}