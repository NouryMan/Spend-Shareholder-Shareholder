using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Models
{
    [Table("FISCAL_YEAR")]
    public class FISCAL_YEAR_Model
    {
        public FISCAL_YEAR_Model()
        {


        }

        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public long YEAR_ID { get; set; }


        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int YEAR { get; set; }


      

       

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }


      
        public Nullable<int> USER_CR { get; set; }

        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }

        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }
        public Nullable<bool> IS_ENABLED { get; set; }
        public Nullable<DateTime> START_DATE_M { get; set; }
        public Nullable<DateTime> END_DATE_M { get; set; }
        public Nullable<DateTime> START_DATE_H { get; set; }
        public Nullable<DateTime> END_DATE_H { get; set; }
        public Nullable<bool> IS_DELETE { get; set; }
        public Nullable<int> STATUS { get; set; }




    }
}
