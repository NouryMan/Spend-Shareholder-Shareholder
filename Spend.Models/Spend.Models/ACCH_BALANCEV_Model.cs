using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Spend.Models
{
    [Table("ACCH_BALANCEV")]
    public class ACCH_BALANCEV_Model
    {
        public ACCH_BALANCEV_Model()
        {
          //  this.ACCH_BALANCEV_Model_Collection = new HashSet<ACCH_BALANCEV_Model>();
        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_HOLDER_NO { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PARENT_ACCH { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string ACC_HOLDER_NAME { get; set; }

      
        public Nullable<double> INVEST_AMNT { get; set; }

        public Nullable<double> BALANCE { get; set; }

        //[ForeignKey("PARENT_ACCH")]
        //public virtual ACCH_BALANCEV_Model ACCH_BALANCEV_Mode { get; set; }

        //[ForeignKey("PARENT_ACCH")]
        //public virtual ACC_HOLDERTBL_Model PARENT_ACC_HOLDERTBL_Model { get; set; }

        [ForeignKey("ACC_HOLDER_NO")]
        public virtual ACC_HOLDERTBL_Model ACC_HOLDERTBL_Model { get; set; }

        // public virtual ICollection<ACCH_BALANCEV_Model> ACCH_BALANCEV_Model_Collection { set; get; }

    }
}
