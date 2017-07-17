using System;
using System.Data.Entity;
using testAdmin.Core.Repositories;
using testAdmin.Models;

namespace testAdmin.Repos.Persistant.Repositories
{
    public class LanguageRepo : testAdmin.Persistant.Repositories.Repository<Language>, ILanguageRepo
    {
        public LanguageRepo(DbContext _context) : base(_context)
        {
        }

        public Language GetSubWithLanguage(int id)
        {
            throw new NotImplementedException();
        }
    }
}