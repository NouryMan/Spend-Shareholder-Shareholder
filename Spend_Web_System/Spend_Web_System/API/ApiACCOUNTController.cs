using Spend_Web_System.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Spend.Business;
using Spend.Api.Models;
using Microsoft.AspNet.Identity;
using Spend.Models;
using System.Transactions;

namespace Spend_Web_System.API
{
    public class ApiACCOUNTController : ApiController
    {

        public IHttpActionResult AdjustingEntry(TRANS_HD_Model data)
        {

            try
            {
               

                    TRANS_HD_Business b = new TRANS_HD_Business();
                   
                    data.USER_CR= User.Identity.GetUserId();
                    int reusrt = b.AdjustingEntry(data);
                   
                
                if (reusrt == 1000010)
                    {
                        return Ok(new { success = false, Message = " لايمكن الاقفال الدئن يساوي المدين ", Status = 1, data = reusrt });
                    }else if (reusrt > 0)
                    {
                        return Ok(new { success = true, Message = "Success", Status = 1, data = reusrt });
                    }
                    else
                    {
                        return Ok(new { success = false, Message = "هناك خطاء لايمكن الاقفال", Status = 0, data = "" });
                    }

              
            }
            catch
            {
                return Ok(new { success = false, Message = "هناك خطاء لايمكن الاقفال", Status = 0, data = "" });

            }
        }


        [HttpPost]


        public IHttpActionResult SallInvoTranse([FromBody] TRANS_HD_Model tRANS_HD_Model)
        {
            long result = 0;
            try
            {
                var bs = tRANS_HD_Model.ACCOUNTTBL_Collection.FirstOrDefault().IS_BOX_HOLDER;

                //if (ModelState.IsValid)
                //{
                    TRANS_HD_Business b = new TRANS_HD_Business();


                if (tRANS_HD_Model.ACCOUNTTBL_Collection.Sum(x => x.FOR_HIM) == tRANS_HD_Model.ACCOUNTTBL_Collection.Sum(x => x.FROM_HIM))
                {
                    tRANS_HD_Model.USER_CR = User.Identity.GetUserId();
                    result = b.SallInvoTranse(tRANS_HD_Model);
                    if (result > 0)
                    {
                        return Ok(new { success = true, Message = "Success", Status = 1, data = "" });
                    }
                }
                else{
                    return Ok(new { success = false, Message = " يجب وزن القيد", Status = 0, data = "" });
                }
               

                //}

                   
            }
            catch
            {

            }
            
            return Ok(new { success = false, Message = "خطاء لم يتم الترحيل", Status = 0, data = "" });

        }

    }

   
}
