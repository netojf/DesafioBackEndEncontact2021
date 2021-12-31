using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;
using Dapper.Contrib.Extensions;

namespace TesteBackendEnContact.Core.Domain.Contact
{
    [Table("Contact")]
    public class Contact
    {
        [Dapper.Contrib.Extensions.Key]
        [Name("Id")]
        public int Id { get; private set; }

        [Required]
        public int? ContactBookId { get; set; }

        public int? CompanyId { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(100)]
        public string Address { get; set; }

    }
}