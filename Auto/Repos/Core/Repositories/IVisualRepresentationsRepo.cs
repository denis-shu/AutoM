using System.Threading.Tasks;
using testAdmin.Models;

namespace testAdmin.Core.Repositories
{
    public interface IVisualRepresentationsRepo:IRepository<VisualRepresentation>
    {
        VisualRepresentation GetImage(int id);
    }
}