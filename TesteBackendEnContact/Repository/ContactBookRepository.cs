using System;
using System.Data;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.DAO;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
	internal class ContactBookRepository : Repository<ContactBook>, IContactBookRepository
	{

		public ContactBookRepository(IDbTransaction transaction) : base(transaction)
		{
			_modelDAO = new ContactBookDAO(transaction); 
		}

		public override async Task<bool> EditAsync(ContactBook model)
		{
			if (model.Name == null)
				throw new ArgumentNullException(nameof(model.Name));
			return await base.EditAsync(model);
		}

		public override async Task<ContactBook> SaveAsync(ContactBook model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model.Name));
			return await base.SaveAsync(model);
		}
	}
}