using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.Company;

namespace TesteBackendEnContact.Repository.Interface
{ 
	public interface ICompanyRepository: IRepository<Company>
	{
		Dictionary<string, dynamic> GetCompanyContactbookContacts(int id);
	}
}