using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.Models
{
    public class WorkDay
    {
        public int EmployeeId { get; set; }
        public int RosterId { get; set; }
        public DateTime DateWork { get; set; }
    }
}
