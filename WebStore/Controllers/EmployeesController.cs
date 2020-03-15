using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
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
        public IActionResult Index() => View(_EmployeesData.GetAll().Select(e => new EmployeeViewModel
        {
            id = e.id,
            Name = e.FirstName,
            SecondName = e.SurName,
            Patronymic = e.Patronymic,
            Age = e.Age
        }));

        //[Route("employee/{id}")]
        public IActionResult Details(int Id)
        {
            //var employee = TestData.Employees.FirstOrDefault(e => e.id == Id);
            var employee = _EmployeesData.GetById(Id);
            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                id = employee.id,
                Name = employee.FirstName,
                SecondName = employee.SurName,
                Patronymic = employee.Patronymic,
                Age = employee.Age

            });
        }

        public IActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(employee));

            if (!ModelState.IsValid)
                return View(new EmployeeViewModel
                {
                    id = employee.id,
                    Name = employee.FirstName,
                    SecondName = employee.SurName,
                    Patronymic = employee.Patronymic,
                    Age = employee.Age

                });

            _EmployeesData.Add(employee);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? Id)
        {
            if (Id is null) return View(new EmployeeViewModel());

            if (Id < 0) return BadRequest();

            var employee = _EmployeesData.GetById((int)Id);
            if (employee is null) return NotFound();

            return View(new EmployeeViewModel
            {
                id = employee.id,
                Name = employee.FirstName,
                SecondName = employee.SurName,
                Patronymic = employee.Patronymic,
                Age = employee.Age

            });
        }

        [HttpPost]
        //public IActionResult Edit([Bind("Id","Name", "SecondName")]EmployeeViewModel Employee) //Bind - свяжет только узазанные параметры модели
        public IActionResult Edit([Bind("Id","Name")]EmployeeViewModel Employee)
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
                _EmployeesData.Add(new Employee
                {
                    id = Employee.id,
                    FirstName = Employee.Name,
                    SurName = Employee.SecondName,
                    Patronymic = Employee.Patronymic,
                    Age = Employee.Age

                });
            else
                _EmployeesData.Edit(id, new Employee
                {
                    id = Employee.id,
                    FirstName = Employee.Name,
                    SurName = Employee.SecondName,
                    Patronymic = Employee.Patronymic,
                    Age = Employee.Age

                });

            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int Id)
        {
            if (Id <= 0) return BadRequest();
            var employee = _EmployeesData.GetById(Id);

            if (employee is null)
                return NotFound();

            return View(new EmployeeViewModel
            {
                id = employee.id,
                Name = employee.FirstName,
                SecondName = employee.SurName,
                Patronymic = employee.Patronymic,
                Age = employee.Age

            });
        }

        public IActionResult DeleteConfirmed(int Id)
        {
            _EmployeesData.Delete(Id);
            _EmployeesData.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}