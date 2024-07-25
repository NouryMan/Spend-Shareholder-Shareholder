using Spend.Business;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shareholder_System.API
{
    public class ApiOPERATIONAL_PALANCEController : ApiController
    {
        public IHttpActionResult Add(ListOPERATIONAL_PALANCE_Model model)
        {
           
            try
            {
                OPERATIONAL_PALANCE_Business b = new OPERATIONAL_PALANCE_Business();
              
              long reusert=  b.Create(model.list);
                if (reusert > 0)
                {
                    return Ok(new { success = true, Message = "success", Status = 1, data = reusert });
                }
                else
                {
                    return Ok(new { success = false, Message = "can not add", Status = 0, data = reusert });
                }
               
            }
            catch
            {
                return Ok(new { success = false, Message = "can not add ", Status = 0, data = "" });
            }
        }

        public IHttpActionResult Edit(ListOPERATIONAL_PALANCE_Model model)
        {

            try
            {
                OPERATIONAL_PALANCE_Business b = new OPERATIONAL_PALANCE_Business();

                long reusert = b.Update(model.list);
                if (reusert > 0)
                {
                    return Ok(new { success = true, Message = "success", Status = 1, data = reusert });
                }
                else
                {
                    return Ok(new { success = false, Message = "can not add", Status = 0, data = reusert });
                }

            }
            catch
            {
                return Ok(new { success = false, Message = "can not add ", Status = 0, data = "" });
            }
        }

    }

    public class ListOPERATIONAL_PALANCE_Model
    {
        public List<OPERATIONAL_PALANCE_Model> list { get; set; }
    }
}
