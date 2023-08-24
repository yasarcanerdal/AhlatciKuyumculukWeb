using AhlatciKuyumculukWeb.Core.Entities;
using AhlatciKuyumculukWeb.Core.IRepository;
using AhlatciKuyumculukWeb.Infrastructure.Context;

namespace AhlatciKuyumculukWeb.Infrastructure.Repositories
{
	public class CategoryRepository : Repository<Category>, ICategoryRepository
	{
		private ApplicationDbContext _applicationDbContext;
		public CategoryRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
			_applicationDbContext = applicationDbContext; // DbContexti çektim
		}

		public void Save()
		{
			_applicationDbContext.SaveChanges();
		}

		public void Update(Category category)
		{
			_applicationDbContext.Update(category);
		}
	}
}
