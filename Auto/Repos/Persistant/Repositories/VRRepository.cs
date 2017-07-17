using System.Data.Entity;
using System.Linq;
using testAdmin.Core.Repositories;
using testAdmin.Models;

namespace testAdmin.Persistant.Repositories
{
    public class VRRepository : Repository<VisualRepresentation>, IVisualRepresentationsRepo
    {
        public VRRepository(DbContext _context) : base(_context)
        {
        }

        public VisualRepresentation GetImage(int id)
        {
            return Context.Set<VisualRepresentation>()
                 .Where(x => x.Id == id).FirstOrDefault();

        }

    }
}