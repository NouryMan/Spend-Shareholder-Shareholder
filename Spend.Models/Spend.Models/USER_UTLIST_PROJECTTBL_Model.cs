using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("USER_UTLIST_PROJECTTBL")]
    public class USER_UTLIST_PROJECTTBL_Model
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int USER_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int UTLIST_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int PROJECT_ID { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DELETED { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [ForeignKey("USER_ID")]
        public virtual USERSTBL_Model USERSTBL_Model { get; set; }
        [ForeignKey("UTLIST_ID")]
        public virtual UTLISTTBL_Model UTLISTTBL_Model { get; set; }
        [ForeignKey("PROJECT_ID")]
        public virtual PROJECTTBL_Model PROJECTTBL_Model { get; set; }
    }
}
