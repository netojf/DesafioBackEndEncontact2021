using CsvHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.Contact;
using TesteBackendEnContact.Mappers;
using TesteBackendEnContact.Services.Interfaces;

namespace TesteBackendEnContact.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ContactController : ControllerBase
	{
		private readonly ILogger<CompanyController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private static IWebHostEnvironment _webHostEnvironment;
		private readonly IContactService _service;

		public ContactController(ILogger<CompanyController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IContactService service)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_webHostEnvironment = webHostEnvironment;
			_service = service;
		}

		[HttpPost]
		public async Task<ActionResult<Contact>> Post(Contact company)
		{
			var result = await _unitOfWork.ContactRepository.SaveAsync(company);
			_unitOfWork.Commit();

			return Ok(result);
		}

		[HttpPut]
		public async Task<ActionResult<Contact>> Put(Contact company)
		{
			var result = await _unitOfWork.ContactRepository.EditAsync(company);
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
			var result = await _unitOfWork.ContactRepository.DeleteAsync(id);
			if (!result) return await Task.FromResult(StatusCode(500, "Erro ao deletar entrada!"));
			_unitOfWork.Commit();

			return await Task.FromResult(Ok());
		}

		[HttpGet]
		public async Task<IEnumerable<Contact>> Get()
		{
			return await _unitOfWork.ContactRepository.GetAllAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Contact>> Get(int id)
		{
			Contact result = await _unitOfWork.ContactRepository.GetAsync(id);

			return result != null ? Ok(result) : NotFound(id);
		}

		[HttpGet]
		[Route("search/{keystring}")]
		public List<Contact> SearchContact(string keystring)
		{
			return  _unitOfWork.ContactRepository.SearchContact(keystring);
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
				var result = await Task<List<Contact>>.FromResult(_service.ReadCsvFileToModelInMemory<ContactMap>(file));

				try
				{
					foreach (var item in result)
					{
						await _unitOfWork.ContactRepository.SaveAsync(item);
					}
				}
				catch (Exception e)
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
		[ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(BadRequestObjectResult), 400)]
		public async Task<IActionResult> ExportAsync()
		{
			var modelList = (List<Contact>)await _unitOfWork.ContactRepository.GetAllAsync();
			byte[] bytes = _service.WriteNewCsvFileBytes(modelList); 
			return File(bytes, "application/octet-stream", "contact.csv");
		}
	}
}