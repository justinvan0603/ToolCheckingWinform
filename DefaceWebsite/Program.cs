using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DefaceWebsite
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
    (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //log.Info("Test Log");
            //log.Info("Khoi dong chuong trinh + " + DateTime.Now);
            Application.Run(new Home());
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledExceptionEvent);
            Environment.Exit(1);
            
        }
        static void UnhandledExceptionEvent(object sender, UnhandledExceptionEventArgs e)
        {
            log.Fatal("Unhandled exception in Program - " + e.ExceptionObject);
            Environment.Exit(1);
        }

    }
}
