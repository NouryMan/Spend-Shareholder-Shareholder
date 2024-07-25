using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ALL_ACC_NOTBL")]
    public class ALL_ACC_NOTBL_Model
    {
        public ALL_ACC_NOTBL_Model()
        {
            this.ALL_ACC_NOTBL_Model_Collection = new HashSet<ALL_ACC_NOTBL_Model>();
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_NO { get; set; }

        
        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        
        public string ACC_NAME { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> ACC_PARENT { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> COMP_NO { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PRANCH_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NAT { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_LEVEL { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool IS_ENABLE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SYS_CODE { get; set; }

        [System.ComponentModel.DefaultValue(false)]
        public bool OP_ACC { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PERSONAL_NO { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]

        public string NOTE { get; set; }
        [ForeignKey("PERSONAL_NO")]
        public virtual PERSONAL_INFOTBL_Model PERSONAL_INFOTBL_Model { get; set; }

        [ForeignKey("ACC_PARENT")]
        public virtual ALL_ACC_NOTBL_Model ALL_ACC_NOTBL_Models { get; set; }

        public virtual ICollection<ALL_ACC_NOTBL_Model> ALL_ACC_NOTBL_Model_Collection { set; get; }

        [NotMapped]
        public string VAT_NO { get; set; }
        [NotMapped]
        public decimal FROM_HIM { get; set; }
        [NotMapped]
        public decimal FOR_HIM { get; set; }


    }
}


