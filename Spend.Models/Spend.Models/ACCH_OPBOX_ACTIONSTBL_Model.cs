using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACCH_OPBOX_ACTIONSTBL")]
    public class ACCH_OPBOX_ACTIONSTBL_Model
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]

        public int ID { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string ACTION_NAME { get; set; }
        
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DELETED { get; set; }
    }
}
