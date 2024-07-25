using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
  public  class SUB_PROJTBL_Business
    {
        oracleDbContextSpend db;
        public SUB_PROJTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<SUB_PROJTBL_Model> getall()
        {
            List<SUB_PROJTBL_Model> obj = db.SUB_PROJTBL_Model.ToList();

            return obj;
        }
        public SUB_PROJTBL_Model GetByID(int id)
        {
            SUB_PROJTBL_Model obj = db.SUB_PROJTBL_Model.Find(id);
            return obj;
        }
    }
}
