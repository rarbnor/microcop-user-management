using System.Linq.Expressions;
using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public partial class GenericRepository<T> :  IGenericRepository<T> where T : BaseEntity
    {
        protected readonly DatabaseContext _dbContext;

        public GenericRepository(DatabaseContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
            _dbContext = dbContext;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }

        public async Task CreateAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await SaveAsync();
        }


        public async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await SaveAsync();
        }


        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
             await SaveAsync();
        }


        public async Task SaveAsync() => await _dbContext.SaveChangesAsync();
    }
}

