using CV19.Models.CV19;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19_Console
{
    internal class Program
    {
        private const string url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        static void Main(string[] args)
        {
            var g = GetData();
            foreach (var item in g)
            {
                Console.WriteLine(item.Name);
                Console.WriteLine("{");
                foreach (var item2 in item.Provinces)
                {
                    Console.WriteLine("\t" + item2.Name + " | " + item2.Counts.Last() + " | " + item2.Dates.Last());
                }
                Console.WriteLine("}");
            }
        }
        private static string DownloadData(string url)
        {
            HttpClient client = new HttpClient();
            var response = client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead).Result;
            return response.Content.ReadAsStringAsync().Result;
        }
        private static IEnumerable<string> GetDataLines() => DownloadData(url)
            .Split('\n');

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(d => DateTime.Parse(d, CultureInfo.InvariantCulture))
            .ToArray();

        private static IEnumerable<string[]> GetAllData() => GetDataLines()
            .Skip(1)
            .Select(s => s
                .Replace("Korea," , "Korea -")
                .Replace("Bonaire,", "Bonaire -")
                .Replace("Saint Helena,", "Saint Helena -")
                .Split(',')
            );

        private static IEnumerable<(string Counrty, ProvinceInfo Province)> GetCountriesData()
        {
            var dates = GetDates();
            var data = GetAllData();


            foreach (var row in data)
            {
                if (row.Length < 3)
                    yield break;

                var _country = row[1].Trim(' ', '"');
                var _province = string.IsNullOrWhiteSpace(row[0]) ? _country : row[0].Trim(' ', '"');
                //var _lat = double.Parse(row[2], CultureInfo.InvariantCulture);
                //var _long = double.Parse(row[3], CultureInfo.InvariantCulture);

                var province = new ProvinceInfo
                {
                    Name = _province,
                    Location = new Point(0, 0),
                    Counts = row.Skip(4).Select(int.Parse).ToArray(),
                    Dates = dates
                };

                yield return (_country, province);
            }
        }
        public static IEnumerable<CountryInfo> GetData()
        {
            var data = GetCountriesData();
            List<CountryInfo> countries = new List<CountryInfo>();

            foreach (var item in data)
            {
                if(countries.Count > 0 && item.Counrty == countries.Last().Name)
                {
                    countries.Last().Provinces.Add(item.Province);
                }
                else
                {
                    CountryInfo country = new CountryInfo();
                    country.Name = item.Counrty;
                    country.Provinces = new List<ProvinceInfo>();
                    country.Provinces.Add(item.Province);
                    countries.Add(country);
                }
            }
            return countries;
        }
    }
}
