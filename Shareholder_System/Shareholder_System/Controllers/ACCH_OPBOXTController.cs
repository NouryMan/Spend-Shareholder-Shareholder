﻿using PagedList;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shareholder_System.Controllers
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
                achinTypeArray =new  List<int> { 4, 1, 6 };

                model = b.GetpyGroup().Where(x => typArray.Contains(x.FirstOrDefault().OP_TYPE.Value));
            }
            else if (type == "distribut")
            {
                typArray = new List<int> {6};
                achinTypeArray = new List<int> {1};

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


        public ActionResult Create()
        {


            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall().Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = BOX_OP.GetAll();

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = OPBOX_ACTIONS.GetAll();

            return View();
        }

        [HttpPost]
        public ActionResult Create(ACCH_OPBOXTBL_Model model)
        {
            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            if (ModelState.IsValid)
            {

                try
                {


                    int reusert = aCCH_OPBOXTBL_B.Create(model);
                    if (reusert > 0)
                    {
                        return RedirectToAction("Index", new {type= "block" });
                    }
                }
                catch (Exception ex)
                {

                }
            }



            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall().Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = BOX_OP.GetAll();

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = OPBOX_ACTIONS.GetAll();

            return View();
        }


        public ActionResult TranseSaleInv(int? ProjNo, int? Transe, DateTime? FromDate, DateTime? ToDate)
        {

            ViewBag.PROJECT_NO = ProjNo;
            ViewBag.Transe = Transe;
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }


            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();

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


        public ActionResult Trans()
        {
            ACC_HOLDERTBL_Business AccHolderB= new ACC_HOLDERTBL_Business();

            ViewBag.AccHolder= AccHolderB.getall();


            return View();
        }
    }
}