using Microsoft.Azure.Amqp.Framing;
using PagedList;
using Rotativa.MVC;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class SALES_INVTBLController : Controller
    {
        // GET: SALES_INVTBL
        [Authorize(Roles = "User_Select")]
       
        public ActionResult Index(int? Page, string search)
        {

            SALES_INVTBL_Business b = new SALES_INVTBL_Business();

            List<SALES_INVTBL_Model> model = new List<SALES_INVTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.NOTE.Contains(search)).OrderByDescending(x => x.INV_NO).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.INV_NO).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }




        [Authorize(Roles = "User_Select")]
        public ActionResult Create ()
        {
            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type = ducB.GetPyACC_TYPE(300);

            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();
            PARTTBL_Business Part_b = new PARTTBL_Business();
            ViewBag.Part_b = Part_b.getall();
            UNITTBL_Business Unit_b = new UNITTBL_Business();
            ViewBag.Unitt_b = Unit_b.getall();

            SALES_CUSTTBL_Business CUST_b = new SALES_CUSTTBL_Business();
            ViewBag.CUST = CUST_b.getall();

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x => x.OP_ACC == true);
            VATTBL_Business VAT_b = new VATTBL_Business();
            ViewBag.VAT = VAT_b.getall();
            MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();
            ViewBag.UNDER_NO = Under_NO_b.getall().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;

            SALES_INVTBL_Business inv_b = new SALES_INVTBL_Business();
            ViewBag.INV_NO = inv_b.GetMaxINV_NO(DateTime.Now.Year);

            SCRIP_OPTYPETBL_Business SCRIP_OPTYPE = new SCRIP_OPTYPETBL_Business();
            ViewBag.SCRIP_OPTYPE = SCRIP_OPTYPE.getall();

            return View();
        }


        public ActionResult Inv_Details(int id)
        {
            SALES_INVTBL_Business b = new SALES_INVTBL_Business();

            var model = b.GetPyID(id);
            return View(model);
        }


        public ActionResult Update(int id)
        {
           

            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type = ducB.GetPyACC_TYPE(300);
            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();
            PARTTBL_Business Part_b = new PARTTBL_Business();
            ViewBag.Part_b = Part_b.getall();
            UNITTBL_Business Unit_b = new UNITTBL_Business();
            ViewBag.Unitt_b = Unit_b.getall();
            SALES_CUSTTBL_Business CUST_b = new SALES_CUSTTBL_Business();
            ViewBag.CUST = CUST_b.getall();
            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x => x.OP_ACC == true);
            VATTBL_Business VAT_b = new VATTBL_Business();
            ViewBag.VAT = VAT_b.getall();
           
            SALES_INVTBL_Business inv_b = new SALES_INVTBL_Business();
          
            SCRIP_OPTYPETBL_Business SCRIP_OPTYPE = new SCRIP_OPTYPETBL_Business();
            ViewBag.SCRIP_OPTYPE = SCRIP_OPTYPE.getall();

            var model = inv_b.GetPyID(id);
            return View(model);

        }

        public ActionResult PrintInvoToPdf(int id)
        {

            var report = new ActionAsPdf("Inv_pdf", new { id = id })  ;
            return report;
        }

        public ActionResult Inv_pdf(int id)
        {
            SALES_INVTBL_Business b = new SALES_INVTBL_Business();

            var model = b.GetPyID(id);
            return View(model);
        }

        public ActionResult PrintPartialViewToPdf(int id)
        {
            SALES_INVTBL_Business b = new SALES_INVTBL_Business();

            var model = b.GetPyID(id);
            var report = new PartialViewAsPdf("~/Views/SALES_INVTBL/_Part_Inv_pdf.cshtml", model) { 
                
            
            };
            return report;


        }

        public ActionResult _Part1_Inv_pdf(int id)
        {
            
            return View();
        }

        public ActionResult InvoTranse(int id)
        {

            SALES_INVTBL_Business b = new SALES_INVTBL_Business();
            ALL_ACC_NOTBL_Business aLL_ACC_NOTBL_Business = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = aLL_ACC_NOTBL_Business.getall().Where(x => x.OP_ACC == true);
            var model = b.GetPyID(id);
            return View(model);
        }


        public ActionResult Report(long? custId, DateTime? FromDate, DateTime? ToDate, string INV_DUC_TYPE, string trans, int? project)
        {
            ViewBag.CustId = null;
            try { ViewBag.CustId = custId; } catch { }

            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type = ducB.GetPyACC_TYPE(300);

            SALES_INVTBL_Business b = new SALES_INVTBL_Business();
            var model = b.Search(custId, FromDate, ToDate, INV_DUC_TYPE, trans, project);

            SALES_CUSTTBL_Business CUST_b = new SALES_CUSTTBL_Business();
            ViewBag.CUST = CUST_b.getall();

           
            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();

            return View(model);
        }

        public bool OpenTransed(long id)
        {
            bool return_result = false;
            SALES_INVTBL_Business b = new SALES_INVTBL_Business();

            int result = b.OpenTransed(id);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }
    }
}