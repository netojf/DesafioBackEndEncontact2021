using CsvHelper.Configuration.Attributes;
using Dapper.Contrib.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TesteBackendEnContact.Core.Domain.Contact
{
	[Dapper.Contrib.Extensions.Table("Contact")]
	public class Contact
	{
		[Dapper.Contrib.Extensions.Key]
		public int Id { get; set; }

		[Required]
		//[ForeignKey("Company")]
		public int? ContactBookId { get; set; }

		//[ForeignKey("ContactBook")]
		public int? CompanyId { get; set; }

		[StringLength(50)]
		public string Name { get; set; }

		[StringLength(20)]
		public string Phone { get; set; }

		[StringLength(50)]
		public string Email { get; set; }

		[StringLength(100)]
		public string Address { get; set; }

		//[JsonIgnore]
		//public virtual ContactBook.ContactBook ContactBook { get; set; }

		//[JsonIgnore]
		//public virtual Company.Company Company { get; set; }

		public Contact()
		{ }

		[JsonConstructor]
		public Contact(int? contactBookId, int? companyId, string name, string phone, string email, string address)
		{
			ContactBookId = contactBookId;
			CompanyId = companyId;
			Name = name;
			Phone = phone;
			Email = email;
			Address = address;
		}
	}
}