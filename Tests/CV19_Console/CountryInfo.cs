using System;
using System.Collections.Generic;
using System.Text;

namespace CV19.Models.CV19
{
    internal class CountryInfo
    {
        public string Name { get; set; }
        public IList<ProvinceInfo> Provinces { get; set; }
    }
}
