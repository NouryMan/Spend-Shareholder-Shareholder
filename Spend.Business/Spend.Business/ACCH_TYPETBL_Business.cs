using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
    public class ACCH_TYPETBL_Business
    {
        oracleDbContextSpend db;
        public ACCH_TYPETBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACCH_TYPETBL_Model> GetAll()
        {
            List<ACCH_TYPETBL_Model> obj = db.ACCH_TYPETBL_Model.ToList();




            return obj;
        }

        public ACCH_TYPETBL_Model GetPyID( int id)
        {
            ACCH_TYPETBL_Model obj = db.ACCH_TYPETBL_Model.Find(id);


            return obj;
        }

    }
}
