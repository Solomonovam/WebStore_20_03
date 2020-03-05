using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
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


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SomeAction()
        {
            return Content("2");
        }

        public IActionResult Employees()
        {
            return View(_Employee);
        }

        public IActionResult Employee(int id)
        {
            var employee = _Employee.FirstOrDefault(e => e.id == id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

    }
}