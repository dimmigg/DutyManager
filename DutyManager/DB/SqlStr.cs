using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.DB
{ 
    public static class SqlStr
    {
        public static string GetEmployees => "tool.uspDutyManagerGetEmployees";
        public static string GetRoster => "tool.uspDutyManagerGetRoster";        
        public static string GetHolidays => "tool.uspDutyManagerGetHolidays";
        public static string GetWorkdays => "tool.uspDutyManagerGetWorkdays";
        public static string GetMainTable => "tool.uspDutyManagerGetMainTable";
    }
}
