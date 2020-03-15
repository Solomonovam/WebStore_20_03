using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;

namespace WebStore.Data
{
    public class TestData
    {
        public static List<Employee> Employees { get; } = new List<Employee> {
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

    }
}

