using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly ILogger<ContactBookController> _logger;
        IUnitOfWork _unitOfWork;

        public ContactBookController(ILogger<ContactBookController> logger, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ContactBook> Post(ContactBook contactBook)
        {
            return await _unitOfWork.ContactBookRepository.SaveAsync(contactBook);
        }

        [HttpDelete]
        public async Task Delete(int id)
        {
            await _unitOfWork.ContactBookRepository.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<ContactBook>> Get()
        {
            return await _unitOfWork.ContactBookRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ContactBook> Get(int id)
        {
            return await _unitOfWork.ContactBookRepository.GetAsync(id);
        }
    }
}
