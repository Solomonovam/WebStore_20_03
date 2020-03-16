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
        [StringLength(maximumLength: 200, MinimumLength = 3, ErrorMessage = "Длинна имени должна быть >3 и <200")]
     //   [RegularExpressionAttribute(@"(?:[А-ЯЁ][а-яё]+)", ErrorMessage = "Вводить только русскаими буквами")]
        public String Name { get; set; }


        [Display(Name = "Фамилия")]
        [Required(ErrorMessage = "Фамилия обязательна")]
        [MinLength(3, ErrorMessage = "Минимальная длинна 3-и символа")]
        public String SecondName { get; set; }

        [Display(Name = "Отчество")]
        public String Patronymic { get; set; }

        [Display(Name = "Возраст")]
        [Required(ErrorMessage = "Возраст обязателен")]
        [Range(18, 75, ErrorMessage = "Возраст не соответствует диапазону от 18 до 75")]
        public int Age { get; set; }

    }
}
