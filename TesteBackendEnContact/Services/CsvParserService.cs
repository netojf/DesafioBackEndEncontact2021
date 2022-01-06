using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace TesteBackendEnContact.Services
{
	public abstract class CsvParserService<M> : ICsvParserService<M> where M : class
	{
		private readonly ILogger<CsvParserService<M>> _logger;

		public CsvParserService(ILogger<CsvParserService<M>> logger)
		{
			_logger = logger;
		}

		public virtual List<M> ReadCsvFileToModel<MAP>(string path) where MAP : ClassMap
		{
			try
			{
				var config = new CsvConfiguration(CultureInfo.InvariantCulture)
				{
					HasHeaderRecord = true,
					PrepareHeaderForMatch = args => args.Header.ToLower()
				};

				using (var reader = new StreamReader(path, Encoding.Default))
				using (var csv = new CsvReader(reader, config))
				{
					csv.Context.RegisterClassMap<MAP>();
					var records = csv.GetRecords<M>().ToList();
					return records;
				}
			}
			catch (UnauthorizedAccessException e)
			{
				_logger.LogInformation(string.Format("An Error ocurred on reading Records on {0}: {1}", typeof(M).Name, e.Message));
				throw new Exception(e.Message);
			}
			catch (FieldValidationException e)
			{
				_logger.LogInformation(string.Format("An Error ocurred on reading Records on {0}: {1}", typeof(M).Name, e.Message));

				throw new Exception(e.Message);
			}
			catch (CsvHelperException e)
			{
				_logger.LogInformation(string.Format("An Error ocurred on reading Records on {0}: {1}", typeof(M).Name, e.Message));

				throw new Exception(e.Message);
			}
			catch (Exception e)
			{
				_logger.LogInformation(string.Format("An Error ocurred on reading Records on {0}: {1}", typeof(M).Name, e.Message));

				throw new Exception(e.Message);
			}
		}
		
		public virtual void WriteNewCsvFile(string path, List<M> models)
		{
			bool exists = Directory.Exists(path);

			if (!exists)
				Directory.CreateDirectory(path);

			using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
			using (CsvWriter cw = new CsvWriter(sw, CultureInfo.InvariantCulture))
			{
				try
				{
					cw.WriteHeader<M>();
				}
				catch (Exception e)
				{
					_logger.LogInformation("An Error has ocurred on writting M csv header");
					_logger.LogInformation(e.Message);
				}
				finally
				{
					cw.NextRecord();
				}

				foreach (M comp in models)
				{
					try
					{
						cw.WriteRecord<M>(comp);
					}
					catch (Exception e)
					{
						_logger.LogInformation("An Error has ocurred on writting M csv data");
						_logger.LogInformation(e.Message);
					}
					finally
					{
						cw.NextRecord();
					}
				}
			}
		}

		public virtual byte[] WriteNewCsvFileBytes(List<M> models)
		{
			using var memoryStream = new MemoryStream();
			using (var streamWriter = new StreamWriter(memoryStream))
			using (var cw = new CsvWriter(streamWriter, CultureInfo.InvariantCulture))
			{
				streamWriter.AutoFlush = true;
				try
				{
					cw.WriteHeader<M>();
				}
				catch (Exception e)
				{
					_logger.LogInformation(string.Format("An Error has ocurred on writting {0} csv header", typeof(M).Name));
					_logger.LogInformation(e.Message);
				}
				finally
				{
					cw.NextRecord();
				}
				cw.WriteRecords<M>(models);
			} // StreamWriter gets flushed here.

			return memoryStream.ToArray();
		}

		public List<M> ReadCsvFileToModelInMemory<MAP>(IFormFile file) where MAP : ClassMap
		{
			try
			{
				var config = new CsvConfiguration(CultureInfo.InvariantCulture)
				{
					HasHeaderRecord = true,
					PrepareHeaderForMatch = args => args.Header.ToLower()
				};

				using var data = file.OpenReadStream();
				using (var reader = new StreamReader(data, Encoding.Default))
				using (var csv = new CsvReader(reader, config))
				{
					csv.Context.RegisterClassMap<MAP>();
					var records = csv.GetRecords<M>().ToList();
					return records;
				}
			}
			catch (UnauthorizedAccessException e)
			{
				_logger.LogInformation(string.Format("An Error ocurred on reading Records on {0}: {1}", typeof(M).Name, e.Message));
				throw new Exception(e.Message);
			}
			catch (FieldValidationException e)
			{
				_logger.LogInformation(string.Format("An Error ocurred on reading Records on {0}: {1}", typeof(M).Name, e.Message));

				throw new Exception(e.Message);
			}
			catch (CsvHelperException e)
			{
				_logger.LogInformation(string.Format("An Error ocurred on reading Records on {0}: {1}", typeof(M).Name, e.Message));

				throw new Exception(e.Message);
			}
			catch (Exception e)
			{
				_logger.LogInformation(string.Format("An Error ocurred on reading Records on {0}: {1}", typeof(M).Name, e.Message));

				throw new Exception(e.Message);
			}
		}
	}
}