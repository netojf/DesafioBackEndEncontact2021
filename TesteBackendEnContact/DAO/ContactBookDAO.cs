using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using TesteBackendEnContact.Core.Domain.ContactBook;

namespace TesteBackendEnContact.DAO
{
    public class ContactBookDAO
    {
        private IDbTransaction transaction;
        IDbConnection Connection { get { return transaction.Connection; } }



        public ContactBookDAO(IDbTransaction transaction)
        {
            this.transaction = transaction;
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await Connection.ExecuteAsync(
                "DELETE FROM ContactBook WHERE Id = @id",
                param: new { Id = id },
                transaction: transaction
            );
        }

        public async Task<ContactBook> GetAsync(int id)
        {
            var result = await Connection.QueryAsync<ContactBook>(
                "SELECT * FROM ContactBook WHERE Id = @Id",
                param: new { Id = id },
                transaction: transaction
            );
            return result.FirstOrDefault();
        }

        public async Task<IEnumerable<ContactBook>> GetAllAsync()
        {
            return await Connection.QueryAsync<ContactBook>("Select * From ContactBook");
        }

        public async Task<ContactBook> SaveAsync(ContactBook model)
        {
            await Connection.InsertAsync(model);
            return model;
        }

    }
}