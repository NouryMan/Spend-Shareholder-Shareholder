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
    public class PERSON_ACC_PROJTBL_Business
    {

        oracleDbContextSpend db;
        public PERSON_ACC_PROJTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
       
        public List<PERSON_ACC_PROJTBL_Model> getall()
        {
            List<PERSON_ACC_PROJTBL_Model> obj1 = db.PERSON_ACC_PROJTBL_Model.Where(x=>x.IS_DELETED==false).ToList();

            //   List<PERSON_ACC_PROJTBL_Model> obj = db.PERSON_ACC_PROJTBL_Modeldb.ToList();

            return obj1;
        }
        public PERSON_ACC_PROJTBL_Model GetPyID(int id)
        {
            PERSON_ACC_PROJTBL_Model obj = db.PERSON_ACC_PROJTBL_Model.Find(id);


            return obj;
        }


        public int Create(PERSON_ACC_PROJTBL_Model PERSON_ACC_PROJTBL)
        {


            PERSON_ACC_PROJTBL.CR_DATE = DateTime.Now;
            try
            {
                PERSON_ACC_PROJTBL.ID = db.PERSON_ACC_PROJTBL_Model.Max(x => x.ID) + 1;
            }
            catch
            {
                PERSON_ACC_PROJTBL.ID = 1;
            }
            db.PERSON_ACC_PROJTBL_Model.Add(PERSON_ACC_PROJTBL);
            int return_value = db.SaveChanges();
            return return_value;
        }

        public int Update(PERSON_ACC_PROJTBL_Model PERSON_ACC_PROJTBL)
        {
            PERSON_ACC_PROJTBL.UP_DATE = DateTime.Now; 


            db.Entry(PERSON_ACC_PROJTBL).State = EntityState.Modified;
         //   db.Entry(PERSON_ACC_PROJTBL).Property(x => x.IS_DELETED).IsModified = false;
           
            db.Entry(PERSON_ACC_PROJTBL).Property(x => x.USER_CR).IsModified = false;
            db.Entry(PERSON_ACC_PROJTBL).Property(x => x.CR_DATE).IsModified = false;
            int return_value = db.SaveChanges();
            return return_value;
        }


        public int delete(int id)
        {
            int return_value = 0;
            PERSON_ACC_PROJTBL_Model obj = db.PERSON_ACC_PROJTBL_Model.Find(id);
            obj.IS_DELETED = true;

            try
            {
                if (obj.CREDENCE_DTTBL_Collection.Where(x=>x.IS_DELETED==false).Count() <= 0)
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
