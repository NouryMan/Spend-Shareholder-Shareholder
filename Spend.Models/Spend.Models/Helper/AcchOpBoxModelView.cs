using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models.Helper
{
    public class AcchOpBoxModelView
    {
        public int ProjectId { get; set; }
        public int? BuildingId { get; set; }
        public int? UnitId { get; set; }
        public int BoxId { get; set; }
        public int OpTypeId { get; set; }
        public int OpActionTypeId { get; set; }
        public DateTime Date { get; set; }
        public double TotalAmount { get; set; }
        public string NOTE { get; set; }
        public List<AcchOpBoxDetailsModelView> AcchOpBoxDetailsModelView { get; set; }
    }

    public class AcchOpBoxDetailsModelView
    {
        public long AccHolderId { get; set; }
        public double percentage { get; set; }
        public bool IsPercentage { get; set; }
        public double Balance { get; set; }
  
        public double Amount { get; set; }
    }

}
