using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Spend.Models
{

    [Table("PERSON_ACC_PROJTBL")]
    public class PERSON_ACC_PROJTBL_Model
    {


        public PERSON_ACC_PROJTBL_Model()
        {
          
              
               this.CREDENCE_DTTBL_Collection = new HashSet<CREDENCE_DTTBL_Model>();
            //try { TOTAL_PRICE = SING_PRICE.Value * QNTY.Value; } catch { }
             
        }
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public int ID { get; set; }

        [Required]
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PERSON_ACC_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PROJECT_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
     //  سقف المدين
        public Nullable<decimal> DEB_AMOUNT_LIMIT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> AMOUNT_PAY_PERIOD { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> QNTY_LIMIT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PERSON_DELEGATE { get; set; }

        [Column(TypeName = "VARCHAR2"), StringLength(200, ErrorMessage = "طول الحقل كبير جداً")]
        public string NOTE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PERSON_DESC_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
      //  سقف الدائن
        public Nullable<decimal> CRED_AMOUNT_LIMIT { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
     //   مدة السداد
        public Nullable<int> CRED_AMNT_PERIOD { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<long> PERSON_ID { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> ACC_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UTL_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UTL_TYPE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> QNTY { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<decimal> SING_PRICE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_CR { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> USER_UP { get; set; }
        public Nullable<DateTime> CR_DATE { get; set; }
        public Nullable<DateTime> UP_DATE { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> SUB_PROJ_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> PART_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> UNIT_NO { get; set; }
        [RegularExpression("^[0-9.]*$", ErrorMessage = "هذا الحقل رقم ")]
        public Nullable<int> EXECUT_PERIOD { get; set; }
        [System.ComponentModel.DefaultValue(false)]
        public Nullable<bool> IS_DELETED { get; set; }


        [ForeignKey("PERSON_ACC_ID")]
        public virtual PERSON_ACCTBL_Model PERSON_ACCTBL_Model { get; set; }

        // //[ForeignKey("PERSON_DELEGATE")]
        // //public virtual PERSONAL_INFOTBL_Model PERSONAL_INFOTBL_Model { get; set; }

        
        [ForeignKey("PROJECT_NO")]
        public virtual PROJECTTBL_Model PROJECTTBL_Model { get; set; }
        [ForeignKey("UTL_NO")]
        public virtual UTLISTTBL_Model UTLISTTBL_Model { get; set; }

        [ForeignKey("UTL_TYPE")]
        public virtual UTL_TYPETBL_Model UTL_TYPETBL_Model { get; set; }
        [ForeignKey("UNIT_NO")]
        public virtual UNITTBL_Model UNITTBL_Model { get; set; }
        [ForeignKey("PART_NO")]
        public virtual PARTTBL_Model PARTTBL_Model { get; set; }

        [ForeignKey("SUB_PROJ_NO")]
        public virtual SUB_PROJTBL_Model SUB_PROJTBL_Model { get; set; }
        [ForeignKey("USER_CR")]
        public virtual USERSTBL_Model USER_CR_Model { get; set; }

        //[ForeignKey("PERSON_ACC_ID")]
        //public virtual PERSON_ACCTBL_Model PERSON_ACCTBL_Model { get; set; }


        public virtual ICollection<CREDENCE_DTTBL_Model> CREDENCE_DTTBL_Collection { set; get; }



        [NotMapped]

        public decimal TOTAL_PRICE { get { try { return SING_PRICE.Value * QNTY.Value; } catch { return 0; } } }
        [NotMapped]
        public decimal NO_PAY_AMOUNT { get {
            try{ return CREDENCE_DTTBL_Collection.Where(x => x.DONE != true && x.IS_DELETED == false).Sum(x => x.AMMOUNT).Value; } catch
                {
                    return 0;
                }
                
            
            } }
        [NotMapped]
        public decimal PAY_AMOUNT { get; set; }
        [NotMapped]
        public decimal REST_TOTAL { get; set; }


    }


}

