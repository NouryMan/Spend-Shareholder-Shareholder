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
public    class CREDENCE_DTTBL_Business
    {
        oracleDbContextSpend db;
        public CREDENCE_DTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<CREDENCE_DTTBL_Model> getall()
        {
       
            List<CREDENCE_DTTBL_Model> obj = db.CREDENCE_DTTBL_Model.Where(x=>x.IS_DELETED==false).ToList();


            return obj;
        }
        public CREDENCE_DTTBL_Model GetPyID(int id,int Hd_Id)
        {
            CREDENCE_DTTBL_Model obj = db.CREDENCE_DTTBL_Model.SingleOrDefault(x=>x.DT_ID==id&&x.HD_ID==Hd_Id);


            return obj;
        }
        public int Create(CREDENCE_DTTBL_Model CREDENCE_DTTBL)
        {


            CREDENCE_DTTBL.CR_DATE = DateTime.Now;
           CREDENCE_DTTBL.EXEC_DATE_M = DateTime.Now;
            CREDENCE_DTTBL.DONE = false;
            CREDENCE_DTTBL.IS_DELETED = false;
            CREDENCE_DTTBL.APPROVED = false;

            try
            {
                CREDENCE_DTTBL.DT_ID = db.CREDENCE_DTTBL_Model.Where(x=>x.HD_ID==CREDENCE_DTTBL.HD_ID).Max(x => x.DT_ID) + 1;
            }
            catch
            {
                CREDENCE_DTTBL.DT_ID = 1;
            }
            db.CREDENCE_DTTBL_Model.Add(CREDENCE_DTTBL);
            int return_value = db.SaveChanges();
            if (return_value > 0)
            {
                return CREDENCE_DTTBL.DT_ID;
            }
            return return_value;
        }

        public int Update(CREDENCE_DTTBL_Model CREDENCE_DTTBL)
        {
            CREDENCE_DTTBL.UP_DATE = DateTime.Now;


            db.Entry(CREDENCE_DTTBL).State = EntityState.Modified;
          //  db.Entry(CREDENCE_DTTBL).Property(x => x.IS_DELETED).IsModified = false;
            db.Entry(CREDENCE_DTTBL).Property(x => x.USER_CR).IsModified = false;
            db.Entry(CREDENCE_DTTBL).Property(x => x.CR_DATE).IsModified = false;
            db.Entry(CREDENCE_DTTBL).Property(x => x.DONE).IsModified = false;
            db.Entry(CREDENCE_DTTBL).Property(x => x.EXEC_DATE_M).IsModified = false;
            db.Entry(CREDENCE_DTTBL).Property(x => x.EMP_NO).IsModified = false;
            int return_value = db.SaveChanges();
            return return_value;
        }


        public int delete(int dt_id, int hd_id)
        {
            int return_value = 0;
            CREDENCE_DTTBL_Model obj = db.CREDENCE_DTTBL_Model.Find(dt_id,hd_id);
            obj.IS_DELETED = true;

            try
            {
                if (obj.DONE == false)
                {
                    return_value = Update(obj);
                }

            }
            catch
            {

            }


            return return_value;
        }

    }
}
