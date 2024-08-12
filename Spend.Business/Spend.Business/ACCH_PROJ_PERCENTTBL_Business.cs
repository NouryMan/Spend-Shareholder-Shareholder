using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Spend.Business
{
    public class ACCH_PROJ_PERCENTTBL_Business
    {
        oracleDbContextSpend db;
        public ACCH_PROJ_PERCENTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACCH_PROJ_PERCENTTBL_Model> GetAll()
        {
            List<ACCH_PROJ_PERCENTTBL_Model> obj = db.ACCH_PROJ_PERCENTTBL_Model.ToList();




            return obj;
        }

        public ACCH_PROJ_PERCENTTBL_Model GetPyID(long id)
        {
            ACCH_PROJ_PERCENTTBL_Model obj = db.ACCH_PROJ_PERCENTTBL_Model.Find(id);


            return obj;
        }
        public List<ACCH_PROJ_PERCENTTBL_Model> GetAllPyProject(int id)
        {
            List<ACCH_PROJ_PERCENTTBL_Model> obj = db.ACCH_PROJ_PERCENTTBL_Model.Where(x => x.PROJ_NO == id).ToList();

            return obj;
        }


        public long Create(ACCH_PROJ_PERCENTTBL_Model item)
        {
            int return_value = 0;
            ACCH_BALANCE_SUMMARYV_Business b = new ACCH_BALANCE_SUMMARYV_Business();


            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    var ACCH_BALANCE = b.GetPyProjNo(item.PROJ_NO);

                    var totalspand = ACCH_BALANCE.Sum(x => x.SPENDING);

                    // اضافة حسابات الصيانة والاشغال واجور اخرى


                    //try
                    //{
                    //    db.ACCH_PROJ_PERCENTTBL_Model.Add(AddOtherAccH(2, item));
                    //    return_value = db.SaveChanges();
                    //}
                    //catch { }

                    //try
                    //{
                    //    db.ACCH_PROJ_PERCENTTBL_Model.Add(AddOtherAccH(3, item));
                    //    return_value = db.SaveChanges();
                    //}
                    //catch { }
                    //try
                    //{
                    //    db.ACCH_PROJ_PERCENTTBL_Model.Add(AddOtherAccH(4, item));
                    //    return_value = db.SaveChanges();
                    //}
                    //catch { }
                    //try
                    //{
                    //    db.ACCH_PROJ_PERCENTTBL_Model.Add(AddOtherAccH(5, item));
                    //    return_value = db.SaveChanges();
                    //}
                    //catch { }

                    ///////


                    foreach (var ACCH_BAL in ACCH_BALANCE.GroupBy(x => x.ACC_HOLDERTBL_Model.PARENT_ACCH))
                    {
                        ACCH_PROJ_PERCENTTBL_Model ACCH_PROJ_PERCENTTBL = new ACCH_PROJ_PERCENTTBL_Model();

                        ACCH_PROJ_PERCENTTBL.ACTIVE = true;
                        ACCH_PROJ_PERCENTTBL.ACC_HOLDER_NO = ACCH_BAL.FirstOrDefault().ACC_HOLDERTBL_Model.PARENT_ACCH.Value;
                        ACCH_PROJ_PERCENTTBL.ACCH_INCOME = item.ACCH_INCOME;
                        ACCH_PROJ_PERCENTTBL.ACCH_INCOMEPER = item.ACCH_INCOMEPER;
                        ACCH_PROJ_PERCENTTBL.ACCH_PERCENT = ACCH_BAL.Sum(y => y.SPENDING) / totalspand;
                        ACCH_PROJ_PERCENTTBL.ACCH_SPEND = ACCH_BAL.Sum(y => y.SPENDING);
                        ACCH_PROJ_PERCENTTBL.COMMIT_PERCENT = ACCH_BAL.Sum(y => y.SPENDING) / totalspand;
                        ACCH_PROJ_PERCENTTBL.NOTE = item.NOTE;
                        ACCH_PROJ_PERCENTTBL.PROJ_NO = item.PROJ_NO;

                        foreach (var ACCH in ACCH_BAL)
                        {
                            ACCH_PROJ_BOX_PERCENTTBL_Model BOX_PERCENTT = new ACCH_PROJ_BOX_PERCENTTBL_Model();

                            BOX_PERCENTT.ACTIVE = true;
                            BOX_PERCENTT.ACC_HOLDER_NO = ACCH.ACC_HOLDER_NO;
                            BOX_PERCENTT.PARENT_ACCH = ACCH_PROJ_PERCENTTBL.ACC_HOLDER_NO;
                            BOX_PERCENTT.CR_DATE = DateTime.Now;
                            BOX_PERCENTT.INCOME_BOX = item.BOX_NO.Value;
                            BOX_PERCENTT.BOX_INCOME = item.BOX_NO.Value;
                            BOX_PERCENTT.INCOME_PERCENT = ACCH.SPENDING / ACCH_PROJ_PERCENTTBL.ACCH_SPEND;
                            BOX_PERCENTT.NOTE = item.NOTE;
                            // BOX_PERCENTT.PARENT_BOX = ;
                            // BOX_PERCENTT.PARENT_PROJ = ;
                            BOX_PERCENTT.PROJ_NO = item.PROJ_NO;
                            // BOX_PERCENTT.SOURCE_PROJ = ;
                            BOX_PERCENTT.BOX_NO = item.BOX_NO.Value;
                            BOX_PERCENTT.SPEND_COST = ACCH.SPENDING;
                            BOX_PERCENTT.PARENT_PERCENT = ACCH_PROJ_PERCENTTBL.ACCH_PERCENT;
                            BOX_PERCENTT.SPEND_PERCENT = ACCH.SPENDING / ACCH_PROJ_PERCENTTBL.ACCH_SPEND;

                            ACCH_PROJ_PERCENTTBL.ACCH_PROJ_BOX_PERCENTTBL_Collection.Add(BOX_PERCENTT);

                        }

                        db.ACCH_PROJ_PERCENTTBL_Model.Add(ACCH_PROJ_PERCENTTBL);
                        return_value = db.SaveChanges();
                    }
                    if (return_value > 0)
                    {
                        scope.Complete();
                    }
                    return return_value;
                }
                catch
                {
                    return return_value = 0;
                }

            }
        }


        public ACCH_PROJ_PERCENTTBL_Model AddOtherAccH(int type, ACCH_PROJ_PERCENTTBL_Model item)
        {

            double persent = 0;

            switch (type)
            {
                case 2:
                    try { persent = item.MAINTE_PERCENT.Value; } catch { }
                    break;
                case 3:
                    try
                    {
                        persent = item.MARKETING_PERCENT.Value;
                    }
                    catch { }
                    break;
                case 4:
                    try
                    {
                        persent = item.WORK_PERCENT.Value;
                    }
                    catch { }
                    break;
                case 5:
                    try
                    {
                        persent = item.OTHER_COST.Value;
                    }
                    catch { }
                    break;

            }

            ACC_HOLDERTBL_Business acch_b = new ACC_HOLDERTBL_Business();

            var acch = acch_b.getall().Where(x => x.ACCH_TYPE == type);

            ACCH_PROJ_PERCENTTBL_Model ACCH_Maint = new ACCH_PROJ_PERCENTTBL_Model();

            ACCH_Maint.ACTIVE = true;
            ACCH_Maint.ACC_HOLDER_NO = acch.FirstOrDefault().ACC_HOLDER_NO;
            ACCH_Maint.ACCH_INCOME = item.ACCH_INCOME;
            ACCH_Maint.ACCH_INCOMEPER = item.ACCH_INCOMEPER;
            ACCH_Maint.ACCH_PERCENT = persent;
            ACCH_Maint.ACCH_SPEND = 0;
            ACCH_Maint.COMMIT_PERCENT = 0;
            ACCH_Maint.NOTE = item.NOTE;
            ACCH_Maint.PROJ_NO = item.PROJ_NO;

            ACCH_PROJ_BOX_PERCENTTBL_Model Maint_PERCENTT = new ACCH_PROJ_BOX_PERCENTTBL_Model();

            Maint_PERCENTT.ACTIVE = true;
            Maint_PERCENTT.ACC_HOLDER_NO = acch.FirstOrDefault().ACC_HOLDER_NO;
            Maint_PERCENTT.PARENT_ACCH = acch.FirstOrDefault().ACC_HOLDER_NO;
            Maint_PERCENTT.CR_DATE = DateTime.Now;
            Maint_PERCENTT.INCOME_BOX = item.BOX_NO.Value;
            Maint_PERCENTT.BOX_INCOME = item.BOX_NO.Value;
            Maint_PERCENTT.INCOME_PERCENT = persent;
            Maint_PERCENTT.NOTE = item.NOTE;
            // BOX_PERCENTT.PARENT_BOX = ;
            // BOX_PERCENTT.PARENT_PROJ = ;
            Maint_PERCENTT.PROJ_NO = item.PROJ_NO;
            // BOX_PERCENTT.SOURCE_PROJ = ;
            Maint_PERCENTT.BOX_NO = item.BOX_NO.Value;
            Maint_PERCENTT.SPEND_COST = 0;
            Maint_PERCENTT.SPEND_PERCENT = persent;

            ACCH_Maint.ACCH_PROJ_BOX_PERCENTTBL_Collection.Add(Maint_PERCENTT);


            return ACCH_Maint;




        }


    }
}
