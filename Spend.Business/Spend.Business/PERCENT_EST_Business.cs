using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
    public class PERCENT_EST_Business
    {
        oracleDbContextSpend db;
        public PERCENT_EST_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<PERCENT_EST_Model> GetAll()
        {
            List<PERCENT_EST_Model> obj = db.PERCENT_EST_Model.ToList();




            return obj;
        }


        public PERCENT_EST_Model GetPyID(long id)
        {
            PERCENT_EST_Model obj = db.PERCENT_EST_Model.Find(id);


            return obj;
        }
        public List<PERCENT_EST_Model> GetAllPyProject(int id)
        {
            //List<PERCENT_EST_Model> obj = db.PERCENT_EST_Model.Where(x => x.TARGET_PROJ == id).ToList();




            return null;
        }

    }
}
