using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MovieTicketApp_MVC_project.Models;
using System.Linq.Expressions;

namespace MovieTicketApp_MVC_project.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntitybase, new()
    {
        private  AppDbContext _context;
        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<T>> GetAll() => await _context.Set<T>().ToListAsync();
        public async Task<T> GetById(int id) => await _context.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        public async Task Add(T entity) {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
                }

        public async Task Update(T entity, int id)
        {
            EntityEntry entityEntry=_context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }



        public async Task Delete(int id)
        {
            var entity=await _context.Set<T>().FirstOrDefaultAsync(x=>x.Id==id);
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll(params Expression<Func<T, object>>[] includeProp)
        {
            IQueryable<T> query = _context.Set<T>();
            query = includeProp.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }
    }
}
