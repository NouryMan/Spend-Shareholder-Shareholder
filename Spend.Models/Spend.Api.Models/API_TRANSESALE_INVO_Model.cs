using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Api.Models
{
    public class API_TRANSESALE_INVO_Model
    {
        public int InovNo { get; set; }
        public double PurCost { get; set; }
        public double Mantinc { get; set; }
        public double Markting { get; set; }
        public double OtherCost { get; set; }
        public int ProjNo { get; set; }
        public string Note { get; set; }

    }
}
