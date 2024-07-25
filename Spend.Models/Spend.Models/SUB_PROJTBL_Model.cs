using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("SUB_PROJTBL")]
    public class SUB_PROJTBL_Model
    {
        public SUB_PROJTBL_Model()
        {
            this.PERSON_ACC_PROJTBL_Collection = new HashSet<PERSON_ACC_PROJTBL_Model>();


        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int SUB_PROJ_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string SUB_PROJ_NAME { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> DECORATE_ENG { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(150, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        public virtual ICollection<PERSON_ACC_PROJTBL_Model> PERSON_ACC_PROJTBL_Collection { set; get; }

    }
}
