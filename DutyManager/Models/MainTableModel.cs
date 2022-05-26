﻿using DutyManager.DB;
using System;
using System.Collections.Generic;

namespace DutyManager.Models
{
    public class MainTableModel
    {
        public int MappingId { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateFinish { get; set; }
        public string DayOfWeekName { get; set; }
        public string LoginName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string OtherInfo { get; set; }
        public static IEnumerable<MainTableModel> GetAllItems() => DBService.GetData<MainTableModel>(SqlStr.GetMainTable);
    }
}