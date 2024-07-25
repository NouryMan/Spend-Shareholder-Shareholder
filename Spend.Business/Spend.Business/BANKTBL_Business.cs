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
  public  class BANKTBL_Business
    {
        oracleDbContextSpend db;
        public BANKTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<BANKTBL_Model> getall()
        {



            List<BANKTBL_Model> obj = db.BANKTBL_Model.Where(x=>x.IS_DELETE==false).ToList();


            return obj;


        }
        public BANKTBL_Model GetPyID(int id)
        {
            BANKTBL_Model obj = db.BANKTBL_Model.Find(id);


            return obj;


        }

        public int GetMaxID()
        {
            int id = 0;
            try
            {
                id = db.BANKTBL_Model.Max(x => x.BANK_NO) + 1;
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
                id = db.BANKTBL_Model.Max(x => x.ACC_NO).Value + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }



        public long Create(BANKTBL_Model owner)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            var parentAcc = b.GetPyID(owner.ACC_PARENT.Value);
            owner.BANK_NO = GetMaxID();
            owner.ACC_NO = Convert.ToInt64(parentAcc.ACC_NO.ToString() + owner.Acc_NOString);




            owner.ACC_LEVEL = parentAcc.ACC_LEVEL + 1;
            ALL_ACC_NOTBL_Business alL_ACC_NOTBL_Business = new ALL_ACC_NOTBL_Business();
            if (alL_ACC_NOTBL_Business.IsExist(owner.ACC_NO.Value))
            {
                return 10001001;
            }

            db.BANKTBL_Model.Add(owner);

            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = owner.BANK_NO;
            }
            return return_value;
        }

        public long Update(BANKTBL_Model bank)
        {
           




            bank.UP_DATE = DateTime.Now;
            //Holder.ACCH_BALANCEV_Model_Collection = null;
            db.Entry(bank).State = EntityState.Modified;
            db.Entry(bank).Property(x => x.ACC_LEVEL).IsModified = false;
            db.Entry(bank).Property(x => x.USER_CR).IsModified = false;
            db.Entry(bank).Property(x => x.CR_DATE).IsModified = false;




            long return_value = db.SaveChanges();


            return return_value;
        }

        public List<BANKTBL_Model> getPayParent_acc(int parent_id)
        {



            List<BANKTBL_Model> obj = db.BANKTBL_Model.Where(x => x.ACC_PARENT == parent_id ).ToList();


            return obj;


        }

        public int Delete(long id)
        {
            int return_value = 0;
            var obj = db.BANKTBL_Model.Find(id);
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
