using DutyManager.Attributes;
using DutyManager.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DutyManager.Models
{
    [HolidayDateComparsion]
    public class Holiday
    {
        [HiddenInput(DisplayValue = false)]
        public int HolidayId { get; set; }
        public int EmployeeId { get; set; }
        [Display(Name = "Сотрудник")]
        public string EmployeeName { get; set; }
        [Display(Name = "Начало отпуска")]
        [Required(ErrorMessage = "Необходимо выбрать день")]
        public DateTime DateStart { get; set; } = DateTime.Today;
        [Display(Name = "Окончание отпуска")]
        [Required(ErrorMessage = "Необходимо выбрать день")]
        public DateTime DateFinish { get; set; } = DateTime.Today;
        [Display(Name = "Сотрудник")]
        [Required(ErrorMessage = "Необходимо выбрать сотрудника")]
        [ValidateNever]
        public IEnumerable<SelectListItem> Employees { get; set; }
        public static IEnumerable<Holiday> GetAllHolidays() => DBService.GetData<Holiday>(SqlStr.GetHolidays);
        public static Holiday GetHolidayById(int id) => id == -1 ? new Holiday() : GetAllHolidays().FirstOrDefault(x => x.HolidayId == id);
        public static void EditHoliday(Holiday day)
        {
            if (day.HolidayId == 0)
                DBService.AddHoliday(day);
            else
                DBService.EditHoliday(day);
        }
        public static void AddHoliday(Holiday day) => DBService.AddHoliday(day);
        public static void DelHoliday(int id) => DBService.DelHoliday(id);
        public static void DelAll() => DBService.DelAll(SqlStr.Holiday);
    }
}