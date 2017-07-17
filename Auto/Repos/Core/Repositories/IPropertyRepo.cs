using System.Collections.Generic;
using testAdmin.Core.Repositories;
using testAdmin.Models;


namespace testAdmin.Repos.Core.Repositories
{
    public interface IPropertyRepo: IRepository<Property>
    {
        Property GetProperty(int id);
        Property GetNewProperty(int id);
        IEnumerable<Property> GetPropertybyId(int id);
        Property GetProp(int id);
        void ClearVisualId(int id);
    }
}
