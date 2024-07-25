using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("HOLDER_PERCENT_HD")]
    public class HOLDER_PERCENT_HD_Model
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ID { get; set; }



        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> TARGET_PROJ { get; set; }

       

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long FISCAL_YEAR_ID { get; set; }


        [System.ComponentModel.DefaultValue(true)]
        public Nullable<bool> STATUS { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]

        public string NOTE { get; set; }

        public double BALANCE { get; set; }
        public Nullable<DateTime> C_DATE { get; set; }



    }
}