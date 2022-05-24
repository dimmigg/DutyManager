using DutyManager.DB;
using DutyManager.Models;
using DutyManager.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DutyManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Менеджер дежурств";
            //new Calculate().StartCalculate();

            IndexModel indexModel = new IndexModel
            {
                Data = DBService.GetData<MainTableModel>(SqlStr.GetMainTable)
            };
            return View(indexModel);
        }

        public IActionResult Employee()
        {
            ViewBag.Title = "Сотрудники";
            EmployeeModel indexModel = new EmployeeModel
            {
                Data = DBService.GetData<Employee>(SqlStr.GetEmployees)
            };
            return View(indexModel);
        }

        public IActionResult EmployeeEdit(int EmployeeId)
        {
            ViewBag.Title = "Сотрудники";
            var Data = DBService.GetData<Employee>(SqlStr.GetEmployees);
            var emp = Data.FirstOrDefault(x => x.EmployeeId == EmployeeId);
            return View(emp);
        }

        [HttpPost]
        public IActionResult EmployeeEdit(Employee emp)
        {
            ViewBag.Title = "Сотрудники";
            if(ModelState.IsValid)
            {
                //Сохранить изменения
                return Redirect("Employee");
            }
            return View();
        }
    }
}
