﻿using CV19.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace CV19.Services
{
    internal class DataService
    {
        private const string _DataSourceAddress = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";

        private static async Task<Stream> GetDataStream()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(_DataSourceAddress, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }
        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = (SynchronizationContext.Current is null ? GetDataStream() : Task.Run(GetDataStream)).Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line = data_reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) continue;
                line = line.Replace("Bonaire,", "Bonaire -");
                line = line.Replace("Saint Helena,", "Saint Helena -");
                yield return line.Replace("Korea,", "Korea -");
            }
        }
        private static IEnumerable<(string Province, string Country, (double Lat, double Lon) Place, int[] Counts)> GetCountriesData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach (var row in lines)
            {
                var province = row[0].Trim();
                var country = row[1].Trim(' ', '"');
                var latitude = double.Parse(row[2].Replace('.', ',') == "" ? "0" : row[2].Replace('.', ','));
                var longitude = double.Parse(row[3].Replace('.', ',') == "" ? "0" : row[3].Replace('.', ','));
                var counts = row.Skip(4).Select(s => int.Parse(s)).ToArray();

                yield return (province,country, (latitude, longitude), counts);
            }
        }
        public IEnumerable<CountryInfo> GetData()
        {
            var dates = GetDates();

            var data = GetCountriesData().GroupBy(d => d.Country);

            foreach(var country_info in data)
            {
                var country = new CountryInfo
                {
                    Name = country_info.Key,
                    ProvincesCounts = country_info.Select(c => new PlaceInfo
                    {
                        Name = c.Province,
                        Location = new Point(c.Place.Lat, c.Place.Lon),
                        Counts = dates.Zip(c.Counts, (date, count) => new ConfirmedCount { Date = date, Count = count})
                    }) 
                };

                yield return country;
            }

        }
    }
}
