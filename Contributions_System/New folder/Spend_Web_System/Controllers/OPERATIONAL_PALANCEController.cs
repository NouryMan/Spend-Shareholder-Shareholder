using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    public class OPERATIONAL_PALANCEController : Controller
    {
        // GET: OPERATIONAL_PALANCE
        public ActionResult Index()
        {
            OPERATIONAL_PALANCE_Business b = new OPERATIONAL_PALANCE_Business();
            var model = b.GetAll().GroupBy(x=>x.TARGET_PROJ);
            return View(model);
        }


        [HttpGet]
        public ActionResult Create()
        {


            ACC_HOLDERTBL_Business ACCH_B = new ACC_HOLDERTBL_Business();

            PROJECTTBL_Business PROJ_B = new PROJECTTBL_Business();


            var model = ACCH_B.getall();
          
          
            ViewBag.PROJ = PROJ_B.getall().Where(x => x.OPERATIONAL_PALANCE_Collection.Count()<=0);




            return View(model);
        }


        [HttpPost]
        public ActionResult Create(ACC_HOLDERTBL_Model model)
        {
            ACC_HOLDERTBL_Business b = new ACC_HOLDERTBL_Business();
            var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {

                try
                {

                    long reusert = b.Create(model);
                    if (reusert > 0)
                    {
                        return RedirectToAction("Index");
                    }
                }
                catch
                {

                }
            }


            ACC_TREETBL_Business Acc_b = new ACC_TREETBL_Business();
            ACC_TYPETBL_Business bT = new ACC_TYPETBL_Business();
            ACC_NATTBL_Business bN = new ACC_NATTBL_Business();
            ViewBag.Woner_NO = b.GetMaxAcc();
            ViewBag.Parent_ACC = Acc_b.getall().Where(x => x.OP_ACC == false);
            ViewBag.ACC_TYPE = bT.getall();
            ViewBag.ACC_NAT = bN.getall();
            ViewBag.PARENT_ACCH = b.getall();



            return View(model);

        }

        [HttpGet]
        public ActionResult Update(int id)
        {


            OPERATIONAL_PALANCE_Business b = new OPERATIONAL_PALANCE_Business();
           

            var model = b.GetAllPyProject(id);

          
            return View(model);
        }




        public ActionResult Details(int id)
        {
            OPERATIONAL_PALANCE_Business b = new OPERATIONAL_PALANCE_Business();
            var model = b.GetAllPyProject(id);
            return View(model);
        }

        public ActionResult PERCENT_Details(long id)
        {
            PERCENT_EST_Business b = new PERCENT_EST_Business();
            var model = b.GetAll().Where(x=>x.PARENT_ACCH==id);
            return View(model);
        }
    }
}