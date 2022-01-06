using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Services;

namespace TesteBackendEnContact.Interfaces.Services
{
	public interface ICompanyService : ICsvParserService<Company> 
	{
	}
}