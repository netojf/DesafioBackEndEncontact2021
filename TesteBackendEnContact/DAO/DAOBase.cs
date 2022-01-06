using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace TesteBackendEnContact.DAO
{
	public abstract class DAOBase<T> where T : class
	{
		private IDbTransaction transaction;

		public IDbConnection Connection
		{ get { return transaction.Connection; } }

		public IDbTransaction Transaction { get => transaction; set => transaction = value; }

		public DAOBase(IDbTransaction transaction)
		{
			this.transaction = transaction;
		}

		public virtual async Task<bool> DeleteAsync(int id)
		{
			return await Connection.GetAsync<T>(id, transaction).ContinueWith(async e =>
		   {
			   return await Connection.DeleteAsync(e.Result, transaction);
		   }).Result;
		}

		public virtual async Task<T> GetAsync(int id)
		{
			var result = await Connection.GetAsync<T>(id, transaction);
			return result;
		}

		public virtual async Task<IEnumerable<T>> GetAllAsync()
		{
			return await Connection.GetAllAsync<T>(transaction);
		}

		public virtual async Task<T> SaveAsync(T model)
		{
			await Connection.InsertAsync(model);
			return model;
		}

		public virtual async Task<bool> EditAsync(T model)
		{
			return await Connection.UpdateAsync(model, transaction);
		}
	}
}