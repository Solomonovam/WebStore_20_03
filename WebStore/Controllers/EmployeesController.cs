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

        public IActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee Employee)
        {
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (!ModelState.IsValid)
                return View(Employee);

            _EmployeesData.Add(Employee);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }





        public IActionResult Edit(int? Id)
        {
            if (Id is null) return View(new Employee());

            if (Id < 0) return BadRequest();

            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null) return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee Employee)
        {
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (!ModelState.IsValid) 
                return View(Employee);

            var id = Employee.id;
            if (id == 0)
                _EmployeesData.Add(Employee);
            else
                _EmployeesData.Edit(id, Employee);

            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}