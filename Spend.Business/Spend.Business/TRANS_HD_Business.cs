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
    public class TRANS_HD_Business
    {
        oracleDbContextSpend db;
        public TRANS_HD_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<TRANS_HD_Model> GetAll()
        {
            List<TRANS_HD_Model> obj = db.TRANS_HD_Model.ToList();


            return obj;
        }
        public TRANS_HD_Model GetPyID(long id)
        {
            TRANS_HD_Model obj = db.TRANS_HD_Model.Find(id);


            return obj;
        }
        public long GetMaxTransNo()
        {
            long maxTransNo = 0;
            try { maxTransNo = db.TRANS_HD_Model.Max(x => x.TRANS_NO).Value + 1; } catch { maxTransNo = 1; }
            return maxTransNo;
        }
        public long GetMaxId()
        {
            long maxTransNo = 0;
            try { maxTransNo = db.TRANS_HD_Model.Max(x => x.ID) + 1; } catch { maxTransNo = 1; }
            return maxTransNo;
        }

        public int Create(TRANS_HD_Model tRANS_HD)
        {
            int reusert = 0;


            tRANS_HD.TRANS_NO = GetMaxTransNo();

            tRANS_HD.ID = GetMaxId();
            tRANS_HD.POSTED = "Y";
            tRANS_HD.IS_ENABLED = true;
            tRANS_HD.FIN_YEAR =Convert.ToInt16(tRANS_HD.JOURNAL_MDATE.Value.ToString("yyyy"));
            tRANS_HD.CONVERSION_FACTOR =1;
            tRANS_HD.DATE_CR = DateTime.Now;
            ACCOUNTTBL_Business aCCOUNTTBL  = new ACCOUNTTBL_Business();
            int op = aCCOUNTTBL.GetMaxOpNo();
            foreach (var acc in tRANS_HD.ACCOUNTTBL_Collection)
            {
                acc.UNDER_NO=tRANS_HD.JOURNAL_NO;
                acc.TRANS_ID=tRANS_HD.ID;
                acc.OP_NO= op++;
            }
           
            db.TRANS_HD_Model.Add(tRANS_HD);
            reusert = db.SaveChanges();


            return reusert;
        }


        public long SallInvoTranse(TRANS_HD_Model tRANS_HD)
        {
            long result = 0;

            using (TransactionScope trns = new TransactionScope())
            {

                SALES_INVTBL_Business sALES_INV_Business = new SALES_INVTBL_Business();
                ALL_ACC_NOTBL_Business allACC_NOTBL_Business = new ALL_ACC_NOTBL_Business();

                var saleInvo = sALES_INV_Business.GetPyID(tRANS_HD.DOC_NO.Value);

                tRANS_HD.JOURNAL_MDATE = saleInvo.DATE_M;
                try { tRANS_HD.JOURNAL_HDATE = saleInvo.DATE_H.Value.ToString("dd-MM-yyyy"); } catch { }
                tRANS_HD.TRANS_NO = saleInvo.INV_NO;
                tRANS_HD.PROJECT_NO = saleInvo.PROJ_NO;
                tRANS_HD.DOC_TYPE = saleInvo.INV_DOC_TYPE;
                tRANS_HD.JOURNAL_NO = saleInvo.UNDER_NO.Value;
                tRANS_HD.COST_CENTER_ID = saleInvo.PROJ_NO;
                tRANS_HD.CREATE_DATE = DateTime.Now;
                try { tRANS_HD.NOTE = "قيد ترحيل فاتورة مبيعات رقم :" + saleInvo.INV_NO + "مشروع" + saleInvo.PROJECTTBL_Model.PROJECT_NAME + " " + saleInvo.PARTTBL_Model.PART_NAME + " " + saleInvo.UNITTBL_Model.UNIT_NAME; } catch { tRANS_HD.NOTE = "قيد ترحيل فاتورة مبيعات رقم :" + saleInvo.INV_NO; }




                foreach (var account in tRANS_HD.ACCOUNTTBL_Collection)
                {
                    var acc = allACC_NOTBL_Business.GetPyID(account.ACC_NO);

                    account.PROJECT_NO = saleInvo.PROJ_NO;
                    account.PART_NO = saleInvo.PART_PROJ;
                    account.UNIT_NO = saleInvo.UNIT_NO;
                    account.ACC_DATE = saleInvo.DATE_M;
                    account.ACC_PARENT = acc.ACC_PARENT;
                    account.DOC_TYPE = tRANS_HD.DOC_TYPE;
                    account.GROUP_NO = 8;
                    //account.CR_DATE = DateTime.Now;

                    try { account.USER_CR = Convert.ToInt32(tRANS_HD.USER_CR); } catch { }

                    account.CLOSED = false;
                  

                    account.NOTE = tRANS_HD.NOTE;

                }

                result = Create(tRANS_HD);

                if (result > 0)
                {

                    result = sALES_INV_Business.UpdateTrans(saleInvo);
                    if (result > 0)
                    {
                        trns.Complete();

                    }
                }
            }
           
            
            return result;
        
        
        }





        public int AdjustingEntry(TRANS_HD_Model tRANS_HD_Model)
        {
            int reuslt = 0;

            using (TransactionScope scope = new TransactionScope())
            {
                ACCOUNTTBL_Business aCCOUNTTBL_Business = new ACCOUNTTBL_Business();
                MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();
                ALL_ACC_NOTBL_Business aLL_ACC_NOTBL_Business = new ALL_ACC_NOTBL_Business();
                var underNo = Under_NO_b.getall().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;


                var opNo = aCCOUNTTBL_Business.GetMaxOpNo();

                // var transId = GetMaxTransNo();



                if (tRANS_HD_Model.ALL_ON_ON == false)
                {

                    foreach (var item in tRANS_HD_Model.ACCOUNTTBL_Collection)
                    {
                        ACCOUNTTBL_Business aCCOUNTTBL_Business1 = new ACCOUNTTBL_Business();
                        TRANS_HD_Model tRANS_HD = new TRANS_HD_Model();
                        
                        var oldAcc = aCCOUNTTBL_Business1.GetPyID(item.ACC_NO, item.UNDER_NO);
                       
                        var toAcc = aLL_ACC_NOTBL_Business.GetPyID(tRANS_HD_Model.TO_ACC.Value);

                        tRANS_HD.JOURNAL_MDATE = oldAcc.ACC_DATE;

                        tRANS_HD.TRANS_NO = oldAcc.UNDER_NO;
                        tRANS_HD.PROJECT_NO = oldAcc.PROJECT_NO;
                        tRANS_HD.DOC_TYPE = oldAcc.DOC_TYPE;
                        tRANS_HD.JOURNAL_NO = underNo;
                        tRANS_HD.NOTE = tRANS_HD_Model.NOTE;

                        ACCOUNTTBL_Model acc = new ACCOUNTTBL_Model();
                        ACCOUNTTBL_Model acc2 = new ACCOUNTTBL_Model();


                        acc.ACC_DATE = DateTime.Now;
                        acc.ACC_NO = item.ACC_NO;
                        acc.ACC_PARENT = oldAcc.ACC_PARENT;
                        acc.CLOSED = false;
                        acc.CUST_NO = oldAcc.CUST_NO;
                        acc.DOC_TYPE = oldAcc.DOC_TYPE;
                        acc.FOR_HIM = oldAcc.FROM_HIM;
                        acc.FROM_HIM = oldAcc.FOR_HIM;
                        acc.GROUP_NO = oldAcc.GROUP_NO;
                        acc.NOTE = tRANS_HD.NOTE;
                       // acc.OP_NO = opNo++;
                        acc.PART_NO = oldAcc.PART_NO;
                        acc.PROJECT_NO = oldAcc.PROJECT_NO;
                        acc.SCRIPT_NO = oldAcc.SCRIPT_NO;
                        acc.SUB_PROJ = oldAcc.SUB_PROJ;
                        // acc.TRANS_ID = transId++;
                        //acc.UNDER_NO = tRANS_HD.JOURNAL_NO;
                        acc.UNIT_NO = oldAcc.UNIT_NO;
                        acc.USER_CR = Convert.ToInt16(tRANS_HD_Model.USER_CR);


                        tRANS_HD.ACCOUNTTBL_Collection.Add(acc);


                        acc2.ACC_DATE = DateTime.Now;
                        acc2.ACC_NO = toAcc.ACC_NO;
                        acc2.ACC_PARENT = toAcc.ACC_PARENT;
                        acc2.CLOSED = false;
                        acc2.CUST_NO = oldAcc.CUST_NO;
                        acc2.DOC_TYPE = oldAcc.DOC_TYPE;
                        acc2.FOR_HIM = oldAcc.FOR_HIM;
                        acc2.FROM_HIM = oldAcc.FROM_HIM;
                        acc2.GROUP_NO = oldAcc.GROUP_NO;
                        acc2.NOTE = tRANS_HD.NOTE;
                       // acc2.OP_NO = opNo++;
                        acc2.PART_NO = oldAcc.PART_NO;
                        acc2.PROJECT_NO = oldAcc.PROJECT_NO;
                        acc2.SCRIPT_NO = oldAcc.SCRIPT_NO;
                        acc2.SUB_PROJ = oldAcc.SUB_PROJ;
                        //  acc2.TRANS_ID = transId++;
                      //  acc.UNDER_NO = tRANS_HD.JOURNAL_NO;
                        acc2.UNIT_NO = oldAcc.UNIT_NO;
                        acc2.USER_CR = Convert.ToInt16(tRANS_HD_Model.USER_CR);
                        tRANS_HD.ACCOUNTTBL_Collection.Add(acc2);
                        reuslt = Create(tRANS_HD);
                        if (reuslt > 0) { underNo++; } else { break; }

                    }
                }



                else if (tRANS_HD_Model.ALL_ON_ON == true)
                {
                    List<ACCOUNTTBL_Model> aCCOUNTTBL_Models = new List<ACCOUNTTBL_Model>();

                    foreach (var item in tRANS_HD_Model.ACCOUNTTBL_Collection)
                    {
                        aCCOUNTTBL_Models.Add(aCCOUNTTBL_Business.GetPyID(item.ACC_NO, item.UNDER_NO));
                    }


                    var fromHim = aCCOUNTTBL_Models.Sum(x => x.FROM_HIM);
                    var forHim = aCCOUNTTBL_Models.Sum(x => x.FOR_HIM);


                    if (fromHim > forHim)
                    {
                        fromHim = fromHim - forHim;
                        forHim = 0;
                    }
                    else if (fromHim < forHim)
                    {
                        forHim = forHim - fromHim;
                        fromHim = 0;
                    }
                    else
                    {
                        return 1000010; //رمز خطاء الدئن متساوي مع المدين 
                    }





                    ACCOUNTTBL_Model acc = new ACCOUNTTBL_Model();
                    ACCOUNTTBL_Model acc2 = new ACCOUNTTBL_Model();

                    var oldAcc = aCCOUNTTBL_Business.GetPyID(tRANS_HD_Model.ACCOUNTTBL_Collection.FirstOrDefault().ACC_NO, tRANS_HD_Model.ACCOUNTTBL_Collection.FirstOrDefault().UNDER_NO);
                    var toAcc = aLL_ACC_NOTBL_Business.GetPyID(tRANS_HD_Model.TO_ACC.Value);

                    TRANS_HD_Model tRANS_HD = new TRANS_HD_Model();

                    tRANS_HD.JOURNAL_MDATE = oldAcc.ACC_DATE;
                    tRANS_HD.TRANS_NO = oldAcc.UNDER_NO;
                    tRANS_HD.PROJECT_NO = oldAcc.PROJECT_NO;
                    tRANS_HD.DOC_TYPE = oldAcc.DOC_TYPE;
                    tRANS_HD.JOURNAL_NO = underNo;
                    tRANS_HD.NOTE = tRANS_HD_Model.NOTE;



                    acc.ACC_DATE = DateTime.Now;
                    acc.ACC_NO = oldAcc.ACC_NO;
                    acc.ACC_PARENT = oldAcc.ACC_PARENT;
                    acc.CLOSED = false;
                    acc.CUST_NO = oldAcc.CUST_NO;
                    acc.DOC_TYPE = oldAcc.DOC_TYPE;
                    acc.FOR_HIM = fromHim;
                    acc.FROM_HIM = forHim;
                    acc.GROUP_NO = oldAcc.GROUP_NO;
                    acc.NOTE = tRANS_HD.NOTE;
                   // acc.OP_NO = opNo++;
                    acc.PART_NO = oldAcc.PART_NO;
                    acc.PROJECT_NO = oldAcc.PROJECT_NO;
                    acc.SCRIPT_NO = oldAcc.SCRIPT_NO;
                    acc.SUB_PROJ = oldAcc.SUB_PROJ;
                    // acc.TRANS_ID = transId++;
                   // acc.UNDER_NO = tRANS_HD.JOURNAL_NO;
                    acc.UNIT_NO = oldAcc.UNIT_NO;
                    acc.USER_CR = Convert.ToInt16(tRANS_HD_Model.USER_CR);

                    tRANS_HD.ACCOUNTTBL_Collection.Add(acc);

                    acc2.ACC_DATE = DateTime.Now;
                    acc2.ACC_NO = toAcc.ACC_NO;
                    acc2.ACC_PARENT = toAcc.ACC_PARENT;
                    acc2.CLOSED = false;
                    acc2.CUST_NO = oldAcc.CUST_NO;
                    acc2.DOC_TYPE = oldAcc.DOC_TYPE;
                    acc2.FOR_HIM = forHim;
                    acc2.FROM_HIM = fromHim;
                    acc2.GROUP_NO = oldAcc.GROUP_NO;
                    acc2.NOTE = tRANS_HD_Model.NOTE;
                   // acc2.OP_NO = opNo;
                    acc2.PART_NO = oldAcc.PART_NO;
                    acc2.PROJECT_NO = oldAcc.PROJECT_NO;
                    acc2.SCRIPT_NO = oldAcc.SCRIPT_NO;
                    acc2.SUB_PROJ = oldAcc.SUB_PROJ;
                    //  acc2.TRANS_ID = transId++;
                    //acc2.UNDER_NO = tRANS_HD.JOURNAL_NO;
                    acc2.UNIT_NO = oldAcc.UNIT_NO;
                    acc2.USER_CR = Convert.ToInt16(tRANS_HD_Model.USER_CR);

                    tRANS_HD.ACCOUNTTBL_Collection.Add(acc2);


                    reuslt = Create(tRANS_HD);

                }


                if (reuslt > 0)
                {
                    scope.Complete();
                }




                return reuslt;
            }
        }







    }
}
