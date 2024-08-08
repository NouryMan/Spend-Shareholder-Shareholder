using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace Spend.Models
{
    [Table("ACCH_PROJECT")]
    public class ACCH_PROJECT_Model
    {
        public ACCH_PROJECT_Model()
        {
            this.Project_Collection = new HashSet<ACCH_PROJECT_Model>();
            this.ACCH_PROJ_BOX_PERCENT_Collection = new HashSet<ACCH_PROJ_BOX_PERCENTTBL_Model>();
            this.OPERATIONAL_PALANCE_Collection = new HashSet<OPERATIONAL_PALANCE_Model>();
        }
        //public int ROW_NO { get; set; }
        public Nullable<Guid> GUID_ID { get; set; }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long PROJECT_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(50, ErrorMessage = "طول الحقل كبير جداً")]
        public string PROJECT_CODE { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        [Required]
        public string PROJECT_AR_NAME { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string PROJECT_EN_NAME { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int PROJECT_TYPE_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PROJECT_PARENT_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> STATUS_ID { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> FIRST_COST { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ROOM_COUNT { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string IMAGE_PATH { get; set; }



        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DELETED { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IS_PARKING { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(20, ErrorMessage = "طول الحقل كبير جداً")]
        public string PARKING_CODE { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DRIVER_ROOM { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IS_WORKING_ROOM { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> SUITE_SPACE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> BATHROOM_COUNT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> HALL_COUNT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> MIN_COST { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(250, ErrorMessage = "طول الحقل كبير جداً")]
        public string PROJECT_LOCATION { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string ADAPTATION { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string FIOORS { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string FRONTAGE { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string FINISHING { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> KITCHEN_COUNT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ESTABLISHED_YEAR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> MEN_ROOM_COUNT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> WOMEN_ROOM_COUNT { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool IS_UPPER_TANK { get; set; }

        [System.ComponentModel.DefaultValue(false)]

        public bool IS_LOWER_TANK { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool IS_EVELATOR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> BEDROOM_COUNT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> DEED_NO { get; set; }






        [Column(TypeName = "VARCHAR2"), StringLength(500, ErrorMessage = "طول الحقل كبير جداً")]
        public string DIRECTIONS { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string PLANNED_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string PIECE_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string ISSUER { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Date only")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public System.Nullable<DateTime> DEED_DATE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SUITE_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ENTRANCES_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> FLOOR_NO { get; set; }

        public string UPDATE_FROM { get; set; }



        [System.ComponentModel.DefaultValue(false)]
        public bool IS_CENTRAL_GAS { get; set; }



        public Nullable<int> SPEND_ID { get; set; }
        public Nullable<Int64> ELECTRICITY_ACC { get; set; }



        [System.ComponentModel.DefaultValue(false)]
        public bool IS_FOR_RENTAL { get; set; }


        public Nullable<int> OWNER_ID { get; set; }
        public string GUARD_NAME { get; set; }
        public Nullable<int> GUARD_ID { get; set; }
        public Nullable<long> GUARD_PHONE { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool HAS_VAT { get; set; }
        public Nullable<double> MANAG_COMMISSION { get; set; }
        public Nullable<int> UNIT_TYPE { get; set; }
        public Nullable<long> WATER_ACC_NO { get; set; }
        public Nullable<int> MAIN_RENTAL_CONTRACT { get; set; }
        public Nullable<double> WIDTH { get; set; }
        public Nullable<double> LENGHT { get; set; }



        [ForeignKey("PROJECT_PARENT_ID")]
        public virtual ACCH_PROJECT_Model ProjectModels { get; set; }

       
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> AGENT_ID { get; set; }

        
        public int BRANCH_ID { get; set; }

        public virtual ICollection<ACCH_PROJECT_Model> Project_Collection { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<OPERATIONAL_PALANCE_Model> OPERATIONAL_PALANCE_Collection { set; get; }

        [JsonIgnore]
        [IgnoreDataMember]

        public virtual ICollection<ACCH_PROJ_BOX_PERCENTTBL_Model> ACCH_PROJ_BOX_PERCENT_Collection { set; get; }


    }
}
