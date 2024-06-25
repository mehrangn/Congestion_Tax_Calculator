using Congestion_Tax_Calculator.BusinessLogic.Persistance;
using Congestion_Tax_Calculator.Domain;
using Congestion_Tax_Calculator.DataAccess.Persistance;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Congestion_Tax_Calculator.DataAccess.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly TaxCalculatorDbContext _dbContext;

        public BaseRepository(TaxCalculatorDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<ICollection<T>> BulkInsert(ICollection<T> entities)
        {
            await _dbContext.Set<T>().AddRangeAsync(entities);
            await _dbContext.SaveChangesAsync();

            return entities;
        }

        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }
    }
}
