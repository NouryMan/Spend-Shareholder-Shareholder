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
    [Table("CREDENCE_DTTBL")]
    [JsonObject(IsReference = true)]
    public class CREDENCE_DTTBL_Model
    {
        public CREDENCE_DTTBL_Model()
        {
            this.ARCHIVETBL_Collection = new HashSet<ARCHIVETBL_Model>();

        }
       

        [Key, Column(Order = 0)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int DT_ID { get; set; }


        [Key, Column(Order = 1)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int HD_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> CREDITOR_ACC { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> DEBTOR_ACC { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PROJECT_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UTL_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string CONTRACT_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CRED_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> AMMOUNT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> EMP_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string EXEC_DATE_H { get; set; }
        public Nullable<DateTime> EXEC_DATE_M { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string ACT_EXEC_DATE_H { get; set; }
        public Nullable<DateTime> ACT_EXEC_DATE_M { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> DONE { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string CREDENCE_TEXt { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> SUSPENDED { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> EMP_SUSPEND { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string REASON { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PERSON_ACC_PROJTBL_ID { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> IS_DELETED { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> APPROVED { get; set; }

        [ForeignKey("HD_ID")]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual CREDENCETBL_Model CREDENCETBL_Model { get; set; }

        [ForeignKey("PERSON_ACC_PROJTBL_ID")]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual PERSON_ACC_PROJTBL_Model PERSON_ACC_PROJTBL_Model { get; set; }

        [ForeignKey("EMP_NO")]
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual USERSTBL_Model USERSTBL_Model { get; set; }

        //[ForeignKey("UTL_NO")]
        //public virtual PERSON_ACC_PROJTBL_Model PERSON_ACC_PROJTBL_Model { get; set; }

         [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ARCHIVETBL_Model> ARCHIVETBL_Collection { set; get; }
    }
}
