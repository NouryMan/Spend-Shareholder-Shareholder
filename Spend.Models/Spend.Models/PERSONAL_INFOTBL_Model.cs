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
    [Table("PERSONAL_INFOTBL")]
    [JsonObject(IsReference = true)]
    public   class PERSONAL_INFOTBL_Model
    {
        public PERSONAL_INFOTBL_Model()
        {
           // this.PERSON_ACC_PROJTBL_Collection = new HashSet<PERSON_ACC_PROJTBL_Model>();
            this.PERSON_ACCTBL_Collection = new HashSet<PERSON_ACCTBL_Model>();
            this.USERSTBL_Collection = new HashSet<USERSTBL_Model>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ID { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string AR_NAME { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string EN_NAME { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string ADDRESS1 { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string ADDRESS2 { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string ADDRESS3 { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string MOBILE_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string TELEPHONE_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(25, ErrorMessage = "طول الحقل كبير جداً")]
        public string C_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PARENT_PERSON { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> JOB_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string DESCRIPTION { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(30, ErrorMessage = "طول الحقل كبير جداً")]
        public string PASSPORT_NO { get; set; }
        public Nullable<DateTime> BIRTH_DATE { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string EMAIL_ADDRESS { get; set; }



        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }

        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> VAT_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(40, ErrorMessage = "طول الحقل كبير جداً")]
        public string BANK_ACC { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string BANK_NAME { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NO { get; set; }

      
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PARENT_ACC { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> ACC_NAT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_LEVEL { get; set; }

       // [System.ComponentModel.DefaultValue(true)]
        public bool IS_ENABLED { get; set; }
       // [System.ComponentModel.DefaultValue(true)]
        public bool OP_ACC { get; set; }




        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> NATIONALITY_NO { get; set; }

        public Nullable<DateTime> C_ID_ISSUE_DATE { get; set; }
        public Nullable<DateTime> C_ID_EXP_DATE { get; set; }
        public Nullable<DateTime> PASSPORT_ISSUE_DATE { get; set; }
        public Nullable<DateTime> PASSPORT_EXP_DATE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> GENDER { get; set; }

        //[RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        //public Nullable<int> PICTURE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> DEGREE { get; set; }





        // public virtual ICollection<PERSON_ACC_PROJTBL_Model> PERSON_ACC_PROJTBL_Collection { set; get; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<PERSON_ACCTBL_Model> PERSON_ACCTBL_Collection { set; get; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<USERSTBL_Model> USERSTBL_Collection { set; get; }
    }
}
