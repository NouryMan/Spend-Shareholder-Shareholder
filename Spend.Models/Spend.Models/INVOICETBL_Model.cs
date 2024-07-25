using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("INVOICETBL")]
    
    public class INVOICETBL_Model
    {
        public INVOICETBL_Model()
        {
            this.INV_DTTBL_Collection = new HashSet<INV_DTTBL_Model>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long INV_SERIAL { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> INV_NO { get; set; }

        public Nullable<DateTime> DATE_M { get; set; }
        public Nullable<DateTime> DATE_H { get; set; }
        public Nullable<DateTime> ENTRY_DATE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PROJ_NO { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> UNDER_NO { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> INV_COST { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> DISCOUNT { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> FISCAL_YEAR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }


        [System.ComponentModel.DefaultValue(true)]
        public Nullable<bool> CLOSED { get; set; }
        [System.ComponentModel.DefaultValue(true)]
        public Nullable<bool> CHECKED { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> INV_TYPE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> PUR_COST { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> STAGE_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_PARENT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> SCRIP_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SCRIP_SAVED { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> DEBTOR_ACC { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> INV_WONER { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم "), System.ComponentModel.DefaultValue(0)]
        public Nullable<int> INV_TRANSED { get; set; }


        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> M_INV { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> VAT_AMOUNT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> CUST_VATNO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> VAT_ACC { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> INV_DUC_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> EMP_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم "), System.ComponentModel.DefaultValue(0)]
        public Nullable<int> VAT_UP { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> DEBTOR_ID { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CREDITOR_ID { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string SUPP_INV_NO { get; set; }


        [ForeignKey("PROJ_NO")]
        public virtual PROJECTTBL_Model PROJECTTBL_Model { get; set; }
        [ForeignKey("ACC_NO")]
        public virtual ALL_ACC_NOTBL_Model ALL_ACC_CREDITOR_Model { get; set; }
        [ForeignKey("INV_DUC_TYPE")]
        public virtual DUC_TYPETBL_Model DUC_TYPETBL_Model { get; set; }


        public virtual ICollection<INV_DTTBL_Model> INV_DTTBL_Collection { set; get; }

    }
}
