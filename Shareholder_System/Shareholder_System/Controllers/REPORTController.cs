using Spend.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shareholder_System.Controllers
{
    [Authorize]
    public class REPORTController : Controller
    {
        // GET: REPORT
        public ActionResult Report()
        {
            ACCH_BALANCE_SUMMARYV_Business b = new ACCH_BALANCE_SUMMARYV_Business();
            var model = b.GetAll().GroupBy(x => x.ACC_HOLDER_NO);

            return View(model);
        }

        public ActionResult Report2(int? id)
        {
            ACC_HOLDERTBL_Business AccHolderB = new ACC_HOLDERTBL_Business();
            ViewBag.AccHolder = AccHolderB.getall();

            ACCH_BALANCE_SUMMARYV_Business b = new ACCH_BALANCE_SUMMARYV_Business();
            var model = b.GetAll().Where(x => x.ACC_HOLDER_NO==id);


            return View(model);
        }

         public ActionResult Report3(int? id)
        {
            PROJECTTBL_Business projB = new PROJECTTBL_Business();
            ViewBag.proj = projB.getall();

            ACCH_BALANCE_SUMMARYV_Business b = new ACCH_BALANCE_SUMMARYV_Business();
            var model = b.GetAll().Where(x => x.TARGET_PROJ==id);


            return View(model);
        }

        public ActionResult Report4()
        {
           
            ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();
            var model = b.GetAll().GroupBy(x=>x.ACC_HOLDER_NO);


            return View(model);
        }

        public ActionResult Report5(long? AccounNo, int? Project,  DateTime? FromDate, DateTime? ToDate)
        {
            ViewBag.Acc = AccounNo;
            ViewBag.PROJECT_NO = Project;
          
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ACC_HOLDERTBL_Business AllAcc_b = new ACC_HOLDERTBL_Business();
            ViewBag.AllAcc = AllAcc_b.getall();

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();

            ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();


            var model = b.Search(AccounNo, Project, FromDate, ToDate);

            //try { model.AddRange(b.SearchByParent(AccounNo, Project, Transe, FromDate, ToDate)); } catch { }
            try
            {
                var OpenBalnce = b.OpenBalnce(AccounNo.Value, Project, FromDate.Value);
                ViewBag.SPEND_COST = Convert.ToDouble((OpenBalnce.Sum(x => x.SPEND_COST)).ToString("0.##"));
                ViewBag.INCOME = Convert.ToDouble((OpenBalnce.Sum(x => x.INCOME)).ToString("0.##"));

            }
            catch
            {
                ViewBag.FOR_HIM = 0;
                ViewBag.FROM_HIM = 0;
            }
          
            return View(model);
        }





    }
}