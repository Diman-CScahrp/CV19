using CV19.Services.Interfaces;
using System;
using System.Threading;

namespace CV19.Services
{
    internal class AsyncDataService : IAsyncDataService
    {
        private const int _SleepTime = 7000;
        public string GetResult(DateTime Value)
        {
            Thread.Sleep(_SleepTime);
            return $"Result Value {Value}";
        }
    }
}
