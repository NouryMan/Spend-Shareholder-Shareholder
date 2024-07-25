using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Spend.Business
{
  public  class PERSON_ACCTBL_Business
    {
        oracleDbContextSpend db;
        public PERSON_ACCTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<PERSON_ACCTBL_Model> getall()
        {
            List<PERSON_ACCTBL_Model> obj = db.PERSON_ACCTBL_Model.ToList();

            return obj;
        }
        public PERSON_ACCTBL_Model GetByID(long id)
        {
            PERSON_ACCTBL_Model obj = db.PERSON_ACCTBL_Model.Find(id);
            return obj;
        }

        public int Create(PERSON_ACCTBL_Model ACC)
        {
            ACC.ID = getMaxid();
            ACC.IS_ENABLED = true;
            ACC.ACC_NO = getMaxAccNO(ACC.PARENT_ACC.Value);
           
            ACC.CR_DATE = DateTime.Now;

            try { ACC.ACC_LEVEL = db.ALL_ACC_NOTBL_Model.Find(ACC.PARENT_ACC.Value).ACC_LEVEL + 1; } catch { }

            db.PERSON_ACCTBL_Model.Add(ACC);
            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = ACC.ID;
            }
            return return_value;
        }



        public int CreateWithPesonal(PERSON_ACCTBL_Model Acc)
        {

            using (TransactionScope trns = new TransactionScope())
            {
                PERSONAL_INFOTBL_Business PERSONAL_B = new PERSONAL_INFOTBL_Business();
                PERSONAL_INFOTBL_Model pERSONAL = new PERSONAL_INFOTBL_Model();
                long PERSONAL_ID;
                if (Acc.PERSON_ID > 0)
                {
                    PERSONAL_INFOTBL_Business PERSONAL_B1 = new PERSONAL_INFOTBL_Business();
                    pERSONAL.ID = Acc.PERSON_ID.Value;

                    pERSONAL.AR_NAME = Acc.PERSONAL_INFOTBL_Model.AR_NAME;
                    pERSONAL.EN_NAME = Acc.PERSONAL_INFOTBL_Model.EN_NAME;
                    pERSONAL.ADDRESS1 = Acc.PERSONAL_INFOTBL_Model.ADDRESS1;
                    pERSONAL.EMAIL_ADDRESS = Acc.PERSONAL_INFOTBL_Model.EMAIL_ADDRESS;
                    pERSONAL.MOBILE_NO = Acc.PERSONAL_INFOTBL_Model.MOBILE_NO;
                    pERSONAL.C_ID = Acc.PERSONAL_INFOTBL_Model.C_ID;
                    pERSONAL.USER_UP = Acc.USER_CR;
                    PERSONAL_ID = PERSONAL_B.Update(pERSONAL);
                }
                else
                {
                    pERSONAL.AR_NAME = Acc.PERSONAL_INFOTBL_Model.AR_NAME;
                    pERSONAL.EN_NAME = Acc.PERSONAL_INFOTBL_Model.EN_NAME;
                    pERSONAL.ADDRESS1 = Acc.PERSONAL_INFOTBL_Model.ADDRESS1;
                    pERSONAL.EMAIL_ADDRESS = Acc.PERSONAL_INFOTBL_Model.EMAIL_ADDRESS;
                    pERSONAL.MOBILE_NO = Acc.PERSONAL_INFOTBL_Model.MOBILE_NO;
                    pERSONAL.C_ID = Acc.PERSONAL_INFOTBL_Model.C_ID;
                    pERSONAL.USER_CR = Acc.USER_CR; 
                    PERSONAL_ID = PERSONAL_B.Create(pERSONAL);

                }
                if (PERSONAL_ID > 0)
                {
                    PERSON_ACCTBL_Model AccM = new PERSON_ACCTBL_Model();
                    AccM.PERSON_ID = PERSONAL_ID;
                    AccM.USER_CR = Acc.USER_CR; 
                  
                    AccM.ACC_NAT = Acc.ACC_NAT;
                    AccM.ACC_NO = Acc.ACC_NO;
                    AccM.ACC_TYPE = Acc.ACC_TYPE;
                    AccM.IS_ENABLED = Acc.IS_ENABLED;
                    AccM.NOTE = Acc.NOTE;
                    AccM.OP_ACC = Acc.OP_ACC;
                    AccM.PARENT_ACC = Acc.PARENT_ACC;
                    AccM.PERSON_DESC_ID = Acc.PERSON_DESC_ID;
                   

                    PERSON_ACCTBL_Business ACC_B = new PERSON_ACCTBL_Business();
                   
                    ALL_ACC_NOTBL_Business alL_ACC_NOTBL_Business = new ALL_ACC_NOTBL_Business();
                    if (alL_ACC_NOTBL_Business.IsExist(Convert.ToInt64( AccM.ACC_NO)))
                    {
                        return 10001001;
                    }

                    int AccM_ID = ACC_B.Create(AccM);

                    if (AccM_ID > 0)
                    {

                        trns.Complete();
                        return AccM_ID;

                    }

                }




            }

            return 0;
        }


            public int Update(PERSON_ACCTBL_Model ACC)
        {



            db.Entry(ACC).State = EntityState.Modified;
            

            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
               
            }

            return return_value;
        }
        public int getMaxid()
        {
            int return_value = 1;
            try
            {
                return_value = db.PERSON_ACCTBL_Model.Max(x => x.ID) + 1;

            }
            catch
            {

            }
            return return_value;
        }


        public decimal getMaxAccNO(decimal PARENT_ACC)
        {
            decimal return_value = PARENT_ACC*100000+1;
            try
            {
                return_value = db.PERSON_ACCTBL_Model.Where(x=>x.PARENT_ACC == PARENT_ACC).Max(x => x.ACC_NO.Value) + 1;

            }
            catch
            {

            }
            return  return_value;
        }


        public List<PERSON_ACCTBL_Model> getPayParent_acc(int parent_id)
        {
            List<PERSON_ACCTBL_Model> obj = db.PERSON_ACCTBL_Model.Where(x => x.PARENT_ACC == parent_id).ToList();
            return obj;
        }



    }
}
