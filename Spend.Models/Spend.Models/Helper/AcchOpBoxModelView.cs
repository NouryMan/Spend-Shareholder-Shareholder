﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models.Helper
{
    public class AcchOpBoxModelView
    {
        [Required]
        public int ProjectId { get; set; }
        public int? BuildingId { get; set; }
        public int? UnitId { get; set; }
        [Required]
        public int BoxId { get; set; }
        [Required]
        public int OpTypeId { get; set; }
        [Required]
        public int OpActionTypeId { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [Required]
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
