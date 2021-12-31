using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Services;

namespace TesteBackendEnContact.Interfaces.Services
{
    public interface IContactBookService : ICsvParserService<ContactBook>
    {

    }
}