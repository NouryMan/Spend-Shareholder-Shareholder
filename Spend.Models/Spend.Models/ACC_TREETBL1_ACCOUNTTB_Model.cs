using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACC_TREETBL1_ACCOUNTTB")]
    public class ACC_TREETBL1_ACCOUNTTB_Model
    {
        

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }

         public long ACC_TREE1_NO { get; set; }
         public long ACC_NO { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [System.ComponentModel.DefaultValue(true)]
        public bool IS_ENABLE { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public bool IS_DELETE { get; set; }

        [ForeignKey("ACC_TREE1_NO")]
        public virtual ACC_TREETBL1_Model ACC_TREETBL1_Model { get; set; }
       
        [ForeignKey("ACC_NO")]
        public virtual  ALL_ACC_NOTBL_Model ALL_ACC_NOTBL_Model { get; set; }

        
    }
}
