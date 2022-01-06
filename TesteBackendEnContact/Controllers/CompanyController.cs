using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Interfaces.Services;
using TesteBackendEnContact.Mappers;

namespace TesteBackendEnContact.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class CompanyController : ControllerBase
	{
		private readonly ILogger<CompanyController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private static IWebHostEnvironment _webHostEnvironment;
		private readonly ICompanyService _service;

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
			var result = await _unitOfWork.CompanyRepository.SaveAsync(company);
			_unitOfWork.Commit();

			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<Company>> Put(Company company)
		{
			var result = await _unitOfWork.CompanyRepository.EditAsync(company);
			_unitOfWork.Commit();

			if (result)
			{
				return Ok(company);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			var result = await _unitOfWork.CompanyRepository.DeleteAsync(id);
			if (!result) return await Task.FromResult(StatusCode(500, "Erro ao deletar entrada!"));
			_unitOfWork.Commit();

			return await Task.FromResult(Ok());
		}

		[HttpGet]
		public async Task<IEnumerable<Company>> Get()
		{
			return await _unitOfWork.CompanyRepository.GetAllAsync();
		}

		[HttpGet]
		[Route("companyfulldata/{id}")]
		public Dictionary<string, dynamic> GetCompanyData(int id)
		{
			return _unitOfWork.CompanyRepository.GetCompanyContactbookContacts(id); 
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Company>> Get(int id)
		{
			Company result = await _unitOfWork.CompanyRepository.GetAsync(id);

			return result != null ? Ok(result) : NotFound(id);
		}

		[Route("import")]
		[HttpPost]
		public async Task<IActionResult> ImportAsync([FromForm] IFormFile file)
		{
			if (file?.ContentType != "application/vnd.ms-excel")
			{
				return await Task.FromResult(StatusCode(500, "Formato não permitido! O arquivo deve ser no formato csv!"));
			}

			try
			{
				var result = await Task<List<Company>>.FromResult(_service.ReadCsvFileToModelInMemory<CompanyMap>(file));

				try
				{
					foreach (var item in result)
					{
						await _unitOfWork.CompanyRepository.SaveAsync(item);
					}
				}
				catch(Exception e)
				{
					_logger.LogError(string.Format("an unexpected error occurred when saving {0}", e.Message));
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
			var modelList = (List<Company>)await _unitOfWork.CompanyRepository.GetAllAsync();
			byte[] bytes = _service.WriteNewCsvFileBytes(modelList);
			return File(bytes, "application/octet-stream", "companies.csv");
		}
	}
}