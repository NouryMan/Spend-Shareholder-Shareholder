using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("CUSTOMERTBL")]
    public class CUSTOMERTBL_Model
    {
        public CUSTOMERTBL_Model()
        {
             this.SALES_INVTBL_Collection = new HashSet<SALES_INVTBL_Model>();
          
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int CUST_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string CUST_NAME { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string ADDRESS { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
       public string MOBILE_NO { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string PHONE_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        // [System.ComponentModel.DefaultValue(true)]
         public bool EMP { get; set; }
        
         [System.ComponentModel.DefaultValue(false)]
          public bool SUPP { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PARENT_ACC { get; set; }
      


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }

        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> ACC_TYPE { get; set; }


       

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NAT { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> VAT_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_LEVEL { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> OP_ACC { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> IS_ENABLE { get; set; }

     
        public virtual ICollection<SALES_INVTBL_Model> SALES_INVTBL_Collection { set; get; }
          
    }
}
