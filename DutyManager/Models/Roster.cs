using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.Models
{
    public class Roster
    {
        public int RosterId { get; set; }
        public int DayWeekId { get; set; }
        public TimeSpan StartTime { get; set; }
        public int DurationOfDuty { get; set; }   
    }
}
