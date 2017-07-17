using System.Collections.Generic;
using System.Threading.Tasks;
using testAdmin.Models;

namespace testAdmin.Core.Repositories
{
    public interface ISubRepo : IRepository<Subject>
    {
       IEnumerable<Subject> GetSubWithTagsVr();
        Subject GetSubWith(int id);
        void ClearVisualId(int id);
        void ClearCharacteristic(int id);
        IEnumerable<Subject> Search(string text);
        Task<SubViewModel> GetSubAtPage(int page);




    }
}
