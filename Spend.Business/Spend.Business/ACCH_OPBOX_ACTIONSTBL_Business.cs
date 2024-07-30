using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
    public class ACCH_OPBOX_ACTIONSTBL_Business
    {
        oracleDbContextSpend db;
        public ACCH_OPBOX_ACTIONSTBL_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<ACCH_OPBOX_ACTIONSTBL_Model> GetAll()
        {
            List<ACCH_OPBOX_ACTIONSTBL_Model> obj = db.ACCH_OPBOX_ACTIONSTBL_Model.Where(x=>x.IS_DELETED==false).ToList();




            return obj;
        }


        public ACCH_OPBOX_ACTIONSTBL_Model GetPyID(long id)
        {
            ACCH_OPBOX_ACTIONSTBL_Model obj = db.ACCH_OPBOX_ACTIONSTBL_Model.Find(id);


            return obj;
        }



    }
}
