using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Api.Models
{
    public class INV_DT
    {
        public int ID { get; set; }
        public int INV_NO { get; set; }
        public Nullable<int> PROJ_NO { get; set; }
        public Nullable<int> PART_PROJ { get; set; }
        public Nullable<int> UNIT_NO { get; set; }
        public Nullable<int> QNTY { get; set; }
        public Nullable<decimal> PRICE { get; set; }
        public string DISCRIPTION { get; set; } 
        public Nullable<decimal> DISCOUNT { get; set; }
        public Nullable<int> UNIT_CODE { get; set; }
        public Nullable<DateTime> DATE_M { get; set; }
        public Nullable<DateTime> DATE_H { get; set; }
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }
        public Nullable<decimal> VAT_AMOUNT { get; set; }
        public Nullable<decimal> VAT_ID { get; set; }
        public Nullable<int> INV_TYPE { get; set; }




    }
}