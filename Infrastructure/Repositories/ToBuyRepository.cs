using AhlatciKuyumculukWeb.Core.Entities;
using AhlatciKuyumculukWeb.Core.IRepository;
using AhlatciKuyumculukWeb.Infrastructure.Context;

namespace AhlatciKuyumculukWeb.Infrastructure.Repositories
{
	public class ToBuyRepository : Repository<ToBuy>, IToBuyRepository
	{
		private ApplicationDbContext _applicationDbContext;
		public ToBuyRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
		{
			_applicationDbContext = applicationDbContext; // DbContexti çektim
		}

		public void Save()
		{
			_applicationDbContext.SaveChanges();
		}

		public void Update(ToBuy toBuy)
		{
			_applicationDbContext.Update(toBuy);
		}
	}
}
