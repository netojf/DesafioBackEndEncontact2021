using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.DAO;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    internal class CompanyRepository : RepositoryBase, ICompanyRepository
    {
        CompanyDAO _companyDAO;

        public CompanyRepository(IDbTransaction transaction) : base(transaction)
        {
            _companyDAO = new CompanyDAO(transaction);
        }

        public async Task DeleteAsync(int id)
        {
            await _companyDAO.DeleteAsync(id);
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _companyDAO.GetAllAsync();
        }

        public Task<Company> GetAsync(int id)
        {
            return _companyDAO.GetAsync(id);
        }

        public async Task<Company> SaveAsync(Company model)
        {
            if (model == null)
                throw new ArgumentNullException("entity");

            return await _companyDAO.SaveAsync(model);
        }


    }

}
