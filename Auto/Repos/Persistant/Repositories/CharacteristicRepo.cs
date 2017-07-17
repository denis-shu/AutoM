using System.Data.Entity;
using System.Linq;
using testAdmin.Core.Repositories;
using testAdmin.Models;
using testAdmin.Persistant.Repositories;

namespace testAdmin.Repos.Persistant.Repositories
{
    public class CharacteristicRepo : testAdmin.Persistant.Repositories.Repository<Characteristic>, ICharacteristicRepo
    {
        public CharacteristicRepo(DbContext _context) : base(_context)
        {
        }

        public Characteristic GetCharacteristicWhithLang(int id)
        {
            return Context.Set<Characteristic>()
                .Where(c => c.Id == id)
                .Include(c => c.Language)
                .FirstOrDefault();
        }
    }
}