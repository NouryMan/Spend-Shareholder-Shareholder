using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("HOLDER_TRANS_HD")]
    public class HOLDER_TRANS_HD_Model
    {
        public HOLDER_TRANS_HD_Model()
        {
            this.TRANS_DT_Collection = new HashSet<HOLDER_TRANS_DT_Model>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ID { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(500, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> COST_CENTER_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> FISCAL_YEAR_ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> BRANCH_ID { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PROJ_ID { get; set; }

        public Nullable<int> USER_CR { get; set; }

        public Nullable<int> USER_UP { get; set; }

        public Nullable<DateTime> DATE_CR { get; set; }
        public Nullable<DateTime> DATE_UP { get; set; }

        public Nullable<DateTime> DOC_DATE { get; set; }



        [System.ComponentModel.DefaultValue(true)]
        public Nullable<bool> IS_ENABLED { get; set; }



        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> CONVERSION_FACTOR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long UNDER_NO { get; set; }

        public Nullable<int> DOC_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> DOC_NO { get; set; }

        public virtual ICollection<HOLDER_TRANS_DT_Model> TRANS_DT_Collection { set; get; }

    }
}
