using CV19.Models.CV19;
using CV19.Services;
using System.Collections;
using System.Linq;
using System.Windows;

namespace CV19
{
    public partial class App : Application
    {
        public static bool IsDesignMode { get; private set; } = true;
        protected override void OnStartup(StartupEventArgs e)
        {
            IsDesignMode = false;
            base.OnStartup(e);
        }
    }
}
