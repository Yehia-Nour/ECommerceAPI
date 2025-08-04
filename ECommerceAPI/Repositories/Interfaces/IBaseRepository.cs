namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        
        Task<T?> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
