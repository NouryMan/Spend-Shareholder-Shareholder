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
 public  class USER_RoleTBLE_Business
    {

        oracleDbContextSpend db;
        public USER_RoleTBLE_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<USER_ROLETBLE_Model> getall()
        {
            List<USER_ROLETBLE_Model> obj = db.USER_ROLETBLE_Model.ToList();


            return obj;
        }
        public USER_ROLETBLE_Model GetPyID(int id)
        {
            USER_ROLETBLE_Model obj = db.USER_ROLETBLE_Model.Find(id);


            return obj;
        }

        public int Create(USER_ROLETBLE_Model UserRole)
        {
            UserRole.ID = getMaxid();
            db.USER_ROLETBLE_Model.Add(UserRole);
            int return_value = db.SaveChanges();
            return return_value;
        }

        public int Update(USER_ROLETBLE_Model UserRole)
        {



            db.Entry(UserRole).State = EntityState.Modified;



            int return_value = db.SaveChanges();
            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.USER_ROLETBLE_Model.Max(x => x.ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }
        public int delete(int id)
        {
            int return_value = 0;
            USER_ROLETBLE_Model obj = db.USER_ROLETBLE_Model.Find(id);

            try
            {
                db.USER_ROLETBLE_Model.Remove(obj);
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