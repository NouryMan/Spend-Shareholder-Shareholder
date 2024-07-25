using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("USER_ROLETBLE")]
   public class USER_ROLETBLE_Model
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]

        public int USER_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ROLE_ID { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]

        public string NOTE { get; set; }

        [ForeignKey("USER_ID")]
        public virtual USERSTBL_Model USERSTBL_Model { get; set; }
        [ForeignKey("ROLE_ID")]
        public virtual ROLETBL_Model ROLETBL_Model { get; set; }


    }
}
