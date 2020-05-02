using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Domain.ViewModels.Identity
{
    public class RegisterUserViewModel
    {
        [Required]
        [MaxLength(250)]
        [Display(Name = "Имя пользователя")]
        [Remote("IsNameFree", "Account", ErrorMessage = "Пользователь с таким именем уже существует")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Повторите пароль")]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }

    }
}
