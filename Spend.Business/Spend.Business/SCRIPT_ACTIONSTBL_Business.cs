using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
 public   class SCRIPT_ACTIONSTBL_Business
    {

        oracleDbContextSpend db;
        public SCRIPT_ACTIONSTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<SCRIPT_ACTIONSTBL_Model> getall()
        {
            List<SCRIPT_ACTIONSTBL_Model> obj = db.SCRIPT_ACTIONSTBL_Model.ToList();


            return obj;
        }
        public SCRIPT_ACTIONSTBL_Model GetPyID(int id)
        {
            SCRIPT_ACTIONSTBL_Model obj = db.SCRIPT_ACTIONSTBL_Model.Find(id);


            return obj;
        }
    }
}
