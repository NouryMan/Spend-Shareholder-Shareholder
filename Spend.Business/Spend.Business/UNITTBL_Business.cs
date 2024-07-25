using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class UNITTBL_Business
    {
        oracleDbContextSpend db;
        public UNITTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<UNITTBL_Model> getall()
        {
            List<UNITTBL_Model> obj = db.UNITTBL_Model.ToList();


            return obj;
        }
        public UNITTBL_Model GetPyID(int id)
        {
            UNITTBL_Model obj = db.UNITTBL_Model.Find(id);


            return obj;
        }
    }
}
