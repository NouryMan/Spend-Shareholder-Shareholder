using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACC_HOLDERTBL")]
    public class ACC_HOLDERTBL_Model
    {
        public ACC_HOLDERTBL_Model()
        {
            this.ACCH_OPBOXTBL_Model_Collection = new HashSet<ACCH_OPBOXTBL_Model>();
            this.ACC_HOLDERTBL_Model_Collection = new HashSet<ACC_HOLDERTBL_Model>();
            this.ACCH_BALANCEV_Model_Collection = new HashSet<ACCH_BALANCEV_Model>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_HOLDER_NO { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]

        public string ACC_HOLDER_NAME { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_HOLDER_MOBILE { get; set; }




        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]

        public string ACC_HOLDER_ADDRESS { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]

        public string NOTE { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> COMMIT_PERCENT { get; set; }
       
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_PARENT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PARENT_ACCH { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_NAT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_LEVEL { get; set; }

        public Nullable<double> MAIN_FIXED_PERCENT { get; set; }
        public Nullable<double> CHILD_FIXED_PERCENT { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool OP_ACC { get; set; }
        [System.ComponentModel.DefaultValue(true)]
        public bool IS_ENABLE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACCH_TYPE { get; set; }



        public virtual ICollection<ACCH_OPBOXTBL_Model> ACCH_OPBOXTBL_Model_Collection { set; get; }
        public virtual ICollection<ACC_HOLDERTBL_Model> ACC_HOLDERTBL_Model_Collection { set; get; }

        public virtual ICollection<ACCH_BALANCEV_Model> ACCH_BALANCEV_Model_Collection { set; get; }

        [ForeignKey("PARENT_ACCH")]
        public virtual ACC_HOLDERTBL_Model ACC_HOLDERTBL_Models { get; set; }
       
        [ForeignKey("ACC_PARENT")]
        public virtual ACC_TREETBL_Model ACC_TREETBL_Model { get; set; }

        [ForeignKey("ACCH_TYPE")]
        public virtual ACCH_TYPETBL_Model ACCH_TYPETBL_Model { get; set; }


        [NotMapped]
        public string Acc_NOString { get; set; }

        [NotMapped]
        public double BALANCE { get; set; }

    }
}
