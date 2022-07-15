using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CV19WinFormsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            new Thread(ComputeValue).Start();
        }
        private void ComputeValue()
        {
            var value = LongProcess(DateTime.Now);
            SetResultValue(value);
        }
        private void SetResultValue(string value)
        {
            if (ResultLabel.InvokeRequired)
                ResultLabel.Invoke(new Action<string>(SetResultValue), value);
            else
                ResultLabel.Text = value;
        }
        private static string LongProcess(DateTime Time)
        {
            Thread.Sleep(2000);
            return $"Value: {Time}";
        }
    }
}
