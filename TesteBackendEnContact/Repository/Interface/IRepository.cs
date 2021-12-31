using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IRepository<T> where T : class
    {


        Task<T> SaveAsync(T model);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);

    }
}