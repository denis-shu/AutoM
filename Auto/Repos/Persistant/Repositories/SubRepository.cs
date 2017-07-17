using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using testAdmin.Core.Repositories;
using testAdmin.Models;

namespace testAdmin.Persistant.Repositories
{
    public class SubRepository : Repository<Subject>, ISubRepo
    {
        public SubRepository(DbContext _context) : base(_context)
        {
        }


        public IEnumerable<Subject> GetSubWithTagsVr()
        {
            return Context.Set<Subject>().Include(X => X.Tags)
                .Include(x => x.VisualRepresentation)
                .Include(x => x.Properties)
                .ToList();
        }

        #region Subjects with Pager

        public async Task<SubViewModel> GetSubAtPage(int page)
        {
            var pageSize = 10;

            IEnumerable<Subject> subs = await Context.Set<Subject>()
                .Include(x => x.VisualRepresentation)
                .Include(x => x.Properties)
                .Include(x => x.Tags)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();


            var totalItems = await Context.Set<Subject>().CountAsync();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var pager = new Logs.Models.Pager(totalItems, page);

            SubViewModel viewModel = new SubViewModel
            {
                Subs = subs,
                Pager = pager
            };
            return viewModel;

        }

        #endregion


        #region Get Subject by id

        public Subject GetSubWith(int id)
        {
            return Context.Set<Subject>().Where(x => x.Id == id)
                .Include(x => x.VisualRepresentation)
                .Include(x => x.Tags)
                .Include(x => x.Characteristic)
                .Include(x => x.Characteristic.Language)
                .Include(x => x.Properties)
                .FirstOrDefault();
        }

        #endregion

        public void ClearVisualId(int id)
        {
            Context.Set<Subject>().Where(x => x.VisualRepresentation_Id == id)
                .FirstOrDefault();
        }



        public void ClearCharacteristic(int id)
        {
            Context.Set<Subject>().Where(x => x.Characteristics_Id == id)
                .FirstOrDefault();
        }


        public IEnumerable<Subject> Search(string text)
        {
            var result = Context.Set<Subject>().Where(x => x.Key.ToLower()
           .StartsWith(text.ToLower())).ToList();
            return result;
        }


    }
}