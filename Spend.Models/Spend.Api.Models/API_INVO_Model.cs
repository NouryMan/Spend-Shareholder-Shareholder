using Spend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spend.Api.Models
{
    public class API_INVO_Model
    {
        public API_INVO_Model()
        {
            this.login = new Login();
            this.iNV_HD = new SALES_INVTBL_Model();
            this.personal_Info = new PERSONAL_INFOTBL_Model();
        }
        public Login login { get; set; }
        public SALES_INVTBL_Model iNV_HD { get; set; }
        public PERSONAL_INFOTBL_Model personal_Info { get; set; }
    }
}