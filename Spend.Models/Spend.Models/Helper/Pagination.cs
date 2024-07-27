using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models.Helper
{
    public class Pagination<T>
    {
        public int CurrentPage { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        public int PerPage { get; set; }
        public int LastPage { get; set; }
        public List<T> Items { get; set; }
    }
}
