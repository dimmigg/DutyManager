using DutyManager.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace DutyManager.Models
{
    public class MainTableModel
    {
        public int MappingId { get; set; }
        [Description ("День недели")]
        public string DayOfWeekName { get; set; }
        [Description ("Начало дежурства")]
        public DateTime DateStart { get; set; }
        [Description ("Окончание дежурства")]
        public DateTime DateFinish { get; set; }
        [Description ("Дежурный")]
        public string FullName { get; set; }
        [Description ("Номер телефона")]
        public string Phone { get; set; }
        [Description ("Логин")]
        public string LoginName { get; set; }
        
        public string OtherInfo { get; set; }
        public static IEnumerable<MainTableModel> GetAllItems() => DBService.GetData<MainTableModel>(SqlStr.GetMainTable);
    }
}