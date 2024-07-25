using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("CREDENCETBL")]
    public class CREDENCETBL_Model
    {
        public CREDENCETBL_Model()
        {

            this.CREDENCE_DTTBL_Collection = new HashSet<CREDENCE_DTTBL_Model>();

        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string CRED_NAME { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(15, ErrorMessage = "طول الحقل كبير جداً")]
        public string DATE_H { get; set; }
        public Nullable<DateTime> DATE_M { get; set; }
        public Nullable<DateTime> TO_DATE_M { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> BALANCE_LIMIT { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(15, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool CLOSED { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DELETED { get; set; }
        public virtual ICollection<CREDENCE_DTTBL_Model> CREDENCE_DTTBL_Collection { set; get; }

    }
}
