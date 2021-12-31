using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using Microsoft.Extensions.Logging;
using TesteBackendEnContact.Core.Domain.Company;
using TesteBackendEnContact.Mappers;

namespace TesteBackendEnContact.Services
{
    public class CompanyService : ICsvParserService<Company>
    {

        private readonly ILogger<CompanyService> _logger;

        public CompanyService(ILogger<CompanyService> logger)
        {
            _logger = logger;
        }

        public void WriteNewCsvFile(string path, List<Company> companyModels)
        {
            using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
            {
                try
                {
                    cw.WriteHeader<Company>();
                }
                catch (Exception e)
                {
                    _logger.LogInformation("An Error has ocurred on writting Company csv header");
                    _logger.LogInformation(e.Message);
                }
                finally
                {
                    cw.NextRecord();
                }

                foreach (Company comp in companyModels)
                {
                    try
                    {
                        cw.WriteRecord<Company>(comp);
                    }
                    catch (Exception e)
                    {
                        _logger.LogInformation("An Error has ocurred on writting Company csv data");
                        _logger.LogInformation(e.Message);
                    }
                    finally
                    {
                        cw.NextRecord();

                    }

                }
            }
        }

        List<Company> ICsvParserService<Company>.ReadCsvFileToEmployeeModel(string path)
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.Default))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CompanyMap>();
                    var records = csv.GetRecords<Company>().ToList();
                    return records;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                _logger.LogInformation("An Error ocurred on reading Records on Company: " + e.Message);
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                _logger.LogInformation("An Error ocurred on reading Records on Company: " + e.Message);

                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                _logger.LogInformation("An Error ocurred on reading Records on Company: " + e.Message);

                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogInformation("An Error ocurred on reading Records on Company: " + e.Message);

                throw new Exception(e.Message);
            }
        }
    }
}