using System;
using System.Collections.Generic;
using System.Threading;
using System.Collections.Concurrent;

namespace CV19_Console
{
    internal class Program
    {
        private static bool _ThreadUpdate = true;
        static void Main(string[] args)
        {
            Thread.CurrentThread.Name = "Main thread";

            var clock_thread = new Thread(ThreadMethod);
            clock_thread.Name = "Second thread";
            clock_thread.IsBackground = true;
            clock_thread.Priority = ThreadPriority.AboveNormal;
            clock_thread.Start(42);

            //var count = 5;
            //var msg = "Hello World!";
            //var timeout = 150;

            //new Thread(() => PrintMethod(msg, count, timeout)) { IsBackground = true }.Start();

            //CheckThread();

            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(100);
            //    Console.WriteLine(i);
            //}

            var values = new List<int>();
            var threads = new Thread[10];

            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        values.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                });
            }

            foreach (var thread in threads)
            {
                thread.Start();
            }

            Console.WriteLine(string.Join(",", values));
            Console.ReadLine();
        }
        private static void ThreadMethod(object parameter)
        {
            var value = (int)parameter;
            Console.WriteLine(value);

            CheckThread();

            while (_ThreadUpdate)
            {
                Thread.Sleep(100);

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
