﻿using DutyManager.DB;
using DutyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DutyManager.Services
{
    public class Calculate
    {
        public DateTime Start { get; set; } = DateTime.Today;
        public DateTime Finish { get; set; } = DateTime.Today.AddDays(30);
        private readonly IEnumerable<Roster> AllRoster = DBService.GetData<Roster>(SqlStr.GetRoster);
        private readonly IEnumerable<Employee> AllEmployees = DBService.GetData<Employee>(SqlStr.GetEmployees);
        private readonly IEnumerable<Holiday> AllHolidays = DBService.GetData<Holiday>(SqlStr.GetHolidays);
        private readonly IEnumerable<WorkDay> AllWorkDays = DBService.GetData<WorkDay>(SqlStr.GetWorkdays);
        private readonly List<MappingModel> DailyDuties = new List<MappingModel>();
        public void StartCalculate()
        {
            MainCalc();
        }

        private void MainCalc()
        {
            DateTime currDate = Start;
            List<MappingModel> mapp = new List<MappingModel>();

            while (currDate <= Finish)
            {
                var dayOfWeek = ((int)currDate.DayOfWeek);
                var curRoster = AllRoster.Where(x => x.DayWeekId == dayOfWeek);

                foreach (var item in curRoster)
                {
                    int emp;
                    if (AllWorkDays.Any(x => x.RosterId == item.RosterId && x.DateWork == currDate))
                    {
                        emp = AllWorkDays.Where(x => x.RosterId == item.RosterId && x.DateWork == currDate).FirstOrDefault().EmployeeId;
                        AddMapping(currDate, item, emp);
                    }
                }

                foreach (var item in curRoster)
                {
                    int emp;

                    if (!AllWorkDays.Any(x => x.RosterId == item.RosterId && x.DateWork == currDate))
                    {       
                        emp = GetFreeEmployeeId(currDate, item);
                        AddMapping(currDate, item, emp);
                    }
                }
                currDate = currDate.AddDays(1);
            }
            DBService.InsertData(DailyDuties, "tool.tDutyManagerMapping");
        }

        private void AddMapping(DateTime currDate, Roster item, int emp)
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
        

        private int GetFreeEmployeeId(DateTime currDate, Roster item)
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