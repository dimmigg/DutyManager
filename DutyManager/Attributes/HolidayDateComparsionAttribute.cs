using DutyManager.Models;
using System.ComponentModel.DataAnnotations;

namespace DutyManager.Attributes
{
    public class HolidayDateComparsionAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value is Holiday day)
            {
                if(day.DateStart >= day.DateFinish)
                {
                    ErrorMessage = "Дата начала отпуска должна быть меньше даты окончания.";
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
