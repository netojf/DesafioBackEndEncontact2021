using System.Data;
using TesteBackendEnContact.Core.Domain.Contact;

namespace TesteBackendEnContact.DAO
{
	public class ContactDAO : DAOBase<Contact>
	{
		public ContactDAO(IDbTransaction transaction) : base(transaction)
		{
		}
	}
}