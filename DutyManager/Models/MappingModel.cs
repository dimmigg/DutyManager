using System;

namespace DutyManager.Models
{
    public class MappingModel
    {
        public int EmployeeId { get; set; } = -1;
        public int RosterId { get; set; }
        public DateTime DateStart { get; set; }
        public MappingModel() { }
        public MappingModel(int rost, DateTime date)
        {
            RosterId = rost;
            DateStart = date;
        }
    }
}
