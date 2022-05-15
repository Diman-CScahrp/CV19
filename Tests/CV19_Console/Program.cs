using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CV19_Console
{
    internal class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_time_series/time_series_covid19_confirmed_global.csv";
        static void Main(string[] args)
        {
            var russia = GetData()
                .First(v => v.Country.Equals("Russia", StringComparison.OrdinalIgnoreCase));

            Console.WriteLine(String.Join("\r\n", GetDates().Zip(russia.Counts, (date, count) => $"{date:dd.MM} - {count}")));
        }
        private static async Task<Stream> GetDataStream()
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }
        private static IEnumerable<string> GetDataLines()
        {
            using var data_stream = GetDataStream().Result;
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
        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
            .ToArray();
        
        private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
        {
            var lines = GetDataLines()
                .Skip(1)
                .Select(line => line.Split(','));

            foreach(var row in lines)
            {
                var province = row[0].Trim();
                var country = row[1].Trim(' ', '"');
                var counts = row.Skip(4).Select(s => int.Parse(s)).ToArray();

                yield return (country, province, counts);
            }
        }
    }
}
