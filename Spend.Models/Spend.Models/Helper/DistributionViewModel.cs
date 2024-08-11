using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models.Helper
{
    public class DistributionViewModel
    {
        [Required]
        public int ProjectId { get; set; }
        public int? BuildingId { get; set; }
        public int? UnitId { get; set; }
        [Required]
        public int BoxId { get; set; }
       
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double TotalAmount { get; set; }
        public string Note { get; set; }
        public List<DistributionDetailsViewModel> DistributionDetailsViewModel { get; set; }
    }

    public class DistributionDetailsViewModel
    {
        [Required]
        public long AccHolderId { get; set; }
        [Required]
        public double percentage { get; set; }
        [Required]
        public double Amount { get; set; }
    }

}
