using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("PERSON_ACCTBL")]
    public class PERSON_ACCTBL_Model
    {
        
        public PERSON_ACCTBL_Model()
        {
           // this.PERSON_ACC_PROJTBL_Collection = new HashSet<PERSON_ACC_PROJTBL_Model>();
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PERSON_ID { get; set; }
        
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PERSON_DESC_ID { get; set; }
       
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> ACC_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PARENT_ACC { get; set; }



        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_TYPE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NAT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_LEVEL { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool IS_ENABLED { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool OP_ACC { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

      
      
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }
       
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }


        [ForeignKey("PERSON_DESC_ID")]
        public virtual PERSON_DESCRIPTIONTBL_Model PERSON_DESCRIPTIONTBL_Model { get; set; }

        [ForeignKey("PERSON_ID")]
        public virtual PERSONAL_INFOTBL_Model PERSONAL_INFOTBL_Model { get; set; }


        [ForeignKey("PARENT_ACC")]
        public virtual ALL_ACC_NOTBL_Model ALL_ACC_NOTBL_Model { get; set; }


        [ForeignKey("ACC_TYPE")]
        public virtual ACC_TYPETBL_Model ACC_TYPETBL_Model { get; set; }

        public virtual ICollection<PERSON_ACC_PROJTBL_Model> PERSON_ACC_PROJTBL_Collection { set; get; }


        [NotMapped]
        public string Acc_NOString { get; set; }
    }
}
