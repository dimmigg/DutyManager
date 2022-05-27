using DutyManager.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.Models
{
    public class Workday
    {
        [HiddenInput(DisplayValue = false)]
        public int WorkdayId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int EmployeeId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int RosterId { get; set; }
        [Display(Name = "Постоянно")]
        public bool IsAlways { get; set; } = false;
        [DataType(DataType.Date), Required(ErrorMessage = "Необходимо выбрать день")]
        [Display(Name = "День")]
        public DateTime DateWork { get; set; } = DateTime.Now;
        public string FullName { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationOfDuty { get; set; }
        [Display(Name = "Сотрудник")]
        [Required(ErrorMessage = "Необходимо выбрать сотрудника")]
        [ValidateNever]
        public IEnumerable<SelectListItem> Employees { get; set; }
        [Display(Name = "Дежурство")]
        [Required(ErrorMessage = "Необходимо выбрать дужерство")]
        [ValidateNever]
        public IEnumerable<SelectListItem> Roster { get; set; }
        public DateTime StartDateWork => DateWork + StartTime;
        public DateTime EndDateWork => StartDateWork.AddHours(DurationOfDuty);
        public static IEnumerable<Workday> GetAllWorkdays() => DBService.GetData<Workday>(SqlStr.GetWorkdays);
        public static Workday GetWorkdayById(int id) => id == -1 ? new Workday() : GetAllWorkdays().FirstOrDefault(x => x.WorkdayId == id);
        public static void EditWorkday(Workday day)
        {
            if (day.WorkdayId == 0)
                DBService.AddWorkday(day);
            else
                DBService.EditWorkday(day);
        }

        public static void AddWorkday(Workday day) => DBService.AddWorkday(day);
        public static void DelWorkday(int id) => DBService.DelWorkday(id);



    }
}
