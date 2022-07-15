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

            CheckThread();

            for (int i = 0; i < 100; i++)
            {
                Thread.Sleep(100);
                Console.WriteLine(i);
            }

            Console.ReadLine();
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
