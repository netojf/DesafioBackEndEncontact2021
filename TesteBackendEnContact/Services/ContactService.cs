using Microsoft.Extensions.Logging;
using TesteBackendEnContact.Core.Domain.Contact;
using TesteBackendEnContact.Services.Interfaces;

namespace TesteBackendEnContact.Services
{
	public class ContactService : CsvParserService<Contact>, IContactService
	{
		public ContactService(ILogger<CsvParserService<Contact>> logger) : base(logger)
		{
		}
	}
}