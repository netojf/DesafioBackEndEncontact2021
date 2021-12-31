using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
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
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.CompanyRepository.DeleteAsync(id);
            if (result == 0) return await Task.FromResult(StatusCode(500, "Erro ao deletar entrada!"));
            _unitOfWork.Commit();
            return await Task.FromResult(Ok());
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
            if (file?.ContentType != "application/vnd.ms-excel")
            {
                return await Task.FromResult(StatusCode(500, "Formato não permitido! O arquivo deve ser no formato csv!"));
            }

            string path = string.Format("{0}\\Data\\Companies", _webHostEnvironment.ContentRootPath);
            string fullPath = string.Format("{0}\\Data\\Companies\\{1}", _webHostEnvironment.ContentRootPath, file.FileName);

            try
            {
                var result = await Utils.SaveDataAsync(file, string.Format("{0}\\Data\\Companies", _webHostEnvironment.ContentRootPath)).ContinueWith(a =>
               Task<List<Company>>.FromResult(_service.ReadCsvFileToModel(fullPath)));

                foreach (var item in result.Result)
                {
                    await _unitOfWork.CompanyRepository.SaveAsync(item);
                }
                _unitOfWork.Commit();

                return await Task.FromResult(Ok());

            }
            catch (System.Exception e)
            {
                return await Task.FromResult(StatusCode(500, e.Message));
            }
        }

        [Route("export")]
        [HttpPost]
        public async Task<IActionResult> ExportAsync()
        {
            var companyList = (List<Company>)await _unitOfWork.CompanyRepository.GetAllAsync();
            string path = string.Format("{0}\\Data\\Companies\\{1}", _webHostEnvironment.ContentRootPath, "companies.csv");
            _service.WriteNewCsvFile(path, companyList);
            return await Task.FromResult(Ok());
        }
    }
}
