using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace TesteBackendEnContact
{
	public class Utils
	{
		private static ILogger _logger;

		public Utils(ILogger logger)
		{
			_logger = logger;
		}

		public static async Task<bool> SaveDataAsync(IFormFile obj, string path)
		{
			_logger?.LogInformation(string.Format("Filename: {0}", obj?.FileName));
			_logger?.LogInformation(string.Format("ContentType: {0}", obj?.ContentType));
			_logger?.LogInformation(string.Format("Path: {0}", path));
			_logger?.LogInformation(string.Format("FullPath: {0}\\{1}", path, obj.FileName));

			if (obj != null)
			{
				try
				{
					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(path);
						_logger?.LogInformation("Directory does not exists, creating...");
					}

					using FileStream filestream = System.IO.File.Create(path + "\\" + obj.FileName);
					await obj.CopyToAsync(filestream);
					filestream.Flush();
					return true;
				}
				catch (Exception)
				{
					throw;
				}
			}
			else
			{
				return false;
			}
		}
	}
}