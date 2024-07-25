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
   public class ENGINEERTBL_Business
    {
        oracleDbContextSpend db;
        public ENGINEERTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<Models.ENGINEERTBL_Model> getall()
        {



            List<Models.ENGINEERTBL_Model> obj = db.ENGINEERTBL_Model.Where(x=>x.IS_DELETE==false).ToList();


            return obj;


        }
        public Models.ENGINEERTBL_Model GetPyID(int id)
        {
            Models.ENGINEERTBL_Model obj = db.ENGINEERTBL_Model.Find(id);


            return obj;


        }
        public int GetMaxID()
        {
            int id = 0;
            try
            {
                id = db.ENGINEERTBL_Model.Max(x => x.ENG_NO) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }


        public long Create(ENGINEERTBL_Model Eng)
        {

            Eng.ENG_NO = GetMaxID();
            db.ENGINEERTBL_Model.Add(Eng);

            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = Eng.ENG_NO;
            }
            return return_value;
        }

        public long Update(ENGINEERTBL_Model enginer)
        {

            db.Entry(enginer).State = EntityState.Modified;
           
            long return_value = db.SaveChanges();


            return return_value;
        }



    public int Delete(int id)
    {
        int return_value = 0;
        var obj = db.ENGINEERTBL_Model.Find(id);
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
