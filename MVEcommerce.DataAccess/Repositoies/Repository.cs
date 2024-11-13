using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MVEcommerce.DataAccess.Data;
using MVEcommerce.DataAccess.Repositoies.IRepositories;

namespace MVEcommerce.DataAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Get a record of T from the database based on the filter
        /// </summary>
        /// <param name="filter">The filter to apply to the query</param>
        /// <param name="includeProperties">An comma-separated string. Ex: "category, customer"</param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            // Include properties will be comma separated
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            #pragma warning disable CS8603 // Possible null reference return.
            return query.Where(filter).FirstOrDefault();
            #pragma warning restore CS8603 // Possible null reference return.
        }

        /// <summary>
        /// Get all records of T from the database
        /// </summary>
        /// <param name="includeProperties">An comma-separated string. Ex: "category, customer"</param>
        /// <returns></returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            // Include properties will be comma separated
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }
    }
}