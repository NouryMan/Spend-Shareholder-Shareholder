using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACCH_BALANCE_SUMMARYV")]
    public class ACCH_BALANCE_SUMMARYV_Model
    {
        [Key, Column(Order = 0)]
        [Required]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_HOLDER_NO { get; set; }
        
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string ACC_HOLDER_NAME { get; set; }
       
        [Key, Column(Order = 1)]
        [Required]

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int TARGET_PROJ { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string PROJECT_NAME { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public double SPENDING { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public double INCOME { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public double REMAIN { get; set; }

        [ForeignKey("ACC_HOLDER_NO")]
        public virtual ACC_HOLDERTBL_Model ACC_HOLDERTBL_Model { get; set; }
    }
}
