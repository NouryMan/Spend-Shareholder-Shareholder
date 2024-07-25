using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("PERCENT_ESTTBL")]
    public class PERCENT_EST_Model
    {
        [Key, Column(Order = 3)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_HOLDER_NO { get; set; }

        [Key, Column(Order = 2)]
        [Required]
      
        public double SPEND_PERCENT { get; set; }


        [Key, Column(Order = 0)]
        [Required]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int TARGET_PROJ { get; set; }

       
        [Key, Column(Order = 1)]
        [Required]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int SOURCE_BOX { get; set; }

        [Key, Column(Order = 4)]
        [Required]
        [System.ComponentModel.DefaultValue(true)]
        public bool STATUS { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]

        public string NOTE { get; set; }



        [System.ComponentModel.DefaultValue(false)]
        public bool SPEND_DETERMINED { get; set; }

        public Nullable<double> DET_COST { get; set; }
      
        public double PARENT_PERCENT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UP_NO { get; set; }
       
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> TARGET_BOX { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PARENT_PROJ { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> SOURCE_PROJ { get; set; }
      
        public Nullable<long> PARENT_ACCH { get; set; }

      
        public Nullable<double> BALANCE { get; set; }


        [ForeignKey("TARGET_PROJ,PARENT_ACCH")]
        public virtual OPERATIONAL_PALANCE_Model OPERATIONAL_PALANCE_Model_ACC_HOLDER_NO { get; set; }
       
    }
}
