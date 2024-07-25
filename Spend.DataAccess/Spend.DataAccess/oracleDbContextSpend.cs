using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using Spend.Models;

namespace Spend.DataAccess
{
    public class oracleDbContextSpend : DbContext
    {
        public oracleDbContextSpend()
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          //  modelBuilder.HasDefaultSchema("spend");
           modelBuilder.HasDefaultSchema("SPEND");
        }
     

        public DbSet<PERSON_ACC_PROJTBL_Model> PERSON_ACC_PROJTBL_Model { set; get; }
        public DbSet<PERSON_ACCTBL_Model> PERSON_ACCTBL_Model { set; get; }
        public DbSet<PERSONAL_INFOTBL_Model> PERSONAL_INFOTBL_Model { get; set; }
        public DbSet<CUSTODY_TYPETBL_Model> CUSTODY_TYPETBL_Model { get; set; }
        public DbSet<PROJECTTBL_Model> PROJECTTBL_Model { set; get; }
        public DbSet<PERSON_DESCRIPTIONTBL_Model> PERSON_DESCRIPTIONTBL_Model { set; get; }
        public DbSet<USERSTBL_Model> USERSTBL_Model { set; get; }
        public DbSet<STAGESTBL_Model> STAGESTBL_Model { set; get; }
        public DbSet<USER_ROLETBLE_Model> USER_ROLETBLE_Model { set; get; }
        public DbSet<ROLETBL_Model> ROLETBL_Model { set; get; }
        public DbSet<FEATURE_ROLETBL_Model> FEATURE_ROLETBL_Model { set; get; }
        public DbSet<FEATURETBL_Model> FEATURETBL_Model { set; get; }
        public DbSet<UTLISTTBL_Model> UTLISTTBL_Model { set; get; }
        public DbSet<USER_UTLIST_PROJECTTBL_Model> USER_UTLIST_PROJECTTBL_Model { set; get; }
        public DbSet<UTL_TYPETBL_Model> UTL_TYPETBL_Model { set; get; }
        public DbSet<UNITTBL_Model> UNITTBL_Model { set; get; }
        public DbSet<PARTTBL_Model> PARTTBL_Model { set; get; }
        public DbSet<SUB_PROJTBL_Model> SUB_PROJTBL_Model { set; get; }
        public DbSet<CREDENCE_DTTBL_Model> CREDENCE_DTTBL_Model { set; get; }
        public DbSet<CREDENCETBL_Model> CREDENCETBL_Model { set; get; }
        public DbSet<SALES_INVTBL_Model> SALES_INVTBL_Model { set; get; }
       public DbSet<SALES_INVDTTBL_Model> SALES_INVDTTBL_Model { set; get; }
       public DbSet<ARCHIVETBL_Model> ARCHIVETBL_Model { set; get; }
        public DbSet<CUSTOMERTBL_Model> CUSTOMERTBL_Model { set; get; }
        public DbSet<ACCOUNTTBL_Model> ACCOUNTTBL_Model { set; get; }
        public DbSet<ACCH_OPBOXTBL_Model> ACCH_OPBOXTBL_Model { set; get; }
        public DbSet<BOX_PROJ_ACTIONSTBL_Model> BOX_PROJ_ACTIONSTBL_Model { set; get; }
        public DbSet<SPEND_SCRIPTBL_Model> SPEND_SCRIPTBL_Model { set; get; }
        public DbSet<INVOICETBL_Model> INVOICETBL_Model { set; get; }
        public DbSet<ACC_TREETBL_Model> ACC_TREETBL_Model { set; get; }
        public DbSet<ACC_TYPETBL_Model> ACC_TYPETBL_Model { set; get; }
        public DbSet<ACC_NATTBL_Model> ACC_NATTBL_Model { set; get; }
        public DbSet<ENGINEERTBL_Model> ENGINEERTBL_Model { set; get; }
        public DbSet<OWNERTBL_Model> OWNERTBL_Model { set; get; }
        public DbSet<BANKTBL_Model> BANKTBL_Model { set; get; }
        public DbSet<CENTERSTBL_Model> CENTERSTBL_Model { set; get; }
        public DbSet<BOXTBL_Model> BOXTBL_Model { set; get; }
        public DbSet<MARKETINGTBL_Model> MARKETINGTBL_Model { set; get; }
        public DbSet<VATTBL_Model> VATTBL_Model { set; get; }
        public DbSet<SCRIPT_ACTIONSTBL_Model> SCRIPT_ACTIONSTBL_Model { set; get; }
        public DbSet<SCRIP_DTTBL_Model> SCRIP_DTTBL_Model { set; get; }
        public DbSet<SCRIP_OPTYPETBL_Model> SCRIP_OPTYPETBL_Model { set; get; }
        public DbSet<SCRIP_TYPETBL_Model> SCRIP_TYPETBL_Model { set; get; }
        public DbSet<ALL_ACC_NOTBL_Model> ALL_ACC_NOTBL_Model { set; get; }
        public DbSet<MAX_UNDER_OPV_Model> MAX_UNDER_OPV_Model { set; get; }
        public DbSet<INV_DTTBL_Model> INV_DTTBL_Model { set; get; }
        public DbSet<DUC_TYPETBL_Model> DUC_TYPETBL_Model { set; get; }
        public DbSet<ITEMTBL_Model> ITEMTBL_Model { set; get; }
        public DbSet<SALES_CUSTTBL_Model> SALES_CUSTTBL_Model { set; get; }
        public DbSet<ACC_HOLDERTBL_Model> ACC_HOLDERTBL_Model { set; get; }
        public DbSet<OPERATIONAL_PALANCE_Model> OPERATIONAL_PALANCE_Model { set; get; }
        public DbSet<PERCENT_EST_Model> PERCENT_EST_Model { set; get; }
        public DbSet<ACCH_BALANCEV_Model> ACCH_BALANCEV_Model { set; get; }
        public DbSet<ACCH_OPBOX_ACTIONSTBL_Model> ACCH_OPBOX_ACTIONSTBL_Model { set; get; }
        public DbSet<BOX_OPTBL_Model> BOX_OPTBL_Model { set; get; }
        public DbSet<ACCH_PROJ_BOX_PERCENTTBL_Model> ACCH_PROJ_BOX_PERCENTTBL_Model { set; get; }
        public DbSet<ACCH_PROJ_PERCENTTBL_Model> ACCH_PROJ_PERCENTTBL_Model { set; get; }
        public DbSet<ACCH_BALANCE_SUMMARYV_Model> ACCH_BALANCE_SUMMARYV_Model { set; get; }
        public DbSet<ACCH_TYPETBL_Model> ACCH_TYPETBL_Model { set; get; }
        public DbSet<ACC_TREETBL1_Model> ACC_TREETBL1_Model { set; get; }
        public DbSet<ACC_TREETBL1_ACCOUNTTB_Model> ACC_TREETBL1_ACCOUNTTB_Model { set; get; }
        public DbSet<BOX_TRANS_OPTYPES_Model> BOX_TRANS_OPTYPES_Model { set; get; }
        public DbSet<TRANS_HD_Model> TRANS_HD_Model { set; get; }
        public DbSet<HOLDER_PERCENT_DT_Model> HOLDER_PERCENT_DT_Model { set; get; }
        public DbSet<HOLDER_PERCENT_HD_Model> HOLDER_PERCENT_HD_Model { set; get; }
        public DbSet<HOLDER_TRANS_DT_Model> HOLDER_TRANS_DT_Model { set; get; }
        public DbSet<HOLDER_TRANS_HD_Model> HOLDER_TRANS_HD_Model { set; get; }
        public DbSet<FISCAL_YEAR_Model> FISCAL_YEAR_Model { set; get; }
       



        public class erpDBInitializerArchive : DropCreateDatabaseIfModelChanges<oracleDbContextSpend>
        {
            //public override void InitializeDatabase(OracleDbContextArchive context)
            //{

            //    base.InitializeDatabase(context);



            //}




            protected override void Seed(oracleDbContextSpend context)
            {
                //sempleData sm = new sempleData();
                //List<ERP_LOGIN> LGL = context.ERP_LOGINS.ToList();

                //sm.addSemplDataLov(context);
                //if (LGL == null || LGL.Count() == 0)
                //{
                //    sm.addSemplDataERP_ROLE_GROUP(context);
                //    sm.addSemplDataPERSONALINFO(context);
                //    sm.addSemplDataLogIn(context);

                //    sm.addSemplDataLovHd(context);
                //    sm.addSemplDataLov(context);
                //    sm.addSemplDataPROJECT(context);
                //    sm.addSemplDataFEATURE(context);
                //    sm.addSemplDataFEATURE_ROLE(context);

                //    sm.addSemplCard(context);
                //    sm.addSemplCardGroup(context);
                //    sm.addSemplFild(context);
                //    sm.addSemplCARdData(context);
                //    sm.addSemplDataREPORT_TO_TYPE(context);
                //}



            }



        }



    }
}
