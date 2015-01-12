using System;
using System.Windows.Forms;
using Gibraltar.Agent;
using Gibraltar.Log;

namespace CaliperTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Log.StartSession();
            using (new Caliper("TestRun"))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            Log.EndSession();
        }
    }
}
