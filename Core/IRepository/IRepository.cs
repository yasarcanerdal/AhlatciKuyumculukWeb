using System.Linq.Expressions;

namespace AhlatciKuyumculukWeb.Core.IRepository
{
	public interface IRepository<T> where T : class // T > Category
	{
		IEnumerable<T> GetAll(string? includeProps = null);
		T Get(Expression<Func<T, bool>> filtre, string? includeProps = null);
		void Add(T entity);
		void Delete(T entity);
		void DeleteRange(IEnumerable<T> entities);
	}
}
