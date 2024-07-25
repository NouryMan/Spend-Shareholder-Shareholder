using Spend.DataAccess;
using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Business
{
   public class CENTERSTBL_Business
    {
        oracleDbContextSpend db;
        public CENTERSTBL_Business()
        {

            db = new oracleDbContextSpend();

        }
        public List<CENTERSTBL_Model> getall()
        {



            List<CENTERSTBL_Model> obj = db.CENTERSTBL_Model.ToList();


            return obj;


        }
        public CENTERSTBL_Model GetPyID(int id)
        {
            CENTERSTBL_Model obj = db.CENTERSTBL_Model.Find(id);


            return obj;


        }

        public int GetMaxID()
        {
            int id = 0;
            try
            {
                id = db.CENTERSTBL_Model.Max(x => x.ID) + 1;
            }
            catch
            {
                id++;
            }
            return id;
        }

        public CENTERSTBL_Model GeCenterAccForProj(long projAcc)
        {
            CENTERSTBL_Model obj = db.CENTERSTBL_Model.Where(x=>x.PROJECT_NO==projAcc).FirstOrDefault();


            return obj;
        }




        public long Create(CENTERSTBL_Model CENTERST)
        {
          
            CENTERST.ID = GetMaxID();




           
            db.CENTERSTBL_Model.Add(CENTERST);

            int return_value = db.SaveChanges();

            if (return_value > 0)
            {
                return_value = CENTERST.ID;
            }
            return return_value;
        }
       

    }
}
