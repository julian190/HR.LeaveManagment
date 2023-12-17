using HR.LeaveManagment.Application.Contracts.Presistance;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagment.Presistance.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LeaveManagmentDbContext _context;

        public GenericRepository(LeaveManagmentDbContext context)
        {
            _context = context;
        }
        public async Task<T> Add(T entity)
        {
           await _context.AddAsync(entity);
           await _context.SaveChangesAsync();
           return entity;
        }

        public async Task Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> Exists(int Id)
        {
            var entity = await Get(Id);
            return entity != null;
        }

        public async Task<T> Get(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();

        }

        public async Task Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
