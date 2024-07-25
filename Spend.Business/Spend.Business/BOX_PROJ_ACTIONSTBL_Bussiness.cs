using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
    public class BOX_TRANS_OPTYPES_Bussiness
    {
        oracleDbContextSpend db;
        public BOX_TRANS_OPTYPES_Bussiness()
        {

            db = new oracleDbContextSpend();

        }

        public List<BOX_TRANS_OPTYPES_Model> GetAll()
        {
            List<BOX_TRANS_OPTYPES_Model> obj = db.BOX_TRANS_OPTYPES_Model.ToList();




            return obj;
        }


        public BOX_TRANS_OPTYPES_Model GetPyID(long id)
        {
            BOX_TRANS_OPTYPES_Model obj = db.BOX_TRANS_OPTYPES_Model.Find(id);


            return obj;
        }
    }
}
