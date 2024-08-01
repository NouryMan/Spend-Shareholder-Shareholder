using PagedList;
using Spend.Api.Models;
using Spend.Business;
using Spend.Models;
using Spend.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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


        public ActionResult SpendProsses()
        {


            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = BOX_OP.GetAll();

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = OPBOX_ACTIONS.GetAll();

            return View();
        }

        [HttpPost]
        public ActionResult SpendProsses(ACCH_OPBOXTBL_Model model)
        {
            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            if (ModelState.IsValid)
            {

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
            }



            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = BOX_OP.GetAll();

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = OPBOX_ACTIONS.GetAll();

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
            ViewBag.proj = proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0);

            BOXTBL_Business Box_B = new BOXTBL_Business();
            ViewBag.Box = Box_B.getall();

            BOX_OPTBL_Business BOX_OP = new BOX_OPTBL_Business();
            ViewBag.BOX_OP = BOX_OP.GetAll();

            ACCH_OPBOX_ACTIONSTBL_Business OPBOX_ACTIONS = new ACCH_OPBOX_ACTIONSTBL_Business();
            ViewBag.OPBOX_ACTIONS = OPBOX_ACTIONS.GetAll();

            return View(model);
        }

        [HttpPost]
        public ActionResult CreateSpend(AcchOpBoxModelView model)
        {
            ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_B = new ACCH_OPBOXTBL_Business();
            if (ModelState.IsValid)
            {

                try
                {


                    //var reusert = aCCH_OPBOXTBL_B.SpendProsses(model);
                    //if (reusert != null)
                    //{
                    //    return RedirectToAction("Index", new { type = "block" });
                    //}
                }
                catch (Exception ex)
                {

                }
            }



            ViewBag.SCRIP_NO = aCCH_OPBOXTBL_B.GetMaxSCRIP_NO();

            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = proj_b.GetAllAsync(1).Result.Where(x => x.OPERATIONAL_PALANCE_Collection.Count() > 0);

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


        public ActionResult Trans()
        {
            ACC_HOLDERTBL_Business AccHolderB= new ACC_HOLDERTBL_Business();

            ViewBag.AccHolder= AccHolderB.getall();
            ACCH_PROJECT_Business proj_b = new ACCH_PROJECT_Business();
            ViewBag.proj = proj_b.GetAllAsync(1).Result;



            return View();
        }



        public ActionResult Distribution()
        {
            ACCH_PROJECT_Business pROJECTTBL = new ACCH_PROJECT_Business();
            ViewBag.Project = pROJECTTBL.GetAllAsync(1).Result.Where(x=>x.ACCH_PROJ_BOX_PERCENT_Collection.Count>0);
            return View();  
        }

        [HttpPost]
        public ActionResult Distribution(API_TRANSESALE_INVO_Model model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ACCH_OPBOXTBL_Business aCCH_OPBOXTBL_Business = new ACCH_OPBOXTBL_Business();


                    if (aCCH_OPBOXTBL_Business.Distribution(model))
                    {
                        return RedirectToAction("Index");
                    }


                }
            }catch(Exception ex) {
                ModelState.AddModelError("",ex.Message);

            }
            ACCH_PROJECT_Business pROJECTTBL = new ACCH_PROJECT_Business();
            ViewBag.Project = pROJECTTBL.GetAllAsync(1).Result.Where(x => x.ACCH_PROJ_BOX_PERCENT_Collection.Count > 0);
            return View(model);
        }







    }
}