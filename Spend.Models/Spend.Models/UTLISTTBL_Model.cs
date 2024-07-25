using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("UTLISTTBL")]
    public class UTLISTTBL_Model
    {
        public UTLISTTBL_Model()
        {
            this.USER_UTLIST_PROJECTTBL_Collection = new HashSet<USER_UTLIST_PROJECTTBL_Model>();
            this.PERSON_ACC_PROJTBL_Collection = new HashSet<PERSON_ACC_PROJTBL_Model>();

        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int UTL_NO { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string UTL_NAME { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PARTS { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SING_PRICE { get; set; }
        [Column(TypeName = "VARCHAR2"), StringLength(100, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UTL_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PARENT_ACC { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم "),System.ComponentModel.DefaultValue(2)]
        public Nullable<int> ACC_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم "), System.ComponentModel.DefaultValue(2)]
        public Nullable<int> ACC_NAT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم "),System.ComponentModel.DefaultValue(5)]
        public Nullable<int> ACC_LEVEL { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_NO_TEMP { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> ACC_PARENT_TEMP { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool OP_ACC { get; set; }
        [System.ComponentModel.DefaultValue(true)]
        public bool IS_ENABLED { get; set; }

        public virtual ICollection<USER_UTLIST_PROJECTTBL_Model> USER_UTLIST_PROJECTTBL_Collection { set; get; }
        public virtual ICollection<PERSON_ACC_PROJTBL_Model> PERSON_ACC_PROJTBL_Collection { set; get; }

    }
}
