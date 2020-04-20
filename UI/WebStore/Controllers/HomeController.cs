using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Domain.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        


        public IActionResult Index() => View();

        public IActionResult Throw(string id) => throw new ApplicationException(id); //Исключение


        public IActionResult SomeAction() => Content("2");

        public IActionResult Error404() => View();
        public IActionResult Blog() => View();
        public IActionResult BlogSingle() => View();
        public IActionResult Cart() => View();
        public IActionResult CheckOut() => View();
        public IActionResult ContactUs() => View();


        public IActionResult ErrorStatus(string Code) => RedirectToAction(nameof(Error404));

    }
}