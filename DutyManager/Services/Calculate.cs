using DutyManager.DB;
using DutyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutyManager.Services
{
    public static class Calculate
    {
        public static DateTime Start { get; set; } = DateTime.Today;
        public static DateTime Finish { get; set; } = DateTime.Today.AddDays(30);
        private static readonly IEnumerable<Roster> AllRoster = DBService.GetData<Roster>(SqlStr.GetRoster);
        private static readonly IEnumerable<Employee> AllEmployees = DBService.GetData<Employee>(SqlStr.GetEmployees);
        private static readonly IEnumerable<Holiday> AllHolidays = DBService.GetData<Holiday>(SqlStr.GetHolidays);
        private static readonly IEnumerable<Workday> AllWorkdays = Workday.GetAllWorkdays();
        private static readonly List<MappingModel> DailyDuties = new List<MappingModel>();
        public static void StartCalculate()
        {
            MainCalc();
        }
        public static void StartCalculate(DateTime start, DateTime finish)
        {
            Start = start;
            Finish = finish;
            MainCalc();
        }

        private static void MainCalc()
        {
            DateTime currDate = Start;
            List<MappingModel> mapp = new List<MappingModel>();

            while (currDate <= Finish)
            {
                var dayOfWeek = ((int)currDate.DayOfWeek);
                var curRoster = AllRoster.Where(x => x.DayOfWeekId == dayOfWeek);

                foreach (var item in curRoster)
                {
                    int emp;
                    if (AllWorkdays.Any(x => x.RosterId == item.RosterId && x.DateWork == currDate))
                    {
                        emp = AllWorkdays.Where(x => x.RosterId == item.RosterId && x.DateWork == currDate).FirstOrDefault().EmployeeId;
                        AddMapping(currDate, item, emp);
                    }
                }

                foreach (var item in curRoster)
                {
                    int emp;

                    if (!AllWorkdays.Any(x => x.RosterId == item.RosterId && x.DateWork == currDate))
                    {       
                        emp = GetFreeEmployeeId(currDate, item);
                        AddMapping(currDate, item, emp);
                    }
                }
                currDate = currDate.AddDays(1);
            }
            DBService.InsertData(DailyDuties, "tool.tDutyManagerMapping");
        }

        private static void AddMapping(DateTime currDate, Roster item, int emp)
        {
            DateTime start = currDate + item.StartTime;
            DateTime finish = start.AddHours(item.DurationOfDuty);
            DailyDuties.Add(new MappingModel
            {
                DateStart = start,
                RosterId = item.RosterId,
                EmployeeId = emp
            });
            AllEmployees.FirstOrDefault(x => x.EmployeeId == emp).CountDuty++;
        }
        

        private static int GetFreeEmployeeId(DateTime currDate, Roster item)
        {
            DateTime startDate = currDate + item.StartTime;
            DateTime finishDate = startDate.AddHours(item.DurationOfDuty);

            var holiDayEmployees = AllHolidays.Where(x => x.DateStart < finishDate && x.DateFinish > startDate).Select(x => x.EmployeeId);
            IEnumerable<Employee> workEmployees;
            if (holiDayEmployees.Any())
                workEmployees = AllEmployees.Where(x => holiDayEmployees.Any(e => e != x.EmployeeId));
            else
                workEmployees = AllEmployees;

            int minCountDuty = workEmployees.Min(x => x.CountDuty);
            var freeEmployees = workEmployees.Where(x => x.CountDuty == minCountDuty).ToList();

            return freeEmployees[new Random().Next(freeEmployees.ToList().Count)].EmployeeId;
        }
    }
}
