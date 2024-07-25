using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("HOLDER_TRANS_DT")]
    public class HOLDER_TRANS_DT_Model
    {
        public HOLDER_TRANS_DT_Model()
        {


        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ID { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACCH_ID { get; set; }


        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long UNDER_NO { get; set; }



        public double FOR_HIM { get; set; }


        public double FROM_HIM { get; set; }
        public Nullable<DateTime> DOC_DATE { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }


        public Nullable<long> DOC_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> GROUP_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }

        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }
        public Nullable<DateTime> DATE_H { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> DOC_TYPE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> TRANS_ID { get; set; }



        [ForeignKey("TRANS_ID")]
        public virtual HOLDER_TRANS_HD_Model HOLDER_TRANS_HD_Model { get; set; }



      


    }
}
