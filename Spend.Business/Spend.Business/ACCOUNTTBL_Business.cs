using Spend.Api.Models;
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
    public class ACCOUNTTBL_Business
    {
        oracleDbContextSpend db;
        public ACCOUNTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACCOUNTTBL_Model> getall()
        {
            List<ACCOUNTTBL_Model> obj = db.ACCOUNTTBL_Model.ToList();


            return obj;
        }
        public ACCOUNTTBL_Model GetPyID(long id, long underNo)
        {
            ACCOUNTTBL_Model obj = db.ACCOUNTTBL_Model.Find(id, underNo);


            return obj;
        }


        public List<ACCOUNTTBL_Model> GetPyProjectNo(int id)
        {
            List<ACCOUNTTBL_Model> obj = db.ACCOUNTTBL_Model.Where(x => x.PROJECT_NO == id).ToList();


            return obj;
        }

        public List<ACCOUNTTBL_Model> GetPyProjectNoAndAccountNO(int ProjNO, decimal Account_NO)
        {
            List<ACCOUNTTBL_Model> obj = db.ACCOUNTTBL_Model.Where(x => x.PROJECT_NO == ProjNO && x.ACC_NO == Account_NO).ToList();


            return obj;
        }

        public List<ACCOUNTTBL_Model> GetPuUnderNO(long UNDER_NO)
        {
            List<ACCOUNTTBL_Model> obj = db.ACCOUNTTBL_Model.Where(x => x.UNDER_NO == UNDER_NO).ToList();


            return obj;
        }

        public List<ACCOUNTTBL_Model> Search(long? ACC_NO, int? PROJECT_NO, int? TRANS, DateTime? FromDate, DateTime? ToDate)
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
            if (ACC_NO > 0)
            {

                model = db.ACCOUNTTBL_Model.Where(x => x.ACC_NO == ACC_NO && x.ACC_DATE >= FromDate && x.ACC_DATE <= ToDate && x.CLOSED != true).OrderBy(x => x.ACC_DATE).ToList();
            }
            if (PROJECT_NO > 0)
            {

                model = model.Where(x => x.PROJECT_NO == PROJECT_NO).OrderBy(x => x.ACC_DATE).ToList();
            }


            if (TRANS == 0 || TRANS == 2)
            {
                List<ACCOUNTTBL_Model> NoTranse = new List<ACCOUNTTBL_Model>();
                INVOICETBL_Business b = new INVOICETBL_Business();
                var NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.ACC_NO == ACC_NO && x.DATE_M >= FromDate && x.DATE_M <= ToDate && (x.INV_DUC_TYPE == 8 || x.INV_DUC_TYPE == 9 || x.INV_DUC_TYPE == 33) && x.INV_TRANSED == 0).ToList();
                var NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.ACC_NO == ACC_NO && x.DATE_M >= FromDate && x.DATE_M <= ToDate && (x.INV_DUC_TYPE == 10 || x.INV_DUC_TYPE == 11 || x.INV_DUC_TYPE == 34) && x.INV_TRANSED == 0).ToList();

                if (PROJECT_NO > 0)
                {
                    NoTranseFOR_HIM_INV = NoTranseFOR_HIM_INV.Where(x => x.PROJ_NO == PROJECT_NO).ToList();
                    NoTranseFROM_HIM_INV = NoTranseFROM_HIM_INV.Where(x => x.PROJ_NO == PROJECT_NO).ToList();

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
        public List<ACCOUNTTBL_Model> SearchByParent(long? ACC_NO, int? PROJECT_NO, int? TRANS, DateTime? FromDate, DateTime? ToDate)
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
            if (ACC_NO > 0)
            {

                model = db.ACCOUNTTBL_Model.Where(x => x.ACC_PARENT == ACC_NO && x.ACC_DATE >= FromDate && x.ACC_DATE <= ToDate && x.CLOSED != true).OrderBy(x => x.ACC_DATE).ToList();
            }
            if (PROJECT_NO > 0)
            {

                model = model.Where(x => x.PROJECT_NO == PROJECT_NO).OrderBy(x => x.ACC_DATE).ToList();
            }


            if (TRANS == 0 || TRANS == 2)
            {
                List<ACCOUNTTBL_Model> NoTranse = new List<ACCOUNTTBL_Model>();
                INVOICETBL_Business b = new INVOICETBL_Business();
                var NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.ACC_PARENT == ACC_NO && x.DATE_M >= FromDate && x.DATE_M <= ToDate && (x.INV_DUC_TYPE == 8 || x.INV_DUC_TYPE == 9 || x.INV_DUC_TYPE == 33) && x.INV_TRANSED == 0).ToList();
                var NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.ACC_PARENT == ACC_NO && x.DATE_M >= FromDate && x.DATE_M <= ToDate && (x.INV_DUC_TYPE == 10 || x.INV_DUC_TYPE == 11 || x.INV_DUC_TYPE == 34) && x.INV_TRANSED == 0).ToList();

                if (PROJECT_NO > 0)
                {
                    NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();
                    NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();

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

        public List<ACCOUNTTBL_Model> OpenBalnce(long ACC_NO, int? PROJECT_NO, int? TRANS, DateTime ToDate)
        {







            List<ACCOUNTTBL_Model> model = new List<ACCOUNTTBL_Model>();
            if (ACC_NO > 0)
            {

                model = db.ACCOUNTTBL_Model.Where(x => x.ACC_NO == ACC_NO && x.ACC_DATE < ToDate && x.CLOSED != true).OrderByDescending(x => x.ACC_DATE).ToList();


                if (PROJECT_NO > 0)
                {

                    model = model.Where(x => x.PROJECT_NO == PROJECT_NO).OrderBy(x => x.ACC_DATE).ToList();
                }


                if (TRANS == 0 || TRANS == 2)
                {
                    List<ACCOUNTTBL_Model> NoTranse = new List<ACCOUNTTBL_Model>();
                    INVOICETBL_Business b = new INVOICETBL_Business();


                    var NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.ACC_NO == ACC_NO && x.DATE_M < ToDate && (x.INV_DUC_TYPE == 8 || x.INV_DUC_TYPE == 9 || x.INV_DUC_TYPE == 33) && x.INV_TRANSED == 0).ToList();
                    var NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.ACC_NO == ACC_NO && x.DATE_M < ToDate && (x.INV_DUC_TYPE == 10 || x.INV_DUC_TYPE == 11 || x.INV_DUC_TYPE == 34) && x.INV_TRANSED == 0).ToList();
                    if (PROJECT_NO > 0)
                    {
                        NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();
                        NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();

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

            }
            return model;
        }

        public List<ACCOUNTTBL_Model> OpenBalnceByParent(long ACC_NO, int? PROJECT_NO, int? TRANS, DateTime ToDate)
        {







            List<ACCOUNTTBL_Model> model = new List<ACCOUNTTBL_Model>();
            if (ACC_NO > 0)
            {

                model = db.ACCOUNTTBL_Model.Where(x => x.ACC_PARENT == ACC_NO && x.ACC_DATE < ToDate && x.CLOSED != true).OrderByDescending(x => x.ACC_DATE).ToList();


                if (PROJECT_NO > 0)
                {

                    model = model.Where(x => x.PROJECT_NO == PROJECT_NO).OrderBy(x => x.ACC_DATE).ToList();
                }


                if (TRANS == 0 || TRANS == 2)
                {
                    List<ACCOUNTTBL_Model> NoTranse = new List<ACCOUNTTBL_Model>();
                    INVOICETBL_Business b = new INVOICETBL_Business();


                    var NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.ACC_PARENT == ACC_NO && x.DATE_M < ToDate && (x.INV_DUC_TYPE == 8 || x.INV_DUC_TYPE == 9 || x.INV_DUC_TYPE == 33) && x.INV_TRANSED == 0).ToList();
                    var NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.ACC_PARENT == ACC_NO && x.DATE_M < ToDate && (x.INV_DUC_TYPE == 10 || x.INV_DUC_TYPE == 11 || x.INV_DUC_TYPE == 34) && x.INV_TRANSED == 0).ToList();

                    if (PROJECT_NO > 0)
                    {
                        NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();
                        NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();

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

            }
            return model;
        }

        public List<ALL_ACC_NOTBL_Model> TrialBalance(long? ACC_NO, int? PROJECT_NO, int? TRANS, DateTime? FromDate, DateTime? ToDate)
        {
            List<ALL_ACC_NOTBL_Model> list = new List<ALL_ACC_NOTBL_Model>();

            if (ACC_NO != null)
            {
                var item = db.ALL_ACC_NOTBL_Model.Find(ACC_NO);
                var plist = TrialBalancePyParent(item, PROJECT_NO, TRANS, FromDate, ToDate);
                var Acc = Search(item.ACC_NO, PROJECT_NO, TRANS, FromDate, ToDate);
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
                var ACC = db.ALL_ACC_NOTBL_Model.Where(x => x.ACC_PARENT == null).ToList();
                foreach (var item in ACC)
                {
                    var plist = TrialBalancePyParent(item, PROJECT_NO, TRANS, FromDate, ToDate);
                    var Acc = Search(item.ACC_NO, PROJECT_NO, TRANS, FromDate, ToDate);
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


        public List<ALL_ACC_NOTBL_Model> TrialBalancePyParent(ALL_ACC_NOTBL_Model parent, int? PROJECT_NO, int? TRANS, DateTime? FromDate, DateTime? ToDate)
        {
            List<ALL_ACC_NOTBL_Model> list = new List<ALL_ACC_NOTBL_Model>();

            var ACC = parent.ALL_ACC_NOTBL_Model_Collection.ToList();

            foreach (var item in ACC)
            {
                var plist = TrialBalancePyParent(item, PROJECT_NO, TRANS, FromDate, ToDate);

                var Acc = Search(item.ACC_NO, PROJECT_NO, TRANS, FromDate, ToDate);
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

        public List<ACCOUNTTBL_Model> SearchWhitParent(long? ACC_NO, int? PROJECT_NO, int? TRANS, DateTime? FromDate, DateTime? ToDate)
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
            decimal vat = 0;
            try { vat = db.PERSON_ACCTBL_Model.Where(x => x.ACC_NO == ACC_NO).FirstOrDefault().PERSONAL_INFOTBL_Model.VAT_NO.Value; } catch { }

            List<ACCOUNTTBL_Model> model = new List<ACCOUNTTBL_Model>();
            if (ACC_NO > 0)
            {


                model = db.ACCOUNTTBL_Model.Where(x => x.ALL_ACC_NOTBL_Model.PERSONAL_INFOTBL_Model.VAT_NO == vat && x.ACC_DATE >= FromDate && x.ACC_DATE <= ToDate && x.CLOSED != true).OrderBy(x => x.ACC_DATE).ToList();
            }
            if (PROJECT_NO > 0)
            {

                model = model.Where(x => x.PROJECT_NO == PROJECT_NO).OrderBy(x => x.ACC_DATE).ToList();
            }


            if (TRANS == 0 || TRANS == 2)
            {
                List<ACCOUNTTBL_Model> NoTranse = new List<ACCOUNTTBL_Model>();
                INVOICETBL_Business b = new INVOICETBL_Business();
                var NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.ALL_ACC_CREDITOR_Model.VAT_NO != null && x.ALL_ACC_CREDITOR_Model.PERSONAL_INFOTBL_Model.VAT_NO == vat && x.DATE_M >= FromDate && x.DATE_M <= ToDate && (x.INV_DUC_TYPE == 8 || x.INV_DUC_TYPE == 9 || x.INV_DUC_TYPE == 33) && x.INV_TRANSED == 0).ToList();
                var NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.ALL_ACC_CREDITOR_Model.VAT_NO != null && x.ALL_ACC_CREDITOR_Model.PERSONAL_INFOTBL_Model.VAT_NO == vat && x.DATE_M >= FromDate && x.DATE_M <= ToDate && (x.INV_DUC_TYPE == 10 || x.INV_DUC_TYPE == 11 || x.INV_DUC_TYPE == 34) && x.INV_TRANSED == 0).ToList();
                if (PROJECT_NO > 0)
                {
                    NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();
                    NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();

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

        public List<ACCOUNTTBL_Model> OpenBalnceWhitParent(long ACC_NO, int? PROJECT_NO, int? TRANS, DateTime ToDate)
        {

            List<ACCOUNTTBL_Model> model = new List<ACCOUNTTBL_Model>();
            decimal vat = 0;
            try { vat = db.PERSON_ACCTBL_Model.Where(x => x.ACC_NO == ACC_NO).FirstOrDefault().PERSONAL_INFOTBL_Model.VAT_NO.Value; } catch { }

            if (ACC_NO > 0)
            {

                model = db.ACCOUNTTBL_Model.Where(x => x.ALL_ACC_NOTBL_Model.PERSONAL_INFOTBL_Model.VAT_NO == vat && x.ACC_DATE < ToDate && x.CLOSED != true).OrderByDescending(x => x.ACC_DATE).ToList();


                if (PROJECT_NO > 0)
                {

                    model = model.Where(x => x.PROJECT_NO == PROJECT_NO).OrderBy(x => x.ACC_DATE).ToList();
                }


                if (TRANS == 0 || TRANS == 2)
                {
                    List<ACCOUNTTBL_Model> NoTranse = new List<ACCOUNTTBL_Model>();
                    INVOICETBL_Business b = new INVOICETBL_Business();


                    var NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.ALL_ACC_CREDITOR_Model.PERSONAL_INFOTBL_Model.VAT_NO == vat && x.DATE_M < ToDate && (x.INV_DUC_TYPE == 8 || x.INV_DUC_TYPE == 9 || x.INV_DUC_TYPE == 33) && x.INV_TRANSED == 0).ToList();
                    var NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.ALL_ACC_CREDITOR_Model.PERSONAL_INFOTBL_Model.VAT_NO == vat && x.DATE_M < ToDate && (x.INV_DUC_TYPE == 10 || x.INV_DUC_TYPE == 11 || x.INV_DUC_TYPE == 34) && x.INV_TRANSED == 0).ToList();
                    if (PROJECT_NO > 0)
                    {
                        NoTranseFOR_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();
                        NoTranseFROM_HIM_INV = db.INVOICETBL_Model.Where(x => x.PROJ_NO == PROJECT_NO).ToList();

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

            }
            return model;
        }

        public int GetMaxOpNo()
        {
            int maxOpNO = 0;
            try { maxOpNO = db.ACCOUNTTBL_Model.Max(x => x.OP_NO).Value + 1; } catch { maxOpNO = 1; }
            return maxOpNO;
        }

        public int Create(ACCOUNTTBL_Model acc)
        {
            int reusert = 0;




            acc.OP_NO = GetMaxOpNo();

            db.ACCOUNTTBL_Model.Add(acc);
            reusert = db.SaveChanges();


            return reusert;
        }



    }
}
