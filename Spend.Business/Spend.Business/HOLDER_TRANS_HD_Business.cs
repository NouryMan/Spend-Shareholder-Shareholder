using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
    public class HOLDER_TRANS_HD_Business
    {
        oracleDbContextSpend db;
        public HOLDER_TRANS_HD_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<HOLDER_TRANS_HD_Model> GetAll()
        {
            List<HOLDER_TRANS_HD_Model> obj = db.HOLDER_TRANS_HD_Model.ToList();

            return obj;
        }
        public HOLDER_TRANS_HD_Model GetPyID(long id)
        {
            HOLDER_TRANS_HD_Model obj = db.HOLDER_TRANS_HD_Model.Find(id);


            return obj;
        }

        public long GetMaxID()
        {
            long id = 0;
            try
            {
                id = db.HOLDER_TRANS_HD_Model.Max(x => x.ID) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }
    }
}
