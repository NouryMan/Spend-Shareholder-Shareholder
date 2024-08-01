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
    [Table("PROJECTTBL")]
    [JsonObject(IsReference = true)]
    public class PROJECTTBL_Model
    {

        public PROJECTTBL_Model()
        {
            this.PERSON_ACC_PROJTBL_Collection = new HashSet<PERSON_ACC_PROJTBL_Model>();
            this.USER_UTLIST_PROJECTTBL_Collection = new HashSet<USER_UTLIST_PROJECTTBL_Model>();
          //  this.OPERATIONAL_PALANCE_Collection = new HashSet<OPERATIONAL_PALANCE_Model>();

        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int PROJECT_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string PROJECT_NAME { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string REMARK { get; set; }
        public Nullable<DateTime> START_DATE { get; set; }
        public Nullable<DateTime> END_DATE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PLAN_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(120, ErrorMessage = "طول الحقل كبير جداً")]
        public string OWNER { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PLAN_ENG { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> OVERSIGHT_ENG { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> COUNSULT_ENG { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CLOD_TEST_ENG { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> STRUCTURAL_ENG { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool CLOSED { get; set; }

      
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> OWNER_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SPEND_AMNT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> M_SPEND { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> INCOME { get; set; }

         [System.ComponentModel.DefaultValue(false)]
        public bool M_CLOSED { get; set; } 
         [System.ComponentModel.DefaultValue(false)]
        public bool SALES_CLOSED { get; set; }



        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> M_RESERVED { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> LAND_COST { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CURR_STAGE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم "),System.ComponentModel.DefaultValue(1)]
        public Nullable<int> PROJ_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ORDER_NO { get; set; }
        public string ADD_FROM { get; set; }

        public Nullable<long> CALL_PROJ_ID { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        [ForeignKey("PROJ_TYPE")]
        public virtual STAGESTBL_Model STAGESTBL_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        [ForeignKey("OVERSIGHT_ENG")]
        public virtual ENGINEERTBL_Model OVERSIGHT_ENG_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        [ForeignKey("PLAN_ENG")]
        public virtual ENGINEERTBL_Model PLAN_ENG_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        [ForeignKey("STRUCTURAL_ENG")]
        public virtual ENGINEERTBL_Model STRUCTURAL_ENG_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        [ForeignKey("CLOD_TEST_ENG")]
        public virtual ENGINEERTBL_Model CLOD_TEST_ENG_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("OWNER_NO")]
        public virtual OWNERTBL_Model OWNERTBL_Model { get; set; }
       
        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DELETE { get; set; }
       
        public string UPDATE_FROM { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<PERSON_ACC_PROJTBL_Model> PERSON_ACC_PROJTBL_Collection { set; get; }
        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<USER_UTLIST_PROJECTTBL_Model> USER_UTLIST_PROJECTTBL_Collection { set; get; }
        //[JsonIgnore]
        //[IgnoreDataMember]

        //public virtual ICollection<OPERATIONAL_PALANCE_Model> OPERATIONAL_PALANCE_Collection { set; get; }
        //[JsonIgnore]
        //[IgnoreDataMember]

        //public virtual ICollection<ACCH_PROJ_BOX_PERCENTTBL_Model> ACCH_PROJ_BOX_PERCENT_Collection { set; get; }

    }
}
