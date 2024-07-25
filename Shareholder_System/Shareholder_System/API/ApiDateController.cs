using Shareholder_System.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shareholder_System.API
{
    public class ApiDateController : ApiController
    {
        [HttpGet]

        public IHttpActionResult GetDate([FromUri] string date)
        {
            string data = "is avild";
            try
            {
                Date dateH = new Date();
               
                if (dateH.IsGreg(date))
                {
                    data = dateH.GregToHijri(date);
                }
                else if(dateH.IsHijri(date))
                {
                    data = dateH.HijriToGreg(date);
                }
                else
                {
                    return Ok(new { success = true, Message = "Success", Status = 1, data = "التاريخ غير معروف" });
                }

                return Ok(new { success = true, Message = "Success", Status = 1, data = data });

            }
            catch
            {
                return Ok(new { success = false, Message = "can not add personal sales_inv", Status = 0, data = data });

            }
        }


    }
}
