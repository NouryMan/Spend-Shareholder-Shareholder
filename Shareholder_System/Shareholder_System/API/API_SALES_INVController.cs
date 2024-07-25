
using Microsoft.AspNet.Identity;
using Spend.Api.Models;
using Spend.Business;
using Spend.Models;
using Shareholder_System.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Http;

namespace Shareholder_System.API
{
    [Authorize]
    public class API_SALES_INVController : ApiController
    {

        [HttpPost]

        public IHttpActionResult AddSales_Inv([FromBody] API_INVO_Model model)
        {
            USERSTBL_Business uSERSTBL_ = new USERSTBL_Business();


            long result = 0;
            var user = uSERSTBL_.getall().Where(x => x.LOGIN_NAME == model.login.User && x.LOGIN_PASS == model.login.Password);

            if (user.Count() > 0)
            {
                using (TransactionScope trns = new TransactionScope())
                {

                    PERSONAL_INFOTBL_Business pers_B = new PERSONAL_INFOTBL_Business();
                    var personalinfolist = pers_B.GetAll().Where(x => x.C_ID == model.personal_Info.C_ID);

                    if (personalinfolist.Count() > 0 || model.personal_Info.C_ID == null)
                    {
                        PERSONAL_INFOTBL_Model oldPersonal = new PERSONAL_INFOTBL_Model();
                        oldPersonal = personalinfolist.FirstOrDefault();
                        oldPersonal.AR_NAME = model.personal_Info.AR_NAME;
                        oldPersonal.MOBILE_NO = model.personal_Info.MOBILE_NO;
                        oldPersonal.ADDRESS1 = model.personal_Info.ADDRESS1;
                        oldPersonal.USER_UP = user.FirstOrDefault().USER_ID;


                        //  model.personal_Info.ID = oldPersonal.ID;
                        result = pers_B.Update(oldPersonal);
                    }
                    else
                    {
                        model.personal_Info.USER_CR = user.FirstOrDefault().USER_ID;

                        result = pers_B.Create(model.personal_Info);

                    }









                    if (result > 0)
                    {
                        //add personal Acc


                        model.personal_Info = pers_B.GetByID(result);

                        PERSON_ACCTBL_Business acc_b = new PERSON_ACCTBL_Business();

                        List<PERSON_ACCTBL_Model> ACCList = acc_b.getall().Where(x => x.PERSON_ID == model.personal_Info.ID && x.PERSON_DESC_ID == 5).ToList();
                        if (ACCList.Count() <= 0)
                        {

                            PERSON_ACCTBL_Model PERSON_ACC = new PERSON_ACCTBL_Model();
                            PERSON_ACC.NOTE = "مساهم شقة بواسطة النظام";
                            PERSON_ACC.PARENT_ACC = 110201;
                            PERSON_ACC.ACC_NAT = 1;
                            PERSON_ACC.ACC_TYPE = 1;
                            PERSON_ACC.ACC_LEVEL = 6;
                            PERSON_ACC.OP_ACC = true;
                            PERSON_ACC.PERSON_DESC_ID = 5;
                            PERSON_ACC.PERSON_ID = model.personal_Info.ID;
                            PERSON_ACC.IS_ENABLED = true;


                            PERSON_ACC.PERSON_ID = model.personal_Info.ID;
                            PERSON_ACC.USER_CR = user.FirstOrDefault().USER_ID;

                            result = acc_b.Create(PERSON_ACC);

                        }
                        else
                        {
                            result = ACCList.FirstOrDefault().ID;

                        }







                        if (result > 0)
                        {
                            var ACC = acc_b.GetByID(result);

                            SALES_INVTBL_Business sales_inv_Hd_b = new SALES_INVTBL_Business();
                            model.iNV_HD.PERSON_ACC_ID = ACC.ID;

                            model.iNV_HD.SALES_ACC = 410101;
                            model.iNV_HD.INV_TYPE = 4;
                            model.iNV_HD.INV_DOC_TYPE = 4;
                            model.iNV_HD.USER_CR = user.FirstOrDefault().USER_ID;
                         //   model.iNV_HD.DATE_M = DateTime.Now;


                            result = sales_inv_Hd_b.Create(model.iNV_HD);

                            if (result > 0)
                            {
                                trns.Complete();
                                return Ok(new { success = true, Message = "Success", Status = 1, data = result });

                            }
                            else
                            {
                                return Ok(new { success = false, Message = "can not add personal sales_inv", Status = 0, data = result });

                            }

                        }
                        else
                        {
                            return Ok(new { success = false, Message = "can not add personal Acc", Status = 0, data = result });
                        }



                    }
                    else
                    {
                        return Ok(new { success = false, Message = "can not add personal info", Status = 0, data = result });
                    }



                }


            }
            else
            {
                return Ok(new { success = false, Message = "can not login", Status = 0, data = result });

            }






        }


