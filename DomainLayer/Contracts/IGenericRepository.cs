using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Contracts
{
    public interface IGenericRepository<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {
        Task<TEntity?> GetByIdAsync(Tkey id);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> specifications);
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, Tkey> specifications);
        Task<int> CountAsync(ISpecifications<TEntity, Tkey> specifications);

        Task AddAsync(TEntity entity);
        void Delete(TEntity entity);
        void Update(TEntity entity);
    }
}
