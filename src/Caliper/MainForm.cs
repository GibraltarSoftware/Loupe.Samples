using System;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using Gibraltar.Log;

namespace CaliperTest
{
    /// <summary>
    /// Simple examples of a couple uses of the Caliper class
    /// </summary>
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// A Caliper can be used to measure the execution time of a block of code within a using block
        /// </summary>
        private void btnDoWork_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Do Work");
            var rand = new Random();
            var ms = rand.Next(10, 1000);
            using (new Caliper("Tests.DoWork"))
            {
                Thread.Sleep(ms);
            }
        }

        /// <summary>
        /// The Start and Stop messages on Caliper object can be called to
        /// measure execution time for non-contiguous blocks of code for
        /// which a using block is not viable.
        /// </summary>
        readonly Caliper _timer = new Caliper("Tests.StartStop");
        private void btnStartWorking_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Start Working...");
            _timer.Start();
        }

        private void btnStopWorking_Click(object sender, EventArgs e)
        {
            Trace.WriteLine("Stop Working");
            _timer.Stop();
        }

        /// <summary>
        /// The Start/Stop methods on a Caliper are also handy for tight loops
        /// in which we want to be extra performant and would like to avoid the
        /// extra level of indentation a using block would impose.
        /// </summary>
        private void btnWorkRepeatedly_Click(object sender, EventArgs e)
        {
            var timer = new Caliper("Tests.Loop");
            var rand = new Random();

            Trace.WriteLine("Begin Loop...");
            Trace.Indent();
            for (var i = 1; i <= nudWorkRepeatedly.Value; i++)
            {
                Trace.WriteLine("Iteration " + i);
                var ms = rand.Next(0, 100);
                timer.Start();
                Thread.Sleep(ms);
                timer.Stop();
            }
            Trace.Unindent();
            Trace.WriteLine("End Loop");
        }
    }
}
