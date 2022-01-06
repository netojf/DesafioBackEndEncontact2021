using CsvHelper.Configuration.Attributes;
using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace TesteBackendEnContact.Core.Domain.ContactBook
{
	[Table("ContactBook")]
	public class ContactBook
	{
		[Dapper.Contrib.Extensions.Key]
		[Name("Id")]
		public int Id { get; set; }

		[Name("Name")]
		[Required]
		[StringLength(50)]
		public string Name { get; set; }
		
		//[JsonIgnore]
		//public ICollection<Company.Company> Companies { get; set; }
		
		//[JsonIgnore]
		//public ICollection<Contact.Contact> Contacts { get; set; }

		public ContactBook()
		{
		}

		[JsonConstructor]
		public ContactBook(string name)
		{
			Name = name;
		}

		public ContactBook(int id, string name)
		{
			Id = id;
			Name = name;
		}
	}
}