using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("USERSTBL")]
    public class USERSTBL_Model
    {
       
        public USERSTBL_Model()
        {
            this.USER_ROLETBLE_Collection = new HashSet<USER_ROLETBLE_Model>();
            this.USER_UTLIST_PROJECTTBL_Collection = new HashSet<USER_UTLIST_PROJECTTBL_Model>();
            this.CREDENCE_DTTBL_Collection = new HashSet<CREDENCE_DTTBL_Model>();

            //  this.USERSTBL_Collection = new HashSet<USERSTBL_Model>();


        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int USER_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PERSON_ID { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(30, ErrorMessage = "طول الحقل كبير جداً")]
        public string LOGIN_NAME { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(20, ErrorMessage = "طول الحقل كبير جداً")]
        public string LOGIN_PASS { get; set; }
        public Nullable<DateTime> REG_DATE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> LOGIN_PERIOD { get; set; }

       
        public Nullable<bool> FULL_GRANTS { get; set; }
       
        public Nullable<bool> CHANGE_PASS { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }
       
        [System.ComponentModel.DefaultValue(false)]
        public bool IS_MAIN { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public bool ENABLE_LOGIN { get; set; }

        [ForeignKey("PERSON_ID")]
        public virtual PERSONAL_INFOTBL_Model PERSONAL_INFOTBL_Model { get; set; }


        public virtual ICollection<USER_ROLETBLE_Model> USER_ROLETBLE_Collection { set; get; }

        public virtual ICollection<USER_UTLIST_PROJECTTBL_Model> USER_UTLIST_PROJECTTBL_Collection { set; get; }
        //   public virtual ICollection<USERSTBL_Model> USERSTBL_Collection { set; get; }

        public virtual ICollection<CREDENCE_DTTBL_Model> CREDENCE_DTTBL_Collection { set; get; }

    }
}
