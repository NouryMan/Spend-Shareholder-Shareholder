using Microsoft.AspNet.Identity;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Spend_Web_System.API
{
    [Authorize]
    public class PUY_INVOICEController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAccCenterForProj(long id)
        {
            try
            {
                CENTERSTBL_Business cENTERSTBL = new CENTERSTBL_Business();

                var data = cENTERSTBL.GeCenterAccForProj(id);
                return Ok(new { success = true, Message = "Success", Status = 1, data = data });
            }
            catch
            {
                return Ok(new { success = false, Message = "can not add personal sales_inv", Status = 0, data = "" });
            }
        }

        [HttpGet]
        public IHttpActionResult GetINV_SERIAL([FromUri] int id)
        {

            long result = 0;

            try
            {
                INVOICETBL_Business b = new INVOICETBL_Business();

                result = b.GetMaxSerial(id);



                if (result > 0)
                {


                    return Ok(new { success = true, Message = "Success", Status = 1, data = result });

                }
                else
                {
                    return Ok(new { success = false, Message = "can not ", Status = 0, data = result });

                }
            }
            catch
            {
                return Ok(new { success = false, Message = "can not ", Status = 0, data = result });

            }


        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] INVOICETBL_Model model)
        {


            int result = 0;



            try
            {
                INVOICETBL_Business b = new INVOICETBL_Business();

                model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
                if (model.INV_TYPE == 2)
                {
                    model.INV_WONER = null;
                }
               

                result = b.Create(model);



                if (result > 0)
                {


                    return Ok(new { success = true, Message = "Successfull", Status = 1, data = result });

                }
                else
                {
                    return Ok(new { success = false, Message = "can not add ", Status = 0, data = result });

                }
            }
            catch
            {
                return Ok(new { success = false, Message = "can not add  ", Status = 0, data = result });

            }


        }


        [HttpGet]
        public IHttpActionResult GetINV_Dt([FromUri] long id)
        {

            List<INV_DTTBL_Model> result = new List<INV_DTTBL_Model>(); ;

            try
            {
                INVOICETBL_Business b = new INVOICETBL_Business();

                result = b.GetPyID(id).INV_DTTBL_Collection.ToList();



                if (result.Count() > 0)
                {


                    return Ok(new { success = true, Message = "Success", Status = 1, data = result });

                }
                else
                {
                    return Ok(new { success = false, Message = "can not ", Status = 0, data = result });

                }
            }
            catch
            {
                return Ok(new { success = false, Message = "can not ", Status = 0, data = result });

            }


        }


        [HttpPost]
        public IHttpActionResult Update([FromBody] INVOICETBL_Model model)
        {


            int result = 0;



            try
            {
                INVOICETBL_Business b = new INVOICETBL_Business();

                model.USER_UP = Convert.ToInt16(User.Identity.GetUserId());
                if (model.INV_TYPE == 2)
                {
                    model.INV_WONER = null;
                }


                result = b.Update(model);



                if (result > 0)
                {


                    return Ok(new { success = true, Message = "Successfull", Status = 1, data = result });

                }
                else
                {
                    return Ok(new { success = false, Message = "can not add ", Status = 0, data = result });

                }
            }
            catch
            {
                return Ok(new { success = false, Message = "can not add  ", Status = 0, data = result });

            }


        }


    }
}
