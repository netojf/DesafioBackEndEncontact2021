using CsvHelper.Configuration;
using TesteBackendEnContact.Core.Domain.Company;

namespace TesteBackendEnContact.Mappers
{
    public sealed class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Map(m => m.Id).Name("id");
            Map(m => m.ContactBookId).Name("contactbookid");
            Map(m => m.Name).Name("name");
        }
    }
}