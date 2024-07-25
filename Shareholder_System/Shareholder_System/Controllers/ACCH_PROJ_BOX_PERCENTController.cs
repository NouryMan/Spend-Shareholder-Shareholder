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
    public class ACCH_PROJ_BOX_PERCENTController : Controller
    {
        // GET: ACCH_PROJ_BOX_PERCENT
        public ActionResult Index()
        {
            ACCH_PROJ_PERCENTTBL_Business b = new ACCH_PROJ_PERCENTTBL_Business();

            var model = b.GetAll().GroupBy(x => x.PROJ_NO);

            return View(model);
        }
        public ActionResult Details(int id)
        {
            ACCH_PROJ_PERCENTTBL_Business b = new ACCH_PROJ_PERCENTTBL_Business();
            var model = b.GetAllPyProject(id);
            return View(model);
        }

        public ActionResult PERCENT_Details(long id)
        {
            ACCH_PROJ_BOX_PERCENTTBL_Business b = new ACCH_PROJ_BOX_PERCENTTBL_Business();
            var model = b.GetAll().Where(x => x.PARENT_ACCH == id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {


         
            PROJECTTBL_Business PROJ_B = new PROJECTTBL_Business();

          
            
            ViewBag.PROJ = PROJ_B.getall().Where(x => x.ACCH_PROJ_BOX_PERCENT_Collection.Count() <= 0); 
            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();



            return View();
        }


        [HttpPost]
        public ActionResult Create(ACCH_PROJ_PERCENTTBL_Model model)
        {
            ACCH_PROJ_PERCENTTBL_Business b = new ACCH_PROJ_PERCENTTBL_Business();
       

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


            PROJECTTBL_Business PROJ_B = new PROJECTTBL_Business();

            ViewBag.PROJ = PROJ_B.getall().Where(x => x.ACCH_PROJ_BOX_PERCENT_Collection.Count() <= 0);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            return View(model);

        }


    }
}