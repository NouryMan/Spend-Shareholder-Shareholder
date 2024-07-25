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
  public  class OWNERTBL_Business
    {
        oracleDbContextSpend db;
        public OWNERTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<OWNERTBL_Model> getall()
        {



            List<OWNERTBL_Model> obj = db.OWNERTBL_Model.Where(x=>x.IS_DELETE==false).ToList();


            return obj;


        }
        public OWNERTBL_Model GetPyID(int id)
        {
            OWNERTBL_Model obj = db.OWNERTBL_Model.Find(id);


            return obj;


        }

        public int GetMaxID()
        {
            int id = 0;
            try
            {
                id = db.OWNERTBL_Model.Max(x => x.OWNER_NO) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }

        public long GetMaxAcc()
        {
            long id = 0;
            try
            {
                id = db.OWNERTBL_Model.Max(x => x.ACC_NO).Value + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }



        public long Create(OWNERTBL_Model owner)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            var parentAcc = b.GetPyID(owner.PARENT_ACC.Value);
            owner.OWNER_NO = GetMaxID();
            owner.ACC_NO = Convert.ToInt64(parentAcc.ACC_NO.ToString() + owner.Acc_NOString);
          
           

           
            owner.ACC_LEVEL = parentAcc.ACC_LEVEL + 1;
            db.OWNERTBL_Model.Add(owner);

            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = owner.OWNER_NO;
            }
            return return_value;
        }
        public List<OWNERTBL_Model> getPayParent_acc(int parent_id)
        {



            List<OWNERTBL_Model> obj = db.OWNERTBL_Model.Where(x => x.PARENT_ACC == parent_id).ToList();


            return obj;


        }


        public long Update(OWNERTBL_Model owner)
        {





            
            //Holder.ACCH_BALANCEV_Model_Collection = null;
            db.Entry(owner).State = EntityState.Modified;
            db.Entry(owner).Property(x => x.ACC_LEVEL).IsModified = false;
            db.Entry(owner).Property(x => x.PARENT_ACC).IsModified = false;
           



            long return_value = db.SaveChanges();


            return return_value;
        }



        public int Delete(long id)
        {
            int return_value = 0;
            var obj = db.OWNERTBL_Model.Find(id);
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
