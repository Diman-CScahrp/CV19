using System;
using System.Threading;
using System.Windows;

namespace CV19_WPFTest
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new Thread(ComputeValue).Start();
        }
        private void ComputeValue()
        {
            var value = LongProcess(DateTime.Now);
            if (ResultBlock.Dispatcher.CheckAccess())
                ResultBlock.Text = value;
            else
                ResultBlock.Dispatcher.Invoke(() => ResultBlock.Text = value);
        }
        private static string LongProcess(DateTime Time) 
        {
            Thread.Sleep(2000);
            return $"Value: {Time}";
        } 
    }
}
