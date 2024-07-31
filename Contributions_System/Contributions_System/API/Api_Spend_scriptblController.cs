
using Contributions_System.API.Models;
using Microsoft.AspNet.Identity;
using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace callcenter.all.Areas.Spend.Api
{
    [Authorize]
    public class Api_Spend_scriptblController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Addspend_scriptbl([FromBody] SPEND_SCRIPTBL_Model model)
        {


            long result = 0;



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


        [HttpPost]
        public IHttpActionResult Addspend_scriptblWhitAccholder([FromBody] OPBOX_SPEND_SCRIPTBL_Model model)
        {


            long result = 1;


            
            try
            {
                SPEND_SCRIPTBL_Business spend_script_B = new SPEND_SCRIPTBL_Business();
                ACCH_OPBOXTBL_Business b = new ACCH_OPBOXTBL_Business();
                ACC_HOLDERTBL_Business ACcHb = new ACC_HOLDERTBL_Business();

                model.SPEND_SCRIPTBL_Model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());
               
                  //  result = spend_script_B.create(model.SPEND_SCRIPTBL_Model);
                //   var script = spend_script_B.GetPyID(result);

                if (result > 0)
                {
                    model.ACCH_OPBOXTBL_Model.DATE_M = DateTime.Now;
                    //aCCH_OPBOXTBL_Model.ACC_HOLDER_NO = model.ACCH_OPBOXTBL_Model.a.Value;
                    model.ACCH_OPBOXTBL_Model.TARGET_PROJ = model.SPEND_SCRIPTBL_Model.PROJECT_NO;
                    model.ACCH_OPBOXTBL_Model.NOTE = model.SPEND_SCRIPTBL_Model.REMARK;
                    model.ACCH_OPBOXTBL_Model.PARENT_ACCH = ACcHb.GetById(model.ACCH_OPBOXTBL_Model.ACC_HOLDER_NO).PARENT_ACCH;
                    //  model.ACCH_OPBOXTBL_Model.SCRIP_NO = script.SCRIP_NO;
                    //  model.ACCH_OPBOXTBL_Model.UNDER_NO = script.UNDER_NO.Value;
                    model.ACCH_OPBOXTBL_Model.SCRIP_NO = 0;
                    model.ACCH_OPBOXTBL_Model.UNDER_NO = 55665;
                    if (model.SPEND_SCRIPTBL_Model.SCRIPT_TYPE == 1)
                    {
                        model.ACCH_OPBOXTBL_Model.SPEND_COST = Convert.ToDouble(model.SPEND_SCRIPTBL_Model.COST);
                    }
                    else if (model.SPEND_SCRIPTBL_Model.SCRIPT_TYPE == 2)
                    {
                        model.ACCH_OPBOXTBL_Model.INCOME = Convert.ToDouble(model.SPEND_SCRIPTBL_Model.COST);
                    }


                    result = b.CreateSpendScript(model.ACCH_OPBOXTBL_Model);
                }

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
