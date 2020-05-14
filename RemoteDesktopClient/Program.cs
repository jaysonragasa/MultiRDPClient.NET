using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Windows.Forms;

namespace MultiRemoteDesktopClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            Application.EnableVisualStyles();

            AppCenter.Start("8e9dbe40-0b00-46ee-8a37-738dcb1a9e4d",
                   typeof(Analytics), typeof(Crashes));

#if DEBUG
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Starting in DEBUG");
#else
            Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Starting in RELEASE");
#endif

            SingleInstanceController controler = new SingleInstanceController();
            controler.Run(Environment.GetCommandLineArgs());
        }
    }

    public class SingleInstanceController : WindowsFormsApplicationBase
    {
        public SingleInstanceController()
        {
            IsSingleInstance = true;

            this.StartupNextInstance += new StartupNextInstanceEventHandler(SingleInstanceController_StartupNextInstance);
        }

        void SingleInstanceController_StartupNextInstance(object sender, StartupNextInstanceEventArgs e)
        {
            RemoteDesktopClient rdc = (RemoteDesktopClient)this.MainForm;

            string[] args = new string[e.CommandLine.Count];
            e.CommandLine.CopyTo(args, 0);

            rdc.DoArguments(args);
        }

        protected override void OnCreateMainForm()
        {
            SplashScreenWindow ssw = new SplashScreenWindow();
            ssw.ShowDialog();

            this.MainForm = new RemoteDesktopClient();
        }
    }
}
