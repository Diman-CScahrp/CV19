using System;
using System.Windows;

namespace CV19.Models.CV19
{
    internal class ProvinceInfo
    {
        public string Name { get; set; }
        public Point Location { get; set; }
        public int[] Counts { get; set; }
        public DateTime[] Dates { get; set; }
    }
}
