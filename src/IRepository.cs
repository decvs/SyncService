namespace SyncService
{
    public interface IRepository<T> where T : Entity
    {
        void Delete(T entity);
        T Find(int id);
        System.Linq.IQueryable<T> GetAll();
        T Insert(T entity);
        T Update(T entity);
    }
}