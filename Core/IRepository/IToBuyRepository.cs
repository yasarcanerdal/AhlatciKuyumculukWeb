using AhlatciKuyumculukWeb.Core.Entities;

namespace AhlatciKuyumculukWeb.Core.IRepository
{
	public interface IToBuyRepository : IRepository<ToBuy>
	{
		void Update(ToBuy toBuy);
		void Save();

	}
}
