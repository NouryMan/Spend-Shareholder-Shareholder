using Spend.Api.Models;
using Spend.Business;
using Shareholder_System.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shareholder_System.API
{
    public class ApiACCH_OPBOXTController : ApiController
    {

        public IHttpActionResult TranseSaleInv(LIST_API_TRANSESALE_INVO_ViewModel data )
        {

            try
            {
                if (ModelState.IsValid)
                {

                    ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();

                 int reusrt=   b.TranseSaleInv(data.list);
                    if (reusrt > 0)
                    {
                        return Ok(new { success = true, Message = "Success", Status = 1, data = "" });
                    }
                    else
                    {
                        return Ok(new { success = false, Message = "can not add transe sales_inv", Status = 0, data = "" });
                    }

                }
                else {  return Ok(new { success = false, Message = "can not add transe sales_inv", Status = 0, data ="" }); }
            }
            catch
            {
                        return Ok(new { success = false, Message = "can not add transe sales_inv", Status = 0,  data = "" });

            }
        }
    }
}
