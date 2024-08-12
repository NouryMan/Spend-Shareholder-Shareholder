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
    public class OPERATIONAL_PALANCE_Business
    {
        oracleDbContextSpend db;
        public OPERATIONAL_PALANCE_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<OPERATIONAL_PALANCE_Model> GetAll()
        {
            List<OPERATIONAL_PALANCE_Model> obj = db.OPERATIONAL_PALANCE_Model.ToList();

            return obj;
        }


        public OPERATIONAL_PALANCE_Model GetPyID(long id)
        {
            OPERATIONAL_PALANCE_Model obj = db.OPERATIONAL_PALANCE_Model.Find(id);
            return obj;
        }
        public List<OPERATIONAL_PALANCE_Model> GetAllPyProject(int id)
        {
            List<OPERATIONAL_PALANCE_Model> obj = db.OPERATIONAL_PALANCE_Model.Where(x => x.TARGET_PROJ == id).ToList();




            return obj;
        }



        public long Create(List<OPERATIONAL_PALANCE_Model> list)
        {
            int return_value = 0;
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    foreach (var item in list)
                    {
                        item.DATE_M = DateTime.Now;

                        // item.DATE_H = DateTime.Now;

                        
                        List<PERCENT_EST_Model> percntList = new List<PERCENT_EST_Model>();
                        var determinedCost = item.PERCENT_EST_Collection.Where(x => x.SPEND_DETERMINED).Sum(x => x.DET_COST);
                        foreach (var child in item.PERCENT_EST_Collection)
                        {
                            if (item.IS_DETERMINED)
                            {
                                if(child.SPEND_DETERMINED == false)
                                {
                                    child.SPEND_DETERMINED = true;
                                    child.DET_COST = (item.DET_COST - determinedCost) / child.SPEND_PERCENT;
                                }
                            }

                                //  child.BALANCE = 1;
                            child.SOURCE_BOX = 1;
                            child.PARENT_ACCH = item.ACC_HOLDER_NO;
                            //child.STATUS = true;
                            //percntList.Add(child);
                        }

                       // item.PERCENT_EST_Collection = null;

                        db.OPERATIONAL_PALANCE_Model.Add(item);


                        return_value = db.SaveChanges();


                       
                    }
                    scope.Complete();
                    return return_value;
                }
                catch
                {
                  return  return_value = 0;
                }

            }
        }


        public long Update(List<OPERATIONAL_PALANCE_Model> list)
        {
            int return_value = 0;
            ACC_TREETBL_Business b = new ACC_TREETBL_Business();
            using (TransactionScope scope = new TransactionScope())
            {
                try
                {
                    foreach (var item in list)
                    {
                        item.DATE_M = DateTime.Now;

                        // item.DATE_H = DateTime.Now;


                        List<PERCENT_EST_Model> percntList = new List<PERCENT_EST_Model>();

                        var determinedCost = item.PERCENT_EST_Collection.Where(x => x.SPEND_DETERMINED).Sum(x => x.DET_COST);
                        foreach (var child in item.PERCENT_EST_Collection)
                        {
                            if (item.IS_DETERMINED)
                            {
                                if (child.SPEND_DETERMINED == false)
                                {
                                    child.SPEND_DETERMINED = true;
                                    child.DET_COST = (item.DET_COST - determinedCost) / child.SPEND_PERCENT;
                                }
                            }

                            //  child.BALANCE = 1;
                            child.SOURCE_BOX = 1;
                            child.PARENT_ACCH = item.ACC_HOLDER_NO;
                            //child.STATUS = true;
                            //percntList.Add(child);

                           

                        }

                      //  item.PERCENT_EST_Collection = null;

                     
                       
                        db.Entry(item).State = EntityState.Modified;
                        return_value = db.SaveChanges();



                    }
                    scope.Complete();
                    return return_value;
                }
                catch
                {
                    return return_value = 0;
                }

            }
        }


    }
}
