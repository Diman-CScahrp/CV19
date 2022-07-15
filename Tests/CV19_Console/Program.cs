using System;
using System.Threading;

namespace CV19_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            var thread = new Thread(ThreadMethod);
            thread.Name = "Second thread";
            thread.IsBackground = true;
            thread.Priority = ThreadPriority.AboveNormal;
            thread.Start(42);

            var count = 5;
            var msg = "Hello World!";
            var timeout = 150;

            new Thread(() => PrintMethod(msg, count, timeout)).Start();

            CheckThread();

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i);
            }

            Console.ReadLine();
        }
        private static void PrintMethod(string message, int count, int timeout)
        {
            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(message);
                Thread.Sleep(timeout);
            }
        }
        private static void ThreadMethod(object parameter)
        {
            var value = (int)parameter;
            Console.WriteLine(value);

            CheckThread();

            while (true)
            {
                Thread.Sleep(2000);

                Console.Title = DateTime.Now.ToString();
            }
        }
        private static void CheckThread()
        {
            var thread = Thread.CurrentThread;
            Console.WriteLine("id:{0} - {1}", thread.ManagedThreadId, thread.Name);
        }
    }
}
