using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string FullName { get; set; }
        public string LoginName { get; set; }
        public string Phone { get; set; }
        public string OtherInfo { get; set; }
        public int CountDuty { get; set; } = 0;
    }
}
