using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("OPERATIONAL_PALANCETBL")]
    public class OPERATIONAL_PALANCE_Model
    {
        public OPERATIONAL_PALANCE_Model()
        {
            this.PERCENT_EST_Collection = new HashSet<PERCENT_EST_Model>();
        }

        [Key, Column(Order = 1)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_HOLDER_NO { get; set; }

        public double OP_PALANCE { get; set; }
        public double PERCENT { get; set; }

        [Key, Column(Order = 0)]
        [Required]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int TARGET_PROJ { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool STATUS { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        public Nullable<DateTime> DATE_M { get; set; }
        public Nullable<DateTime> DATE_H { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DETERMINED { get; set; }
        public Nullable<double> DET_COST { get; set; }

        public virtual ICollection<PERCENT_EST_Model> PERCENT_EST_Collection { set; get; }

        [ForeignKey("ACC_HOLDER_NO")]
        public virtual ACC_HOLDERTBL_Model ACC_HOLDERTBL_Model { get; set; }

        [ForeignKey("TARGET_PROJ")]
        public virtual ACCH_PROJECT_Model ACCH_PROJECT_Model { get; set; }
    }

}
