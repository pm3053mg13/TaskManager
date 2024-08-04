using System.Linq.Expressions;

namespace TaskManagementApplication.Server.Infrastructure
{
    public interface IRepository<T>
    {
        T Get(int id);
        T Get(Expression<Func<T, bool>> expression);

        IEnumerable<T> Fetch(Expression<Func<T, bool>> expression);
        IEnumerable<T> Fetch(Expression<Func<T, bool>> expression, int pageNumber, int pageSize);

        void Save(T entity);

        IQueryable<T> Table { get; }
    }
}
