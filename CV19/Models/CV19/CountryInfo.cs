using System.Collections.Generic;

namespace CV19.Models.CV19
{
    internal class CountryInfo
    {
        public string Name { get; set; }
        public IList<ProvinceInfo> Provinces { get; set; }
        public override string ToString()
        {
            return $"{Name} ({Provinces[0].Location.X},{Provinces[0].Location.X})";
        }
    }
}
