using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class DUC_TYPETBL_Business
    {
        oracleDbContextSpend db;
        public DUC_TYPETBL_Business()
        {

            db = new oracleDbContextSpend();

        }

        public List<DUC_TYPETBL_Model> getall()
        {
            List<DUC_TYPETBL_Model> obj = db.DUC_TYPETBL_Model.ToList();




            return obj;
        }
        public DUC_TYPETBL_Model GetPyID(int id, int type)
        {
            DUC_TYPETBL_Model obj = db.DUC_TYPETBL_Model.Find(id, type);


            return obj;
        }
        public List<DUC_TYPETBL_Model> GetPyACC_TYPE( int type)
        {
           List<DUC_TYPETBL_Model> obj = db.DUC_TYPETBL_Model.Where(x=>x.ACC_TYPE_CODE== type).ToList();


            return obj;
        }
    }
}
