
using callcenter.business.call;
using callcenter.model.call;
using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;

namespace Spend.Business
{
    public class SPEND_SCRIPTBL_Business
    {
        oracleDbContextSpend db;
        public SPEND_SCRIPTBL_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<SPEND_SCRIPTBL_Model> getall()
        {
            List<SPEND_SCRIPTBL_Model> obj = db.SPEND_SCRIPTBL_Model.ToList();




            return obj;
        }

        public long GetMaxID()
        {
            long SCRIP_NO = 0;
            try
            {
                SCRIP_NO = db.SPEND_SCRIPTBL_Model.Max(x=>x.ID);
            }
            catch
            {
             
            }

            return ++SCRIP_NO;
        }

        public long create(SPEND_SCRIPTBL_Model SCRIPT)
        {
            try
            {
                using (TransactionScope trns = new TransactionScope())
                {

                    var deb_Acc = db.ALL_ACC_NOTBL_Model.Find(SCRIPT.DEBTOR_ACC);
                    var cred_acc = db.ALL_ACC_NOTBL_Model.Find(SCRIPT.CREDITOR_ACC);

                    SCRIPT.FISCAL_YEAR = SCRIPT.DATE_M.Value.Year.ToString();
                    SCRIPT.SCRIP_NO = getMaxNO(SCRIPT.FISCAL_YEAR, SCRIPT.SCRIPT_TYPE);
                    try { SCRIPT.UNDER_NO = db.MAX_UNDER_OPV_Model.Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO; } catch { SCRIPT.UNDER_NO = 1; }
                    SCRIPT.CLOSED = true;
                    SCRIPT.CR_DATE = DateTime.Now;
                    SCRIPT.DEBTOR_ID = deb_Acc.PERSONAL_NO;
                    SCRIPT.CREDITOR_ID = cred_acc.PERSONAL_NO;
                    SCRIPT.ID = GetMaxID();

                    try { SCRIPT.DEBTOR_ID = db.ALL_ACC_NOTBL_Model.Find(SCRIPT.DEBTOR_ACC).PERSONAL_NO; } catch { }
                    try { SCRIPT.CREDITOR_ID = db.ALL_ACC_NOTBL_Model.Find(SCRIPT.CREDITOR_ACC).PERSONAL_NO; } catch { }




                    db.SPEND_SCRIPTBL_Model.Add(SCRIPT);

                    long return_value = db.SaveChanges();

                    if (return_value > 0)
                    {
                        return_value = SCRIPT.ID;
                        trns.Complete();
                        return return_value;
                    }


                    //if (return_value > 0)
                    //{
                    //    int Dt_ID = 1;
                    //    try
                    //    {
                    //        Dt_ID = db.SCRIP_DTTBL_Model.Max(x => x.ID) + 1;
                    //    }
                    //    catch
                    //    {

                    //    }
                    //    SCRIP_DTTBL_Model DEBTOR_Dt = new SCRIP_DTTBL_Model();

                    //    DEBTOR_Dt.ACC_ID = deb_Acc.PERSONAL_NO;
                    //    DEBTOR_Dt.ACC_NO = SCRIPT.DEBTOR_ACC;
                    //    DEBTOR_Dt.ACC_PARENT = deb_Acc.ACC_PARENT;
                    //    DEBTOR_Dt.ACTION_TYPE = SCRIPT.ACTION_TYPE;
                    //    DEBTOR_Dt.CLOSED = SCRIPT.CLOSED;
                    //    DEBTOR_Dt.CR_DATE = SCRIPT.CR_DATE;
                    //    DEBTOR_Dt.DATE_H = SCRIPT.SCRIP_DATE;
                    //    DEBTOR_Dt.DATE_M = SCRIPT.DATE_M;
                    //    DEBTOR_Dt.FISCAL_YEAR = SCRIPT.FISCAL_YEAR;
                    //    // DEBTOR_Dt.FOR_HIM = 0;
                    //    DEBTOR_Dt.FROM_HIM = SCRIPT.COST;
                    //    DEBTOR_Dt.ID = Dt_ID;
                    //    DEBTOR_Dt.NOTE = SCRIPT.REMARK;
                    //    DEBTOR_Dt.PART_NO = SCRIPT.PART_NO;
                    //    DEBTOR_Dt.PROJ_NO = SCRIPT.PROJECT_NO;
                    //    DEBTOR_Dt.SCRIP_NO = SCRIPT.SCRIP_NO;
                    //    DEBTOR_Dt.SCRIP_TRANS = SCRIPT.SCRIP_TRANS;
                    //    DEBTOR_Dt.SCRIP_TYPE = SCRIPT.SCRIPT_TYPE;
                    //    DEBTOR_Dt.STAGE_NO = SCRIPT.GROUP_NO;
                    //    DEBTOR_Dt.SUB_PROJ = SCRIPT.SUB_PROJ;
                    //    DEBTOR_Dt.UNDER_NO = SCRIPT.UNDER_NO;
                    //    DEBTOR_Dt.UNIT_NO = SCRIPT.UNIT_NO;
                    //    DEBTOR_Dt.USER_CR = SCRIPT.USER_CR;
                    //    DEBTOR_Dt.HD_ID = SCRIPT.ID;
                    //    // SCRIPT.SCRIP_DTTBL_Collection.Add(DEBTOR_Dt);

                    //    SCRIP_DTTBL_Model CREDITOR_Dt = new SCRIP_DTTBL_Model();

                    //    CREDITOR_Dt.ACC_ID = cred_acc.PERSONAL_NO;
                    //    CREDITOR_Dt.ACC_NO = SCRIPT.CREDITOR_ACC;
                    //    CREDITOR_Dt.ACC_PARENT = cred_acc.ACC_PARENT;
                    //    CREDITOR_Dt.ACTION_TYPE = SCRIPT.ACTION_TYPE;
                    //    CREDITOR_Dt.CLOSED = SCRIPT.CLOSED;
                    //    CREDITOR_Dt.CR_DATE = SCRIPT.CR_DATE;
                    //    CREDITOR_Dt.DATE_H = SCRIPT.SCRIP_DATE;
                    //    CREDITOR_Dt.DATE_M = SCRIPT.DATE_M;
                    //    CREDITOR_Dt.FISCAL_YEAR = SCRIPT.FISCAL_YEAR;
                    //    CREDITOR_Dt.FOR_HIM = SCRIPT.COST;
                    //    //  CREDITOR_Dt.FROM_HIM = 0;
                    //    CREDITOR_Dt.ID = Dt_ID + 1;
                    //    CREDITOR_Dt.NOTE = SCRIPT.REMARK;
                    //    CREDITOR_Dt.PART_NO = SCRIPT.PART_NO;
                    //    CREDITOR_Dt.PROJ_NO = SCRIPT.PROJECT_NO;
                    //    CREDITOR_Dt.SCRIP_NO = SCRIPT.SCRIP_NO;
                    //    CREDITOR_Dt.SCRIP_TRANS = SCRIPT.SCRIP_TRANS;
                    //    CREDITOR_Dt.SCRIP_TYPE = SCRIPT.SCRIPT_TYPE;
                    //    CREDITOR_Dt.STAGE_NO = SCRIPT.GROUP_NO;
                    //    CREDITOR_Dt.SUB_PROJ = SCRIPT.SUB_PROJ;
                    //    CREDITOR_Dt.UNDER_NO = SCRIPT.UNDER_NO;
                    //    CREDITOR_Dt.UNIT_NO = SCRIPT.UNIT_NO;
                    //    CREDITOR_Dt.USER_CR = SCRIPT.USER_CR;
                    //    CREDITOR_Dt.HD_ID = SCRIPT.ID;
                    //    // SCRIPT.SCRIP_DTTBL_Collection.Add(CREDITOR_Dt);


                    //    db.SCRIP_DTTBL_Model.Add(DEBTOR_Dt);
                    //    return_value = db.SaveChanges();
                    //    if (return_value > 0)
                    //    {
                    //        db.SCRIP_DTTBL_Model.Add(CREDITOR_Dt);
                    //        return_value = db.SaveChanges();
                    //        if (return_value > 0)
                    //        {
                    //            return_value = SCRIPT.ID;
                    //            trns.Complete();
                    //        }
                    //    }


                    //    return return_value;
                    //}


                    return 0;
                }

            }
            catch (Exception ex) { }
            return 0;
        }



