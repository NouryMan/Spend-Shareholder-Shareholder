using Microsoft.AspNet.Identity;
using PagedList;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Spend_Web_System.Controllers
{
    [Authorize]
    public class ITEMController : Controller
    {
        // GET: ITEMT
        public ActionResult Index(int? Page, string search)
        {

    


            ITEMTBL_Business b = new ITEMTBL_Business();

            List<ITEMTBL_Model> model = new List<ITEMTBL_Model>();

            if (!String.IsNullOrEmpty(search))
            {
                model = b.getall().Where(x => x?.ITEM_NAME !=null && x.ITEM_NAME.Contains(search) || (x?.ITEM_NO != null && x.ITEM_NO.ToString().Contains(search))).ToList();
            }
            else
            {
                model = b.getall().ToList();
            }



            int pageSize = 30;
            int pageNumber = (Page ?? 1);
            return View(model.ToPagedList(pageNumber, pageSize));

        }

        [HttpGet]
        public ActionResult Create()
        {
           ITEMTBL_Business b = new ITEMTBL_Business();
            ViewBag.Parent_item = b.getall().Where(x => x.IS_ENABLED);

            return View();
        }


        [HttpPost]
        public ActionResult Create(ITEMTBL_Model model)
        {
            ITEMTBL_Business b = new ITEMTBL_Business();
            if (ModelState.IsValid)
            {

                try
                {
                    model.USER_CR= Convert.ToInt16(User.Identity.GetUserId());
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

            ViewBag.Parent_item = b.getall().Where(x => x.IS_ENABLED);
            return View(model);

        }


        [HttpGet]
        public ActionResult Update(long ItemNo,long ParentItemNo)
        {
            ITEMTBL_Business b = new ITEMTBL_Business();
            var model = b.GetPyID(ItemNo, ParentItemNo);
            ViewBag.Parent_item = b.getall();

            return View(model);
        }


        [HttpPost]
        public ActionResult Update(ITEMTBL_Model model)
        {
            ITEMTBL_Business b = new ITEMTBL_Business();
            if (ModelState.IsValid)
            {

                try
                {
                    model.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
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

            ViewBag.Parent_item = b.getall();
            return View(model);

        }


        [HttpGet]
        public ActionResult Details(long ItemNo, long ParentItemNo)
        {
            ITEMTBL_Business b = new ITEMTBL_Business();
            var model = b.GetPyID(ItemNo, ParentItemNo);
            ViewBag.Parent_item = b.getall();

            return View(model);
        }

        public ActionResult  ItemMovement(long? ItemNo, int? Project,  DateTime? FromDate, DateTime? ToDate)
        {
            ViewBag.ItemNo = ItemNo;
            ViewBag.PROJECT_NO = Project;
           
            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ITEMTBL_Business IitemB = new ITEMTBL_Business();
            ViewBag.AllItemNo = IitemB.getall();

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();
            var model = new List<INV_DTTBL_Model>();
            try
            {

                 model = IitemB.ItemMovement(ItemNo.Value, Project, FromDate, ToDate);
            }
            catch
            {

            }
           
           
            return View(model);
        }

        public ActionResult ItemQuantity(long? ItemNo, long? ItemParent, int? Project, DateTime? FromDate, DateTime? ToDate)
        {
            ViewBag.ItemNo = ItemNo;
            ViewBag.ItemParent = ItemParent;
            ViewBag.PROJECT_NO = Project;

            try { ViewBag.FromDate = FromDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.FromDate = DateTime.Now.ToString("yyyy-MM-dd"); }
            try { ViewBag.ToDate = ToDate.Value.ToString("yyyy-MM-dd"); } catch { ViewBag.ToDate = DateTime.Now.ToString("yyyy-MM-dd"); }

            ITEMTBL_Business IitemB = new ITEMTBL_Business();
            ViewBag.AllItemNo = IitemB.getall();

            PROJECTTBL_Business pROJECTTBL = new PROJECTTBL_Business();
            ViewBag.Project = pROJECTTBL.getall();

            var model = new List<ITEMTBL_Model>();
            try
            {

                model = IitemB.ItemQuantity(ItemNo, ItemParent, Project, FromDate, ToDate);
            }
            catch
            {

            }


            return View(model);
        }


    }
}