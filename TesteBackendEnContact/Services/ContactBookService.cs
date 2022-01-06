using Microsoft.Extensions.Logging;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Interfaces.Services;

namespace TesteBackendEnContact.Services
{
	public class ContactBookService : CsvParserService<ContactBook>, IContactBookService
	{
		public ContactBookService(ILogger<CsvParserService<ContactBook>> logger) : base(logger)
		{ }

		//private readonly ILogger<ContactBookService> _logger;

		//public ContactBookService(ILogger<ContactBookService> logger)
		//{
		//    _logger = logger;
		//}

		//public void WriteNewCsvFile(string path, List<ContactBook> models)
		//{
		//    using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
		//    using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
		//    {
		//        try
		//        {
		//            cw.WriteHeader<ContactBook>();
		//        }
		//        catch (Exception e)
		//        {
		//            _logger.LogInformation("An Error has ocurred on writting ContactBook csv header");
		//            _logger.LogInformation(e.Message);
		//        }
		//        finally
		//        {
		//            cw.NextRecord();
		//        }

		//        foreach (ContactBook comp in models)
		//        {
		//            try
		//            {
		//                cw.WriteRecord<ContactBook>(comp);
		//            }
		//            catch (Exception e)
		//            {
		//                _logger.LogInformation("An Error has ocurred on writting ContactBook csv data");
		//                _logger.LogInformation(e.Message);
		//            }
		//            finally
		//            {
		//                cw.NextRecord();

		//            }

		//        }
		//    }
		//}

		//List<ContactBook> ICsvParserService<ContactBook>.ReadCsvFileToModel(string path)
		//{
		//    try
		//    {
		//        using (var reader = new StreamReader(path, Encoding.Default))
		//        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
		//        {
		//            csv.Context.RegisterClassMap<ContactBookMap>();
		//            var records = csv.GetRecords<ContactBook>().ToList();
		//            return records;
		//        }
		//    }
		//    catch (UnauthorizedAccessException e)
		//    {
		//        _logger.LogInformation("An Error ocurred on reading Records on ContactBook: " + e.Message);
		//        throw new Exception(e.Message);
		//    }
		//    catch (FieldValidationException e)
		//    {
		//        _logger.LogInformation("An Error ocurred on reading Records on ContactBook: " + e.Message);

		//        throw new Exception(e.Message);
		//    }
		//    catch (CsvHelperException e)
		//    {
		//        _logger.LogInformation("An Error ocurred on reading Records on ContactBook: " + e.Message);

		//        throw new Exception(e.Message);
		//    }
		//    catch (Exception e)
		//    {
		//        _logger.LogInformation("An Error ocurred on reading Records on ContactBook: " + e.Message);

		//        throw new Exception(e.Message);
		//    }
		//}
	}
}