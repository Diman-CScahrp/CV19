namespace OP
{
    class Program
    {
        public static void Main()
        {
            Timer timer = new Timer();
            timer.Tick = 2000;
            timer.TimerCallBack += () =>
            {
                Console.Title = DateTime.Now.ToString();
            };
            timer.Start();

            string msg = Console.ReadLine();
            if (msg == "stop")
                timer.Stop();
        }
    }
    class Timer
    {
        public event Action TimerCallBack;
        private bool isEnabled = true;
        private Thread thread;
        public int Tick { get; set; }

        public void Start()
        {
            isEnabled = true;
            thread = new Thread(() =>
            {
                while (isEnabled)
                {
                    Thread.Sleep(Tick);
                    TimerCallBack.Invoke();
                }
            });
            thread.Start();
        }
        public void Stop()
        {
            isEnabled = false;
        }
    }
}