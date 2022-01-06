using System.Collections.Generic;
using System.Threading.Tasks;

namespace TesteBackendEnContact.Repository.Interface
{
	public interface IRepository<T> where T : class
	{
		Task<T> SaveAsync(T model);

		Task<bool> DeleteAsync(int id);

		Task<IEnumerable<T>> GetAllAsync();

		Task<bool> EditAsync(T model);

		Task<T> GetAsync(int id);
	}
}