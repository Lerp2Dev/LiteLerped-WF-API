using LiteLerped_WF_API.Classes;
using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;

namespace LiteLerped_WF_API
{
    public static class Program
    {
        public static LanguageManager lang;
        public static Lerp2Web.Lerp2Web web;
        public static frmCredentials cred;

        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("es");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            cred = new frmCredentials();

            Application.Run(cred);
        }
    }
}