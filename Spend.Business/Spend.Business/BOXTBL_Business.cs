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
  public  class BOXTBL_Business
    {
        oracleDbContextSpend db;
        public BOXTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<BOXTBL_Model> getall()
        {



            List<BOXTBL_Model> obj = db.BOXTBL_Model.Where(x=>x.IS_DELETE==false).ToList();


            return obj;


        }
        public BOXTBL_Model GetPyID(int id)
        {
            BOXTBL_Model obj = db.BOXTBL_Model.Find(id);


            return obj;


        }
        public int GetMaxNo()
        {
            int id = 0;
            try
            {
                id = db.BOXTBL_Model.Max(x => x.BOX_NO) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }

        public long Create(BOXTBL_Model box)
        {
            int return_value = 0;
            try
            {

                box.BOX_NO = GetMaxNo();
                ALL_ACC_NOTBL_Business alL_ACC_NOTBL_Business = new ALL_ACC_NOTBL_Business();
                

                db.BOXTBL_Model.Add(box);

                 return_value = db.SaveChanges();
            }
            catch { }
            
            return return_value;
        }
        public long Update(BOXTBL_Model box)
        {





          
            db.Entry(box).State = EntityState.Modified;
            db.Entry(box).Property(x => x.CR_DATEM).IsModified = false;




            long return_value = db.SaveChanges();


            return return_value;
        }

        public int Delete(long id)
        {
            int return_value = 0;
            var obj = db.BOXTBL_Model.Find(id);
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
