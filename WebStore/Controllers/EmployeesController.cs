using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;



        //[Route("employees")]
        public IActionResult Index() => View(_EmployeesData.GetAll());

        //[Route("employee/{id}")]
        public IActionResult Details(int Id) {
            //var employee = TestData.Employees.FirstOrDefault(e => e.id == Id);
            var employee = _EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();
            return View(employee);
        }

    }
}