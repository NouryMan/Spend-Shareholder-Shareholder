using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
   
        [Table("SCRIPT_ACTIONSTBL")]
        public class SCRIPT_ACTIONSTBL_Model { 
            [Key]
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.None)]
            [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
            public int ACTION_NO { get; set; }


            [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
            public string ACTION_NAME { get; set; }


            [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
            public string NOTE { get; set; }


        }
    }

