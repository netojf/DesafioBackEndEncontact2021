using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact
{
    public interface IUnitOfWork : IDisposable
    {
        ICompanyRepository CompanyRepository { get; }
        IContactBookRepository ContactBookRepository { get; }

        void Commit();
    }
}