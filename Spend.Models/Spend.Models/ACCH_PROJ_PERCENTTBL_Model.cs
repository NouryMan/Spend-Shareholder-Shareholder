using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("ACCH_PROJ_PERCENTTBL")]
    public class ACCH_PROJ_PERCENTTBL_Model
    {
        public ACCH_PROJ_PERCENTTBL_Model()
        {
                this.ACCH_PROJ_BOX_PERCENTTBL_Collection=new HashSet<ACCH_PROJ_BOX_PERCENTTBL_Model>(); 
        }
        [Key, Column(Order = 0)]
        [Required]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int PROJ_NO { get; set; }


        [Key, Column(Order = 1)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long ACC_HOLDER_NO { get; set; }

       
        public double ACCH_SPEND { get; set; }

        public double ACCH_PERCENT { get; set; }
      
        public double ACCH_INCOME { get; set; }
       
        public double ACCH_INCOMEPER { get; set; }

        [System.ComponentModel.DefaultValue(true)]
        public Nullable<bool> ACTIVE { get; set; }


        [Column(TypeName = "VARCHAR2"), StringLength(400, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }

         public double COMMIT_PERCENT { get; set; }
        [ForeignKey("ACC_HOLDER_NO")]
        public virtual ACC_HOLDERTBL_Model ACC_HOLDERTBL_Model { get; set; }

        [ForeignKey("PROJ_NO")]
        public virtual PROJECTTBL_Model PROJECTTBL_Model { get; set; }

        //[ForeignKey("PROJ_NO,ACCH_PERCENT")]
        //public virtual ACCH_PROJ_BOX_PERCENTTBL_Model ACCH_PROJ_BOX_PERCENTTBL_Model { get; set; }

        public virtual ICollection<ACCH_PROJ_BOX_PERCENTTBL_Model> ACCH_PROJ_BOX_PERCENTTBL_Collection { set; get; }


        [NotMapped]
        public Nullable<double> MAINTE_PERCENT { get; set; }
        [NotMapped]
        public Nullable<double> MARKETING_PERCENT { get; set; }
        [NotMapped]
        public Nullable<double> OTHER_COST { get; set; }
        [NotMapped]
        public Nullable<double> WORK_PERCENT { get; set; }

            [NotMapped]
            public Nullable<int> BOX_NO { get; set; }
    }
}
