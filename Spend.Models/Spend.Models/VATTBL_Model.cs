using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("VATTBL")]
    public  class VATTBL_Model
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string VAT_NAME { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> VAT_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> VAT_PERCENT { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(1, ErrorMessage = "طول الحقل كبير جداً")]
        public string IS_ENABLE { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
    }
}
