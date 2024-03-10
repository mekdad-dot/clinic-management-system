using ClinicManagementSystem.Core.DTOs;

namespace ClinicManagementSystem.Core.Interfaces.BaseRepos
{
    public interface IRepo<TClass>
    {
        ValueTask<IEnumerable<TClass>> GetAllAsync(int skip,int take);
        public ValueTask<TClass> GetByIdAsync(Guid id);
        public void Add(TClass item);
        public void Update(TClass value);
        public ValueTask SaveChangesAsync();
    }
}
