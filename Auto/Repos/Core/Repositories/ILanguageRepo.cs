using testAdmin.Models;

namespace testAdmin.Core.Repositories
{
    public interface ILanguageRepo : IRepository<Language>
    {
        Language GetSubWithLanguage(int id);
    }
}
