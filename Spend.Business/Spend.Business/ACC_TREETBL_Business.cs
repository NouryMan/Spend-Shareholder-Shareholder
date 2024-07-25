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
   public class ACC_TREETBL_Business
    {
        oracleDbContextSpend db;
        public ACC_TREETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACC_TREETBL_Model> getall()
        {
           

         
            List<ACC_TREETBL_Model> obj = db.ACC_TREETBL_Model.Where(x=>x.IS_DELETE==false).ToList();


            return obj;

          
        }
        public ACC_TREETBL_Model GetPyID(long id)
        {
            ACC_TREETBL_Model obj = db.ACC_TREETBL_Model.Find(id);


            return obj;

           
        }


        public List<ACC_TREETBL_Model> getPayParent_acc(int parent_id)
        {



            List<ACC_TREETBL_Model> obj = db.ACC_TREETBL_Model.Where(x=>x.ACC_PARENT== parent_id && x.IS_DELETE==false).ToList();


            return obj;


        }
      

        public long Create(ACC_TREETBL_Model Acc_Tree)
        {
            if (Acc_Tree.ACC_PARENT > 0)
            {
                var parentAcc = GetPyID(Acc_Tree.ACC_PARENT.Value);
                Acc_Tree.ACC_LEVEL = parentAcc.ACC_LEVEL + 1;
                Acc_Tree.ACC_NO = Convert.ToInt64(parentAcc.ACC_NO.ToString() + Acc_Tree.Acc_NOString);
            }
            else
            {

                Acc_Tree.ACC_LEVEL = 1;
                Acc_Tree.ACC_NO = Convert.ToInt64(Acc_Tree.Acc_NOString);
            }
          
            ALL_ACC_NOTBL_Business alL_ACC_NOTBL_Business = new ALL_ACC_NOTBL_Business();
            if (alL_ACC_NOTBL_Business.IsExist(Acc_Tree.ACC_NO))
            {
                return 10001001;
            }

            db.ACC_TREETBL_Model.Add(Acc_Tree);

            long return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = Acc_Tree.ACC_NO;
            }
            return return_value;
        }

        public long Update(ACC_TREETBL_Model Acc_Tree)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();





            Acc_Tree.UP_DATE =DateTime.Now;
            //Holder.ACCH_BALANCEV_Model_Collection = null;
            db.Entry(Acc_Tree).State = EntityState.Modified;
            db.Entry(Acc_Tree).Property(x => x.ACC_LEVEL).IsModified = false;
            db.Entry(Acc_Tree).Property(x => x.USER_CR).IsModified = false;
            db.Entry(Acc_Tree).Property(x => x.CR_DATE).IsModified = false;
            db.Entry(Acc_Tree).Property(x => x.ACC_PARENT).IsModified = false;

           


           long return_value = db.SaveChanges();


            return return_value;
        }

        public int Delete(long id)
        {
            int return_value = 0;
            var obj = db.ACC_TREETBL_Model.Find(id);
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

        public int UpdateTEMP()
        {
           int return_value = 1;
            foreach (var Acc_Tree in db.ACC_TREETBL_Model)
            {
                if (db.ACC_TREETBL_Model.Where(x => x.ACC_PARENT == Acc_Tree.ACC_NO).Count() < 1)
                {
                    Acc_Tree.OP_ACC = true;
                    db.Entry(Acc_Tree).State = EntityState.Modified;
                    db.Entry(Acc_Tree).Property(x => x.ACC_LEVEL).IsModified = false;
                    db.Entry(Acc_Tree).Property(x => x.USER_CR).IsModified = false;
                    db.Entry(Acc_Tree).Property(x => x.CR_DATE).IsModified = false;
                }
                else
                {
                    Acc_Tree.OP_ACC = false;
                    db.Entry(Acc_Tree).State = EntityState.Modified;
                    db.Entry(Acc_Tree).Property(x => x.ACC_LEVEL).IsModified = false;
                    db.Entry(Acc_Tree).Property(x => x.USER_CR).IsModified = false;
                    db.Entry(Acc_Tree).Property(x => x.CR_DATE).IsModified = false;
                }
               




                 return_value = db.SaveChanges();

            }





           


            return return_value;
        }



    }
}
