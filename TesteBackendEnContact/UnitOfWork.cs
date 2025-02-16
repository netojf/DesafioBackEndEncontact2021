using Microsoft.Data.Sqlite;
using System;
using System.Data;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact
{
	public class UnitOfWork : IUnitOfWork
	{
		private IDbConnection _connection;
		private IDbTransaction _transaction;
		private ICompanyRepository _companyRepository;
		private IContactBookRepository _contactBookRepository;
		private IContactRepository _contactRepository;
		private bool _disposed;

		public UnitOfWork(DatabaseConfig databaseConfig)
		{
			_connection = new SqliteConnection(databaseConfig.ConnectionString);
			_connection.Open();
			_transaction = _connection.BeginTransaction();
		}

		public ICompanyRepository CompanyRepository
		{
			get { return _companyRepository ??= new CompanyRepository(_transaction); }
		}

		public IContactBookRepository ContactBookRepository
		{
			get { return _contactBookRepository ??= new ContactBookRepository(_transaction); }
		}

		public IContactRepository ContactRepository
		{
			get { return _contactRepository ??= new ContactRepository(_transaction); }
		}

		public void Commit()
		{
			try
			{
				_transaction.Commit();
			}
			catch
			{
				_transaction.Rollback();
				throw;
			}
			finally
			{
				_transaction.Dispose();
				_transaction = _connection.BeginTransaction();
				resetRepositories();
			}
		}

		private void resetRepositories()
		{
			_companyRepository = null;
			_contactBookRepository = null;
			_contactRepository = null;
		}

		public void Dispose()
		{
			dispose(true);
			GC.SuppressFinalize(this);
		}

		private void dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
				{
					if (_transaction != null)
					{
						_transaction.Dispose();
						_transaction = null;
					}
					if (_connection != null)
					{
						_connection.Dispose();
						_connection = null;
					}
				}
				_disposed = true;
			}
		}

		~UnitOfWork()
		{
			dispose(false);
		}
	}
}