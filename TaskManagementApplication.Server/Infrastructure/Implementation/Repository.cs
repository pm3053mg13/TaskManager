using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TaskManagementApplication.Server.Core;
using TaskManagementApplication.Server.Database;

namespace TaskManagementApplication.Server.Infrastructure.Implementation
{
    public class Repository<T> : IRepository<T> where T : DomainBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public IQueryable<T> Table
        {
            get
            {
                return DbSet.AsQueryable();
            }
        }

        DbSet<T> DbSet { get { return _dbContext.Set<T>(); } }

        public T Get(int id)
        {
            return DbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression).SingleOrDefault();
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> expression)
        {
            return DbSet.Where(expression).ToArray();
        }

        public IEnumerable<T> Fetch(Expression<Func<T, bool>> expression, int pageSize, int pageNumber)
        {
            return DbSet.Where(expression).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToArray();
        }

        public void Save(T entity)
        {
            if (entity.IsNew)
            {
                Insert(entity);
            }
            else
            {
                Update(entity);
            }

            _dbContext.SaveChanges();
        }

        public void Insert(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Added;
        }

        public void Update(T entity)
        {
            var original = DbSet.Find(entity.Id);

            _dbContext.Entry(original).CurrentValues.SetValues(entity);
            _dbContext.Entry(original).State = EntityState.Modified;
        }

    }
}
