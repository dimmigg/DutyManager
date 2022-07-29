using DutyManager.DB;
using Microsoft.AspNetCore.Mvc;
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
    public class DayOfWeek
    {
        [HiddenInput(DisplayValue = false)]
        public int DayOfWeekId { get; set; }

        public string DayOfWeekName { get; set; }

        public static IEnumerable<DayOfWeek> GetAllDaysOfWeek() => DBService.GetData<DayOfWeek>(SqlStr.GetDaysOfWeek);
        public static DayOfWeek GetDayOfWeekById(int id) => id == -1 ? new DayOfWeek() : GetAllDaysOfWeek().FirstOrDefault(x => x.DayOfWeekId == id);
    }
}
