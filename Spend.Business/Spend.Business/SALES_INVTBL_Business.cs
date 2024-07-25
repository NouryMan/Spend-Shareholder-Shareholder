
using callcenter.business.call;
using callcenter.model.call;
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
  public  class SALES_INVTBL_Business
    {

        oracleDbContextSpend db;
        public SALES_INVTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<SALES_INVTBL_Model> getall()
        {

         

            List<SALES_INVTBL_Model> obj = db.SALES_INVTBL_Model.ToList();


            return obj;

          
        }
        public SALES_INVTBL_Model GetPyID(long id)
        {
            SALES_INVTBL_Model obj = db.SALES_INVTBL_Model.Find(id);


            return obj;

          //  return null;
        }




        public long Create(SALES_INVTBL_Model INV_HD)
        {
            INV_HD.INV_NO = getMaxid();
            INV_HD.CR_DATE = DateTime.Now;
            INV_HD.INV_TYPE = INV_HD.INV_DOC_TYPE;
           // try { INV_HD.INV_PUR = INV_HD.PRICE.Value + INV_HD.VAT_AMOUNT.Value; } catch { }

          
            try { INV_HD.UNDER_NO = db.MAX_UNDER_OPV_Model.Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO; } catch { INV_HD.UNDER_NO = 1; }
            if (INV_HD.SALES_INVDTTBL_Collection.Count() > 0)
            {
                var idt = 0;
                try
                {
                    idt = db.SALES_INVDTTBL_Model.Max(x => x.ID);
                }
                catch
                {
                }

                idt++;
                foreach (var dt in INV_HD.SALES_INVDTTBL_Collection)
                {
                    dt.ID = idt;
                    dt.INV_NO = INV_HD.INV_NO;
                   
                    dt.UNDER_NO = INV_HD.UNDER_NO;
                    dt.UNIT_CODE =Convert.ToInt32(dt.PROJ_NO.ToString()+dt.PART_PROJ.ToString()+dt.UNIT_NO.ToString());
                    dt.CR_DATE = DateTime.Now;
                 
                    dt.INV_TYPE = INV_HD.INV_TYPE;
                    dt.DATE_M = INV_HD.DATE_M;

                   // try { dt.PUR_PRICE = dt.PRICE.Value + dt.VAT_AMOUNT.Value; } catch { }
                    idt++;
                }
            }

            db.SALES_INVTBL_Model.Add(INV_HD);

            long return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = INV_HD.INV_NO;
            }
            return return_value;
        }

        public long Update(SALES_INVTBL_Model INV)
        {
            try
            {
                using (TransactionScope trns = new TransactionScope())
                {
                    MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();

                   
                   
                        if (INV.UNDER_NO == null)
                        {

                            INV.UNDER_NO = Under_NO_b.getall().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;
                        }
                    


                    INV.UP_DATE = DateTime.Now;

                    
                    

                    var oldDt = db.SALES_INVDTTBL_Model.Where(x => x.INV_NO == INV.INV_NO).ToList();


                    int Id_dt = 1;
                    try
                    {
                        Id_dt = db.SALES_INVDTTBL_Model.Max(x => x.ID) + 1;
                    }
                    catch
                    {

                    }

                    foreach (var item in INV.SALES_INVDTTBL_Collection)
                    {
                        item.ID = Id_dt;
                        item.INV_NO = INV.INV_NO;

                        item.UNDER_NO = INV.UNDER_NO;
                        item.UNIT_CODE = Convert.ToInt32(item.PROJ_NO.ToString() + item.PART_PROJ.ToString() + item.UNIT_NO.ToString());
                        item.CR_DATE = DateTime.Now;

                        item.INV_TYPE = INV.INV_TYPE;
                        item.DATE_M = INV.DATE_M;

                        // try { dt.PUR_PRICE = dt.PRICE.Value + dt.VAT_AMOUNT.Value; } catch { }
                        Id_dt++;
                    }
                    db.SALES_INVDTTBL_Model.AddRange(INV.SALES_INVDTTBL_Collection);
                    int return_value = db.SaveChanges();

                    if (return_value > 0)
                    {
                        db.Entry(INV).State = EntityState.Modified;
                        db.Entry(INV).Property(x => x.USER_CR).IsModified = false;
                        db.Entry(INV).Property(x => x.SCRIP_SAVED).IsModified = false;
                        db.Entry(INV).Property(x => x.SCRIP_NO).IsModified = false;
                      
                        db.Entry(INV).Property(x => x.CR_DATE).IsModified = false;
                        db.Entry(INV).Property(x => x.UNDER_NO).IsModified = false;
                        db.Entry(INV).Property(x => x.CONTRACT_ID).IsModified = false;
                        return_value = db.SaveChanges();

                        if (return_value > 0)
                        {
                            db.SALES_INVDTTBL_Model.RemoveRange(oldDt);
                            return_value = db.SaveChanges();
                            if (return_value > 0)
                            {
                                trns.Complete();




                                return INV.INV_NO;
                            }
                        }


                        return 0;
                    }
                }

            }
            catch (Exception ex) { }
            return 0;
        }
        public long UpdateTrans(SALES_INVTBL_Model INV)
        {
            long return_value = 0;
            try
            {
                INV.TRANSED = true;
                db.Entry(INV).State = EntityState.Modified;
                db.Entry(INV).Property(x => x.USER_CR).IsModified = false;
                db.Entry(INV).Property(x => x.SCRIP_SAVED).IsModified = false;
                db.Entry(INV).Property(x => x.SCRIP_NO).IsModified = false;
                db.Entry(INV).Property(x => x.CR_DATE).IsModified = false;
                db.Entry(INV).Property(x => x.UNDER_NO).IsModified = false;
                db.Entry(INV).Property(x => x.CONTRACT_ID).IsModified = false;
                return_value = db.SaveChanges();
            }
            catch
            {

            }
            return return_value;


        }
        public long getMaxid()
        {
            long return_value = 1;
            try
            {
                return_value = db.SALES_INVTBL_Model.Max(x => x.INV_NO) + 1;

            }
            catch
            {

            }
            return return_value;
        }


        public long GetMaxINV_NO(int year)
        {
            long INV_INV_NO = Convert.ToInt64(year);
            try
            {
                INV_INV_NO = db.SALES_INVTBL_Model.Where(x => x.FISCAL_YEAR == year).Max(x => x.INV_NO) + 1;
            }
            catch (Exception ex)
            {
                INV_INV_NO = INV_INV_NO * 10000 + 1;
            }

            return INV_INV_NO;
        }



        public List<SALES_INVTBL_Model> GetTransForBox(long PROJECT_NO, bool BoxTRANS, DateTime? FromDate, DateTime? ToDate)
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


            List<SALES_INVTBL_Model> model = new List<SALES_INVTBL_Model>();

            CENTERSTBL_Business C_b = new CENTERSTBL_Business();
            //long ACC_NO = C_b.GeCenterAccForProj(PROJECT_NO).SALES_ACC.Value;


            model = db.SALES_INVTBL_Model.Where(x=> x.DATE_M >= FromDate && x.DATE_M <= ToDate  && x.PROJ_NO == PROJECT_NO && x.BOX_TRANSED==BoxTRANS &&x.TRANSED==true).OrderBy(x => x.DATE_M).ToList();
            
            foreach (var item in model)
            {
                ACCOUNTTBL_Business accB = new ACCOUNTTBL_Business();
                ACCOUNTTBL_Model cc = new ACCOUNTTBL_Model();
                item.INV_PUR = 0;


                item.INV_PUR = db.ACCOUNTTBL_Model.Where(x => x.UNDER_NO == item.UNDER_NO && x.IS_BOX_HOLDER == true).Max(x => x.FOR_HIM) - db.ACCOUNTTBL_Model.Where(x => x.UNDER_NO == item.UNDER_NO && x.IS_BOX_HOLDER == true).Max(x => x.FROM_HIM);
            }



            return model;
        }


        public int UpdateBoxTranse(SALES_INVTBL_Model INV)
        {
            try
            {
                       int return_value = 0;


                        db.Entry(INV).State = EntityState.Modified;
                        db.Entry(INV).Property(x => x.CONTRACT_ID).IsModified = false;

                        return_value = db.SaveChanges();


                      return return_value;


            }
            catch (Exception ex) { return 0; }
           
        }
        public List<SALES_INVTBL_Model> Search(long? SALES_CUST_ID, DateTime? FromDate, DateTime? ToDate, string INV_DUC_TYPE, string trans, int? project)
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


            List<SALES_INVTBL_Model> model = new List<SALES_INVTBL_Model>();
            if (SALES_CUST_ID > 0)
            {

                model = db.SALES_INVTBL_Model.Where(x => x.SALES_CUST_ID == SALES_CUST_ID && x.DATE_M >= FromDate && x.DATE_M <= ToDate).OrderByDescending(x => x.CR_DATE).ToList();
            }
            else
            {
                model = db.SALES_INVTBL_Model.Where(x => x.DATE_M >= FromDate && x.DATE_M <= ToDate).OrderByDescending(x => x.CR_DATE).ToList();
            }
            if (project > 0)
            {

                model = model.Where(x => x.PROJ_NO == project).ToList();
            }

            if (!String.IsNullOrEmpty(INV_DUC_TYPE))
            {

                model = model.Where(x => x.INV_DOC_TYPE == Convert.ToInt16(INV_DUC_TYPE)).ToList();
            }
            if (!String.IsNullOrEmpty(trans))
            {

                model = model.Where(x => x.INV_DOC_TYPE == Convert.ToInt16(trans)).ToList();
            }
            return model;
        }


        public int OpenTransed(long id)
        {
            int reusert = 0;
            try
            {
                using (TransactionScope trns = new TransactionScope())
                {
                    var entity = db.SALES_INVTBL_Model.Find(id);
                    if (entity.BOX_TRANSED == false)
                    {
                        entity.TRANSED = false;
                        db.Entry(entity).State = EntityState.Modified;
                        reusert = db.SaveChanges();

                        if (reusert > 0)
                        {
                            var transHd = db.TRANS_HD_Model.Where(x => x.JOURNAL_NO == entity.UNDER_NO).FirstOrDefault();
                            db.TRANS_HD_Model.Remove(transHd);
                            reusert = db.SaveChanges();
                            trns.Complete();
                        }
                        else
                        {
                            reusert = 0;
                        }
                    }
                }
            }
            catch
            {
                reusert = 0;
            }
            return reusert;
        }






        public IEnumerable<SALES_INVTBL_Model> GetBtwenDate(int? ProjNo, int? BulidingNo, DateTime fromdate, DateTime todate)
        {

            List<SALES_INVTBL_Model> obj = db.SALES_INVTBL_Model.Where(x => x.DATE_M >= fromdate && x.DATE_M < todate ).ToList();


            if (ProjNo != null)
            {
                obj = obj.Where(x =>x.PROJECTTBL_Model!=null && x.PROJECTTBL_Model.CALL_PROJ_ID == ProjNo).ToList();
            }
            if (BulidingNo != null)
            {
                obj = obj.Where(x => x.PART_PROJ == BulidingNo).ToList();
            }


            return obj;

        }
    }
}
