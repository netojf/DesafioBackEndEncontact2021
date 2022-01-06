using CsvHelper.Configuration;
using TesteBackendEnContact.Core.Domain.Contact;

namespace TesteBackendEnContact.Mappers
{
	public sealed class ContactMap : ClassMap<Contact>
	{
		public ContactMap()
		{
			Map(m => m.Id).Name("id");
			Map(m => m.ContactBookId).Name("contactbookid");
			Map(m => m.CompanyId).Name("companyId");
			Map(m => m.Name).Name("name");
			Map(m => m.Phone).Name("phone");
			Map(m => m.Email).Name("email");
			Map(m => m.Address).Name("address");
		}
	}
}