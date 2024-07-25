using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("STAGESTBL")]
    public   class STAGESTBL_Model
    {
        public STAGESTBL_Model()
        {
            this.PROJECTTBL_Collection = new HashSet<PROJECTTBL_Model>();
           
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int STAGE_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string STAGE_NAME { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PERCENT_FROM_ALL { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> S_TYPE { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PARENT_STAGE { get; set; }
        public virtual ICollection<PROJECTTBL_Model> PROJECTTBL_Collection { set; get; }

    }
}
