using System.Collections.Generic;
using TesteBackendEnContact.Core.Domain.Contact;

namespace TesteBackendEnContact.Repository.Interface
{
	public interface IContactRepository: IRepository<Contact>
	{
		List<Contact> SearchContact(string keystring);
	}
}