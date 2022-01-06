using System.Data;
using TesteBackendEnContact.Core.Domain.ContactBook;

namespace TesteBackendEnContact.DAO
{
	public class ContactBookDAO : DAOBase<ContactBook>
	{
		public ContactBookDAO(IDbTransaction transaction) : base(transaction)
		{
		}
	}
}