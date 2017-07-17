using System;
using System.Threading.Tasks;
using testAdmin.Core.Repositories;
using testAdmin.Repos.Core.Repositories;

namespace testAdmin.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ISubRepo Subs { get; }
        ITagRepo Tags { get; }
        
        IVisualRepresentationsRepo VisualRepresentations { get; }
        ICharacteristicRepo Characteristics { get; }
        ILanguageRepo Languages { get; }
        IDescriptionRepo Descriptions { get; }
        IPropertyRepo Properties { get; }
        void Complete();

    }
}
