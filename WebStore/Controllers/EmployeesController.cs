using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Infrastructure.Mapping;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    //[Route("users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _EmployeesData;
        public EmployeesController(IEmployeesData EmployeesData) => _EmployeesData = EmployeesData;



        //[Route("employees")]
        public IActionResult Index() => View(_EmployeesData.GetAll().Select(e => e.ToView()));

        //[Route("employee/{id}")]
        public IActionResult Details(int Id)
        {
            //var employee = TestData.Employees.FirstOrDefault(e => e.id == Id);
            var employee = _EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }

        public IActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (!ModelState.IsValid)
                return View(employee);

            _EmployeesData.Add(employee.FromView());
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? Id)
        {
            if (Id is null) return View(new EmployeeViewModel());

            if (Id < 0) return BadRequest();

            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null) return NotFound();

            return View(employee.ToView());
        }

        [HttpPost]
        //public IActionResult Edit([Bind("Id","Name", "SecondName")]EmployeeViewModel Employee) //Bind - свяжет только узазанные параметры модели
        public IActionResult Edit(EmployeeViewModel Employee)
        {
            if (Employee is null)
                throw new ArgumentNullException(nameof(Employee));

            //Вывод ошибки в заголовке страницы
            if(Employee.Age == 74)
            ModelState.AddModelError(String.Empty,"Вывод ошибки в верхнюю часть экрана");

            if (!ModelState.IsValid)
                return View(Employee);

            var id = Employee.id;
            if (id == 0)
                _EmployeesData.Add(Employee.FromView());
            else
                _EmployeesData.Edit(id, Employee.FromView());

            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            if (Id <= 0) return BadRequest();
            var employee = _EmployeesData.GetById(Id);

            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }

        public IActionResult DeleteConfirmed(int Id)
        {
            _EmployeesData.Delete(Id);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}