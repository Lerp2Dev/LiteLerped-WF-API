using Lerp2Web;
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

        public static void Run<T>(string prefix, T main) where T : Form
        {
            frmCredentials.SetLoginCallback(main);

            //Do config stuff...
            API.LoadConfigCallback(() =>
            {
                Console.WriteLine("Config callback!!");
                if (!API.config.AppSettings.Settings.IsEmpty(API.usernameConfig))
                    cred.a_txtUsername.Text = API.config.AppSettings.Settings[API.usernameConfig].Value;

                if (!API.config.AppSettings.Settings.IsEmpty(API.passwordConfig))
                    cred.a_txtPassword.Text = API.config.AppSettings.Settings[API.passwordConfig].Value;

                cred.doingAutologin = !API.config.AppSettings.Settings.IsEmpty(API.usernameConfig) && !API.config.AppSettings.Settings.IsEmpty(API.passwordConfig);
                cred.a_chkRemember.Checked = API.RememberingAuth;
            });
            API.LoadConfig();

            //And later, do things that will require it...
            web = new Lerp2Web.Lerp2Web(prefix);
            web.sessionSha = web.StartSession();
        }
    }
}