        public long Update(SPEND_SCRIPTBL_Model SCRIPT)
        {
            try
            {
                using (TransactionScope trns = new TransactionScope())
                {
                  
                    
                    var deb_Acc = db.ALL_ACC_NOTBL_Model.Find(SCRIPT.DEBTOR_ACC);
                    var cred_acc = db.ALL_ACC_NOTBL_Model.Find(SCRIPT.CREDITOR_ACC);
                  
                    
                    
                   
                    SCRIPT.UP_DATE = DateTime.Now;
                    SCRIPT.DEBTOR_ID = deb_Acc.PERSONAL_NO;
                    SCRIPT.CREDITOR_ID = cred_acc.PERSONAL_NO;
                  
                    try { SCRIPT.DEBTOR_ID = db.ALL_ACC_NOTBL_Model.Find(SCRIPT.DEBTOR_ACC).PERSONAL_NO; } catch { }
                    try { SCRIPT.CREDITOR_ID = db.ALL_ACC_NOTBL_Model.Find(SCRIPT.CREDITOR_ACC).PERSONAL_NO; } catch { }

                   
                    db.Entry(SCRIPT).State = EntityState.Modified;
                    db.Entry(SCRIPT).Property(x => x.CR_DATE).IsModified = false;
                    db.Entry(SCRIPT).Property(x => x.USER_CR).IsModified = false;
                    db.Entry(SCRIPT).Property(x => x.CLOSED).IsModified = false;
                    db.Entry(SCRIPT).Property(x => x.UNDER_NO).IsModified = false;

                    long return_value = db.SaveChanges();

                    if (return_value > 0)
                    {
                        return_value = SCRIPT.ID;
                        trns.Complete();
                        return return_value;
                    }


                    


                    return 0;
                }

            }
            catch (Exception ex) { }
            return 0;
        }



