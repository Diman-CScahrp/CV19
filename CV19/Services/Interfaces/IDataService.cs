using CV19.Models.CV19;
using System.Collections.Generic;

namespace CV19.Services.Interfaces
{
    internal interface IDataService
    {
        IEnumerable<CountryInfo> GetData();
    }
}
