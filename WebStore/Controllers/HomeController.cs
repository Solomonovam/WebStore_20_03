using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _Employee = new List<Employee> {
            new Employee{
                id = 1,
                Patronymic = "1",
                SurName = "11",
                FirstName = "111",
                Age = 11
            },
            new Employee{
                id = 2,
                Patronymic = "2",
                SurName = "22",
                FirstName = "222",
                Age = 22
            },
            new Employee{
                id = 3,
                Patronymic = "3",
                SurName = "33",
                FirstName = "333",
                Age = 33
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
            return View();
        }
    }
}