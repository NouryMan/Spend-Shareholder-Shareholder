using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("SCRIP_OPTYPETBL")]
    public  class SCRIP_OPTYPETBL_Model
    {
        
        
            [Key]
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
            public int ID_NO { get; set; }


            [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
            public string NAME { get; set; }


            [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
            public string NOTE { get; set; }


        }
    
}
