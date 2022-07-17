namespace OP
{
    class Program
    {
        public static void Main()
        {
            ManualResetEvent resetEvent = new ManualResetEvent(false);
            List<string> tasks = new List<string>();

            Thread thread_addTask = new Thread(() =>
            {
                //
                tasks.Add("new task");
                resetEvent.Set();
                Console.WriteLine("Sleeep 1");
                Thread.Sleep(3000);
                tasks.Add("one more task");
                resetEvent.Set();
                Console.WriteLine("Sleep 2");
                Thread.Sleep(3000);
                tasks.Add("old task");
            });
            thread_addTask.Start();

            Thread thread_runTask = new Thread(() =>
            {
                while (true)
                {
                    resetEvent.WaitOne();
                    Console.WriteLine(tasks.Last());
                    resetEvent.Reset();
                }
            });
            thread_runTask.Start();
        }
    }
}