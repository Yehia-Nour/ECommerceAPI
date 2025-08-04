namespace ECommerceAPI.Repositories.Interfaces
{
    public interface IGetAllRepository<T> where T : class
    {
        IQueryable<T> GetAll();
    }
}
