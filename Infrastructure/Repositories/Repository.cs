using AhlatciKuyumculukWeb.Core.IRepository;
using AhlatciKuyumculukWeb.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace AhlatciKuyumculukWeb.Infrastructure.Repositories
{
	public class Repository<T> : IRepository<T> where T : class
	{
		private readonly ApplicationDbContext _applicationDbContext;
		internal DbSet<T> dbSet; 

		public Repository(ApplicationDbContext applicationDbContext)
		{
			_applicationDbContext = applicationDbContext;
			this.dbSet = _applicationDbContext.Set<T>();
			_applicationDbContext.Products.Include(x => x.Category).Include(x => x.CategoryId); // Kategorileri ürünler sayfasına frogin Key çekme işlemi
		}
		public void Add(T entity)
		{
			dbSet.Add(entity);
		}

		public void Delete(T entity)
		{
			dbSet.Remove(entity); // Tek bir kaydı siler
		}

		public void DeleteRange(IEnumerable<T> entities)
		{
			dbSet.RemoveRange(entities); // Gönderdiğimiz aralıktaki işlemleri siler
		}

		public T Get(System.Linq.Expressions.Expression<Func<T, bool>> filtre, string? includeProps = null)
		{
			IQueryable<T> query = dbSet;
			query = query.Where(filtre);

			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return query.FirstOrDefault(); // ilkini döndür.
		}

		public IEnumerable<T> GetAll(string? includeProps = null)
		{
			IQueryable<T> query = dbSet;

			if (!string.IsNullOrEmpty(includeProps))
			{
				foreach (var includeProp in includeProps.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProp);
				}
			}

			return query.ToList();
		}

		public async Task<IQueryable<T>> GetAllAsync(params string[] includeColumns)
		{
			IQueryable<T> query = dbSet;

			if (includeColumns.Any())
			{
				foreach (var includeColumn in includeColumns)
				{
					query = query.Include(includeColumn);
				}
			}
			return await Task.FromResult(query);
		}
	}
}
