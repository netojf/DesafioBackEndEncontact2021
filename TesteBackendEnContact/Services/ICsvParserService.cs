using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace TesteBackendEnContact.Services
{
	public interface ICsvParserService<T>
	{
		List<T> ReadCsvFileToModel<MAP>(string path) where MAP : ClassMap;
		List<T> ReadCsvFileToModelInMemory<MAP>(IFormFile data) where MAP : ClassMap;
		byte[] WriteNewCsvFileBytes(List<T> models); 
		void WriteNewCsvFile(string path, List<T> models);
	}
}