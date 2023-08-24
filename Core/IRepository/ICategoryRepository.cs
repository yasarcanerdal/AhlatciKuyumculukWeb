using AhlatciKuyumculukWeb.Core.Entities;

namespace AhlatciKuyumculukWeb.Core.IRepository
{
	public interface ICategoryRepository : IRepository<Category>
	{
		void Update(Category category);
		void Save();

	}
}
