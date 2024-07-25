using PagedList;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class PUY_INVOICETBLController : Controller
    {
        // GET: PUY_INVOICETBL
        public ActionResult Index(int? Page, string search, int? INV_DUC_TYPE, int? trans)
        {
            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type = ducB.GetPyACC_TYPE(100);


            ViewBag.search = search;
            ViewBag.INV_DUC_TYPE = INV_DUC_TYPE;
            ViewBag.trans = trans;

            INVOICETBL_Business b = new INVOICETBL_Business();
            List<INVOICETBL_Model> model = new List<INVOICETBL_Model>();
            if (!String.IsNullOrEmpty(search))
            {

                model = b.getall().Where(x => x.SUPP_INV_NO.ToString().Contains(search) || x.INV_SERIAL.ToString().Contains(search) || x.PUR_COST.ToString().Contains(search)).OrderByDescending(x => x.INV_SERIAL).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.INV_SERIAL).ToList();
            }

            if (INV_DUC_TYPE > 0)
            {

                model = model.Where(x => x.INV_DUC_TYPE == Convert.ToInt16(INV_DUC_TYPE)).ToList();
            }
            if (trans > 0)
            {

                model = model.Where(x => x.INV_TRANSED == Convert.ToInt16(trans)).ToList();
            }



            int pageSize = 10;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));
        }



        [HttpGet]
        public ActionResult Create()
        {
            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type= ducB.GetPyACC_TYPE(100);

            INVOICETBL_Business inv_b = new INVOICETBL_Business();
            ViewBag.Serial = inv_b.GetMaxSerial(DateTime.Now.Year);

            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();

            STAGESTBL_Business STAGES_b = new STAGESTBL_Business();
            ViewBag.STAGES = STAGES_b.getall();

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x => x.OP_ACC == true);

            ITEMTBL_Business ITEMTBL_B = new ITEMTBL_Business();
            ViewBag.ITEMT = ITEMTBL_B.getall();

            MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();
            ViewBag.UNDER_NO = Under_NO_b.getall().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;

            PERSON_ACCTBL_Business pERSONAL = new PERSON_ACCTBL_Business();
            ViewBag.pERSONAL = pERSONAL.getall().Where(x => x.PERSON_DESC_ID == 1);

            return View();
        }


       
        [HttpGet]
        public ActionResult Update(long id)
        {

            INVOICETBL_Business inv_b = new INVOICETBL_Business();
            var model = inv_b.GetPyID(id);


            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type = ducB.GetPyACC_TYPE(100);

            

            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();

            STAGESTBL_Business STAGES_b = new STAGESTBL_Business();
            ViewBag.STAGES = STAGES_b.getall();

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x => x.OP_ACC == true);

            ITEMTBL_Business ITEMTBL_B = new ITEMTBL_Business();
            ViewBag.ITEMT = ITEMTBL_B.getall();

            MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();
            ViewBag.UNDER_NO = Under_NO_b.getall().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;

            PERSON_ACCTBL_Business pERSONAL = new PERSON_ACCTBL_Business();
            ViewBag.pERSONAL = pERSONAL.getall().Where(x => x.PERSON_DESC_ID == 1);

            return View(model);
        }


        [HttpGet]
        public ActionResult Details(long id)
        {

            INVOICETBL_Business inv_b = new INVOICETBL_Business();
            var model = inv_b.GetPyID(id);


            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type = ducB.GetPyACC_TYPE(100);



            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();

            STAGESTBL_Business STAGES_b = new STAGESTBL_Business();
            ViewBag.STAGES = STAGES_b.getall();

            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x => x.OP_ACC == true);

            ITEMTBL_Business ITEMTBL_B = new ITEMTBL_Business();
            ViewBag.ITEMT = ITEMTBL_B.getall();

            MAX_UNDER_OPV_Business Under_NO_b = new MAX_UNDER_OPV_Business();
            ViewBag.UNDER_NO = Under_NO_b.getall().Where(x => x.NAM == "under_no").FirstOrDefault().MAX_NO;

            PERSON_ACCTBL_Business pERSONAL = new PERSON_ACCTBL_Business();
            ViewBag.pERSONAL = pERSONAL.getall().Where(x => x.PERSON_DESC_ID == 1);
            ACCOUNTTBL_Business Accb = new ACCOUNTTBL_Business();
            try
            {
                var ACCOUNT = Accb.GetPuUnderNO(model.UNDER_NO.Value);
                ViewBag.Acc = ACCOUNT;
                ViewBag.FOR_HIM = ACCOUNT.Sum(x => x.FOR_HIM);
                ViewBag.FROM_HIM = ACCOUNT.Sum(x => x.FROM_HIM);
            }
            catch {
                ViewBag.Acc = null;
                ViewBag.FOR_HIM = null;
                ViewBag.FROM_HIM =null;
            }
            return View(model);
        }


        public bool OpenTransed(int? id)
        {
            bool return_result = false;
            INVOICETBL_Business b = new INVOICETBL_Business();
           
            int result = b.OpenTransed(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }

        public ActionResult Report(long? supplier, DateTime? FromDate, DateTime? ToDate, string INV_DUC_TYPE, string trans, int? project)
        {

            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type = ducB.GetPyACC_TYPE(100);

            INVOICETBL_Business b = new INVOICETBL_Business();
            var model = b.Search(supplier, FromDate, ToDate, INV_DUC_TYPE, trans, project);
            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x => x.OP_ACC == true);
            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();

            return View(model);
        }

        public ActionResult TaxReport(long? supplier, DateTime? FromDate, DateTime? ToDate, string INV_DUC_TYPE, string trans, int? project)
        {

            DUC_TYPETBL_Business ducB = new DUC_TYPETBL_Business();
            ViewBag.Duc_Type = ducB.GetPyACC_TYPE(100);

            INVOICETBL_Business b = new INVOICETBL_Business();
            var model = b.Search(supplier, FromDate, ToDate, INV_DUC_TYPE, trans, project);
            ALL_ACC_NOTBL_Business AllAcc_b = new ALL_ACC_NOTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall().Where(x => x.OP_ACC == true);
            PROJECTTBL_Business proj_b = new PROJECTTBL_Business();
            ViewBag.proj = proj_b.getall();

            return View(model);
        }





    }
}