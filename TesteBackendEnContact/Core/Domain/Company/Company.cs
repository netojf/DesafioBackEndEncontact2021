using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;
using Dapper.Contrib.Extensions;

namespace TesteBackendEnContact.Core.Domain.Company
{
    [Table("Company")]
    public class Company
    {
        public int Id { get; set; }

        [Required]
        public int? ContactBookId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }


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
