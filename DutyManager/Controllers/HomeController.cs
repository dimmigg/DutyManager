using DutyManager.Models;
using DutyManager.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DutyManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => View(MainTableModel.GetAllItems());
        public IActionResult Calc()
        {
            Calculate.StartCalculate();
            return Redirect("Index");
        }

        [HttpPost]
        public IActionResult Calc(DateTime BeginDate, DateTime EndDate)
        {
            Calculate.StartCalculate(BeginDate, EndDate);
            return Redirect("Index");
        }

        [HttpGet]
        public IActionResult Export()
        {
            FileExport.WriteToExcel(MainTableModel.GetAllItems(), $"E:\\Cache\\DutyManager\\temp.xlsx");
            return File(System.IO.File.ReadAllBytes($"E:\\Cache\\DutyManager\\temp.xlsx"), "application/octet-stream", "дежурства.xlsx");
        }


        #region Employee
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
            if (ModelState.IsValid)
            {
                Employee.EditEmployee(emp);
                return Redirect("ListEmployee");
            }
            return View("_EmployeeEdit", emp);
        }
        #endregion

        #region Workday
        public IActionResult ListWorkdays() => View(Workday.GetAllWorkdays());
        public IActionResult WorkdayEditById(int Id) => PartialView("_WorkdayEdit", Workday.GetWorkdayById(Id));
        public IActionResult WorkdayDeleteById(int Id) => PartialView("_WorkdayDelete", Workday.GetWorkdayById(Id));
        public IActionResult WorkdayAdd() => PartialView("_WorkdayAdd");
        public IActionResult WorkdayDel(int WorkdayId)
        {
            if (WorkdayId != 0)
                Workday.DelWorkday(WorkdayId);
            else
                Workday.DelAll();
            return Redirect("ListWorkdays");
        }

        [HttpPost]
        public IActionResult WorkdayEdit(Workday day)
        {
            if (ModelState.IsValid)
            {
                Workday.EditWorkday(day);
                return Redirect("ListWorkdays");
            }
            return View("_WorkdayEdit", day);
        }
        #endregion

        #region Holiday
        public IActionResult ListHolidays() => View(Holiday.GetAllHolidays());
        public IActionResult HolidayEditById(int Id) => PartialView("_HolidayEdit", Holiday.GetHolidayById(Id));
        public IActionResult HolidayDeleteById(int Id) => PartialView("_HolidayDelete", Holiday.GetHolidayById(Id));
        public IActionResult HolidayAdd() => PartialView("_HolidayAdd");
        public IActionResult HolidayDel(int HolidayId)
        {
            if (HolidayId != 0)
                Holiday.DelHoliday(HolidayId);
            else
                Holiday.DelAll();
            return Redirect("ListHolidays");
        }

        [HttpPost]
        public IActionResult HolidayEdit(Holiday day)
        {
            if (ModelState.IsValid)
            {
                Holiday.EditHoliday(day);
                return Redirect("ListHolidays");
            }
            return View("_HolidayEdit", day);
        }
        #endregion

        #region Roster
        public IActionResult ListRoster() => View(Roster.GetAllRoster());
        public IActionResult RosterEditById(int Id) => PartialView("_RosterEdit", Roster.GetRosterById(Id));
        public IActionResult RosterDeleteById(int Id) => PartialView("_RosterDelete", Roster.GetRosterById(Id));
        public IActionResult RosterAdd() => PartialView("_RosterAdd");
        public IActionResult RosterDel(int RosterId)
        {
            Roster.DelRoster(RosterId);
            return Redirect("ListRoster");
        }

        [HttpPost]
        public IActionResult RosterEdit(Roster ros)
        {
            if (ModelState.IsValid)
            {
                Roster.EditRoster(ros);
                return Redirect("ListRoster");
            }
            return View("_RosterEdit", ros);
        }

        public ActionResult GetRosterByDate(int id)
        {
            ViewBag.DayOfWeekId = id;
            return PartialView("_RosterOption", new Workday());
        }
        #endregion
    }
}
