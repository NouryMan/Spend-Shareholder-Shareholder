using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACCH_OPBOXTBL")]
    public  class ACCH_OPBOXTBL_Model
    {

    
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
       
        public int ID { get; set; }
        public long ACC_HOLDER_NO { get; set; }

        public double INCOME { get; set; }


        
        public int SOURCE_BOX { get; set; }

        
        public double SPEND_COST { get; set; }


       
        public int TARGET_PROJ { get; set; }
        public int? BUILDING_ID { get; set; }
        public int? UNIT_ID { get; set; }




        public Nullable<DateTime> DATE_M { get; set; }
        public string DATE_H { get; set; }



        
        public double OP_NO { get; set; }

        
          public decimal UNDER_NO { get; set; }

        public double SCRIP_NO { get; set; }
       


        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> OUT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]

        public Nullable<int> OP_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PARENT_BOX { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PARENT_ACCH { get; set; }



        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string OLD_VAL { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACTION_TYPE { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DELETE { get; set; }
        public Nullable<int> SCRIPT_TYPE { get; set; }
        public Nullable<int> SPEND_SCRIPT_NO { get; set; }
        [ForeignKey("ACC_HOLDER_NO")]
        public virtual ACC_HOLDERTBL_Model ACC_HOLDERTBL_Model { get; set; }
        [ForeignKey("TARGET_PROJ")]
        public virtual ACCH_PROJECT_Model PROJECTTBL_Model { get; set; }

        [ForeignKey("BUILDING_ID")]
        public virtual ACCH_PROJECT_Model BUILDING_Model { get; set; }
        [ForeignKey("UNIT_ID")]
        public virtual ACCH_PROJECT_Model UNIT_Model { get; set; }

        [ForeignKey("SOURCE_BOX")]
        public virtual BOXTBL_Model BOXTBL_Model { get; set; }

        [ForeignKey("OP_TYPE")]
        public virtual BOX_OPTBL_Model BOX_OPTBL_Model { get; set; }
    }
}
