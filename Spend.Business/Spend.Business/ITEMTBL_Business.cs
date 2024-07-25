using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Spend.Business
{

    public class ITEMTBL_Business
    {
        oracleDbContextSpend db;
        public ITEMTBL_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<ITEMTBL_Model> getall()
        {
            List<ITEMTBL_Model> obj = db.ITEMTBL_Model.ToList();




            return obj;
        }
        public ITEMTBL_Model GetPyID(long id, long type)
        {
            ITEMTBL_Model obj = db.ITEMTBL_Model.Find(id);


            return obj;
        }


        public long GetMaxItemNo(long? ItemNoParent)
        {
            if (ItemNoParent == null)
            {
                try
                {
                    return db.ITEMTBL_Model.Where(x => x.PARENT_ITEM == null).Max(x => x.ITEM_NO) + 1;
                }
                catch
                {
                    return 1;
                }
            }
            else
            {
                try
                {
                    return db.ITEMTBL_Model.Where(x => x.PARENT_ITEM == ItemNoParent).Max(x => x.ITEM_NO) + 1;
                }
                catch
                {
                    return db.ITEMTBL_Model.Find(ItemNoParent).ITEM_NO * 1000 + 1;
                }
            }


        }

        public long Create(ITEMTBL_Model item)
        {

            item.ITEM_NO = GetMaxItemNo(item.PARENT_ITEM);
            item.CR_DATE = DateTime.Now;
            if (item.PARENT_ITEM > 0)
            {
                item.LVL = db.ITEMTBL_Model.Find(item.PARENT_ITEM).LVL + 1;
            }
            else
            {
                item.LVL = 1;
            }



            db.ITEMTBL_Model.Add(item);

            long return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = item.ITEM_NO;
            }
            return return_value;
        }

        public int Update(ITEMTBL_Model item)
        {
            item.UP_DATE = DateTime.Now;
            if (item.PARENT_ITEM > 0)
            {
                item.LVL = db.ITEMTBL_Model.Find(item.PARENT_ITEM).LVL + 1;
            }
            else
            {
                item.LVL = 1;
            }


            db.Entry(item).State = EntityState.Modified;
            //  db.Entry(CREDENCE_DTTBL).Property(x => x.IS_DELETED).IsModified = false;
            db.Entry(item).Property(x => x.USER_CR).IsModified = false;
            db.Entry(item).Property(x => x.CR_DATE).IsModified = false;

            int return_value = db.SaveChanges();
            return return_value;
        }

        public List<INV_DTTBL_Model> ItemMovement(long ItemNo, int? PROJECT_NO, DateTime? FromDate, DateTime? ToDate)
        {

            if (ToDate == null)
            {
                ToDate = new DateTime();

            }
            if (FromDate == null)
            {
                FromDate = new DateTime();

            }

            ToDate = ToDate.Value.Date.AddDays(1).AddTicks(-1);




            List<INV_DTTBL_Model> model = new List<INV_DTTBL_Model>();
            var plist = ItemMovementParent(ItemNo, PROJECT_NO, FromDate, ToDate);



            model = db.INV_DTTBL_Model.Where(x => x.ITEM_NO == ItemNo && x.INVOICETBL_Model.DATE_M >= FromDate && x.INVOICETBL_Model.DATE_M <= ToDate && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();


            if (PROJECT_NO > 0)
            {

                model = model.Where(x => x.INVOICETBL_Model.PROJ_NO == PROJECT_NO && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();
            }

            plist.AddRange(model);



            return plist.OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();
        }

        List<INV_DTTBL_Model> ItemMovementParent(long ItemNo, int? PROJECT_NO, DateTime? FromDate, DateTime? ToDate)
        {
            List<INV_DTTBL_Model> list = new List<INV_DTTBL_Model>();

            var Items = db.ITEMTBL_Model.Where(x => x.PARENT_ITEM == ItemNo).ToList();

            foreach (var item in Items)
            {

                List<INV_DTTBL_Model> plist = ItemMovementParent(item.ITEM_NO, PROJECT_NO, FromDate, ToDate);
                List<INV_DTTBL_Model> model = new List<INV_DTTBL_Model>();



                model = db.INV_DTTBL_Model.Where(x => x.ITEM_NO == item.ITEM_NO && x.INVOICETBL_Model.DATE_M >= FromDate && x.INVOICETBL_Model.DATE_M <= ToDate && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();


                if (PROJECT_NO > 0)
                {

                    model = model.Where(x => x.INVOICETBL_Model.PROJ_NO == PROJECT_NO && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();
                }
                list.AddRange(model);
                list.AddRange(plist);


            }
            return list;
        }



        public List<ITEMTBL_Model> ItemQuantity(long? ItemNo, long? parentItem, int? PROJECT_NO, DateTime? FromDate, DateTime? ToDate)
        {

            if (ToDate == null)
            {
                ToDate = new DateTime();

            }
            if (FromDate == null)
            {
                FromDate = new DateTime();

            }

            ToDate = ToDate.Value.Date.AddDays(1).AddTicks(-1);


            if (ItemNo == 0) //get all item
            {
                List<ITEMTBL_Model> list = new List<ITEMTBL_Model>();

                var Items = db.ITEMTBL_Model.Where(x => x.PARENT_ITEM == null).OrderBy(x => x.ITEM_NO).ToList();
                foreach (var item in Items)
                {
                    var plist = ItemQuantityParent(item, PROJECT_NO, FromDate, ToDate);//add parent for item to list
                    //add item to list
                    List<INV_DTTBL_Model> InvDt = new List<INV_DTTBL_Model>();
                    InvDt = db.INV_DTTBL_Model.Where(x => x.ITEM_NO == item.ITEM_NO && x.INVOICETBL_Model.DATE_M >= FromDate && x.INVOICETBL_Model.DATE_M <= ToDate && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();
                    if (PROJECT_NO > 0)
                    {

                        InvDt = InvDt.Where(x => x.INVOICETBL_Model.PROJ_NO == PROJECT_NO && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();

                    }
                    var InvIn = InvDt.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33);
                    var InvOut = InvDt.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34);
                    item.Qin = InvIn.Sum(x => x.QNTY).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.Qin);
                    item.Qout = InvOut.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE != 34).Sum(x => x.QNTY).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.Qout);
                    item.CostIn = InvIn.Sum(x => x.TOTAL_PRICE).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.CostIn);
                    item.CostOut = InvOut.Sum(x => x.TOTAL_PRICE).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.CostOut);
                    item.VatIn = InvIn.Sum(x => x.VAT_AMOUNT).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.VatIn);
                    item.VatOut = InvOut.Sum(x => x.VAT_AMOUNT).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.VatOut);

                    list.Add(item);


                    list.AddRange(plist);


                }
                return list;
            }
            else //get on item
            {
                ITEMTBL_Model Item = new ITEMTBL_Model();

                List<INV_DTTBL_Model> InvDt = new List<INV_DTTBL_Model>();


                Item = db.ITEMTBL_Model.Find(ItemNo);

                var parelist = ItemQuantityParent(Item, PROJECT_NO, FromDate, ToDate);//add parent for item to list

                InvDt = db.INV_DTTBL_Model.Where(x => x.ITEM_NO == ItemNo && x.INVOICETBL_Model.DATE_M >= FromDate && x.INVOICETBL_Model.DATE_M <= ToDate && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();

                if (PROJECT_NO > 0)
                {

                    InvDt = InvDt.Where(x => x.INVOICETBL_Model.PROJ_NO == PROJECT_NO && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();

                }
                var InvIn = InvDt.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33);
                var InvOut = InvDt.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34);
                Item.Qin = InvIn.Sum(x => x.QNTY).Value + parelist.Where(x => x.PARENT_ITEM == Item.ITEM_NO).Sum(x => x.Qin);
                Item.Qout = InvOut.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE != 34).Sum(x => x.QNTY).Value + parelist.Where(x => x.PARENT_ITEM == Item.ITEM_NO).Sum(x => x.Qout);
                Item.CostIn = InvIn.Sum(x => x.TOTAL_PRICE).Value + parelist.Where(x => x.PARENT_ITEM == Item.ITEM_NO).Sum(x => x.CostIn);
                Item.CostOut = InvOut.Sum(x => x.TOTAL_PRICE).Value + parelist.Where(x => x.PARENT_ITEM == Item.ITEM_NO).Sum(x => x.CostOut);
                Item.VatIn = InvIn.Sum(x => x.VAT_AMOUNT).Value + parelist.Where(x => x.PARENT_ITEM == Item.ITEM_NO).Sum(x => x.VatIn);
                Item.VatOut = InvOut.Sum(x => x.VAT_AMOUNT).Value + parelist.Where(x => x.PARENT_ITEM == Item.ITEM_NO).Sum(x => x.VatOut);

                var plist = new List<ITEMTBL_Model>();
                plist.Add(Item);
                plist.AddRange(parelist);
                return plist;
            }


        }

        List<ITEMTBL_Model> ItemQuantityParent(ITEMTBL_Model Item, int? PROJECT_NO, DateTime? FromDate, DateTime? ToDate)
        {
            List<ITEMTBL_Model> list = new List<ITEMTBL_Model>();

            var Items = Item.ITEMTBL_Model_Collection.OrderBy(x => x.ITEM_NO).ToList();

            foreach (var item in Items)
            {

                //add parent for item to list
                List<ITEMTBL_Model> plist = ItemQuantityParent(item, PROJECT_NO, FromDate, ToDate);

                //add item to list
                List<INV_DTTBL_Model> InvDt = new List<INV_DTTBL_Model>();

                InvDt = db.INV_DTTBL_Model.Where(x => x.ITEM_NO == item.ITEM_NO && x.INVOICETBL_Model.DATE_M >= FromDate && x.INVOICETBL_Model.DATE_M <= ToDate && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();
                if (PROJECT_NO > 0)
                {

                    InvDt = InvDt.Where(x => x.INVOICETBL_Model.PROJ_NO == PROJECT_NO && (x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33 || x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34)).OrderBy(x => x.INVOICETBL_Model.DATE_M).ToList();

                }
                var InvIn = InvDt.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE == 8 || x.INVOICETBL_Model.INV_DUC_TYPE == 9 || x.INVOICETBL_Model.INV_DUC_TYPE == 33);
                var InvOut = InvDt.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE == 10 || x.INVOICETBL_Model.INV_DUC_TYPE == 11 || x.INVOICETBL_Model.INV_DUC_TYPE == 34);
                item.Qin = InvIn.Sum(x => x.QNTY).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.Qin);
                item.Qout = InvOut.Where(x => x.INVOICETBL_Model.INV_DUC_TYPE != 34).Sum(x => x.QNTY).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.Qout);
                item.CostIn = InvIn.Sum(x => x.TOTAL_PRICE).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.CostIn);
                item.CostOut = InvOut.Sum(x => x.TOTAL_PRICE).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.CostOut);
                item.VatIn = InvIn.Sum(x => x.VAT_AMOUNT).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.VatIn);
                item.VatOut = InvOut.Sum(x => x.VAT_AMOUNT).Value + plist.Where(x => x.PARENT_ITEM == item.ITEM_NO).Sum(x => x.VatOut);

                list.Add(item);


                list.AddRange(plist);


            }
            return list;
        }


    }
}
