using AhlatciKuyumculukWeb.Core.Entities;
using AhlatciKuyumculukWeb.Core.IRepository;
using AhlatciKuyumculukWeb.Infrastructure.Context;

namespace AhlatciKuyumculukWeb.Infrastructure.Repositories
{
	public class ProductRepository : Repository<Product>, IProductRepository
	{
		private ApplicationDbContext _applicationDbContext;
		public ProductRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
			_applicationDbContext = applicationDbContext; // DbContexti çektim
		}

		public void Save()
		{
			_applicationDbContext.SaveChanges();
		}

		public void Update(Product product)
		{
			_applicationDbContext.Update(product);
		}
	}
}
