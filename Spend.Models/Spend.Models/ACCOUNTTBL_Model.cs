using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACCOUNTTBL")]
    public class ACCOUNTTBL_Model
    {

        public ACCOUNTTBL_Model()
        {
          

        }
        [Key, Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_NO { get; set; }

        [Key, Column(Order = 1)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long UNDER_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PROJECT_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> FOR_HIM { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> FROM_HIM { get; set; }


        public Nullable<DateTime> ACC_DATE { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }


        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> CLOSED { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> SCRIPT_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SUB_PROJ { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UNIT_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PART_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> GROUP_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_PARENT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }

        //public Nullable<DateTime> CR_DATE { get; set; }
        //public Nullable<DateTime> UP_DATE { get; set; }
        //public Nullable<DateTime> DATE_H { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> DOC_TYPE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> OP_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> TRANS_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> CUST_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NO_TEMP { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_PARENT_TEMP { get; set; }
        public bool IS_BOX_HOLDER { get; set; }


        [ForeignKey("ACC_NO")]
        public virtual ALL_ACC_NOTBL_Model ALL_ACC_NOTBL_Model { get; set; }
        [ForeignKey("TRANS_ID")]
        public virtual TRANS_HD_Model TRANS_HD_Model { get; set; }

        [ForeignKey("DOC_TYPE")]
        public virtual DUC_TYPETBL_Model DUC_TYPETBL_Model { get; set; }

        [ForeignKey("PROJECT_NO")]
        public virtual PROJECTTBL_Model PROJECTTBL_Model { get; set; }

    }
}
