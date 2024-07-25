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
    public class ROLEController : Controller
    {
        // GET: ROLE
        [HttpGet]
        [Authorize(Roles = "Role_Select")]
        public ActionResult Index()
        {
            ROLETBL_Business ROLE_B = new ROLETBL_Business();
            var model = ROLE_B.getall();
            return View(model);
         
        }
        [HttpGet]
        [Authorize(Roles = "Role_Create")]
        public ActionResult Create()
        {
            FEATURETBL_Business FEATURE_B = new FEATURETBL_Business();
            ViewBag.feature = FEATURE_B.getall();
            return View();

        }
        [HttpPost]
       [Authorize(Roles = "Role_Create")]
        public ActionResult Create(ROLETBL_Model Role,string feature)
        {
            ROLETBL_Business Roll_B = new ROLETBL_Business();

            if (ModelState.IsValid)
            {
               
                int reusrt = Roll_B.Create(Role, feature);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            FEATURETBL_Business FEATURE_B = new FEATURETBL_Business();
            ViewBag.feature = FEATURE_B.getall();
            return View(Role);

        }
        [HttpGet]
        [Authorize(Roles = "Role_Update")]
        public ActionResult Edit(int id)
        {
            ROLETBL_Business Roll_B = new ROLETBL_Business();
            FEATURETBL_Business FEATURE_B = new FEATURETBL_Business();
            var model = Roll_B.GetPyID(id);
            ViewBag.feature = FEATURE_B.getall();
            return View(model);

        }

        [HttpPost]
        [Authorize(Roles = "Role_Update")]
        public ActionResult Edit(ROLETBL_Model Role, string feature)
        {
            ROLETBL_Business Roll_B = new ROLETBL_Business();

            if (ModelState.IsValid)
            {
               
                int reusrt =  Roll_B.Update(Role, feature);
                if (reusrt > 0)
                {
                    return RedirectToAction("Index");
                }

            }
            FEATURETBL_Business FEATURE_B = new FEATURETBL_Business();
            ViewBag.feature = FEATURE_B.getall();
            var model = Roll_B.GetPyID(Role.ID);

            return View(model);

        }

    }
}