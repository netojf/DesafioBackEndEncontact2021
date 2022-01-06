using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Interfaces.Services;

namespace TesteBackendEnContact.Services
{
	public class CompanyService : CsvParserService<Company>, ICompanyService
	{
		public CompanyService(ILogger<CsvParserService<Company>> logger) : base(logger)
		{
		}

		public override List<Company> ReadCsvFileToModel<MAP>(string path) 
		{
			return base.ReadCsvFileToModel<MAP>(path);
		}

		//private readonly ILogger<CompanyService> _logger;

		//public CompanyService(ILogger<CompanyService> logger)
		//{
		//    _logger = logger;
		//}

		//public void WriteNewCsvFile(string path, List<Company> companyModels)
		//{
		//    using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
		//    using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
		//    {
		//        try
		//        {
		//            cw.WriteHeader<Company>();
		//        }
		//        catch (Exception e)
		//        {
		//            _logger.LogInformation("An Error has ocurred on writting Company csv header");
		//            _logger.LogInformation(e.Message);
		//        }
		//        finally
		//        {
		//            cw.NextRecord();
		//        }

		//        foreach (Company comp in companyModels)
		//        {
		//            try
		//            {
		//                cw.WriteRecord<Company>(comp);
		//            }
		//            catch (Exception e)
		//            {
		//                _logger.LogInformation("An Error has ocurred on writting Company csv data");
		//                _logger.LogInformation(e.Message);
		//            }
		//            finally
		//            {
		//                cw.NextRecord();

		//            }

		//        }
		//    }
		//}

		//List<Company> ICsvParserService<Company>.ReadCsvFileToModel(string path)
		//{
		//    try
		//    {
		//        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
		//        {
		//            HasHeaderRecord = true,
		//            PrepareHeaderForMatch = args => args.Header.ToLower()
		//        };

		//        using (var reader = new StreamReader(path, Encoding.Default))
		//        using (var csv = new CsvReader(reader, config))
		//        {
		//            csv.Context.RegisterClassMap<CompanyMap>();
		//            var records = csv.GetRecords<Company>().ToList();
		//            return records;
		//        }
		//    }
		//    catch (UnauthorizedAccessException e)
		//    {
		//        _logger.LogInformation("An Error ocurred on reading Records on Company: " + e.Message);
		//        throw new Exception(e.Message);
		//    }
		//    catch (FieldValidationException e)
		//    {
		//        _logger.LogInformation("An Error ocurred on reading Records on Company: " + e.Message);

		//        throw new Exception(e.Message);
		//    }
		//    catch (CsvHelperException e)
		//    {
		//        _logger.LogInformation("An Error ocurred on reading Records on Company: " + e.Message);

		//        throw new Exception(e.Message);
		//    }
		//    catch (Exception e)
		//    {
		//        _logger.LogInformation("An Error ocurred on reading Records on Company: " + e.Message);

		//        throw new Exception(e.Message);
		//    }
		//}
	}
}