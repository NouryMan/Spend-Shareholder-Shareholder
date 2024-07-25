using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
    public class ACCH_BALANCEV_Business
    {
        oracleDbContextSpend db;
        public ACCH_BALANCEV_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<ACCH_BALANCEV_Model> GetAll()
        {
            List<ACCH_BALANCEV_Model> obj = db.ACCH_BALANCEV_Model.ToList();

            //foreach(ACCH_BALANCEV_Model model in obj)
            //{
            //    model.ACCH_BALANCEV_Mo = db.ACCH_BALANCEV_Model.Find(model.PARENT_ACCH);
            //}


            return obj;
        }


        public ACCH_BALANCEV_Model GetPyID(long id)
        {
            ACCH_BALANCEV_Model obj = db.ACCH_BALANCEV_Model.Find(id);


            return obj;
        }
    }
}
