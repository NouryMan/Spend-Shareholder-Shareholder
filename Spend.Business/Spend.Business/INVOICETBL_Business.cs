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
   public class INVOICETBL_Business
    {
        oracleDbContextSpend db;
        public INVOICETBL_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<INVOICETBL_Model> getall()
        {
            List<INVOICETBL_Model> obj = db.INVOICETBL_Model.OrderByDescending(x=>x.INV_NO).ToList();




            return obj;
        }
        public INVOICETBL_Model GetPyID(long id)
        {
            INVOICETBL_Model obj = db.INVOICETBL_Model.Find(id);


            return obj;
        }


       public long GetMaxSerial(int year)
        {
            long INV_SERIAL = Convert.ToInt64(year);
            try
            {
                INV_SERIAL = db.INVOICETBL_Model.Where(x=>x.FISCAL_YEAR==year).Max(x => x.INV_SERIAL)+1 ;
            }
           catch (Exception ex)
            {
                INV_SERIAL = INV_SERIAL * 10000 + 1;
            }

            return INV_SERIAL;
        }


        public int Create(INVOICETBL_Model INV)
        {
            try
            {
                using (TransactionScope trns = new TransactionScope())
                {
                    MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();

                    if (INV.INV_DUC_TYPE != 8 && INV.INV_DUC_TYPE != 9 && INV.INV_DUC_TYPE != 10 && INV.INV_DUC_TYPE != 11 && INV.INV_DUC_TYPE != 21 && INV.INV_DUC_TYPE != 23 && INV.INV_DUC_TYPE != 30 && INV.INV_DUC_TYPE != 31 && INV.INV_DUC_TYPE != 33 && INV.INV_DUC_TYPE != 34)
                    {
                        INV.INV_TRANSED = 0;
                    }
                    else
                    {
                        
                    }

                   
                    INV.INV_SERIAL = GetMaxSerial(INV.FISCAL_YEAR.Value);
                    INV.CR_DATE = DateTime.Now;
                    INV.INV_NO = INV.INV_SERIAL;
                    INV.SCRIP_NO = INV.INV_SERIAL;
                    INV.SCRIP_SAVED = 0;
                    try { INV.DEBTOR_ID = db.ALL_ACC_NOTBL_Model.Find(INV.DEBTOR_ACC).PERSONAL_NO; } catch { }

                    if (INV.INV_DUC_TYPE == 36)
                    {
                        INV.UNDER_NO = null;
                    }
                    else { INV.UNDER_NO = Under_NO_b.getall().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO; }

                    int Id_dt = 1;
                    try
                    {
                        Id_dt = db.INV_DTTBL_Model.Max(x => x.ID) + 1;
                    }
                    catch
                    {

                    }

                    foreach (var item in INV.INV_DTTBL_Collection)
                    {
                        item.ID = Id_dt;
                        item.ACC_NO = INV.ACC_NO;
                        item.CR_DATE = INV.CR_DATE;
                        item.HD_INV_SERIAL = INV.INV_SERIAL;
                        item.INV_NO = INV.INV_NO;
                        item.PROJ_NO = INV.PROJ_NO;
                        item.STAGE_NO = INV.STAGE_NO;
                        item.USER_CR = INV.USER_CR;
                        //db.INV_DTTBL_Model.Add(item);
                        //return_value = db.SaveChanges();
                        Id_dt++;
                    }

                    db.INVOICETBL_Model.Add(INV);

                    int return_value = db.SaveChanges();

                    if (return_value > 0)
                    {
                       
                        
                            trns.Complete();
                     



                        return return_value;
                    }


                    return 0;
                }

            }
            catch (Exception ex) { }
            return 0;
        }

        public int Update(INVOICETBL_Model INV)
        {
            try
            {
                using (TransactionScope trns = new TransactionScope())
                {
                    MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();

                    if (INV.INV_DUC_TYPE != 8 && INV.INV_DUC_TYPE != 9 && INV.INV_DUC_TYPE != 10 && INV.INV_DUC_TYPE != 11 && INV.INV_DUC_TYPE != 21 && INV.INV_DUC_TYPE != 23 && INV.INV_DUC_TYPE != 30 && INV.INV_DUC_TYPE != 31 && INV.INV_DUC_TYPE != 33 && INV.INV_DUC_TYPE != 34)
                    {
                        INV.INV_TRANSED = 0;
                    }
                    else
                    {
                        if (INV.UNDER_NO == null)
                        {

                            INV.UNDER_NO= Under_NO_b.getall().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;
                        }
                    }


                    INV.UP_DATE = DateTime.Now;

                    INV.SCRIP_SAVED = 0;
                    try { INV.DEBTOR_ID = db.ALL_ACC_NOTBL_Model.Find(INV.DEBTOR_ACC).PERSONAL_NO; } catch { }

                    var oldDt = db.INV_DTTBL_Model.Where(x => x.HD_INV_SERIAL == INV.INV_SERIAL).ToList() ;


                    int Id_dt = 1;
                    try
                    {
                        Id_dt = db.INV_DTTBL_Model.Max(x => x.ID) + 1;
                    }
                    catch
                    {

                    }

                    foreach (var item in INV.INV_DTTBL_Collection)
                    {
                        item.ID = Id_dt;
                        item.ACC_NO = INV.ACC_NO;
                        item.CR_DATE = INV.CR_DATE;
                        item.HD_INV_SERIAL = INV.INV_SERIAL;
                        item.INV_NO = INV.INV_NO;
                        item.PROJ_NO = INV.PROJ_NO;
                        item.STAGE_NO = INV.STAGE_NO;
                        item.UP_DATE = INV.UP_DATE;
                        //db.INV_DTTBL_Model.Add(item);
                        //return_value = db.SaveChanges();
                        Id_dt++;
                    }
                    db.INV_DTTBL_Model.AddRange(INV.INV_DTTBL_Collection);
                  int  return_value = db.SaveChanges();
                   
                    if (return_value > 0)
                    {
                        db.Entry(INV).State = EntityState.Modified;
                        db.Entry(INV).Property(x => x.USER_CR).IsModified = false;
                        db.Entry(INV).Property(x => x.SCRIP_SAVED).IsModified = false;
                        db.Entry(INV).Property(x => x.SCRIP_NO).IsModified = false;
                        db.Entry(INV).Property(x => x.INV_NO).IsModified = false;
                        db.Entry(INV).Property(x => x.CR_DATE).IsModified = false;
                        db.Entry(INV).Property(x => x.UNDER_NO).IsModified = false;
                        return_value = db.SaveChanges();

                        if (return_value > 0)
                        {
                            db.INV_DTTBL_Model.RemoveRange(oldDt);
                            return_value = db.SaveChanges();
                            if (return_value > 0)
                            {
                                trns.Complete();




                                return return_value;
                            }
                        }


                        return 0;
                    }
                }

            }
            catch (Exception ex) { }
            return 0;
        }

        public List<INVOICETBL_Model> Search(long? supplier, DateTime? FromDate, DateTime? ToDate, string INV_DUC_TYPE, string trans, int? project)
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

          
            List<INVOICETBL_Model> model = new List<INVOICETBL_Model>();
            if (supplier > 0)
            {

                model = db.INVOICETBL_Model.Where(x => x.ACC_NO == supplier && x.DATE_M >= FromDate && x.DATE_M <= ToDate).OrderByDescending(x => x.CR_DATE).ToList();
            }
            else
            {
                model = db.INVOICETBL_Model.Where(x =>  x.DATE_M >= FromDate && x.DATE_M <= ToDate).OrderByDescending(x => x.CR_DATE).ToList();
            }
            if (project > 0)
            {

                model = model.Where(x => x.PROJ_NO == project).ToList();
            }

            if (!String.IsNullOrEmpty(INV_DUC_TYPE))
            {

                model = model.Where(x => x.INV_DUC_TYPE == Convert.ToInt16(INV_DUC_TYPE)).ToList();
            }
            if (!String.IsNullOrEmpty(trans))
            {

                model = model.Where(x => x.INV_TRANSED == Convert.ToInt16(trans)).ToList();
            }
            return model;
        }



        public int OpenTransed(int id)
        {
            int reusert = 0;
            try
            {
                var entity = db.INVOICETBL_Model.Find(id);
                entity.INV_TRANSED = 0;
                db.Entry(entity).State = EntityState.Modified;
                reusert= db.SaveChanges();
            }
            catch
            {

            }
            return reusert;
        }


    }
}
