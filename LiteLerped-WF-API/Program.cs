using Lerp2Web;
using Lerp2Web.Properties;
using LiteLerped_WF_API.Classes;
using System;
using System.Windows.Forms;

namespace LiteLerped_WF_API
{
    public static class Program
    {
        public static LanguageManager lang;
        public static Lerp2Web.Lerp2Web web;

        private static frmCredentials _cred;

        public static frmCredentials cred
        {
            get
            {
                if (_cred == null) _cred = new frmCredentials();
                return _cred; //(frmCredentials) Application.OpenForms["frmCredentials"];
            }
        }

        public static void Run<T>(string prefix, T main, Action firstConfigExecution) where T : Form
        {
            frmCredentials.SetLoginCallback(main);

            //Do config stuff...
            API.LoadConfigCallback(() =>
            {
                Console.WriteLine("Config callback!!");
                if (!string.IsNullOrEmpty(ManagedSettings.LoginUsername))
                    cred.a_txtUsername.Text = ManagedSettings.LoginUsername;

                if (!string.IsNullOrEmpty(ManagedSettings.LoginPassword))
                    cred.a_txtPassword.Text = ManagedSettings.LoginPassword;

                cred.doingAutologin = !string.IsNullOrEmpty(ManagedSettings.LoginUsername) && !string.IsNullOrEmpty(ManagedSettings.LoginPassword);
                cred.a_chkRemember.Checked = API.RememberingAuth;
            });
            API.LoadConfig(firstConfigExecution);

            //And later, do things that will require it...
            web = new Lerp2Web.Lerp2Web(prefix);
            web.sessionSha = web.StartSession();

            Application.Run(main);
        }
    }
}