﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq.Expressions;

namespace eTickets.Data.Base
{
    // IEntityBaseRepository den implement ediliyor.
    // Burada esas VT tarafına ulaşma işi burada olcak
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        // 33
        // VT ile ilişkili değişgen ve metotlar
        private readonly AppDbContext _context;

        public EntityBaseRepository(AppDbContext context)
        {
            _context = context;   
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            // Burası her bir model için ortaklaştırılıyor. --> Set<T> bildirimiyle

            return await _context.Set<T>().ToListAsync();
            
        }

        // Bu metot polymorphism olarak aynı isimde içeriği farklı ilişkilendirmelere göre çalışacak olan metot.(aslında Movie durumu için gerekli oldu)
        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();

            query=includeProperties.Aggregate(query,(current,includeProperty) =>current.Include(includeProperty));

            return await query.ToListAsync();
           
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FirstOrDefaultAsync(t => t.Id == id);
        }

        // İlşkisel yapıya göre
        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query=_context.Set<T>();

            query= includeProperties.Aggregate(query,(current,includeProperty)=>current.Include(includeProperty));

            return await query.FirstOrDefaultAsync(n => n.Id == id);

        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, T entity)
        {
            EntityEntry entityEntry=_context.Entry<T>(entity);

            entityEntry.State = EntityState.Modified;

            await _context.SaveChangesAsync();

        }

        public async Task DeleteAsync(int id)
        {
            var entity=await _context.Set<T>().FirstOrDefaultAsync(n=> n.Id == id);

            EntityEntry entityEntry = _context.Entry<T>(entity);

            entityEntry.State = EntityState.Deleted;

            await _context.SaveChangesAsync();
        }

    }
}
