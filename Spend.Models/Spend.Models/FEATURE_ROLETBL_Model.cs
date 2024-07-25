using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("FEATURE_ROLETBL")]
    public class FEATURE_ROLETBL_Model
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int FEATURE_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ROLE_ID { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [ForeignKey("FEATURE_ID")]
        public virtual FEATURETBL_Model FEATURETBL_Model { get; set; }

        [ForeignKey("ROLE_ID")]
        public virtual ROLETBL_Model ROLETBL_Model { get; set; }

    }
}
