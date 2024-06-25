using Congestion_Tax_Calculator.Domain;

namespace Congestion_Tax_Calculator.BusinessLogic.Persistance
{
    public interface IBaseRepository<T>where T : class
    {
        Task<ICollection<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<ICollection<T>> BulkInsert(ICollection<T> entities);
    }
}
