using PagedList;
using Spend.Business;
using Spend.Models;
using Spend_Web_System.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class BoxController : Controller
    {
        public ActionResult Index(int? Page, string search)
        {

            BOXTBL_Business b = new BOXTBL_Business();

            List<BOXTBL_Model> model = new List<BOXTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x.BOX_NAME.Contains(search)).OrderByDescending(x => x.BOX_NO).ToList();
            }
            else
            {
                model = b.getall().OrderByDescending(x => x.BOX_NO).ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }


        [HttpGet]
        public ActionResult Create()
        {
            BOXTBL_Business b = new BOXTBL_Business();
            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();


            ViewBag.MaxBoxNo = b.GetMaxNo();
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();

            return View();
        }


        [HttpPost]
        public ActionResult Create(BOXTBL_Model model)
        {
            BOXTBL_Business b = new BOXTBL_Business();
            if (ModelState.IsValid)
            {



                try
                {
                    Date d = new Date();
                    model.CR_DATEM = DateTime.Now;
                   // model.CR_DATEH =Convert.ToDateTime(d.GregToHijri(DateTime.Now.ToShortDateString()));
                    long reusert = b.Create(model);
                    if (reusert > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                }
            }

            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();


            ViewBag.MaxBoxNo = b.GetMaxNo();
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();

            

            return View(model);

        }



        [HttpGet]
        public ActionResult Edit(int id)
        {
            BOXTBL_Business b = new BOXTBL_Business();
            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();

            var model = b.GetPyID(id);

           
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();

            return View(model);
        }


        [HttpPost]
        public ActionResult Edit(BOXTBL_Model model)
        {
            BOXTBL_Business b = new BOXTBL_Business();
            if (ModelState.IsValid)
            {



                try
                {
                     long reusert = b.Update(model);
                    if (reusert > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {

                }
            }

            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();


            ViewBag.MaxBoxNo = b.GetMaxNo();
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();



            return View(model);

        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            BOXTBL_Business b = new BOXTBL_Business();
            BOX_TRANS_OPTYPES_Bussiness BOX_TRANS_OPTYPES_B = new BOX_TRANS_OPTYPES_Bussiness();
            BOX_OPTBL_Business BOX_OPTBL_B = new BOX_OPTBL_Business();

            var model = b.GetPyID(id);

            ViewBag.MaxBoxNo = b.GetMaxNo();
            ViewBag.BoxType = BOX_TRANS_OPTYPES_B.GetAll();
            ViewBag.BoxOp = BOX_OPTBL_B.GetAll();

            return View(model);
        }


        public bool delete(int? id)
        {
            bool return_result = false;
            BOXTBL_Business boxB = new BOXTBL_Business();

            int result = boxB.Delete(id.Value);
            if (result == 1)
            {
                return_result = true;
            }


            return return_result;
        }


    }
}