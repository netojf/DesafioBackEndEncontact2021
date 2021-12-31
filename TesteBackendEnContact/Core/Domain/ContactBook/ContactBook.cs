using CsvHelper.Configuration.Attributes;
using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Core.Domain.ContactBook
{
    public class ContactBook : IContactBook
    {
        [Name("Id")]
        public int Id { get; set; }

        [Name("Name")]
        public string Name { get; set; }

        public ContactBook(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
