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
    public class ACC_HOLDERTBL_Business
    {
        oracleDbContextSpend db;
        public ACC_HOLDERTBL_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<ACC_HOLDERTBL_Model> getall()
        {
            List<ACC_HOLDERTBL_Model> obj = db.ACC_HOLDERTBL_Model.Where(x=>x.IS_DELETE==false).ToList();

            foreach (var model in obj)
            {
                try {
                    ACCH_BALANCEV_Business b = new ACCH_BALANCEV_Business();
                    var item =b.GetPyID(model.ACC_HOLDER_NO);
                    model.BALANCE = item.BALANCE.Value; } catch { model.BALANCE = 0; }
            }


            return obj;
        }
        public ACC_HOLDERTBL_Model GetById(long id)
        {
            ACC_HOLDERTBL_Model obj = db.ACC_HOLDERTBL_Model.FirstOrDefault(x=>x.ACC_HOLDER_NO==id);


            return obj;
        }

        public List<ACC_HOLDERTBL_Model> getPayParent_acc(int parent_id)
        {



            List<ACC_HOLDERTBL_Model> obj = db.ACC_HOLDERTBL_Model.Where(x => x.ACC_PARENT == parent_id && x.IS_DELETE==false).ToList();


            return obj;


        }

        public long GetMaxAcc()
        {
            long id = 0;
            try
            {
                id = db.ACC_HOLDERTBL_Model.Max(x => x.ACC_NO).Value + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }


        public long GetMaxID()
        {
            long id = 0;
            try
            {
                id = db.ACC_HOLDERTBL_Model.Max(x => x.ACC_HOLDER_NO) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }
        public long Create(ACC_HOLDERTBL_Model Holder)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();

            var parentAcc = b.GetPyID(Holder.ACC_PARENT.Value);
            Holder.ACC_HOLDER_NO = GetMaxID();
            Holder.IS_DELETE = false;


            Holder.ACC_NO = Convert.ToInt64(parentAcc.ACC_NO.ToString() + Holder.Acc_NOString);




            Holder.ACC_LEVEL = parentAcc.ACC_LEVEL + 1;
            Holder.ACCH_BALANCEV_Model_Collection = null;
            db.ACC_HOLDERTBL_Model.Add(Holder);

            long return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = Holder.ACC_HOLDER_NO;
            }
            return return_value;
        }

        public long Update(ACC_HOLDERTBL_Model Holder)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();

            var parentAcc = b.GetPyID(Holder.ACC_PARENT.Value);
           

            Holder.ACC_NO = Convert.ToInt64(parentAcc.ACC_NO.ToString() + Holder.Acc_NOString);




            Holder.ACC_LEVEL = parentAcc.ACC_LEVEL + 1;
            Holder.ACCH_BALANCEV_Model_Collection=null; 

            db.Entry(Holder).State = EntityState.Modified;
            db.Entry(Holder).Property(x => x.IS_DELETE).IsModified = false;


            long return_value = db.SaveChanges();

          
            return return_value;
        }

        public void Delete(long id)
        {
            try
            {
                var obj = GetById(id);
                if (obj == null)
                {
                    throw new Exception("Entity not found.");
                }
               obj.ACCH_BALANCEV_Model_Collection = null;   

                obj.IS_DELETE = true;
               
                db.SaveChanges();

                return;
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
