using System;
using System.Collections.Generic;
using System.Linq;

namespace CV19.Models.CV19
{
    internal class CountryInfo
    {
        public string Name { get; set; }
        private int[] _Counts;
        public ConfirmedCount[] Points
        {
            get
            {
                //List<ConfirmedCount> data = new List<ConfirmedCount>();
                //for (int i = 0; i < Dates.Length; i++)
                //{
                //    data.Add(new ConfirmedCount()
                //    {
                //        Count = Counts[i],
                //        Date = Dates[i]
                //    });
                //}
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
