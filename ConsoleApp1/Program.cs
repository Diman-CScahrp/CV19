namespace OP
{
    class Program
    {
        public static void Main()
        {
            List<int> ints = new List<int>();
            Thread[] threads = new Thread[10];
            for (int i = 0; i < threads.Length; i++)
            {
                threads[i] = new Thread(() =>
                {
                    for (int j = 0; j < 10; j++)
                    {
                        Thread.Sleep(10);
                        ints.Add(Thread.CurrentThread.ManagedThreadId);
                    }
                });
            }

            foreach (var item in threads)
            {
                item.Start();
            }

            Console.ReadLine();
            Console.WriteLine(String.Join(",", ints));
            Console.ReadLine();
        }
    }
}