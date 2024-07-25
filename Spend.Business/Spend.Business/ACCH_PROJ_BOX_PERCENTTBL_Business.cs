using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Spend.Business
{
    public class ACCH_PROJ_BOX_PERCENTTBL_Business
    {
        oracleDbContextSpend db;
        public ACCH_PROJ_BOX_PERCENTTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<ACCH_PROJ_BOX_PERCENTTBL_Model> GetAll()
        {
            List<ACCH_PROJ_BOX_PERCENTTBL_Model> obj = db.ACCH_PROJ_BOX_PERCENTTBL_Model.ToList();

            return obj;
        }

        public ACCH_PROJ_BOX_PERCENTTBL_Model GetPyID(long id)
        {
            ACCH_PROJ_BOX_PERCENTTBL_Model obj = db.ACCH_PROJ_BOX_PERCENTTBL_Model.Find(id);

            return obj;
        }


        public List<ACCH_PROJ_BOX_PERCENTTBL_Model> GetAllPyProject(int id)
        {
            List<ACCH_PROJ_BOX_PERCENTTBL_Model> obj = db.ACCH_PROJ_BOX_PERCENTTBL_Model.Where(x => x.PROJ_NO == id).ToList();

            return obj;
        }

      


    }
}
