using testAdmin.Models;

namespace testAdmin.Core.Repositories
{
    public interface ICharacteristicRepo: IRepository<Characteristic>
    {
        Characteristic GetCharacteristicWhithLang(int id);
        
    }


}