        public long getMaxNO(string year, int type)
        {
            long SCRIP_NO = Convert.ToInt64(year);
            try
            {
                SCRIP_NO = db.SPEND_SCRIPTBL_Model.Where(x => x.FISCAL_YEAR == year && x.SCRIPT_TYPE == type).Max(x => x.SCRIP_NO) + 1;
            }
            catch
            {
                SCRIP_NO = SCRIP_NO * 10000 + 1;
            }

            return SCRIP_NO;
        }


        public SPEND_SCRIPTBL_Model GetPyID(long id)
        {
            SPEND_SCRIPTBL_Model obj = db.SPEND_SCRIPTBL_Model.Find(id);

            callContractBusiness contract = new callContractBusiness();
            callContractModel contr = contract.getbyid(obj.CONTRACT_ID);
            ConvertNumbersToArabicAlphabet convertNumbersToArabic = new ConvertNumbersToArabicAlphabet();
            try
            {
                obj.Cost_String = convertNumbersToArabic.ConvertNumberToAlpha(obj.COST.ToString());
            }
            catch
            {

            }
            try
            {


                obj.customer_name = contr.callCustomerModel.CUST_AR_NAME;

                obj.project_name = contr.callProjectModel.PROJECT_AR_NAME + " - " + contr.callProjectModel.callProjectModels.PROJECT_AR_NAME + " - من  مشروع  " + contr.callProjectModel.callProjectModels.callProjectModels.PROJECT_AR_NAME;
                obj.contract_cost = contr.FIRST_COST.ToString();
            }
            catch { }
            return obj;
        }


        public int OpenTransed(long id)
        {
            int reusert = 0;
            try
            {
                var entity = db.SPEND_SCRIPTBL_Model.Find(id);
                entity.SCRIP_TRANS = false;
                db.Entry(entity).State = EntityState.Modified;
                reusert = db.SaveChanges();
            }
            catch
            {

            }
            return reusert;
        }


    }
}
