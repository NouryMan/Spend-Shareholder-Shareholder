using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using callcenter.business.call;

namespace Spend.Business
{
   public class SALES_CUSTTBL_Business
    {
        oracleDbContextSpend db;
        public SALES_CUSTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<SALES_CUSTTBL_Model> getall()
        {



            List<SALES_CUSTTBL_Model> obj = db.SALES_CUSTTBL_Model.Where(x=>x.IS_DELETE==false).ToList();


            return obj;


        }
        public SALES_CUSTTBL_Model GetPyID(long id)
        {
            SALES_CUSTTBL_Model obj = db.SALES_CUSTTBL_Model.Find(id);


            return obj;


        }
        public long GetMaxId()
        {
            long id = 0;
            try
            {
                id = db.SALES_CUSTTBL_Model.Max(x => x.CUST_ID) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }
        public List<SALES_CUSTTBL_Model> GetbyParentAcc(long parent_id)
        {
            List<SALES_CUSTTBL_Model> obj = db.SALES_CUSTTBL_Model.Where(x => x.CUST_ACC_PARENT == parent_id).ToList();


            return obj;

        }
        public long Create(SALES_CUSTTBL_Model model)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            var parentAcc = b.GetPyID(model.CUST_ACC_PARENT);


            model.CUST_ID = GetMaxId();
            model.CUST_ACC = Convert.ToInt64(parentAcc.ACC_NO.ToString() + model.Acc_NOString);

            model.CR_DATE = DateTime.Now;


            ALL_ACC_NOTBL_Business alL_ACC_NOTBL_Business = new ALL_ACC_NOTBL_Business();
            if (alL_ACC_NOTBL_Business.IsExist(model.CUST_ACC))
            {
                return 10001001;
            }
            db.SALES_CUSTTBL_Model.Add(model);

            int return_value = db.SaveChanges();

            
            return return_value;
        }

        public long Update(SALES_CUSTTBL_Model model)
        {
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            var parentAcc = b.GetPyID(model.CUST_ACC_PARENT);

            model.CUST_ACC = Convert.ToInt64(parentAcc.ACC_NO.ToString() + model.Acc_NOString);
            model.UP_DATE = DateTime.Now;


            db.Entry(model).State = EntityState.Modified;
            db.Entry(model).Property(x => x.CR_DATE).IsModified = false;
            db.Entry(model).Property(x => x.USER_CR).IsModified = false;
            db.Entry(model).Property(x => x.IS_DELETE).IsModified = false;
          

            long return_value = db.SaveChanges();


            return return_value;
        }



        public int Delete(long id)
        {
            int return_value = 0;
            var obj = db.SALES_CUSTTBL_Model.Find(id);
            obj.IS_DELETE = true;

            try
            {
                db.Entry(obj).State = EntityState.Modified;

                return_value = db.SaveChanges();


            }
            catch
            { }

            return return_value;
        }
        public SALES_CUSTTBL_Model GetPyCALL_CUST_ID(int id)
        {
            SALES_CUSTTBL_Model obj = db.SALES_CUSTTBL_Model.Where(x => x.CALL_CUST_ID == id).FirstOrDefault();


            return obj;


        }

        public long GetMaxAcc(long parntAcc)
        {

            long maxAcc;
            try
            {
                maxAcc = db.SALES_CUSTTBL_Model.Where(x => x.CUST_ACC_PARENT == parntAcc).Max(x => x.CUST_ACC) + 1;
            }
            catch
            {
                maxAcc = parntAcc * 100000 + 1;
            }

            return maxAcc;


        }


        public long Synchronization()
        {
            callCustomerBusiness customerBusiness = new callCustomerBusiness();
            var callCustomers = customerBusiness.getall();
            var spendCustomers = db.SALES_CUSTTBL_Model;
            long reusert=0;
            foreach (var item in callCustomers)
            {
                try
                {
                    if (spendCustomers.Any(x => x.CALL_CUST_ID == item.ID))
                    {

                        var customer = spendCustomers.FirstOrDefault(x => x.CALL_CUST_ID == item.ID);
                        customer.CUST_NAME_AR = item.CUST_AR_NAME;

                        customer.CUST_NAME_AR = item.CUST_AR_NAME;
                        customer.CUST_NAME_EN = item.CUST_EN_NAME;
                        customer.CUST_ADDRESS = item.CUST_ADDRESS;
                        customer.MOBILE_NO = item.CUST_MOBILE_1;
                        customer.PHONE = item.CUST_MOBILE_2;
                        customer.NOTE = item.NOTE;
                        
                        //customer.CUST_ACC = GetMaxAcc(customer.CUST_ACC_PARENT);
                        customer.UP_DATE = DateTime.Now;


                        db.Entry(customer).State = EntityState.Modified;
                        reusert += db.SaveChanges(); ;

                    }
                    else
                    {
                        SALES_CUSTTBL_Model sALES_CUSTTBL = new SALES_CUSTTBL_Model();
                        sALES_CUSTTBL.CUST_NAME_AR = item.CUST_AR_NAME;
                        sALES_CUSTTBL.CUST_NAME_EN = item.CUST_EN_NAME;
                        sALES_CUSTTBL.CUST_CODE = GetMaxId().ToString();
                        sALES_CUSTTBL.CUST_ID = GetMaxId();
                       
                        sALES_CUSTTBL.CUST_ACC_PARENT = item.PARNT_ACC.Value;
                        sALES_CUSTTBL.CUST_ACC = GetMaxAcc(item.PARNT_ACC.Value);
                      
                        sALES_CUSTTBL.MOBILE_NO = item.CUST_MOBILE_1;
                        sALES_CUSTTBL.PHONE = item.CUST_MOBILE_2;
                        sALES_CUSTTBL.NOTE = item.NOTE;
                        sALES_CUSTTBL.CR_DATE = item.DATE_CR;
                        sALES_CUSTTBL.CALL_CUST_ID = item.ID;
                        sALES_CUSTTBL.IS_DELETE = item.IS_DELETED;
                        sALES_CUSTTBL.ENABLED = true;
                        db.SALES_CUSTTBL_Model.Add(sALES_CUSTTBL);

                        reusert += db.SaveChanges();
                    }
                }
                catch
                {

                }
               
            }
            return reusert;
        }

    }
}
