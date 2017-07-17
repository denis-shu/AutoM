using System.Collections.Generic;
using testAdmin.Models;

namespace testAdmin.Core.Repositories
{
    public interface ITagRepo:IRepository<Tag>
    {
        IEnumerable<Tag> GetTagWithSub(int id);
        void RemoveRange(IEnumerable<Tag> tags);
        void AddRange(List<Tag> tags);
    }
}
