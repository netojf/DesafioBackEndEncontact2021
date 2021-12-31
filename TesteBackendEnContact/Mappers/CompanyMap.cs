using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CsvHelper.Configuration;
using TesteBackendEnContact.Core.Domain.Company;

namespace TesteBackendEnContact.Mappers
{
    public sealed class CompanyMap : ClassMap<Company>
    {
        public CompanyMap()
        {
            Map(m => m.Id).Name("Id");
            Map(m => m.ContactBookId).Name("ContactBookId");
            Map(m => m.Name).Name("Name");
        }
    }
}