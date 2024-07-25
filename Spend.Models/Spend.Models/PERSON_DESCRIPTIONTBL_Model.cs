using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("PERSON_DESCRIPTIONTBL")]
    public  class PERSON_DESCRIPTIONTBL_Model
    {
        public PERSON_DESCRIPTIONTBL_Model()
        {
            this.PERSON_ACCTBL_Collection = new HashSet<PERSON_ACCTBL_Model>();
            //this.PERSON_ACCTBL_Collection = new HashSet<PERSON_ACCTBL_Model>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string DESC_NAME { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PARENT_ID { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }  [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string SOURCE_TABLE { get; set; }
        public virtual ICollection<PERSON_ACCTBL_Model> PERSON_ACCTBL_Collection { set; get; }


    }
}
