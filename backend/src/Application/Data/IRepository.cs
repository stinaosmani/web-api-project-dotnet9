namespace backend.src.Application.Data
{
    public interface IRepository<T, TKey> where T : class
    {
        Task<T> GetByIdAsync(TKey id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> AsQueryable();
        Task AddAsync(T entity);
        Task<T> ReloadEntity(T entity, TKey id);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
