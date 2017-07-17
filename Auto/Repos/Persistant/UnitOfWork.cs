using testAdmin.Core;
using testAdmin.Core.Repositories;
using testAdmin.Persistant.Repositories;
using testAdmin.Repos.Core.Repositories;
using testAdmin.Repos.Persistant.Repositories;

namespace testAdmin.Persistant
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AutomateDbContext _context;
        

        public UnitOfWork(AutomateDbContext context)
        {
            _context = context;
            Subs = new SubRepository(_context);
            Tags = new TagRepository(_context);
            VisualRepresentations = new VRRepository(_context);
            Languages = new LanguageRepo(_context);
            Characteristics = new CharacteristicRepo(_context);
            Properties = new PropertyRepo(_context);
            Descriptions = new DescriptionRepo(_context);           


        }

        public ISubRepo Subs { get; private set; }
        public ITagRepo Tags { get; private set; }
        public IVisualRepresentationsRepo VisualRepresentations { get; private set; }
        public ILanguageRepo Languages { get; private set; }
        public ICharacteristicRepo Characteristics { get; private set; }
        public IPropertyRepo Properties { get; private set; }
        public IDescriptionRepo Descriptions { get; private set; }

        public void Complete()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}