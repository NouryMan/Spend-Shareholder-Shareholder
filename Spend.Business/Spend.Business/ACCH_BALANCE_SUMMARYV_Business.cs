using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
    public class ACCH_BALANCE_SUMMARYV_Business
    {
        oracleDbContextSpend db;
        public ACCH_BALANCE_SUMMARYV_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACCH_BALANCE_SUMMARYV_Model> GetAll()
        {
            List<ACCH_BALANCE_SUMMARYV_Model> obj = db.ACCH_BALANCE_SUMMARYV_Model.ToList();




            return obj;
        }

        public ACCH_BALANCE_SUMMARYV_Model GetPyID(long AcchNo,int ProjNo)
        {
            ACCH_BALANCE_SUMMARYV_Model obj = db.ACCH_BALANCE_SUMMARYV_Model.Find(AcchNo,ProjNo);


            return obj;
        }


        public List<ACCH_BALANCE_SUMMARYV_Model> GetPyProjNo(int ProjNo)
        {
            List<ACCH_BALANCE_SUMMARYV_Model> obj = db.ACCH_BALANCE_SUMMARYV_Model.Where(x=>x.TARGET_PROJ== ProjNo).ToList();


            return obj;
        }
    }
}
