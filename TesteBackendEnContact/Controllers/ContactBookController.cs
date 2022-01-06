using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Interfaces.Services;
using TesteBackendEnContact.Mappers;

namespace TesteBackendEnContact.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ContactBookController : ControllerBase
	{
		private readonly ILogger<ContactBookController> _logger;
		private IUnitOfWork _unitOfWork;
		private static IWebHostEnvironment _webHostEnvironment;
		private readonly IContactBookService _service;

		public ContactBookController(ILogger<ContactBookController> logger, IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, IContactBookService service)
		{
			_unitOfWork = unitOfWork;
			_logger = logger;
			_service = service;
			_webHostEnvironment = webHostEnvironment;
		}

		[HttpPost]
		public async Task<ContactBook> Post(ContactBook contactBook)
		{
			var result = await _unitOfWork.ContactBookRepository.SaveAsync(contactBook);
			_unitOfWork.Commit();

			return result;
		}

		[HttpPut]
		public async Task<ActionResult<ContactBook>> Put(ContactBook model)
		{
			var result = await _unitOfWork.ContactBookRepository.EditAsync(model);
			_unitOfWork.Commit();

			if (result)
			{
				return Ok(model);
			}
			else
			{
				return BadRequest(result);
			}
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			bool result = await _unitOfWork.ContactBookRepository.DeleteAsync(id);
			if (!result) return await Task.FromResult(StatusCode(500, "Erro ao deletar entrada!"));
			_unitOfWork.Commit();

			return await Task.FromResult(Ok());
		}

		[HttpGet]
		public async Task<IEnumerable<ContactBook>> Get()
		{
			return await _unitOfWork.ContactBookRepository.GetAllAsync();
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<ContactBook>> Get(int id)
		{
			var result = await _unitOfWork.ContactBookRepository.GetAsync(id);
			return result == null ? NotFound(id) : Ok(result);
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
				var result = await Task<List<ContactBook>>.FromResult(_service.ReadCsvFileToModelInMemory<ContactBookMap>(file));

				try
				{
					foreach (var item in result)
					{
						await _unitOfWork.ContactBookRepository.SaveAsync(item);
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
		public async Task<IActionResult> ExportAsync()
		{
			var modelList = (List<ContactBook>)await _unitOfWork.ContactBookRepository.GetAllAsync();
			byte[] bytes = _service.WriteNewCsvFileBytes(modelList);
			return File(bytes, "application/octet-stream", "contactBook.csv");
		}
	}
}