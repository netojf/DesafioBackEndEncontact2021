using CsvHelper.Configuration.Attributes;
using TesteBackendEnContact.Core.Interface.Company;

namespace TesteBackendEnContact.Core.Domain.Company
{
    public class Company : ICompany
    {
        [Name("Id")]
        public int Id { get; private set; }

        [Name("ContactBookId")]
        public int ContactBookId { get; private set; }

        [Name("Name")]
        public string Name { get; private set; }

        public Company(int id, int contactBookId, string name)
        {
            Id = id;
            ContactBookId = contactBookId;
            Name = name;
        }
    }
}
