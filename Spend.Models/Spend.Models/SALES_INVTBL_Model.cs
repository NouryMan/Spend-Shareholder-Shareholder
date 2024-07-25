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
    [Table("SALES_INVTBL")]
    [JsonObject(IsReference = true)]
    public class SALES_INVTBL_Model
    {
        public SALES_INVTBL_Model()
        {
            this.SALES_INVDTTBL_Collection = new HashSet<SALES_INVDTTBL_Model>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long INV_NO { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PROJ_NO { get; set; }



        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PART_PROJ { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UNIT_NO { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> CUST_ACC { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> PRICE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> DISCOUNT { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> INV_PUR { get; set; }

        public Nullable<DateTime> DATE_M { get; set; }
        public Nullable<DateTime> DATE_H { get; set; }




        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string SALE_CONTRACT { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> MARKETING_CON { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SCRIP_NO { get; set; }


        public Nullable<bool> SCRIP_SAVED { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }
        public Nullable<bool> CHECKED { get; set; }





        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SALES_ACC { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> FISCAL_YEAR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PRANCH { get; set; }


        [System.ComponentModel.DefaultValue(false)]
        public bool TRANSED { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> UNDER_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> MARKETING_COST { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> WORKING_COST { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> OTHER_COST { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> VAT_AMOUNT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> TAXED_COST { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> INV_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> INV_DOC_TYPE { get; set; }


        [System.ComponentModel.DefaultValue(false)]
        public bool BOX_TRANSED { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CUST_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> VAT_ID { get; set; }



        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PERSON_ACC_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> SALES_CUST_ID { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(1000, ErrorMessage = "طول الحقل كبير جداً")]
        public string QRCODE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CONTRACT_ID { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> OPTYPE { get; set; }



        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("CUST_ID")]
        public virtual CUSTOMERTBL_Model CUSTOMERTBL_Model { get; set; }

        [IgnoreDataMember]
        [ForeignKey("SALES_CUST_ID")]
        public virtual SALES_CUSTTBL_Model SALES_CUSTTBL_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("PROJ_NO")]
        public virtual PROJECTTBL_Model PROJECTTBL_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("PART_PROJ")]
        public virtual PARTTBL_Model PARTTBL_Model { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("UNIT_NO")]
        public virtual UNITTBL_Model UNITTBL_Model { get; set; }


        [ForeignKey("INV_DOC_TYPE")]
        public virtual DUC_TYPETBL_Model DUC_TYPETBL_Model { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("OPTYPE")]
        public virtual SCRIP_OPTYPETBL_Model SCRIP_OPTYPETBL_Model { get; set; }


        public virtual ICollection<SALES_INVDTTBL_Model> SALES_INVDTTBL_Collection { set; get; }

        [NotMapped]
        public string customer_name { get; set; }

        [NotMapped]
        public string project_name { get; set; }
        [NotMapped]
        public string customer_Phone { get; set; }


    }
}