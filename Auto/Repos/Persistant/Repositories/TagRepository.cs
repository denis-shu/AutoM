using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using testAdmin.Core.Repositories;
using testAdmin.Models;

namespace testAdmin.Persistant.Repositories
{
    public class TagRepository : Repository<Tag>, ITagRepo
    {
        public TagRepository(DbContext _context) : base(_context)
        {
        }

        public void AddRange(List<Tag> tags)
        {
            Context.Set<Tag>().AddRange(tags);
        }

        public IEnumerable<Tag> GetTagWithSub(int id)
        {
            return Context.Set<Tag>().Where(x => x.Sub_Id == id).ToList();
        }

        public void RemoveRange(IEnumerable<Tag> tags)
        {
            Context.Set<Tag>().RemoveRange(tags);
        }
    }
}