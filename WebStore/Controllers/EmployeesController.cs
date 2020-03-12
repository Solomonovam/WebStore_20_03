using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("users")]
    public class EmployeesController : Controller
    {

        //[Route("employees")]
        public IActionResult Index() => View(TestData.Employees);

        //[Route("employee/{id}")]
        public IActionResult Details(int id) {
            var employee = TestData.Employees.FirstOrDefault(e => e.id == id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

    }
}