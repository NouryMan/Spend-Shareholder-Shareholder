using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class ACC_TREETBL1_Business
    {
        oracleDbContextSpend db;
        public ACC_TREETBL1_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACC_TREETBL1_Model> getall()
        {
           

         
            List<ACC_TREETBL1_Model> obj = db.ACC_TREETBL1_Model.ToList();


            return obj;

          
        }
        public ACC_TREETBL1_Model GetPyID(long id)
        {
            ACC_TREETBL1_Model obj = db.ACC_TREETBL1_Model.Find(id);


            return obj;

           
        }


        public List<ACC_TREETBL1_Model> getPayParent_acc(int parent_id)
        {



            List<ACC_TREETBL1_Model> obj = db.ACC_TREETBL1_Model.Where(x=>x.ACC_PARENT== parent_id).ToList();


            return obj;


        }
      

        public long Create(ACC_TREETBL1_Model Acc_Tree)
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
           
            Acc_Tree.CR_DATE = DateTime.Now;
          
          

            db.ACC_TREETBL1_Model.Add(Acc_Tree);

            long return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = Acc_Tree.ACC_NO;
            }
            return return_value;
        }

        public List<ACC_TREETBL1_Model> TrialBalance(long? ACC_NO, int? PROJECT_NO, int? TRANS, DateTime? FromDate, DateTime? ToDate)
        {
            List<ACC_TREETBL1_Model> list = new List<ACC_TREETBL1_Model>();

            if (ACC_NO != null)
            {
                var item = db.ACC_TREETBL1_Model.Find(ACC_NO);
                var plist = TrialBalancePyParent(item, PROJECT_NO, TRANS, FromDate, ToDate);

               
                var Acc = Search(item, PROJECT_NO, TRANS, FromDate, ToDate);
               
                try
                {
                    item.FROM_HIM = Acc.Sum(x => x.FROM_HIM).Value + plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FROM_HIM);
                    item.FOR_HIM = Acc.Sum(x => x.FOR_HIM).Value + plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FOR_HIM);
                }
                catch
                {
                    item.FROM_HIM = plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FROM_HIM);
                    item.FOR_HIM = plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FOR_HIM);
                }
                list.Add(item);
                list.AddRange(plist);
            }
            else
            {
                var ACC = db.ACC_TREETBL1_Model.Where(x => x.ACC_PARENT == null).ToList();
                foreach (var item in ACC)
                {
                    var plist = TrialBalancePyParent(item, PROJECT_NO, TRANS, FromDate, ToDate);
                  
                    var Acc = Search(item, PROJECT_NO, TRANS, FromDate, ToDate);
                   
                    try
                    {
                        item.FROM_HIM = Acc.Sum(x => x.FROM_HIM).Value + plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FROM_HIM);
                        item.FOR_HIM = Acc.Sum(x => x.FOR_HIM).Value + plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FOR_HIM);
                    }
                    catch
                    {
                        item.FROM_HIM = plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FROM_HIM);
                        item.FOR_HIM = plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FOR_HIM);
                    }
                    list.Add(item);
                    list.AddRange(plist);
                }

            }
            return list;
        }


        public List<ACC_TREETBL1_Model> TrialBalancePyParent(ACC_TREETBL1_Model parent, int? PROJECT_NO, int? TRANS, DateTime? FromDate, DateTime? ToDate)
        {
            List<ACC_TREETBL1_Model> list = new List<ACC_TREETBL1_Model>();

            
            var ACC = parent.ACC_TREETBL1_Collection.ToList();

            foreach (var item in ACC)
            {
                var plist = TrialBalancePyParent(item, PROJECT_NO, TRANS, FromDate, ToDate);


                var Acc = Search(item, PROJECT_NO, TRANS, FromDate, ToDate);
              
                try
                {
                    item.FROM_HIM = Acc.Sum(x => x.FROM_HIM).Value + plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FROM_HIM);
                    item.FOR_HIM = Acc.Sum(x => x.FOR_HIM).Value + plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FOR_HIM);
                }
                catch
                {
                    item.FROM_HIM = plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FROM_HIM);
                    item.FOR_HIM = plist.Where(x => x.ACC_PARENT == item.ACC_NO).Sum(x => x.FOR_HIM);
                }

                list.Add(item);
                list.AddRange(plist);
            }

            return list;

        }



        public List<ACCOUNTTBL_Model> Search(ACC_TREETBL1_Model ACC_tree, int? PROJECT_NO, int? TRANS, DateTime? FromDate, DateTime? ToDate)
        {

            if (ToDate == null)
            {
                ToDate = new DateTime();

            }
            if (FromDate == null)
            {
                FromDate = new DateTime();

            }

            ToDate = ToDate.Value.Date.AddDays(1).AddTicks(-1);


            List<ACCOUNTTBL_Model> model = new List<ACCOUNTTBL_Model>();

            foreach (var item in ACC_tree.ACC_TREETBL1_ACCOUNTTB_Collection)
            {

                model.AddRange( db.ACCOUNTTBL_Model.Where(x => x.ACC_NO == item.ACC_NO && x.ACC_DATE >= FromDate && x.ACC_DATE <= ToDate && x.CLOSED != true).OrderBy(x => x.ACC_DATE).ToList());
            }
            if (PROJECT_NO > 0)
            {

                model = model.Where(x => x.PROJECT_NO == PROJECT_NO).OrderBy(x => x.ACC_DATE).ToList();
            }


            if (TRANS == 0 || TRANS == 2)
            {
                List<ACCOUNTTBL_Model> NoTranse = new List<ACCOUNTTBL_Model>();
                INVOICETBL_Business b = new INVOICETBL_Business();

                List<INVOICETBL_Model> NoTranseFOR_HIM_INV=new List<INVOICETBL_Model>();
                List<INVOICETBL_Model> NoTranseFROM_HIM_INV = new List<INVOICETBL_Model>();
                foreach (var item in ACC_tree.ACC_TREETBL1_Collection)
                {
                     NoTranseFOR_HIM_INV.AddRange(db.INVOICETBL_Model.Where(x => x.ACC_NO == item.ACC_NO && x.DATE_M >= FromDate && x.DATE_M <= ToDate && (x.INV_DUC_TYPE == 8 || x.INV_DUC_TYPE == 9 || x.INV_DUC_TYPE == 33) && x.INV_TRANSED == 0 && x.PROJ_NO == PROJECT_NO).ToList());
                     NoTranseFROM_HIM_INV.AddRange(db.INVOICETBL_Model.Where(x => x.ACC_NO ==item.ACC_NO && x.DATE_M >= FromDate && x.DATE_M <= ToDate && (x.INV_DUC_TYPE == 10 || x.INV_DUC_TYPE == 11 || x.INV_DUC_TYPE == 34) && x.INV_TRANSED == 0 && x.PROJ_NO == PROJECT_NO).ToList());

                }
                if (NoTranseFOR_HIM_INV.Count > 0)
                {
                    var NoTranseFOR_HIM = NoTranseFOR_HIM_INV.Select(x => new ACCOUNTTBL_Model { UNDER_NO = x.UNDER_NO.Value, NOTE = x.NOTE, ACC_DATE = x.DATE_M, FOR_HIM = x.PUR_COST, FROM_HIM = 0 }).ToList();
                    NoTranse.AddRange(NoTranseFOR_HIM);
                }
                if (NoTranseFROM_HIM_INV.Count > 0)
                {
                    var NoTranseFROM_HIM = NoTranseFROM_HIM_INV.Select(x => new ACCOUNTTBL_Model { UNDER_NO = x.UNDER_NO.Value, NOTE = x.NOTE, ACC_DATE = x.DATE_M, FROM_HIM = x.PUR_COST, FOR_HIM = 0 }).ToList();
                    NoTranse.AddRange(NoTranseFROM_HIM);
                }





                if (TRANS == 0)
                {
                    model = NoTranse;


                }
                if (TRANS == 2)
                {

                    model.AddRange(NoTranse);
                }
            }


            return model.OrderBy(x => x.ACC_DATE).ToList(); ;
        }


    }
}
