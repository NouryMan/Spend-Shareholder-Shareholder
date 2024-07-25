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
    public class FEATURE_ROLETBL_Business
    {
        oracleDbContextSpend db;
        public FEATURE_ROLETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<FEATURE_ROLETBL_Model> getall()
        {
            List<FEATURE_ROLETBL_Model> obj = db.FEATURE_ROLETBL_Model.ToList();


            return obj;
        }
        public FEATURE_ROLETBL_Model GetPyID(int id)
        {
            FEATURE_ROLETBL_Model obj = db.FEATURE_ROLETBL_Model.Find(id);


            return obj;
        }

        public int Create(FEATURE_ROLETBL_Model ROLE)
        {
            ROLE.ID = getMaxid();
            db.FEATURE_ROLETBL_Model.Add(ROLE);
            int return_value = db.SaveChanges();
            return return_value;
        }

        public int Update(FEATURE_ROLETBL_Model ROLE)
        {



            db.Entry(ROLE).State = EntityState.Modified;



            int return_value = db.SaveChanges();
            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.FEATURE_ROLETBL_Model.Max(x => x.ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }
        public int delete(int id)
        {
            int return_value = 0;
            FEATURE_ROLETBL_Model obj = db.FEATURE_ROLETBL_Model.Find(id);

            try
            {
                db.FEATURE_ROLETBL_Model.Remove(obj);
                 return_value = db.SaveChanges();
                return return_value;

            }
            catch
            {

            }


            return return_value;
        }
    }
}
