using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACC_TREETBL1")]
    public class ACC_TREETBL1_Model
    {
        public ACC_TREETBL1_Model()
        {
            this.ACC_TREETBL1_Collection=new HashSet<ACC_TREETBL1_Model>();
            this.ACC_TREETBL1_ACCOUNTTB_Collection=new HashSet<ACC_TREETBL1_ACCOUNTTB_Model>();
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_NO { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]

        public string ACC_NAME { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_PARENT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> COMP_NO { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PRANCH_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NAT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_LEVEL { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool IS_ENABLE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SYS_CODE { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool OP_ACC { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NO_TEMP { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_PARENT_TEMP { get; set; }

        [ForeignKey("ACC_NAT")]
        public virtual ACC_NATTBL_Model ACC_NATTBL_Model { get; set; }


        [ForeignKey("ACC_TYPE")]
        public virtual ACC_TYPETBL_Model ACC_TYPETBL_Model { get; set; }
        


        [ForeignKey("ACC_PARENT")]
        public virtual ACC_TREETBL1_Model ACC_TREETBL1_Mode { get; set; }


        public virtual ICollection<ACC_TREETBL1_Model> ACC_TREETBL1_Collection { get; set; }

        public virtual ICollection<ACC_TREETBL1_ACCOUNTTB_Model> ACC_TREETBL1_ACCOUNTTB_Collection { get; set; }

        [NotMapped]
        public string Acc_NOString { get; set; }

        [NotMapped]
        public decimal FOR_HIM { get; set; }

        [NotMapped]
        public decimal FROM_HIM { get; set; }
    }
}
