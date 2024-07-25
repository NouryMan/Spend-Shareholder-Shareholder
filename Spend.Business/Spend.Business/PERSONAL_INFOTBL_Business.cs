using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class PERSONAL_INFOTBL_Business
    {
        oracleDbContextSpend db;
        public PERSONAL_INFOTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<PERSONAL_INFOTBL_Model> GetAll()
        {
            List<PERSONAL_INFOTBL_Model> obj1 = db.PERSONAL_INFOTBL_Model.ToList();
            return obj1;
        }
        public PERSONAL_INFOTBL_Model GetByID(long id)
        {
           PERSONAL_INFOTBL_Model obj = db.PERSONAL_INFOTBL_Model.Find(id);
            return obj;
        }

        public long Create(PERSONAL_INFOTBL_Model PERSONAL)
        {
            PERSONAL.CR_DATE = DateTime.Now;
            PERSONAL.IS_ENABLED = true;
            
            PERSONAL.ID = getMaxid();
            db.PERSONAL_INFOTBL_Model.Add(PERSONAL);
            long return_value = db.SaveChanges();
            if(return_value > 0)
            {
                return_value = PERSONAL.ID;
            }
            return return_value;
        }
        public long getMaxid()
        {
            long return_value = 1;
            try
            {
                return_value = db.PERSONAL_INFOTBL_Model.Max(x => x.ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }


        public long Update(PERSONAL_INFOTBL_Model PERSONAL)
        {
            PERSONAL.UP_DATE = DateTime.Now;

            
            db.Entry(PERSONAL).State = EntityState.Modified;
            db.Entry(PERSONAL).Property(x => x.ACC_LEVEL).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.USER_CR).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.CR_DATE).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.ACC_NAT).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.ACC_NO).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.ACC_TYPE).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.ADDRESS2).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.ADDRESS3).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.BANK_ACC).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.BANK_NAME).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.BIRTH_DATE).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.C_ID_EXP_DATE).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.C_ID_ISSUE_DATE).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.DEGREE).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.DESCRIPTION).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.GENDER).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.IS_ENABLED).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.JOB_NO).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.NATIONALITY_NO).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.OP_ACC).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.PARENT_ACC).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.PARENT_PERSON).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.PASSPORT_EXP_DATE).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.PASSPORT_ISSUE_DATE).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.PASSPORT_NO).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.TELEPHONE_NO).IsModified = false;
            db.Entry(PERSONAL).Property(x => x.VAT_NO).IsModified = false;
            long return_value = db.SaveChanges();
         
            if (return_value > 0)
            {
                return_value = PERSONAL.ID;
            }
            return return_value;
        }
      

    }
}
