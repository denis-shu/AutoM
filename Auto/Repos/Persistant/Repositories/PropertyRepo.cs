using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using testAdmin.Models;
using testAdmin.Repos.Core.Repositories;

namespace testAdmin.Repos.Persistant.Repositories
{
    public class PropertyRepo : testAdmin.Persistant.Repositories.Repository<Property>, IPropertyRepo
    {
        public PropertyRepo(DbContext _context) : base(_context)
        {
        }

    #region Get Property

        public Property GetProperty(int id)
        {
            return Context.Set<Property>()
                .Where(x => x.Id == id)
                .Include(x => x.Description)
                .Include(x=>x.Subject)
                .Include(x=>x.VisualRepresentation)
                .FirstOrDefault();
        }
        #endregion

        public IEnumerable<Property> GetPropertybyId(int id)
        {
            return Context.Set<Property>()
                .Where(x => x.Subject_id == id)              
                .ToList();
        }

        public Property GetNewProperty(int id)
        {
            return Context.Set<Property>()
                .FirstOrDefault(x => x.Subject_id == id);
        }

        public Property GetProp(int id)
        {
            return Context.Set<Property>()
                .FirstOrDefault(x => x.Id == id);
        }

    #region Clear VisualId

        public void ClearVisualId(int id)
        {
            var property = Context.Set<Property>()
                .FirstOrDefault(x => x.VisualRepresentation_Id == id);

            if (property != null)
            {
                property.VisualRepresentation_Id = null;

                Context.Entry<Property>(property).State = EntityState.Modified;
                Context.SaveChanges();
            }

        }

#endregion


    }
}