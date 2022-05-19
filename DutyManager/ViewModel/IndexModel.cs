using DutyManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DutyManager.ViewModel
{
    public class IndexModel
    { 
        public IEnumerable<MainTableModel> Data { get; set; }
    }
}
