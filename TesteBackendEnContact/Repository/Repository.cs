using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TesteBackendEnContact.DAO;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
	internal abstract class Repository<M> : IRepository<M> where M : class
	{
		protected DAOBase<M> _modelDAO;

		public Repository(IDbTransaction transaction)
		{ }

		public virtual async Task<bool> DeleteAsync(int id)
		{
			return await _modelDAO.DeleteAsync(id);
		}

		public virtual async Task<IEnumerable<M>> GetAllAsync()
		{
			return await _modelDAO.GetAllAsync();
		}

		public virtual Task<M> GetAsync(int id)
		{
			return _modelDAO.GetAsync(id);
		}

		public virtual async Task<M> SaveAsync(M model)
		{
			return await _modelDAO.SaveAsync(model);
		}

		public virtual async Task<bool> EditAsync(M model)
		{
			if (model == null)
				throw new ArgumentNullException("Entity Can't be null");
			return await _modelDAO.EditAsync(model);
		}
	}
}