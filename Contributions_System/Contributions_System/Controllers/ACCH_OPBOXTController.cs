﻿using PagedList;
using Spend.Api.Models;
using Spend.Business;
using Spend.Models;
using Spend.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Transactions;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace Contributions_System.Controllers
{
    [Authorize]
    public class ACCH_OPBOXTController : Controller
    {

        public ActionResult Index(string  type,int? Page, string search)
        {
            ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();
       
            IEnumerable<IGrouping<decimal, ACCH_OPBOXTBL_Model>> model;
           
             List<int> typArray=new List<int>();
             List<int> achinTypeArray=new List<int>();

            ViewBag.type = type;


            model = b.GetpyGroup().OrderByDescending(x => x.Key);
            if (type == "block")
            {
                typArray =new List<int> { 3, 7, 5 };
                achinTypeArray =new  List<int> {1};

                model = b.GetpyGroup().Where(x => typArray.Contains(x.FirstOrDefault().OP_TYPE.Value ) && achinTypeArray.Contains(x.FirstOrDefault().ACTION_TYPE.Value));
            }
            else if (type == "distribut")
            {
                typArray = new List<int> {1};
                achinTypeArray = new List<int> {6};

                model = b.GetpyGroup().Where(x => typArray.Contains(x.FirstOrDefault().OP_TYPE.Value) && achinTypeArray.Contains(x.FirstOrDefault().ACTION_TYPE.Value));
            }
          
         

           
            
             if (!String.IsNullOrEmpty(search))
            {
                model =model.Where(x => x.FirstOrDefault().ACC_HOLDERTBL_Model.ACC_HOLDER_NAME.Contains(search));
            }
            



            int pageSize = 50;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Details(decimal UnderNo,string reuternPage)
        {
            ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();
            var model = b.GetAll().Where(x => x.UNDER_NO == UnderNo).ToList();
            ViewBag.reuternPage = reuternPage;
            return View(model);
        }


        public ActionResult SpendProsses()
        {


            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj =new SelectList(proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0), "ID", "PROJECT_AR_NAME");
            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = new SelectList(Box_B.getall(), "BOX_NO", "BOX_NAME");

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = new SelectList(BOX_OP.GetAll(), "OP_NO", "OP_NAME");

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = new SelectList(OPBOX_ACTIONS.GetAll().Where(x => x.ID == 1), "ID", "ACTION_NAME");

            return View();
        }

        [HttpPost]
        public ActionResult SpendProsses(ACCH_OPBOXTBL_Model model)
        {
            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            //if (ModelState.IsValid)
            //{

                try
                {


                    var reusert = aCCH_OPBOXTBL_B.SpendProsses(model);
                    if (reusert!=null)
                    {
                        TempData["reusert"] = reusert;
                        return RedirectToAction("CreateSpend");
                    }
                }
                catch (Exception ex)
                {

                }
          //  }



            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0), "ID", "PROJECT_AR_NAME",model.TARGET_PROJ);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box =  new SelectList(Box_B.getall(), "BOX_NO", "BOX_NAME", model.SOURCE_BOX);

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = new SelectList(BOX_OP.GetAll(), "OP_NO", "OP_NAME", model.OP_NO);

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS =  new SelectList(OPBOX_ACTIONS.GetAll().Where(x => x.ID == 1), "ID", "ACTION_NAME", model.ACTION_TYPE);

           

            return View();
        }


        [HttpGet]
        public ActionResult CreateSpend()
        {

            var model = TempData["reusert"] as AcchOpBoxModelView;
            if (model == null)
            {
                return RedirectToAction("SpendProsses");
            }


            ACC_HOLDERTBL_Business aCC_HOLDERTBL_Business = new ACC_HOLDERTBL_Business();
            ViewBag.Holder = aCC_HOLDERTBL_Business.getall();


            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0), "ID", "PROJECT_AR_NAME",model.ProjectId);
            ViewBag.building = new SelectList(proj_b.GetProjectListByParentIdAsync(model.ProjectId).Result, "ID", "PROJECT_AR_NAME", model.BuildingId);
            ViewBag.unit = new SelectList(proj_b.GetProjectListByParentIdAsync(model.BuildingId).Result, "ID", "PROJECT_AR_NAME", model.UnitId);


            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = BOX_OP.GetAll();

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = OPBOX_ACTIONS.GetAll().Where(x => x.ID == 1);

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSpend(AcchOpBoxModelView model)
        {
            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            MAX_UNDER_OPV_Business mAX_UNDER_OPV_Business = new MAX_UNDER_OPV_Business();
            if (ModelState.IsValid)
            {

                try
                {


                    var scriptNo = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();
                    var opNo = aCCH_OPBOXTBL_B.GetMaxOP_NO();
                    long underNo = 0;
                    try
                    {
                         underNo = mAX_UNDER_OPV_Business.GetAll().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;
                    }
                    catch
                    {

                    }

                    using (TransactionScope tran = new TransactionScope())
                    {

                        foreach (var item in model.AcchOpBoxDetailsModelView)
                        {
                            ACCH_OPBOXTBL_Model aCCH_OPBOXTBL_Model = new ACCH_OPBOXTBL_Model();

                           
                            aCCH_OPBOXTBL_Model.DATE_M = model.Date;
                            aCCH_OPBOXTBL_Model.INCOME = 0;
                            aCCH_OPBOXTBL_Model.UNDER_NO = underNo;
                            aCCH_OPBOXTBL_Model.ACC_HOLDER_NO = item.AccHolderId;
                            aCCH_OPBOXTBL_Model.ACTION_TYPE = model.OpActionTypeId;
                            aCCH_OPBOXTBL_Model.NOTE = model.NOTE;
                            // aCCH_OPBOXTBL_Model.OLD_VAL = model.OLD_VAL;
                            aCCH_OPBOXTBL_Model.OP_NO = opNo++;
                            aCCH_OPBOXTBL_Model.OP_TYPE = model.OpTypeId;
                            aCCH_OPBOXTBL_Model.TARGET_PROJ = model.ProjectId;
                            aCCH_OPBOXTBL_Model.BUILDING_ID = model.BuildingId;
                            aCCH_OPBOXTBL_Model.UNIT_ID = model.UnitId;
                            aCCH_OPBOXTBL_Model.SOURCE_BOX = model.BoxId;
                            aCCH_OPBOXTBL_Model.SCRIP_NO = scriptNo;
                            //  aCCH_OPBOXTBL_Model.PARENT_BOX = model.PARENT_BOX;
                            //  aCCH_OPBOXTBL_Model.PARENT_ACCH = model.PARENT_ACCH;
                            aCCH_OPBOXTBL_Model.SPEND_COST = item.Amount;

                            aCCH_OPBOXTBL_B.Create(aCCH_OPBOXTBL_Model);
                        }
                        tran.Complete();    
                        return RedirectToAction("Details", new { UnderNo = underNo, reuternPage = "block" });
                    }
                    
                }
                catch (Exception ex)
                {

                }
            }

            ACC_HOLDERTBL_Business aCC_HOLDERTBL_Business = new ACC_HOLDERTBL_Business();
            ViewBag.Holder = aCC_HOLDERTBL_Business.getall();


            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0), "ID", "PROJECT_AR_NAME", model.ProjectId);
            ViewBag.building = new SelectList(proj_b.GetProjectListByParentIdAsync(model.ProjectId).Result, "ID", "PROJECT_AR_NAME", model.BuildingId);
            ViewBag.unit = new SelectList(proj_b.GetProjectListByParentIdAsync(model.BuildingId).Result, "ID", "PROJECT_AR_NAME", model.UnitId);


            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = BOX_OP.GetAll();

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = OPBOX_ACTIONS.GetAll().Where(x => x.ID == 1);

            return View(model);
        }











        public ActionResult Trans()
        {
            ACC_HOLDERTBL_Business AccHolderB= new ACC_HOLDERTBL_Business();
            ViewBag.AccHolder= AccHolderB.getall();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result, "ID", "PROJECT_AR_NAME");
            
            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = new SelectList(Box_B.getall(), "BOX_NO", "BOX_NAME");

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = new SelectList(BOX_OP.GetAll(), "OP_NO", "OP_NAME");

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = new SelectList(OPBOX_ACTIONS.GetAll(), "ID", "ACTION_NAME");



            return View(new TransModelView());
        }


        [HttpPost]
        public ActionResult Trans(TransModelView model)
        {
            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            MAX_UNDER_OPV_Business mAX_UNDER_OPV_Business = new MAX_UNDER_OPV_Business();
       
                try
                {
              

                    var scriptNo = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();
                    var opNo = aCCH_OPBOXTBL_B.GetMaxOP_NO();
                    long underNo = 0;
                    try
                    {
                        underNo = mAX_UNDER_OPV_Business.GetAll().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;
                    }
                    catch
                    {

                    }

                    using (TransactionScope tran = new TransactionScope())
                    {

                        foreach (var item in model.ACCH_OPBOXTBLs)
                        {

                            item.DATE_M = model.DATE_M;
                            item.UNDER_NO = underNo;
                            item.ACTION_TYPE = model.ACTION_TYPE;
                            item.NOTE = model.NOTE;
                            item.OP_NO = opNo++;
                            item.OP_TYPE = model.OP_TYPE;
                            item.TARGET_PROJ = model.TARGET_PROJ;
                            item.BUILDING_ID = model.BUILDING_ID;
                            item.UNIT_ID = model.UNIT_ID;
                            item.SOURCE_BOX = model.SOURCE_BOX;
                            item.SCRIP_NO = scriptNo;

                            aCCH_OPBOXTBL_B.Create(item);
                        }

                        tran.Complete();
                        return RedirectToAction("Index");
                    }
                }
                
                catch (Exception ex)
                {

                }
            


            ACC_HOLDERTBL_Business AccHolderB = new ACC_HOLDERTBL_Business();
            ViewBag.AccHolder = AccHolderB.getall();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result, "ID", "PROJECT_AR_NAME");

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = new SelectList(Box_B.getall(), "BOX_NO", "BOX_NAME");

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = new SelectList(BOX_OP.GetAll(), "OP_NO", "OP_NAME");

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = new SelectList(OPBOX_ACTIONS.GetAll(), "ID", "ACTION_NAME");

            return View(model);
        }






        public ActionResult Distribution()
        {
            ACC_HOLDERTBL_Business AccHolderB = new ACC_HOLDERTBL_Business();
            ViewBag.AccHolder = AccHolderB.getall();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.Project = new SelectList(proj_b.GetAllAsync(1).Result.Where(x => x.ACCH_PROJ_BOX_PERCENT_Collection.Count > 0), "ID", "PROJECT_AR_NAME");
            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = new SelectList(Box_B.getall(), "BOX_NO", "BOX_NAME");

            return View();  
        }

        [HttpPost]
        public ActionResult Distribution(DistributionViewModel model)
        {
            try
            {
                if (model.DistributionDetailsViewModel!=null  && model.TotalAmount < model.DistributionDetailsViewModel.Sum(x => x.Amount))
                {
                    ModelState.AddModelError("", "يجب ان يكون صافي المبلغ بالموجب ");
                }
                else
                {

                     ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_Business = new ACCH_OPBOXTBL_Business();

                        var result = aCCH_OPBOXTBL_Business.Distribution(model);
                        if (result > 0)
                        {
                            return RedirectToAction("Details", new { UnderNo = result, reuternPage = "distribut" }); ;


                        }



                    
                }
            }catch(Exception ex) {
                ModelState.AddModelError("",ex.Message);

            }


            ACC_HOLDERTBL_Business AccHolderB = new ACC_HOLDERTBL_Business();
            ViewBag.AccHolder = AccHolderB.getall();
            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.Project = new SelectList(proj_b.GetAllAsync(1).Result.Where(x => x.ACCH_PROJ_BOX_PERCENT_Collection.Count > 0), "ID", "PROJECT_AR_NAME");
            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = new SelectList(Box_B.getall(), "BOX_NO", "BOX_NAME");
            return View(model);
        }






        public ActionResult Edit(int id)
        {
            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_Business = new ACCH_OPBOXTBL_Business();
            var model=aCCH_OPBOXTBL_Business.GetById(id);


            ACC_HOLDERTBL_Business aCC_HOLDERTBL_Business = new ACC_HOLDERTBL_Business();
            ViewBag.Holder = new SelectList(aCC_HOLDERTBL_Business.getall(), "ACC_HOLDER_NO", "ACC_HOLDER_NAME", model.ACC_HOLDER_NO); 

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result, "ID", "PROJECT_AR_NAME", model.TARGET_PROJ);
            ViewBag.building = new SelectList(proj_b.GetProjectListByParentIdAsync(model.TARGET_PROJ).Result, "ID", "PROJECT_AR_NAME", model.BUILDING_ID);
            ViewBag.unit = new SelectList(proj_b.GetProjectListByParentIdAsync(model.BUILDING_ID).Result, "ID", "PROJECT_AR_NAME", model.UNIT_ID);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = new SelectList(Box_B.getall(), "BOX_NO", "BOX_NAME", model.SOURCE_BOX); 

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = new SelectList(BOX_OP.GetAll(), "OP_NO", "OP_NAME", model.OP_TYPE); 

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = new SelectList(OPBOX_ACTIONS.GetAll(), "ID", "ACTION_NAME", model.ACTION_TYPE);

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(ACCH_OPBOXTBL_Model model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_Business = new ACCH_OPBOXTBL_Business();

                    aCCH_OPBOXTBL_Business.Update(model);
                    return RedirectToAction("Details", new { UnderNo=model.UNDER_NO ,type = "block" });

                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);

            }

            ACC_HOLDERTBL_Business aCC_HOLDERTBL_Business = new ACC_HOLDERTBL_Business();
            ViewBag.Holder = new SelectList(aCC_HOLDERTBL_Business.getall(), "ACC_HOLDER_NO", "ACC_HOLDER_NAME", model.ACC_HOLDER_NO);

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = new SelectList(proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0), "ID", "PROJECT_AR_NAME", model.TARGET_PROJ);
            ViewBag.building = new SelectList(proj_b.GetProjectListByParentIdAsync(model.TARGET_PROJ).Result, "ID", "PROJECT_AR_NAME", model.BUILDING_ID);
            ViewBag.unit = new SelectList(proj_b.GetProjectListByParentIdAsync(model.BUILDING_ID).Result, "ID", "PROJECT_AR_NAME", model.UNIT_ID);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = new SelectList(Box_B.getall(), "BOX_NO", "BOX_NAME", model.SOURCE_BOX);

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = new SelectList(BOX_OP.GetAll(), "OP_NO", "OP_NAME", model.OP_TYPE);

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = new SelectList(OPBOX_ACTIONS.GetAll(), "ID", "ACTION_NAME", model.ACTION_TYPE);
            return View(model);
        }




        [Authorize]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            bool result = false;
            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_Business = new ACCH_OPBOXTBL_Business();

            try
            {
                aCCH_OPBOXTBL_Business.Delete(id);
                result = true;
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = result });
        }



        public ActionResult TranseSaleInv(int? ProjNo, int? Transe, DateTime? FromDate, DateTime? ToDate)
        {

            ViewBag.PROJECT_NO = ProjNo;
            ViewBag.Transe = Transe;
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }


            ACCH_PROJECT_Business pROJECTTBL = new ACCH_PROJECT_Business();
            ViewBag.Project = pROJECTTBL.GetAllAsync(1).Result;

            SALES_INVTBL_Business b = new SALES_INVTBL_Business();
            List<SALES_INVTBL_Model> model = new List<SALES_INVTBL_Model>();

            try
            {
                bool boxTrans;
                if (Transe.Value == 1)
                {
                    boxTrans = true;
                }
                else { boxTrans = false; }
                model = b.GetTransForBox(ProjNo.Value, boxTrans, FromDate, ToDate);


            }
            catch
            {

            }

            return View(model);
        }



    }
}