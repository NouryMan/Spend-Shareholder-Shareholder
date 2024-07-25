
using Microsoft.AspNet.Identity;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace callcenter.all.Areas.Spend.Api
{
    [Authorize]
    public class Api_Spend_scriptblController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Addspend_scriptbl([FromBody] SPEND_SCRIPTBL_Model model)
        {


            int result = 0;



                try
                {
                    SPEND_SCRIPTBL_Business spend_script_B = new SPEND_SCRIPTBL_Business();
                   
                    model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());

                result = spend_script_B.create(model);



                    if (result > 0)
                    {


                        return Ok(new { success = true, Message = "Successfull", Status = 1, data = result });

                    }
                    else
                    {
                        return Ok(new { success = false, Message = "can not add ", Status = 0, data = result });

                    }
                }
                catch (Exception ex)
                {
                    return Ok(new { success = false, Message = "can not add  ", Status = 0, data = result });

                }


            }



        [HttpGet]
        public IHttpActionResult GetScript_NO([FromUri] int id,[FromUri]int type)
        {

          long  result = 0;

            try
            {
                SPEND_SCRIPTBL_Business spend_script_B = new SPEND_SCRIPTBL_Business();

                result = spend_script_B.getMaxNO(id.ToString(),type);



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



    }
}
