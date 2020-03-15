using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels
{
    public class EmployeeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int id { get; set; }

        [Display(Name = "Имя")]
        [Required(ErrorMessage = "Имя обязательно")]
        [MinLength(3, ErrorMessage = "Минимальная длинна 3-и символа")]
        public String Name { get; set; }


        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательно")]
        [MinLength(3, ErrorMessage = "Минимальная длинна 3-и символа")]
        public String SecondName { get; set; }

        [Display(Name = "Отчество")]
        public String Patronymic { get; set; }

        [Display(Name = "Возраст")]
        public int Age { get; set; }

    }
}
