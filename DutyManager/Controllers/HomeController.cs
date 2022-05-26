using DutyManager.DB;
using DutyManager.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DutyManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View(MainTableModel.GetAllItems());

        public IActionResult ListEmployee() => View(Employee.GetAllEmployees());

        public IActionResult EmployeeEditById(int Id) => PartialView("_EmployeeEdit", Employee.GetEmployeeById(Id));
        public IActionResult EmployeeDeleteById(int Id) => PartialView("_EmployeeDelete", Employee.GetEmployeeById(Id));
        public IActionResult EmployeeAdd() => PartialView("_EmployeeAdd");
        public IActionResult EmployeeDel(int EmployeeId)
        {
            Employee.DelEmployee(EmployeeId);
            return Redirect("ListEmployee");
        }

        [HttpPost]
        public IActionResult EmployeeEdit(Employee emp)
        {
            if(ModelState.IsValid)
            {
                Employee.EditEmployee(emp);
                return Redirect("ListEmployee");
            }
            return View("_EmployeeEdit", emp);
        }
    }
}
