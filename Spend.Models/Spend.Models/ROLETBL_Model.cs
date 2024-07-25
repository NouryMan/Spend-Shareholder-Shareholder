using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ROLETBL")]
  public  class ROLETBL_Model
    {
        public ROLETBL_Model()
        {
            this.USER_ROLETBLE_Collection = new HashSet<USER_ROLETBLE_Model>();
            this.FEATURE_ROLETBL_Collection = new HashSet<FEATURE_ROLETBL_Model>();
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NAME_AR { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NAME_EN { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        public virtual ICollection<USER_ROLETBLE_Model> USER_ROLETBLE_Collection { set; get; }
        public virtual ICollection<FEATURE_ROLETBL_Model> FEATURE_ROLETBL_Collection { set; get; }
    }
}
