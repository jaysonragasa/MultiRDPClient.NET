using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.VisualBasic.ApplicationServices;

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
