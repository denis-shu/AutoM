using System.Data.Entity;
using System.Linq;
using testAdmin.Models;
using testAdmin.Repos.Core.Repositories;

namespace testAdmin.Repos.Persistant.Repositories
{
    public class DescriptionRepo : testAdmin.Persistant.Repositories.Repository<Description>, IDescriptionRepo
    {
        public DescriptionRepo(DbContext _context) : base(_context)
        { }
        

    }
}