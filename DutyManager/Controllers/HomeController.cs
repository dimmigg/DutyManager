using DutyManager.DB;
using DutyManager.Extensions;
using DutyManager.Models;
using DutyManager.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Менеджер дежурств";
            new Calculate().StartCalculate();

            return View();
        }
    }
}
