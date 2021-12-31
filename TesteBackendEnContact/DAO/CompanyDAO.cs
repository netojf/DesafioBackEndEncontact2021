using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using TesteBackendEnContact.Core.Domain.Company;

namespace TesteBackendEnContact.DAO
{
    public class CompanyDAO
    {
        private IDbTransaction transaction;
        IDbConnection Connection { get { return transaction.Connection; } }


        public CompanyDAO(IDbTransaction transaction)
        {
            this.transaction = transaction;
        }


        public async Task<int> DeleteAsync(int id)
        {
            return await Connection.ExecuteAsync(
                "DELETE FROM Company WHERE Id = @Id",
                param: new { Id = id },
                transaction: transaction
            );

        }

        public async Task<Company> GetAsync(int id)
        {
            var result = await Connection.QueryAsync<Company>(
                "SELECT * FROM Company WHERE Id = @Id",
                param: new { Id = id },
                transaction: transaction
            );
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await Connection.QueryAsync<Company>("Select * From Company");
        }

        public async Task<Company> SaveAsync(Company model)
        {
            await Connection.InsertAsync(model);
            return model;
        }
    }
}