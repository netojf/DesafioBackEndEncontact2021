using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Interfaces.Services;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;
        IUnitOfWork _unitOfWork;
        private static IWebHostEnvironment _webHostEnvironment;
        ICompanyService _service;


        public CompanyController(ILogger<CompanyController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, ICompanyService service)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Company>> Post(Company company)
        {
            return Ok(await _unitOfWork.CompanyRepository.SaveAsync(company));
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation(id.ToString());
            await _unitOfWork.CompanyRepository.DeleteAsync(id);
            _unitOfWork.Commit();
        }

        [HttpGet]
        public async Task<IEnumerable<Company>> Get()
        {
            return (IEnumerable<Company>)await _unitOfWork.CompanyRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<Company> Get(int id)
        {
            return await _unitOfWork.CompanyRepository.GetAsync(id);
        }

        [Route("import")]
        [HttpPost]
        public async Task<IActionResult> ImportAsync([FromForm] IFormFile file)
        {
            _logger?.LogInformation(file?.ContentType);

            if (file?.ContentType != "application/vnd.ms-excel")
            {
                return await Task.FromResult(StatusCode(500, "Formato não permitido! O arquivo deve ser no formato csv!"));
            }
            string path = string.Format("{0}\\Data\\Companies", _webHostEnvironment.ContentRootPath);
            string fullPath = string.Format("{0}\\Data\\Companies\\{1}", _webHostEnvironment.ContentRootPath, file.FileName);
            var result = await Utils.SaveDataAsync(file, string.Format("{0}\\Data\\Companies", _webHostEnvironment.ContentRootPath)).ContinueWith(a =>
                Task<List<Company>>.FromResult(_service.ReadCsvFileToEmployeeModel(fullPath))
            );

            _logger?.LogInformation(result?.ToString());

            // var saved = await Task<List<Company>>.FromResult(_service.ReadCsvFileToEmployeeModel(fullPath));

            return await Task.FromResult(Ok());
        }
    }
}
