using DutyManager.DB;
using DutyManager.Extensions;
using DutyManager.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace DutyManager.Services
{
    public static class Calculate
    {
        public static DateTime Start { get; set; } = DateTime.Today.AddDays(2);
        public static DateTime Finish { get; set; } = DateTime.Today.AddDays(32);
        private static IEnumerable<Roster> AllRoster = Roster.GetAllRoster();
        private static IEnumerable<Employee> AllEmployees = Employee.GetAllEmployees();
        private static IEnumerable<Holiday> AllHolidays = Holiday.GetAllHolidays();
        private static IEnumerable<Workday> AllWorkdays = Workday.GetAllWorkdays();
        private static List<MappingModel> DailyDuties = new List<MappingModel>();
        private static LinkedList<MappingModel> MyDailyDuties = new LinkedList<MappingModel>();

        public static void StartCalculate()
        {
            Init();
            CalcHandRoster();
            CalcAutoRoster();
            DBService.InsertData(MyDailyDuties);
        }
        public static void StartCalculate(DateTime start, DateTime finish)
        {

        }

        private static IEnumerable<int> GetHoliDayEmployees(DateTime currDate, int roster)
        {
            var item = AllRoster.Where(x => x.RosterId == roster).FirstOrDefault();
            DateTime finishDate = currDate.AddHours(item.DurationOfDuty);

            return AllHolidays.Where(x => x.DateStart <= finishDate && x.DateFinish >= currDate).Select(x => x.EmployeeId);
        }

        private static void ResetCountDay(DateTime date)
        {
            foreach (var emp in AllEmployees)
            {
                emp.CountDuty = 0;
            }
            foreach (var item in DailyDuties)
            {
                if (item.DateStart.WeekOfYear() == date.WeekOfYear())
                    AllEmployees.FirstOrDefault(x => x.EmployeeId == item.EmployeeId).CountDuty++;
            }

        }

        private static void Init()
        {
            AllRoster = Roster.GetAllRoster();
            AllEmployees = Employee.GetAllEmployees();
            AllHolidays = Holiday.GetAllHolidays();
            AllWorkdays = Workday.GetAllWorkdays();
            MyDailyDuties = new LinkedList<MappingModel>();
            var currDate = Start;
            while (currDate <= Finish)
            {
                var currRoster = AllRoster.Where(x => x.DayOfWeekId == currDate.DayOfWeek());
                foreach (var item in currRoster)
                {
                    MyDailyDuties.Add(new MappingModel(item.RosterId, currDate + item.StartTime));
                }
                currDate = currDate.AddDays(1);
            }
        }

        private static void CalcHandRoster()
        {
            var currDate = Start;
            var workdaysWithAlways = AllWorkdays.Where(x => x.IsAlways);
            var WorkdaysWithDay = AllWorkdays.Where(x => !x.IsAlways);

            foreach (var item in MyDailyDuties)
            {
                var holiDayEmployees = GetHoliDayEmployees(item.DateStart, item.RosterId);
                if (workdaysWithAlways.Any(x => x.RosterId == item.RosterId && !holiDayEmployees.Any(h => h == workdaysWithAlways.Where(w => w.RosterId == item.RosterId).FirstOrDefault().EmployeeId)))
                {
                    item.EmployeeId = workdaysWithAlways.Where(x => x.RosterId == item.RosterId).FirstOrDefault().EmployeeId;
                }

                if (WorkdaysWithDay.Any(x => x.RosterId == item.RosterId && x.StartDateWork == item.DateStart))
                {
                    item.EmployeeId = WorkdaysWithDay.Where(x => x.RosterId == item.RosterId && x.StartDateWork == item.DateStart).FirstOrDefault().EmployeeId;
                }
            }
        }

        private static void CalcAutoRoster()
        {
            var currNode = MyDailyDuties.Head;
            while (currNode.Next != null)
            {
                if (currNode.Data.DateStart.DayOfWeek() == 1)
                    ResetCountDay(currNode.Data.DateStart);
                if (currNode.Data.EmployeeId == -1)
                {
                    currNode.Data.EmployeeId = GetFreeEmployee(currNode);
                    if (currNode.Data.EmployeeId != -1)
                        AllEmployees.FirstOrDefault(x => x.EmployeeId == currNode.Data.EmployeeId).CountDuty++;
                }
                currNode = currNode.Next;
            }
        }

        private static int GetFreeEmployee(Node<MappingModel> node)
        {
            var holiDayEmployees = GetHoliDayEmployees(node.Data.DateStart, node.Data.RosterId);
            IEnumerable<Employee> workEmployees;
            if (holiDayEmployees.Any())
                workEmployees = AllEmployees.Where(x => holiDayEmployees.Any(e => e != x.EmployeeId));
            else
                workEmployees = AllEmployees;

            if (workEmployees.Count() > 0)
            {
                int count = 0;
                IEnumerable<Employee> allFreeEmployees = workEmployees;
                if (node.Previous != null && node.Next != null)
                {
                    count = workEmployees.Count(x => x.EmployeeId != node.Previous.Data.EmployeeId && x.EmployeeId != node.Next.Data.EmployeeId);
                    if (count > 0)
                        allFreeEmployees = workEmployees.Where(x => x.EmployeeId != node.Previous.Data.EmployeeId && x.EmployeeId != node.Next.Data.EmployeeId);
                }
                else if (node.Next != null)
                {
                    count = workEmployees.Count(x => x.EmployeeId != node.Next.Data.EmployeeId);
                    if (count > 0)
                        allFreeEmployees = workEmployees.Where(x => x.EmployeeId != node.Next.Data.EmployeeId);
                }
                else if (node.Previous != null)
                {
                    count = workEmployees.Count(x => x.EmployeeId != node.Previous.Data.EmployeeId);
                    if (count > 0)
                        allFreeEmployees = workEmployees.Where(x => x.EmployeeId != node.Previous.Data.EmployeeId);
                }

                int minCountDuty = allFreeEmployees.Min(x => x.CountDuty);
                var freeEmployees = allFreeEmployees.Where(x => x.CountDuty == minCountDuty).ToList();
                return freeEmployees[new Random().Next(freeEmployees.Count)].EmployeeId;
            }
            else
                return -1;
        }
    }
}
