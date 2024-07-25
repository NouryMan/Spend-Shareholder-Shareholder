using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("TRANS_HD")]
    public class TRANS_HD_Model
    {
        public TRANS_HD_Model()
        {
            this.ACCOUNTTBL_Collection = new HashSet<ACCOUNTTBL_Model>();
        }

        [Key, Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ID { get; set; }
        public Nullable<DateTime> JOURNAL_MDATE { get; set; }
        public string JOURNAL_HDATE { get; set; }
       
        [Column(TypeName = "VARCHAR2"), StringLength(500, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> COST_CENTER_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> FIN_YEAR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> BRANCH_ID { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string POSTED { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SYSTEM_CODE { get; set; }

        public string USER_CR { get; set; }

        public string USER_UP { get; set; }

        public Nullable<DateTime> DATE_CR { get; set; }
        public Nullable<DateTime> DATE_UP { get; set; }

        public Nullable<int> CURR_ID { get; set; }
        public Nullable<DateTime> CREATE_DATE { get; set; }



        [System.ComponentModel.DefaultValue(true)]
        public Nullable<bool> IS_ENABLED { get; set; }



        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CONVERSION_FACTOR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long JOURNAL_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string JOURNAL_CODE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> TRANS_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PROJECT_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> DOC_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> DOC_NO { get; set; }

        public virtual ICollection<ACCOUNTTBL_Model> ACCOUNTTBL_Collection { set; get; }

        [ForeignKey("DOC_TYPE")]
        public virtual DUC_TYPETBL_Model DUC_TYPETBL_Model { get; set; }


        [ForeignKey("PROJECT_NO")]
        public virtual PROJECTTBL_Model PROJECTTBL_Model { get; set; }

        [NotMapped]
        public bool ALL_ON_ON { get; set; }

        [NotMapped]
        public Nullable<long> TO_ACC { get; set; }
    }
}
