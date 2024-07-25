using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
    public class BOX_OPTBL_Business
    {
        oracleDbContextSpend db;
        public BOX_OPTBL_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<BOX_OPTBL_Model> GetAll()
        {
            List<BOX_OPTBL_Model> obj = db.BOX_OPTBL_Model.ToList();




            return obj;
        }


        public BOX_OPTBL_Model GetPyID(long id)
        {
            BOX_OPTBL_Model obj = db.BOX_OPTBL_Model.Find(id);


            return obj;
        }
    }
}
