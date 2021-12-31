using System.Collections.Generic;

namespace TesteBackendEnContact.Services
{
    public interface ICsvParserService<T>
    {
        List<T> ReadCsvFileToEmployeeModel(string path);
        void WriteNewCsvFile(string path, List<T> employeeModels);
    }
}
