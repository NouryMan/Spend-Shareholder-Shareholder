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
    [Table("ITEMTBL")]
    [JsonObject(IsReference = true)]
    public class ITEMTBL_Model
    {

        public ITEMTBL_Model()
        {
            this.ITEMTBL_Model_Collection = new HashSet<ITEMTBL_Model>();
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ITEM_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string ITEM_NAME { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string MET_UNIT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PARENT_ITEM { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string BAR_CODE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> PUR_COST { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> AVG_COST { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> PACKING { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }

        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool IS_ENABLED { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> VAT_PERCENT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> MAIN_MET_UNIT { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string MAIN_SUB { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> LVL { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ITEM_NO_TEMP { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ITEM_PARENT_TEMP { get; set; }


        [JsonIgnore]
        [IgnoreDataMember]
        [ForeignKey("PARENT_ITEM")]
        public virtual ITEMTBL_Model ITEMTBL_Models { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<ITEMTBL_Model> ITEMTBL_Model_Collection { set; get; }


        [NotMapped]
        public decimal Qin { get; set; }
        [NotMapped]
        public decimal Qout { get; set; }
        [NotMapped]
        public decimal CostIn { get; set; }
        [NotMapped]
        public decimal CostOut { get; set; }
        [NotMapped]
        public decimal VatIn { get; set; }
        [NotMapped]
        public decimal VatOut { get; set; }

    }
}
