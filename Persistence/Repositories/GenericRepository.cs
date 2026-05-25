using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class GenericRepository<TEntity, Tkey>(StoreDbContext dbContext) : IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        public async Task AddAsync(TEntity entity)
        {
            await dbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            dbContext.Set<TEntity>().Remove(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbContext.Set<TEntity>().ToListAsync();
        }


        public async Task<TEntity?> GetByIdAsync(Tkey id)
        {
            return await dbContext.Set<TEntity>().FindAsync(id);
        }
        public void Update(TEntity entity)
        {
            dbContext.Set<TEntity>().Update(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            
            return await SpecificationEvaluator
               .CreateQuery(dbContext.Set<TEntity>(), specifications)
               .ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            return await SpecificationEvaluator
                .CreateQuery(dbContext.Set<TEntity>(), specifications)
                .FirstOrDefaultAsync();
        }

        public async Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications)
        {
            return await SpecificationEvaluator
                .CreateQuery(dbContext.Set<TEntity>(), specifications).CountAsync();
        }
    }
}
