using Spend.Api.Models;
using Spend.DataAccess;
using Spend.Models;
using Spend.Models.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Spend.Business
{
    public class ACCH_OPBOXTBL_Business
    {
        oracleDbContextSpend db;
        public ACCH_OPBOXTBL_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<ACCH_OPBOXTBL_Model> GetAll()
        {
            List<ACCH_OPBOXTBL_Model> obj = db.ACCH_OPBOXTBL_Model.Where(x=>x.IS_DELETE==false).ToList();




            return obj;
        }
        public IEnumerable<IGrouping<decimal, ACCH_OPBOXTBL_Model>> GetpyGroup()
        {

            var obj = db.ACCH_OPBOXTBL_Model.Where(x => x.IS_DELETE == false).GroupBy(x => x.UNDER_NO).ToList();




            return obj;
        }


        public ACCH_OPBOXTBL_Model GetById(long id)
        {
            ACCH_OPBOXTBL_Model obj = db.ACCH_OPBOXTBL_Model.Find(id);


            return obj;
        }
        public List<ACCH_OPBOXTBL_Model> GetAllPyProject(int id)
        {
            List<ACCH_OPBOXTBL_Model> obj = db.ACCH_OPBOXTBL_Model.Where(x => x.TARGET_PROJ == id && x.IS_DELETE == false).ToList();




            return obj;
        }


        public double GetMaxSCRIP_NO()
        {
            double id = 0;
            try
            {
                id = db.ACCH_OPBOXTBL_Model.Max(x => x.SCRIP_NO) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }

        public int GetMaxSpendSCRIP_NO(int year, int type)
        {
            int SCRIP_NO = year;
            try
            {
                SCRIP_NO = db.ACCH_OPBOXTBL_Model.Where(x => x.DATE_M.HasValue && x.DATE_M.Value.Year == year && x.SPEND_SCRIPT_NO.HasValue && x.SCRIPT_TYPE == type).Max(x => x.SPEND_SCRIPT_NO.Value) + 1;
            }
            catch
            {
                SCRIP_NO = SCRIP_NO * 10000 + 1;
            }

            return SCRIP_NO;

        }
        public double GetMaxOP_NO()
        {
            double id = 0;
            try
            {
                id = db.ACCH_OPBOXTBL_Model.Max(x => x.OP_NO) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }

        public int GetMaxId()
        {
            int id = 0;
            try
            {
                id = db.ACCH_OPBOXTBL_Model.Max(x => x.ID) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }

        public void Create(ACCH_OPBOXTBL_Model OpBox)
        {
            if (OpBox.ACTION_TYPE == 1 && OpBox.ACTION_TYPE == 4 && OpBox.ACTION_TYPE == 6)  // اذا كانت  تشغلية ليست خاصة او غير تشغلية
            {
                OpBox.OUT = false;
            }
            else { OpBox.OUT = true; }
            OpBox.ID = GetMaxId();
            OpBox.IS_DELETE =false;
            db.ACCH_OPBOXTBL_Model.Add(OpBox); 
            db.SaveChanges();
        }

        public void Update(ACCH_OPBOXTBL_Model OpBox)
        {
            if (OpBox.ACTION_TYPE == 1 && OpBox.ACTION_TYPE == 4 && OpBox.ACTION_TYPE == 6)  // اذا كانت  تشغلية ليست خاصة او غير تشغلية
            {
                OpBox.OUT = false;
            }
            else { OpBox.OUT = true; }
            
            db.Entry(OpBox).State = EntityState.Modified;
            db.Entry(OpBox).Property(x => x.IS_DELETE).IsModified = false;
            db.Entry(OpBox).Property(x => x.UNDER_NO).IsModified = false;
            db.Entry(OpBox).Property(x => x.OP_NO).IsModified = false;
            db.Entry(OpBox).Property(x => x.DATE_M).IsModified = false;
            db.Entry(OpBox).Property(x => x.DATE_H).IsModified = false;
        
          

            db.SaveChanges();


          
        }


        public void Delete(int id)
        {
            try
            {
                var obj = GetById(id);
                if (obj == null)
                {
                    throw new Exception("Entity not found.");
                }
              

                obj.IS_DELETE = true;

                db.SaveChanges();

                return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AcchOpBoxModelView SpendProsses(ACCH_OPBOXTBL_Model OpBox)
        {

            AcchOpBoxModelView acchOpBoxModelView = new AcchOpBoxModelView();
            ACCH_BALANCEV_Business aCCH_BALANCEV_Business = new ACCH_BALANCEV_Business();
           

            acchOpBoxModelView.ProjectId = OpBox.TARGET_PROJ;
            acchOpBoxModelView.BoxId = OpBox.SOURCE_BOX;
            acchOpBoxModelView.OpTypeId = OpBox.OP_TYPE.Value;
            acchOpBoxModelView.OpActionTypeId = OpBox.ACTION_TYPE.Value;
            acchOpBoxModelView.Date = OpBox.DATE_M.Value;
            acchOpBoxModelView.TotalAmount = OpBox.SPEND_COST;
            acchOpBoxModelView.NOTE = OpBox.NOTE;
            acchOpBoxModelView.BuildingId = OpBox.BUILDING_ID;
            acchOpBoxModelView.UnitId = OpBox.UNIT_ID;

            List<AcchOpBoxDetailsModelView> acchOpBoxDetails = new List<AcchOpBoxDetailsModelView>();



            try
            {


                if (OpBox.SPEND_COST > 0)
                {
                    var persentList = db.PERCENT_EST_Model.Where(x => x.TARGET_PROJ == OpBox.TARGET_PROJ);//نسب المشروع




                    foreach (var persent in persentList.Where(x => x.SPEND_DETERMINED == true && x.DET_COST > 0))//اولا حجز المبلغ من اصحاب المبالغ المحددة
                    {

                        if (OpBox.SPEND_COST > 0)
                        {
                            AcchOpBoxDetailsModelView acchOpBox = new AcchOpBoxDetailsModelView();

                            acchOpBox.AccHolderId = persent.ACC_HOLDER_NO;
                            acchOpBox.percentage = persent.DET_COST.Value;
                            acchOpBox.IsPercentage = false;
                          
                            acchOpBox.Balance = aCCH_BALANCEV_Business.GetPyID(persent.ACC_HOLDER_NO).BALANCE??0;


                            //حساب المبلغ المصروف
                            double oldDET_COST = 0;
                            try { oldDET_COST = db.ACCH_OPBOXTBL_Model.Where(x => x.ACC_HOLDER_NO == persent.ACC_HOLDER_NO && x.TARGET_PROJ == persent.TARGET_PROJ).Sum(x => x.SPEND_COST); } catch { }
                            double DET_COST = persent.DET_COST.Value - oldDET_COST;

                            if (OpBox.SPEND_COST >= DET_COST)//اذا كان المبلغ المصروف اكبر من المحدد
                            {
                                OpBox.SPEND_COST -= DET_COST;
                                acchOpBox.Amount = DET_COST;
                            }
                            else
                            {
                                acchOpBox.Amount = OpBox.SPEND_COST;

                                OpBox.SPEND_COST = 0;
                            }


                            if (acchOpBox.Amount > 0)
                            {
                                acchOpBoxDetails.Add(acchOpBox);
                            }
                        }

                    }

                    foreach (var persent in persentList.Where(x => x.DET_COST <= 0 && x.SPEND_PERCENT > 0))//ثانيا حجز المبلغ من اصحاب النسب



                        if (OpBox.SPEND_COST > 0)
                        {
                            AcchOpBoxDetailsModelView acchOpBox = new AcchOpBoxDetailsModelView();
                           

                            acchOpBox.AccHolderId = persent.ACC_HOLDER_NO;
                            acchOpBox.percentage = persent.SPEND_PERCENT * persent.PARENT_PERCENT;
                            acchOpBox.IsPercentage = true;
                            acchOpBox.Balance = aCCH_BALANCEV_Business.GetPyID(persent.ACC_HOLDER_NO).BALANCE ?? 0;






                            acchOpBox.Amount = OpBox.SPEND_COST * persent.SPEND_PERCENT * persent.PARENT_PERCENT;

                            acchOpBoxDetails.Add(acchOpBox);



                        }


                }
            }
            catch
            {

            }
            acchOpBoxModelView.AcchOpBoxDetailsModelView = acchOpBoxDetails;

            return acchOpBoxModelView;



        }







        public int TranseSaleInv(List<API_TRANSESALE_INVO_Model> items)
        {
            using (TransactionScope tran = new TransactionScope())
            {

                int return_value = 0;

                try
                {
                    //فحص ان الفاتورة غير مرحلة وتعديلها
                    foreach (var item in items)
                    {
                        SALES_INVTBL_Business SaleInvoB = new SALES_INVTBL_Business();
                        var Inv = SaleInvoB.GetPyID(item.InovNo);
                        if (Inv.BOX_TRANSED)
                        {
                            return 0;
                        }
                        else
                        {
                            Inv.BOX_TRANSED = true;
                            return_value = SaleInvoB.UpdateBoxTranse(Inv);
                            if (return_value <= 0)
                            {
                                return 0;
                            }

                        }

                    }





                    List<ACCH_OPBOXTBL_Model> list = new List<ACCH_OPBOXTBL_Model>();

                    decimal UnderNo = 0;
                    try { UnderNo = db.MAX_UNDER_OPV_Model.Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO; } catch { UnderNo = 1; }

                    var SCRIP_NO = GetMaxSCRIP_NO();
                    var OpNo = GetMaxOP_NO();

                    CENTERSTBL_Business center_b = new CENTERSTBL_Business();
                    var Box = center_b.GeCenterAccForProj(items.FirstOrDefault().ProjNo).INCOME_BOX.Value;

                    var ProjNo = items.FirstOrDefault().ProjNo;
                    ACCH_PROJECT_Business b = new ACCH_PROJECT_Business();

                    var projectAsync = b.GetByIdAsync(items.FirstOrDefault().ProjNo);
                    var project = projectAsync.Result;

                    var Note = "فواتير مشروع " + project.PROJECT_AR_NAME + " من رقم " + items.OrderBy(x => x.InovNo).FirstOrDefault().InovNo + " رقم " + items.OrderBy(x => x.InovNo).LastOrDefault().InovNo;

                    //ترحيل مبالغ الصيانة والتسويق والحسابات الاخرى///////////////////////////

                    ACC_HOLDERTBL_Business acch_b = new ACC_HOLDERTBL_Business();

                    if (items.Sum(x => x.Mantinc) > 0)
                    {
                        var acch = acch_b.getall().Where(x => x.ACCH_TYPE == 2).FirstOrDefault();

                        list.Add(AddOtherAccH(acch.ACC_HOLDER_NO, 6, acch.PARENT_ACCH.Value, items.Sum(x => x.Mantinc), ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++));
                    }
                    if (items.Sum(x => x.Markting) > 0)
                    {
                        var acch = acch_b.getall().Where(x => x.ACCH_TYPE == 3).FirstOrDefault();
                        list.Add(AddOtherAccH(acch.ACC_HOLDER_NO, 6, acch.PARENT_ACCH.Value, items.Sum(x => x.Mantinc), ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++));
                    }
                    if (items.Sum(x => x.OtherCost) > 0)
                    {
                        var acch = acch_b.getall().Where(x => x.ACCH_TYPE == 4).FirstOrDefault();
                        list.Add(AddOtherAccH(acch.ACC_HOLDER_NO, 6, acch.PARENT_ACCH.Value, items.Sum(x => x.Mantinc), ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++));
                    }
                    /////////////


                    double TotalCost = items.Sum(x => x.PurCost) - items.Sum(x => x.Mantinc) - items.Sum(x => x.Markting) - items.Sum(x => x.OtherCost);
                    ACCH_BALANCE_SUMMARYV_Business BS_b = new ACCH_BALANCE_SUMMARYV_Business();
                    var Balnce = BS_b.GetPyProjNo(ProjNo);

                    double TotalIncome = Balnce.Sum(x => x.INCOME);
                    double TotallSpend = Balnce.Sum(x => x.SPENDING);
                    double WorkCost = 0;
                    ACCH_PROJ_BOX_PERCENTTBL_Business aCCH_PROJ_BOX_PERCENTTBL_B = new ACCH_PROJ_BOX_PERCENTTBL_Business();
                    var acchList = aCCH_PROJ_BOX_PERCENTTBL_B.GetAllPyProject(ProjNo);

                    if ((TotalCost + TotalIncome) - TotallSpend > 0)// add workpersent
                    {

                        var acch = acchList.Where(x => x.ACC_HOLDERTBL_Model.ACCH_TYPE == 3).FirstOrDefault();
                        if (acch != null)
                        {
                            if (TotalIncome >= TotallSpend)
                            {
                                WorkCost = TotalCost * acch.INCOME_PERCENT;
                            }
                            else
                            {
                                WorkCost = ((TotalCost + TotalIncome) - TotallSpend) * acch.INCOME_PERCENT;
                            }


                            TotalCost = TotalCost - WorkCost;


                            list.Add(AddOtherAccH(acch.ACC_HOLDER_NO, 6, acch.PARENT_ACCH, WorkCost, ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++));
                        }
                    }


                    foreach (var item in acchList.Where(x => x.ACC_HOLDERTBL_Model.ACCH_TYPE != 2 && x.ACC_HOLDERTBL_Model.ACCH_TYPE != 3 && x.ACC_HOLDERTBL_Model.ACCH_TYPE != 4))//add acc holder persent
                    {

                        double Cost = TotalCost * item.INCOME_PERCENT * item.ACCH_PROJ_PERCENTTBL_Model.ACCH_PERCENT;

                        list.Add(AddOtherAccH(item.ACC_HOLDER_NO, 6, item.PARENT_ACCH, Cost, ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++));


                    }

                    db.ACCH_OPBOXTBL_Model.AddRange(list);
                    return_value = db.SaveChanges();

                    if (return_value > 0)
                    {
                        tran.Complete();
                    }

                }
                catch
                {
                    return_value = 0;
                }
                return return_value;
            }

        }


        public bool Distribution(API_TRANSESALE_INVO_Model Distrib)
        {

            using (TransactionScope tran = new TransactionScope())
            {

                int return_value = 0;

                try
                {


                    List<ACCH_OPBOXTBL_Model> list = new List<ACCH_OPBOXTBL_Model>();
                    ACC_HOLDERTBL_Business b = new ACC_HOLDERTBL_Business();
                    decimal UnderNo = 0;
                    try { UnderNo = db.MAX_UNDER_OPV_Model.Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO; } catch { UnderNo = 1; }

                    var SCRIP_NO = GetMaxSCRIP_NO();
                    var OpNo = GetMaxOP_NO();

                    CENTERSTBL_Business center_b = new CENTERSTBL_Business();
                    //  var Box = center_b.GeCenterAccForProj(Distrib.ProjNo).INCOME_BOX.Value;

                    var Box = 1;


                    var ProjNo = Distrib.ProjNo;


                    var Note = Distrib.Note;

                    //ترحيل مبالغ الصيانة والتسويق والحسابات الاخرى///////////////////////////

                    ACC_HOLDERTBL_Business acch_b = new ACC_HOLDERTBL_Business();

                    if (Distrib.Mantinc > 0)
                    {
                        var acch = acch_b.getall().Where(x => x.ACCH_TYPE == 2).FirstOrDefault();
                        if (acch == null)
                        {
                            throw new Exception("لايوجد حساب صيانة للمشروع");
                        }
                        var obj = AddOtherAccH(acch.ACC_HOLDER_NO, 6, acch.PARENT_ACCH.Value, Distrib.Mantinc, ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++);
                        list.Add(obj);
                    }
                    if (Distrib.Markting > 0)
                    {

                        var acch = acch_b.getall().Where(x => x.ACCH_TYPE == 3).FirstOrDefault();
                        if (acch == null)
                        {
                            throw new Exception("لايوجد حساب تسويق للمشروع");
                        }
                        list.Add(AddOtherAccH(acch.ACC_HOLDER_NO, 6, acch.PARENT_ACCH.Value, Distrib.Mantinc, ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++));
                    }
                    if (Distrib.OtherCost > 0)
                    {

                        var acch = acch_b.getall().Where(x => x.ACCH_TYPE == 4).FirstOrDefault();
                        if (acch == null)
                        {
                            throw new Exception("لايوجد حساب للمصروفات الاخرى للمشروع");
                        }
                        list.Add(AddOtherAccH(acch.ACC_HOLDER_NO, 6, acch.PARENT_ACCH.Value, Distrib.Mantinc, ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++));
                    }
                    /////////////


                    double TotalCost = Distrib.PurCost;

                    ACCH_BALANCE_SUMMARYV_Business BS_b = new ACCH_BALANCE_SUMMARYV_Business();
                    var Balnce = BS_b.GetPyProjNo(ProjNo);

                    double TotalIncome = Balnce.Sum(x => x.INCOME);
                    double TotallSpend = Balnce.Sum(x => x.SPENDING);
                    double WorkCost = 0;
                    ACCH_PROJ_BOX_PERCENTTBL_Business aCCH_PROJ_BOX_PERCENTTBL_B = new ACCH_PROJ_BOX_PERCENTTBL_Business();
                    var acchList = aCCH_PROJ_BOX_PERCENTTBL_B.GetAllPyProject(ProjNo);

                    if ((TotalCost + TotalIncome) - TotallSpend > 0)// add workpersent
                    {

                        var acch = acchList.Where(x => x.ACC_HOLDERTBL_Model.ACCH_TYPE == 3).FirstOrDefault();
                        if (acch != null)
                        {
                            if (TotalIncome >= TotallSpend)
                            {
                                WorkCost = TotalCost * acch.INCOME_PERCENT;
                            }
                            else
                            {
                                WorkCost = ((TotalCost + TotalIncome) - TotallSpend) * acch.INCOME_PERCENT;
                            }


                            TotalCost = TotalCost - WorkCost;


                            list.Add(AddOtherAccH(acch.ACC_HOLDER_NO, 6, acch.PARENT_ACCH, WorkCost, ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++));
                        }
                    }


                    foreach (var item in acchList.Where(x => x.ACC_HOLDERTBL_Model.ACCH_TYPE != 2 && x.ACC_HOLDERTBL_Model.ACCH_TYPE != 3 && x.ACC_HOLDERTBL_Model.ACCH_TYPE != 4))//add acc holder persent
                    {

                        double Cost = TotalCost * item.INCOME_PERCENT * item.ACCH_PROJ_PERCENTTBL_Model.ACCH_PERCENT;
                        var obj = AddOtherAccH(item.ACC_HOLDER_NO, 6, item.PARENT_ACCH, Cost, ProjNo, UnderNo, 1, Note, OpNo++, Box, SCRIP_NO++);
                        list.Add(obj);


                    }

                    db.ACCH_OPBOXTBL_Model.AddRange(list);
                    return_value = db.SaveChanges();

                    if (return_value > 0)
                    {
                        tran.Complete();
                        return true;
                    }

                }
                catch (Exception ex) { throw new Exception(ex.Message); }
                return false;
            }
        }







        public ACCH_OPBOXTBL_Model AddOtherAccH(long AcchNo, int Action_Type, long ParentAcch, double Cost, int ProjectNo, decimal UnderNo, int OpType, string Note, double OpNo, int Box, double SCRIP_NO)
        {


            ACCH_OPBOXTBL_Model aCCH_OPBOXTBL_Model = new ACCH_OPBOXTBL_Model();

            if (Action_Type == 1 && Action_Type == 4 && Action_Type == 6)
            {
                aCCH_OPBOXTBL_Model.OUT = false;
            }
            else
            {
                aCCH_OPBOXTBL_Model.OUT = true;
            }
            aCCH_OPBOXTBL_Model.DATE_M = DateTime.Now;
            // aCCH_OPBOXTBL_Model.INCOME = 0;
            aCCH_OPBOXTBL_Model.UNDER_NO = UnderNo;
            //aCCH_OPBOXTBL_Model.DATE_H = OpBox.DATE_H;
            aCCH_OPBOXTBL_Model.PARENT_ACCH = ParentAcch;
            aCCH_OPBOXTBL_Model.ACC_HOLDER_NO = AcchNo;
            aCCH_OPBOXTBL_Model.ACTION_TYPE = Action_Type;
            aCCH_OPBOXTBL_Model.NOTE = Note;
            // aCCH_OPBOXTBL_Model.OLD_VAL = OpBox.OLD_VAL;
            aCCH_OPBOXTBL_Model.OP_NO = OpNo;
            aCCH_OPBOXTBL_Model.OP_TYPE = OpType;

            aCCH_OPBOXTBL_Model.TARGET_PROJ = ProjectNo;
            aCCH_OPBOXTBL_Model.SOURCE_BOX = Box;
            aCCH_OPBOXTBL_Model.SCRIP_NO = SCRIP_NO;


            aCCH_OPBOXTBL_Model.INCOME = Cost;


            return aCCH_OPBOXTBL_Model;




        }



        public int CreateSpendScript(ACCH_OPBOXTBL_Model item)
        {
            int reusert = 0;

            try
            {
                decimal UnderNo = 0;
                try { UnderNo = db.MAX_UNDER_OPV_Model.Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO; } catch { UnderNo = 1; }

                var OpNo = GetMaxOP_NO();
                item.UNDER_NO = UnderNo;
                item.ID = GetMaxId();
                item.OP_NO = OpNo;
                if (item.ACTION_TYPE == 1 && item.ACTION_TYPE == 4 && item.ACTION_TYPE == 6)
                {
                    item.OUT = false;
                }
                else
                {
                    item.OUT = true;
                }
                db.ACCH_OPBOXTBL_Model.Add(item);

                reusert = db.SaveChanges();

            }
            catch (Exception ex)
            {

            }
            return reusert;
        }

        public List<ACCH_OPBOXTBL_Model> Search(long? ACC_NO, int? PROJECT_NO, DateTime? FromDate, DateTime? ToDate)
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


            List<ACCH_OPBOXTBL_Model> model = new List<ACCH_OPBOXTBL_Model>();
            if (ACC_NO > 0)
            {

                model = db.ACCH_OPBOXTBL_Model.Where(x => x.ACC_HOLDER_NO == ACC_NO && x.DATE_M >= FromDate && x.DATE_M <= ToDate && x.IS_DELETE == false).OrderBy(x => x.DATE_M).ToList();
            }
            if (PROJECT_NO > 0)
            {

                model = model.Where(x => x.TARGET_PROJ == PROJECT_NO).OrderBy(x => x.ACC_HOLDER_NO).ToList();
            }

            return model.OrderBy(x => x.DATE_M).ToList(); ;
        }

        public List<ACCH_OPBOXTBL_Model> OpenBalnce(long ACC_NO, int? PROJECT_NO, DateTime ToDate)
        {


            List<ACCH_OPBOXTBL_Model> model = new List<ACCH_OPBOXTBL_Model>();
            if (ACC_NO > 0)
            {

                model = db.ACCH_OPBOXTBL_Model.Where(x => x.ACC_HOLDER_NO == ACC_NO && x.DATE_M < ToDate && x.IS_DELETE == false).OrderByDescending(x => x.DATE_M).ToList();


                if (PROJECT_NO > 0)
                {

                    model = model.Where(x => x.TARGET_PROJ == PROJECT_NO).OrderBy(x => x.DATE_M).ToList();
                }


            }
            return model;
        }

        public List<ACCH_OPBOXTBL_Model> BoxStatement(long? boxId, int? PROJECT_NO, DateTime? FromDate, DateTime? ToDate)
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


            List<ACCH_OPBOXTBL_Model> model = new List<ACCH_OPBOXTBL_Model>();
            if (boxId > 0)
            {

                model = db.ACCH_OPBOXTBL_Model.Where(x => x.SOURCE_BOX == boxId && x.DATE_M >= FromDate && x.DATE_M <= ToDate && x.IS_DELETE == false).OrderBy(x => x.DATE_M).ToList();
            }
            if (PROJECT_NO > 0)
            {

                model = model.Where(x => x.TARGET_PROJ == PROJECT_NO).OrderBy(x => x.ACC_HOLDER_NO).ToList();
            }

            return model.OrderBy(x => x.DATE_M).ToList(); ;
        }
        public List<ACCH_OPBOXTBL_Model> BoxOpenBalnce(long boxId, int? PROJECT_NO, DateTime ToDate)
        {


            List<ACCH_OPBOXTBL_Model> model = new List<ACCH_OPBOXTBL_Model>();
            if (boxId > 0)
            {

                model = db.ACCH_OPBOXTBL_Model.Where(x => x.SOURCE_BOX == boxId && x.DATE_M < ToDate && x.IS_DELETE == false).OrderByDescending(x => x.DATE_M).ToList();


                if (PROJECT_NO > 0)
                {

                    model = model.Where(x => x.TARGET_PROJ == PROJECT_NO).OrderBy(x => x.DATE_M).ToList();
                }


            }
            return model;
        }




    }


}



