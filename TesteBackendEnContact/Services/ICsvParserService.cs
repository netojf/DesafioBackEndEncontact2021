using System.Collections.Generic;

namespace TesteBackendEnContact.Services
{
    public interface ICsvParserService<T>
    {
        List<T> ReadCsvFileToModel(string path);
        void WriteNewCsvFile(string path, List<T> models);
    }
}
