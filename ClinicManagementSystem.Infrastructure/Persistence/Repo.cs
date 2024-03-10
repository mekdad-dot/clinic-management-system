using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.Interfaces.BaseRepos;
using ClinicManagementSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Persistence
{
    public class Repo<TClass> : IRepo<TClass> where TClass : class
    {
        private readonly ApplicationDbContext _context;
        protected readonly DbSet<TClass> _table;

        public Repo(ApplicationDbContext context)
        {
            _context = context;
            _table = context.Set<TClass>();
        }

        public async ValueTask<IEnumerable<TClass>> GetAllAsync(int skip, int take) => 
            await _table.AsNoTracking().Skip(skip).Take(take).ToListAsync();

        public async ValueTask<TClass> GetByIdAsync(Guid id) => await _table.FindAsync(id);

        public void Add(TClass item) => _table.Add(item);

        public void Update(TClass item) => _table.Update(item);

        public async ValueTask SaveChangesAsync() => await _context.SaveChangesAsync();
    }
}
