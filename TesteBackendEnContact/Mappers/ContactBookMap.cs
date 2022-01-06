using CsvHelper.Configuration;
using TesteBackendEnContact.Core.Domain.ContactBook;

namespace TesteBackendEnContact.Mappers
{
	public sealed class ContactBookMap : ClassMap<ContactBook>
	{
		public ContactBookMap()
		{
			Map(m => m.Id).Name("Id");
			Map(m => m.Name).Name("Name");
		}
	}
}