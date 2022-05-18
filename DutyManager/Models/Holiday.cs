using System;

namespace DutyManager.Models
{
    public class Holiday
    {
        public int EmployeeId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
    }
}