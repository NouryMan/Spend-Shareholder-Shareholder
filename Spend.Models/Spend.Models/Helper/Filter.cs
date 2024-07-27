using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models.Helper
{
    public class Filter
    {
        public int? Id { get; set; }
        public int? Type { get; set; }
        public int? Status { get; set; }
        public long? FromDate { get; set; }
        public long? Todate { get; set; }
        public string  KeywordSearch { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 15;

    }
}
