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
   public class PROJECTTBL_Business
    {
        oracleDbContextSpend db;
        public PROJECTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<PROJECTTBL_Model> getall()
        {
            List<PROJECTTBL_Model> obj = db.PROJECTTBL_Model.Where(x=>x.IS_DELETE==false).ToList();

            return obj;
        }
        public PROJECTTBL_Model GetByID(int id)
        {
            PROJECTTBL_Model obj = db.PROJECTTBL_Model.Find(id);
            return obj;
        }
       
        public int GetMaxID()
        {
            int id = 0;
            try
            {
                id = db.PROJECTTBL_Model.Max(x => x.PROJECT_NO)+1;
            }
            catch
            {
                id++;
            }
            return id;
        }


        public long Create(PROJECTTBL_Model Proj)
        {

           // callProjectBusiness b = new callProjectBusiness(false);

            Proj.PROJECT_NO = GetMaxID()+1;
            Proj.ADD_FROM ="S";
           // Proj.CALL_PROJ_ID =b.GetMaxID();
            Proj.PROJ_TYPE = 1;
            db.PROJECTTBL_Model.Add(Proj);

            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = Proj.PROJECT_NO;
            }
            return return_value;
        }


        public long Update(PROJECTTBL_Model Proj)
        { Proj.UPDATE_FROM="S";
          
            db.Entry(Proj).State = EntityState.Modified;
            db.Entry(Proj).Property(x => x.CALL_PROJ_ID).IsModified = false;
            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = Proj.PROJECT_NO;
            }
            return return_value;
        }

        public int Delete(long id)
        {
            int return_value = 0;
            var obj = db.PROJECTTBL_Model.Find(id);
            obj.IS_DELETE = true;

            try
            {
                db.Entry(obj).State = EntityState.Modified;

                return_value = db.SaveChanges();

            }
            catch
            {

            }

            return return_value;
        }

    }
}
