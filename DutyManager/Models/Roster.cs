using DutyManager.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.Models
{
    public class Roster
    {
        [HiddenInput(DisplayValue = false)]
        public int RosterId { get; set; }
        [HiddenInput(DisplayValue = false)]
        public int DayOfWeekId { get; set; }
        [DataType(DataType.Time), Required(ErrorMessage = "Необходимо ввести время")]
        [Display(Name = "Начало дежурства")]
        public TimeSpan StartTime { get; set; }
        [DataType(DataType.Time), Required]
        public TimeSpan EndTime => StartTime.Add(TimeSpan.FromHours(DurationOfDuty));
        [Display(Name = "Длительность дежурства")]
        [Required(ErrorMessage = "Необходимо ввести длительность")]
        public int DurationOfDuty { get; set; }
        public string DayOfWeekName { get; set; }
        public string RosterName => $"{DayOfWeekName}: {StartTime:hh\\:mm} - {EndTime:hh\\:mm}";
        [Display(Name = "Дни недели")]
        public IEnumerable<SelectListItem> DaysOfWeek { get; set; }
        public static IEnumerable<Roster> GetAllRoster() => DBService.GetData<Roster>(SqlStr.GetRoster);
        public static Roster GetRosterById(int id) => id == -1 ? new Roster() : GetAllRoster().FirstOrDefault(x => x.RosterId == id);
        public static void EditRoster(Roster ros)
        {
            if (ros.RosterId == 0)
                DBService.AddRoster(ros);
            else
                DBService.EditRoster(ros);
        }

        public static void AddRoster(Roster ros) => DBService.AddRoster(ros);
        public static void DelRoster(int id) => DBService.DelRoster(id);
    }
}
