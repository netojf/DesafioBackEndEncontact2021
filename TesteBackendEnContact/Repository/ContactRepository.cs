using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.Contact;
using TesteBackendEnContact.DAO;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
	internal class ContactRepository : Repository<Contact>, IContactRepository
	{
		private readonly ContactBookDAO _contactBookDAO;
		private readonly CompanyDAO _CompanyDAO;

		public ContactRepository(IDbTransaction transaction) : base(transaction)
		{
			_modelDAO = new ContactDAO(transaction);
			_contactBookDAO = new ContactBookDAO(transaction);
			_CompanyDAO = new CompanyDAO(transaction); 
		}

		public override async Task<bool> EditAsync(Contact model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model.ContactBookId));

			return await base.EditAsync(model);
		}

		public override async Task<Contact> SaveAsync(Contact model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model.ContactBookId));

			int? _contactBookId = model.ContactBookId;
			if (_contactBookId != null)
			{
				return await _contactBookDAO.GetAsync((int)_contactBookId).ContinueWith<Task<Contact>>(async cb =>
				{
					return cb.Result == null
						? throw new Exception("The related ContactIdBook Data must be an existing ContactBook on the database")
						: await base.SaveAsync(model);
				}).Result;
			}
			throw new Exception("");
		}

		public List<Contact> SearchContact(string keystring)
		{
			//todo: make this with use of reflections
			string query =string.Format(@"SELECT *
							FROM Contact
							INNER JOIN Company ON Company.Id = Contact.CompanyId
							WHERE (Company.Name LIKE '%{0}%') or  (Company.ContactBookId LIKE '%{0}%')
							or (Contact.Name LIKE '%{0}%') or (Contact.Email LIKE '%{0}%')
							or (Contact.Phone LIKE '%{0}%') or (Contact.Address LIKE '%{0}%'); ", keystring);
			return _modelDAO.Connection.Query<Contact>(query, _modelDAO.Transaction).AsList<Contact>(); 
		}
	}
}