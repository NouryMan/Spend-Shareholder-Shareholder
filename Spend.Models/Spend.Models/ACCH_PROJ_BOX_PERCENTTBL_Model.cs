using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACCH_PROJ_BOX_PERCENTTBL")]
    public class ACCH_PROJ_BOX_PERCENTTBL_Model
    {
        [Key, Column(Order = 1)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_HOLDER_NO { get; set; }

        [Key, Column(Order = 0)]
        [Required]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int PROJ_NO { get; set; }


        [Key, Column(Order = 2)]
        [Required]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int BOX_NO { get; set; }



        //[Key, Column(Order = 2)]
        //[Required]
        public double SPEND_COST { get; set; }

      
        public double SPEND_PERCENT { get; set; }


        [System.ComponentModel.DefaultValue(true)]
        public Nullable<bool> ACTIVE { get; set; }

        
         public double PARENT_PERCENT { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int USER_CR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int USER_UP { get; set; }


        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }

       
        public double BOX_INCOME { get; set; }
       
        public double INCOME_PERCENT { get; set; }
       
        public double INCOME_BOX { get; set; }
      
        public int PARENT_PROJ { get; set; }
      
        public long SOURCE_PROJ { get; set; }
      
        public long PARENT_BOX { get; set; }
        public long PARENT_ACCH { get; set; }

        [ForeignKey("ACC_HOLDER_NO")]
        public virtual ACC_HOLDERTBL_Model ACC_HOLDERTBL_Model { get; set; }
      
        [ForeignKey("PROJ_NO")]
        public virtual ACCH_PROJECT_Model PROJECTTBL_Model { get; set; }


        [ForeignKey("PROJ_NO,PARENT_ACCH")]
        public virtual ACCH_PROJ_PERCENTTBL_Model ACCH_PROJ_PERCENTTBL_Model { get; set; }

        [NotMapped]
        public double MAINTE_PERCENT { get; set; }
        [NotMapped]
        public double MARKETING__PERCENT { get; set; }
        [NotMapped]
        public double OTHER_COST { get; set; }
        [NotMapped]
        public double WORK_PERCENT { get; set; }
    }
}
