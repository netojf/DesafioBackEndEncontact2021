using AutoMapper;
using Dapper;
using Dapper.Contrib.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Core.Domain.Contact;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.DAO;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
	internal class CompanyRepository : Repository<Company>, ICompanyRepository
	{
		private readonly ContactBookDAO _contactBookDAO;
		private readonly ContactDAO _contactDAO;


		public CompanyRepository(IDbTransaction transaction) : base(transaction)
		{
			_modelDAO = new CompanyDAO(transaction);
			_contactBookDAO = new ContactBookDAO(transaction);
			_contactDAO = new ContactDAO(transaction); 
		}

		public override async Task<Company> SaveAsync(Company model)
		{
			if (model.ContactBookId == null)
				throw new ArgumentNullException(nameof(model.ContactBookId));

			if (model.Name == null)
				throw new ArgumentNullException(nameof(model.Name));

			int _contactId = (int)model.ContactBookId;

			return await _contactBookDAO.GetAsync(_contactId).ContinueWith(async cb =>
			{
				if (cb.Result != null)
				{
					return await base.SaveAsync(model);
				}
				else
				{
					throw new Exception("The related ContactIdBook Data must be an existing ContactBook in the database");
				}
			}).Result;
		}

		public override async Task<bool> EditAsync(Company model)
		{
			if (model.ContactBookId == null)
				throw new ArgumentNullException(nameof(model.ContactBookId));

			if (model.Name == null)
				throw new ArgumentNullException(nameof(model.Name));

			int _contactId = model.ContactBookId != null ? (int)model.ContactBookId : 0;

			return await _contactBookDAO.GetAsync(_contactId).ContinueWith<Task<bool>>(async cb =>
			{
				return cb.Result == null
					? throw new Exception("The related ContactIdBook Data must be an existing ContactBook in the database")
					: await base.EditAsync(model);
			}).Result;
		}

		public Dictionary<string, dynamic> GetCompanyContactbookContacts(int id)
		{

			Dictionary<string, dynamic> companydummy = new Dictionary<string, dynamic>();
			Company company = _modelDAO.Connection.Get<Company>(id); 

			if(company!= null)
			{
				companydummy.Add("Id", company.Id);
				companydummy.Add("Name", company.Name);
				ContactBook contactBook;
				if (company.ContactBookId != null)
				{
					contactBook = _contactBookDAO.Connection.Get<ContactBook>((int)company.ContactBookId, _contactBookDAO.Transaction);

					if (contactBook != null)
					{
						Dictionary<string, dynamic> contactBookDummy = new Dictionary<string, dynamic>();
						contactBookDummy.Add("Id", contactBook.Id);
						contactBookDummy.Add("Name", contactBook.Name);
						companydummy.Add("ContactBookId", contactBook.Id);

						int companyId = (int)company.Id;
						int contactBookId = (int)contactBook.Id;

						string sqlQuery = string.Format(@"SELECT * FROM Contact
													WHERE Contact.CompanyId = {0} and Contact.ContactBookId = {1};", companyId, contactBookId);
						var contacts = _contactDAO.Connection.Query<Contact>(sqlQuery, _contactDAO.Transaction).ToDictionary(x => x.Id, x => x);
						if (contacts != null)
						{
							contactBookDummy.Add("Contacts", contacts); 
						}
						companydummy.Add("ContactBook", contactBookDummy);
					}
				}
			}
			return companydummy; 
		}
	}
}