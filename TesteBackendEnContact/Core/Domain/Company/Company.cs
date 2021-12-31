using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using Dapper.Contrib.Extensions;

namespace TesteBackendEnContact.Core.Domain.Company
{
    [Table("Company")]
    public class Company
    {
        [Name("Id")]
        public int Id { get; private set; }

        [Name("ContactBookId")]
        [Required]
        public int? ContactBookId { get; private set; }

        [Name("Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; private set; }


        public Company()
        {
        }

        [JsonConstructorAttribute]
        public Company(string Name, int? ContactBookId)
        {
            this.Name = Name;
            this.ContactBookId = ContactBookId;

        }


    }
}
