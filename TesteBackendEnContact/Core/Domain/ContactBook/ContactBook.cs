using System.ComponentModel.DataAnnotations;
using CsvHelper.Configuration.Attributes;

namespace TesteBackendEnContact.Core.Domain.ContactBook
{
    public class ContactBook
    {
        [Key]
        [Name("Id")]
        public int Id { get; set; }

        [Name("Name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ContactBook(int id, string name)
        {
            Id = id;
            Name = name;
        }

    }
}
