using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("CUSTODY_TYPETBL")]
  
    public   class CUSTODY_TYPETBL_Model
    {

        public CUSTODY_TYPETBL_Model()
        {
            //this.PERSON_ACC_PROJTBL_Collection = new HashSet<PERSON_ACC_PROJTBL_Model>();
            //this.PERSON_ACCTBL_Collection = new HashSet<PERSON_ACCTBL_Model>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string CUSTODY_NAME { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(300, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
    }
}
