using AhlatciKuyumculukWeb.Core.Entities;

namespace AhlatciKuyumculukWeb.Core.IRepository
{
	public interface IProductRepository : IRepository<Product>
	{
		void Update(Product product);
		void Save();

	}
}
