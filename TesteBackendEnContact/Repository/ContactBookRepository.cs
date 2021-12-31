using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.DAO;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    internal class ContactBookRepository : RepositoryBase, IContactBookRepository
    {
        ContactBookDAO _contactBookDAO;

        public ContactBookRepository(IDbTransaction transaction) : base(transaction)
        {
            _contactBookDAO = new ContactBookDAO(transaction);
        }

        public async Task DeleteAsync(int id)
        {
            await _contactBookDAO.DeleteAsync(id);
        }

        public async Task<IEnumerable<ContactBook>> GetAllAsync()
        {
            return await _contactBookDAO.GetAllAsync();
        }

        public Task<ContactBook> GetAsync(int id)
        {
            return _contactBookDAO.GetAsync(id);
        }

        public async Task<ContactBook> SaveAsync(ContactBook model)
        {
            if (model == null)
                throw new ArgumentNullException("entity");

            return await _contactBookDAO.SaveAsync(model);
        }

        // public async Task<ContactBook> SaveAsync(ContactBook contactBook)
        // {
        //     using var connection = new SqliteConnection(databaseConfig.ConnectionString);

        //     await connection.InsertAsync(contactBook);

        //     return contactBook;
        // }


        // public async Task DeleteAsync(int? id)
        // {
        //     using var connection = new SqliteConnection(databaseConfig.ConnectionString);
        //     int? modelId = id;
        //     bool deleted = await connection.DeleteAsync(modelId);
        //     // TODO
        //     var sql = "";

        //     await connection.ExecuteAsync(sql);
        // }




        // public async Task<IEnumerable<ContactBook>> GetAllAsync()
        // {
        //     using var connection = new SqliteConnection(databaseConfig.ConnectionString);

        //     var query = "SELECT * FROM ContactBook";
        //     var result = await connection.QueryAsync<ContactBook>(query);

        //     var returnList = new List<ContactBook>();

        //     foreach (var AgendaSalva in result.ToList())
        //     {
        //         ContactBook Agenda = new ContactBook(AgendaSalva.Id, AgendaSalva.Name.ToString());
        //         returnList.Add(Agenda);
        //     }

        //     return returnList.ToList();
        // }
        // public async Task<ContactBook> GetAsync(int id)
        // {
        //     var list = await GetAllAsync();

        //     return list.ToList().Where(item => item.Id == id).FirstOrDefault();
        // }
    }

    // [Table("ContactBook")]
    // public class ContactBookDao : ContactBook
    // {
    //     [Key]
    //     public int Id { get; set; }
    //     public string Name { get; set; }

    //     public ContactBookDao()
    //     {
    //     }

    //     public ContactBookDao(ContactBook contactBook)
    //     {
    //         Id = contactBook.Id;
    //         Name = Name;
    //     }

    //     public ContactBook Export() => new ContactBook(Id, Name);
    // }
}
