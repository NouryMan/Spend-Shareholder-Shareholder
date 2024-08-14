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

        public async Task<Pagination<ACCH_PROJECT_Model>> GetAllAsync(AcchFilter filter)
        {
            var query = db.ACCH_PROJECT_Model.Where(x => x.IS_DELETED == false);

            if (filter.Type.HasValue)
            {
                query = query.Where(x => x.PROJECT_TYPE_ID == filter.Type);
            }
            if (filter.Id.HasValue)
            {
                query = query.Where(x => x.ID == filter.Id);
            }
            if (filter.Status.HasValue)
            {
                query = query.Where(x => x.STATUS_ID == filter.Status);
            }

            // Add an OrderBy clause here. For example, you can sort by ID or any other relevant property
            query = query.OrderBy(x => x.ID);

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

        public async Task<List<ACCH_PROJECT_Model>> GetAllAsync(int? Type)
        {
            return await db.ACCH_PROJECT_Model.Where(x => x.IS_DELETED == false && ( !Type.HasValue || x.PROJECT_TYPE_ID==Type)   ).ToListAsync();
          
        }


        public async Task<int> GetCountByTypeAsync(int type)
        {

            var query = db.ACCH_PROJECT_Model.Where(x => x.PROJECT_TYPE_ID == type && x.IS_DELETED == false );
            if (type == 2)
            {
                query = query.Where(x => x.ProjectModels.IS_DELETED == false);

            }

            if (type == 3)
            {
                query = query.Where(x => x.ProjectModels.ProjectModels.IS_DELETED == false && x.ProjectModels.IS_DELETED == false);

            }

            return await query.CountAsync();
        }
        

        public async Task<int> GetMaxIDAsync()
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
        public async Task<long> GetMaxByProjectParent(long? id, int project_type)
        {
            int prefix = 1;
            long max = 0;
            long length = 0;


            try
            {
                if (id == 0)
                {
                    max =await db.ACCH_PROJECT_Model.Where(x => x.PROJECT_PARENT_ID == id || x.PROJECT_PARENT_ID == null ).MaxAsync(r => r.PROJECT_NO);
                }
                else
                {
                    max =await db.ACCH_PROJECT_Model.Where(x => x.PROJECT_PARENT_ID == id ).MaxAsync(r => r.PROJECT_NO);

                }

                max++;
            }
            catch (Exception ex)
            {
                ACCH_PROJECT_Model mod =await db.ACCH_PROJECT_Model.Where(x => x.ID == id ).FirstOrDefaultAsync();
                if (project_type == 1)
                {
                    length = 3;
                    max = 1001;
                }
                else if (project_type == 2)
                {
                    length = 2;
                    string x1 = "01";
                    string x2 = mod.PROJECT_NO.ToString();
                    string ss = x2 + x1;
                    max = long.Parse(ss);
                }
                else if (project_type == 3)
                {
                    length = 2;
                    string ss = mod.PROJECT_NO.ToString() + "01";
                    max = long.Parse(ss);
                }
                else
                {
                    length = 4;
                }

            }

            return max++;

        }

        public async Task CreateAsync(ACCH_PROJECT_Model item)
        {

            try
            {
                try
                {
                  

                        item.ID = await GetMaxIDAsync();
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


        public async Task UpdateAsync(ACCH_PROJECT_Model item)
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



        public async Task Distribution(int  id,bool distribut)
        {

            try
            {
                var item = await GetByIdAsync(id);
                item.IS_DISTRIBUTION = distribut;

                await db.SaveChangesAsync();


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }



        }

        public async Task<List<ACCH_PROJECT_Model>> SuiteByStuteAndProjectAsync(int? ProjectId, int? status)
        {
            return await db.ACCH_PROJECT_Model.Where(x => x.IS_DELETED == false && (status == null || x.STATUS_ID == status)
                                   && x.PROJECT_TYPE_ID == 3 && (ProjectId == null || x.ProjectModels.PROJECT_PARENT_ID == ProjectId)).ToListAsync();

        }

        public async Task<List<ACCH_PROJECT_Model>> GetProjectListByParentIdAsync(int? id)
        {
            List<ACCH_PROJECT_Model> aCCH_PROJECT_s = null;
            if (id != null && id > 0)
            {
                aCCH_PROJECT_s = await db.ACCH_PROJECT_Model.Where(x =>  x.PROJECT_PARENT_ID == id && x.IS_DELETED == false ).OrderBy(d => d.ID).ToListAsync();
            }
            else
            {

                aCCH_PROJECT_s = await db.ACCH_PROJECT_Model.Where(x =>  x.PROJECT_PARENT_ID == null && x.IS_DELETED == false ).OrderBy(d => d.ID).ToListAsync();
            }

            return aCCH_PROJECT_s;

        }
    }
}
