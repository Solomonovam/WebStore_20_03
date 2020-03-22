using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.ViewModels.Identity
{
    public class LoginViewModel
    {
        [Required]
        [MaxLength(250)]
        [Display(Name = "Имя пользователя")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Запомни меня")]
        [Compare(nameof(Password))]
        public bool RememberMy { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string ReturnUrl { get; set; }

    }
}
