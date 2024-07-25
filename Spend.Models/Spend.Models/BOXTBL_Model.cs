using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("BOXTBL")]
    public   class BOXTBL_Model
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int BOX_NO { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string BOX_NAME { get; set; }

        public Nullable<DateTime> CR_DATEH { get; set; }
        public Nullable<DateTime> CR_DATEM { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> OP_PALANCE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> REMAINING_AMNT { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool ACTIVE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> BOX_TYPE { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool SPEND_BOX { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> OP_TYPE { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DELETE { get; set; }

        [ForeignKey("OP_TYPE")]
        public virtual BOX_OPTBL_Model BOX_OPTBL_Model { get; set; }


        [ForeignKey("BOX_TYPE")]
        public virtual BOX_TRANS_OPTYPES_Model BOX_TRANS_OPTYPES_Model { get; set; }


    }
}
