using callcenter.model.call;
using Microsoft.AspNet.Identity;
using Spend.DataAccess;
using Spend.Models;
using Spend.Models.Helper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Spend.Business
{
    public class ACCH_PROJECT_Business
    {
        oracleDbContextSpend db;
        public ACCH_PROJECT_Business()
        {

            db = new oracleDbContextSpend();

        }

        public async Task<ACCH_PROJECT_Model> GetByIdAsync(int id)
        {
            return await db.ACCH_PROJECT_Model.FirstOrDefaultAsync(x => x.ID == id && x.IS_DELETED==false );
        }

        public async Task<Pagination<ACCH_PROJECT_Model>> GetAll(Filter filter)
        {

            var query = db.ACCH_PROJECT_Model.Where(x => x.IS_DELETED == false );

            var totalCount = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);
            var pagedQuery = query.Skip((filter.PageNumber - 1) * filter.PageSize).Take(filter.PageSize);
            var ACCH_PROJECT_Models = await pagedQuery.ToListAsync();

            return new Pagination<ACCH_PROJECT_Model>
            {
                CurrentPage = filter.PageNumber,
                TotalItems = totalCount,
                TotalPages = totalPages,
                PerPage = filter.PageSize,
                LastPage = totalPages,
                Items = ACCH_PROJECT_Models
            };
        }


        public async Task<int> GetMaxID()
        {
            int max = 0;
            try
            {

                max = await db.ACCH_PROJECT_Model.MaxAsync(x => (int?)x.ID) ?? 0;

            }
            catch
            {

            }
            return max + 1;
        }

        public async Task Create(ACCH_PROJECT_Model item)
        {

            try
            {
                try
                {
                  

                        item.ID = await GetMaxID();
                        item.GUID_ID = Guid.NewGuid();
                        item.IS_FOR_RENTAL = false;


                        db.ACCH_PROJECT_Model.Add(item);
                       await  db.SaveChangesAsync();


                 

                }//close try
                catch (Exception ex) {

                    throw new  Exception(ex.Message);
                
                }
             

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }


        public async Task Update(ACCH_PROJECT_Model item)
        {


            try
            {
                item.UPDATE_FROM = "C";
                db.Entry(item).State = EntityState.Modified;
                db.Entry(item).Property(x => x.IS_DELETED).IsModified = false;
                db.Entry(item).Property(x => x.STATUS_ID).IsModified = false;
                db.Entry(item).Property(x => x.GUID_ID).IsModified = false;
                db.Entry(item).Property(x => x.IS_FOR_RENTAL).IsModified = false;
                db.Entry(item).Property(x => x.MAIN_RENTAL_CONTRACT).IsModified = false;
                db.Entry(item).Property(x => x.BRANCH_ID).IsModified = false;

                //db.Entry(cust).Property(x => x.PROJECT_PARENT_ID).IsModified = false;

                await db.SaveChangesAsync();
               

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

        public async Task Delete(int id)
        {

            try
            {
                var item = await GetByIdAsync(id);
                item.IS_DELETED = true;
                
                await db.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

    }
}
