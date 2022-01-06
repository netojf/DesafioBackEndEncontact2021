using Dapper.Contrib.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TesteBackendEnContact.Core.Domain.Company
{
	[Dapper.Contrib.Extensions.Table("Company")]
	public class Company
	{
		[Dapper.Contrib.Extensions.Key]
		public int Id { get; set; }

		[Required]
		//[ForeignKey("ContactBook")]
		public int? ContactBookId { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		//[JsonIgnore]
		//public virtual ContactBook.ContactBook ContactBook { get; set; }

		//[JsonIgnore]
		//public virtual ICollection<Contact.Contact> Contacts { get; set; }

		public Company()
		{
		}

		[JsonConstructor]
		public Company(string Name, int? ContactBookId)
		{
			this.Name = Name;
			this.ContactBookId = ContactBookId;
		}
	}
}