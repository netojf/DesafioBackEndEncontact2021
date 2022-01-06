using System.Data;
using TesteBackendEnContact.Core.Domain.Company;

namespace TesteBackendEnContact.DAO
{
	public class CompanyDAO : DAOBase<Company>
	{
		public CompanyDAO(IDbTransaction transaction) : base(transaction)
		{
		}
	}
}