        [HttpPost]


        public IHttpActionResult Add([FromBody] SALES_INVTBL_Model model)
        {


            long result = 0;




            SALES_INVTBL_Business sales_inv_Hd_b = new SALES_INVTBL_Business();
          

            model.USER_CR = Convert.ToInt16(User.Identity.GetUserId());

            model.QRCODE = QrCode.encodeQrText("مجموعة شركة جدة الأولى للاستثمارات العقارية", "310166139600003", model.DATE_M.Value.ToString("u"), model.INV_PUR.ToString(), model.VAT_AMOUNT.ToString());
          
            foreach (var item in model.SALES_INVDTTBL_Collection)
            {
                item.USER_CR = model.USER_CR;
                item.DATE_M = model.DATE_M;
                item.DATE_H = model.DATE_M;
         

                // item.PUR_PRICE = model.PRICE + model.VAT_AMOUNT;

            }


            result = sales_inv_Hd_b.Create(model);

            if (result > 0)
            {
                SALES_INVTBL_Business b = new SALES_INVTBL_Business();

                return Ok(new { success = true, Message = "Success", Status = 1, data = b.GetPyID(result) });

            }
            else
            {
                return Ok(new { success = false, Message = "can not add personal sales_inv", Status = 0, data = result });

            }



        }


        [HttpPost]


        public IHttpActionResult Edite([FromBody] SALES_INVTBL_Model model)
        {


            long result = 0;




            SALES_INVTBL_Business sales_inv_Hd_b = new SALES_INVTBL_Business();


            model.USER_UP = Convert.ToInt16(User.Identity.GetUserId());

            model.QRCODE = QrCode.encodeQrText("مجموعة شركة جدة الأولى للاستثمارات العقارية", "310166139600003", model.DATE_M.Value.ToString("u"), model.INV_PUR.ToString(), model.VAT_AMOUNT.ToString());

            foreach (var item in model.SALES_INVDTTBL_Collection)
            {
                item.USER_CR = model.USER_CR;
                item.DATE_M = model.DATE_M;
                item.DATE_H = model.DATE_M;


                // item.PUR_PRICE = model.PRICE + model.VAT_AMOUNT;

            }


            result = sales_inv_Hd_b.Update(model);

            if (result > 0)
            {
                SALES_INVTBL_Business b = new SALES_INVTBL_Business();

                return Ok(new { success = true, Message = "Success", Status = 1, data = b.GetPyID(result) });

            }
            else
            {
                return Ok(new { success = false, Message = "can not add personal sales_inv", Status = 0, data = result });

            }



        }



        [HttpGet]

        public IHttpActionResult GetSales_Inv([FromUri] int id)
        {

            try { 

            SALES_INVTBL_Business sales_inv_Hd_b = new SALES_INVTBL_Business();

                var data = sales_inv_Hd_b.GetPyID(id);


                return Ok(new { success = true, Message = "Success", Status = 1, data = data });

            }
            catch
            {
                return Ok(new { success = false, Message = "can not add personal sales_inv", Status = 0, data = "" });

            }
        }

        [HttpGet]
        public IHttpActionResult GetINV_Dt([FromUri] int id)
        {

            List<SALES_INVDTTBL_Model> result = new List<SALES_INVDTTBL_Model>(); ;

            try
            {
                SALES_INVTBL_Business b = new SALES_INVTBL_Business();

                result = b.GetPyID(id).SALES_INVDTTBL_Collection.ToList();



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


        [HttpGet]
        public IHttpActionResult GetINV_SERIAL([FromUri] int id)
        {

            long result = 0;

            try
            {
                SALES_INVTBL_Business b= new SALES_INVTBL_Business();

                result = b.GetMaxINV_NO(id);



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



