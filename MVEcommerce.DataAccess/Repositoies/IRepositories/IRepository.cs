using System.Linq.Expressions;

namespace MVEcommerce.DataAccess.Repositoies.IRepositories
{
	public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get all records of T from the database
        /// </summary>
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null);

        /// <summary>
        /// Get a record of T from the database based on the filter
        /// </summary>
        /// <param name="filter">The filter to apply to the query</param>
        /// <param name="includeProperties">The properties to include in the query</param>
        /// <returns></returns>
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);

        /// <summary>
        /// Add a record to database
        /// </summary>
        /// <param name="entity">entity to be added</param>
        void Add(T entity);

        /// <summary>
        /// Remove a record from the database
        /// </summary>
        /// <param name="entity">entity to ve removed</param>
        void Remove(T entity);

        /// <summary>
        /// Remove a range of records from the database
        /// </summary>
        /// <param name="entity">entity to be removed</param>
        /// <returns></returns>
        void RemoveRange(IEnumerable<T> entity);
    }
}