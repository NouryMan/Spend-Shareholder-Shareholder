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
 public   class CREDENCETBL_Business
    {
        oracleDbContextSpend db;
        public CREDENCETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<CREDENCETBL_Model> getall()
        {
            List<CREDENCETBL_Model> obj = db.CREDENCETBL_Model.Where(x=>x.IS_DELETED==false).ToList();


            return obj;
        }
        public CREDENCETBL_Model GetPyID(int id)
        {
            CREDENCETBL_Model obj = db.CREDENCETBL_Model.Find(id);


            return obj;
        }

        public int Create(CREDENCETBL_Model CREDENCE)
        {
            CREDENCE.ID_NO = getMaxid();




            db.CREDENCETBL_Model.Add(CREDENCE);
            int return_value = db.SaveChanges();
            return return_value;
        }

        public int Update(CREDENCETBL_Model CREDENCE)
        {
          


            db.Entry(CREDENCE).State = EntityState.Modified;
          
          
           
            int return_value = db.SaveChanges();
            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.CREDENCETBL_Model.Max(x => x.ID_NO)+1;

            }
            catch
            {

            }
            return return_value;
        }
        public int delete(int id)
        {
            int return_value = 0;
            CREDENCETBL_Model obj = db.CREDENCETBL_Model.Find(id);
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
