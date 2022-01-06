using System;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact
{
	public interface IUnitOfWork : IDisposable
	{
		ICompanyRepository CompanyRepository { get; }
		IContactBookRepository ContactBookRepository { get; }
		IContactRepository ContactRepository { get; }

		void Commit();
	}
}