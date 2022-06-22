using System;
using System.Collections.Generic;
using System.Text;

namespace CV19
{
    class Program
    {
        [STAThread]
        public static void Main()
        {
            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
