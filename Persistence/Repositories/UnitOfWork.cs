using DomainLayer.Contracts;
using DomainLayer.Models;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UnitOfWork(StoreDbContext dbContext) : IUnitOfWork
    {
        // we create a dictionary which holds all repositories
        private readonly Dictionary<string, object> _repositories = [];

        // we create a method to get the repositories from the dictionary and create if does not exist
        public IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>() where TEntity : BaseEntity<Tkey>
        {
            //get the name of the type from the outer
            var typeName = typeof(TEntity).Name;

            //check if the name exists in the dictionary
            if (_repositories.ContainsKey(typeName))
            {
                // if exists return a cast from the dictionary 
                return (IGenericRepository<TEntity, Tkey>)_repositories[typeName];
            }
            else
            {
                // if does not exist create an object from this name 
                var repo = new GenericRepository<TEntity, Tkey>(dbContext);

                // add the object to the dictionary
                _repositories.Add(typeName, repo);

                // retrun the obj
                return repo;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await dbContext.SaveChangesAsync();
        }
    }
}
