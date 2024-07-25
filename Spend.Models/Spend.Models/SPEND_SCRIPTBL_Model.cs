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
    [Table("SPEND_SCRIPTBL")]

    public class SPEND_SCRIPTBL_Model
    {

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ID { get; set; }


      
      
     
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long SCRIP_NO { get; set; }


      
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int SCRIPT_TYPE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int PROJECT_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]

        public string SCRIP_NAME { get; set; }

        public Nullable<DateTime> SCRIP_DATE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> COST { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string REMARK { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string WRITTEN_COST { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> DEBTOR_ACC { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> GROUP_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SUB_PROJ { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UNIT_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PART_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> CREDITOR_ACC { get; set; }
        public Nullable<DateTime> DATE_M { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACTION_TYPE { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string FISCAL_YEAR { get; set; }


        public Nullable<int> OP_TYPE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> UNDER_NO { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool INV_SAVED { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool SCRIP_TRANS { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool M_SCRIP { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> SHAKE_NO { get; set; }


        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> CLOSED { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }

        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool RECEIVED { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> DEBTOR_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> CREDITOR_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CONTRACT_ID { get; set; }

        //[JsonIgnore]
        //[IgnoreDataMember]
        [ForeignKey("OP_TYPE")]
        public virtual SCRIP_OPTYPETBL_Model SCRIP_OPTYPETBL_Model { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("ACTION_TYPE")]
        public virtual SCRIPT_ACTIONSTBL_Model SCRIPT_ACTIONSTBL_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("PROJECT_NO")]
        public virtual PROJECTTBL_Model PROJECTTBL_Model { get; set; }


       
        [ForeignKey("SCRIPT_TYPE")]
        public virtual SCRIP_TYPETBL_Model SCRIP_TYPETBL_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("DEBTOR_ACC")]
        public virtual ALL_ACC_NOTBL_Model DEBTOR_ACC_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("CREDITOR_ACC")]
        public virtual ALL_ACC_NOTBL_Model CREDITOR_Model { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("CONTRACT_ID")]
      
        public virtual ICollection<SCRIP_DTTBL_Model> SCRIP_DTTBL_Collection { set; get; }


        [NotMapped]

        public Nullable<long> ACCH_NO { get; set; }
        [NotMapped]
        public string customer_name { get; set; }

        [NotMapped]
        public string project_name { get; set; }

        [NotMapped]
        public string contract_cost { get; set; }
        [NotMapped]
        public string Cost_String { get; set; }
       

    }
}
