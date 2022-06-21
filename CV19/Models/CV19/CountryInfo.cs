using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CV19.Models.CV19
{
    internal class CountryInfo
    {
        public string Name { get; set; }
        private int[] _Counts;
        public Point Location
        {
            get
            {
                if (Provinces.Count <= 0)
                    return default;

                return Provinces[0].Location;
            }
            set
            {
                if (Provinces.Count > 0)
                    Provinces[0].Location = value;
            }
        }
        public ConfirmedCount[] Points
        {
            get
            {
                var data = Counts.Zip(Dates, (count, date) => new ConfirmedCount { Count = count, Date = date}).ToArray();
                return data;
            }
        }
        public int[] Counts
        {
            get
            {
                if (_Counts != null) return _Counts;
                if (Provinces.Count <= 0) return null;
                if (Provinces.Count == 1) return Provinces[0].Counts;

                _Counts = new int[Provinces[0].Counts.Length];

                foreach (var province in Provinces)
                {
                    for (int i = 0; i < province.Counts.Length; i++)
                    {
                        _Counts[i] += province.Counts[i];
                    }
                }

                return _Counts;
            }
        }
        public DateTime[] Dates
        {
            get
            {
                if (Provinces.Count <= 0)
                    return null;

                return Provinces[0].Dates;
            }
        }
        public IList<ProvinceInfo> Provinces { get; set; }
        public override string ToString()
        {
            return $"{Name} ({Provinces[0].Location.X},{Provinces[0].Location.X})";
        }
    }
}
