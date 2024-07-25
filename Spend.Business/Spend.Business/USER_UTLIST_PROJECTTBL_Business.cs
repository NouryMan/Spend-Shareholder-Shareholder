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
  public  class USER_UTLIST_PROJECTTBL_Business
    {
        oracleDbContextSpend db;
        public USER_UTLIST_PROJECTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<USER_UTLIST_PROJECTTBL_Model> getall()
        {
            List<USER_UTLIST_PROJECTTBL_Model> obj = db.USER_UTLIST_PROJECTTBL_Model.Where(x => x.IS_DELETED == false).ToList();


            return obj;
        }
        public USER_UTLIST_PROJECTTBL_Model GetPyID(int id)
        {
            USER_UTLIST_PROJECTTBL_Model obj = db.USER_UTLIST_PROJECTTBL_Model.Find(id);


            return obj;
        }

        public int Create(USER_UTLIST_PROJECTTBL_Model USER_UTLIST_PROJECTTBL)
        {
            USER_UTLIST_PROJECTTBL.ID = getMaxid();




            db.USER_UTLIST_PROJECTTBL_Model.Add(USER_UTLIST_PROJECTTBL);
            int return_value = db.SaveChanges();
            return return_value;
        }

        public int Update(USER_UTLIST_PROJECTTBL_Model USER_UTLIST_PROJECTTBL)
        {



            db.Entry(USER_UTLIST_PROJECTTBL).State = EntityState.Modified;

          

            int return_value = db.SaveChanges();
            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.USER_UTLIST_PROJECTTBL_Model.Max(x => x.ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }
        public int delete(int id)
        {
            int return_value = 0;
            USER_UTLIST_PROJECTTBL_Model obj = db.USER_UTLIST_PROJECTTBL_Model.Find(id);
            obj.IS_DELETED = true;

            try
            {
                return_value = Update(obj);

            }
            catch
            {

            }


            return return_value;
        }
    }
}
