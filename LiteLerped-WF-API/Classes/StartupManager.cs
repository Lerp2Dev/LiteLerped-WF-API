using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace LiteLerped_WF_API.Classes
{
    public static class StartupManager
    {
        public static void RunAsAdmin()
        {
            ProcessStartInfo proc = new ProcessStartInfo();
            proc.UseShellExecute = true;
            proc.WorkingDirectory = Environment.CurrentDirectory;
            proc.FileName = Application.ExecutablePath;
            proc.Verb = "runas";
            try
            {
                Process.Start(proc);
            }
            catch
            {
                // The user refused the elevation.
                // Do nothing and return directly ...
                return;
            }
            Application.Exit();  // Quit itself
        }

        public static bool CheckIfRunningUnderVsHost()
        {
            return AppDomain.CurrentDomain.FriendlyName.Contains(".vshosts.exe");
        }

        public static void AddApplicationToStartup(string appName, bool allUsers = false)
        {
            RegistryKey k = allUsers ? Registry.LocalMachine : Registry.CurrentUser;
            using (RegistryKey key = k.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(appName, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static void RemoveApplicationFromStartup(string appName, bool allUsers = false)
        {
            RegistryKey k = allUsers ? Registry.LocalMachine : Registry.CurrentUser;
            using (RegistryKey key = k.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.DeleteValue(appName, false);
            }
        }

        public static string GetStartupKey(string appName, bool allUsers = false)
        {
            RegistryKey k = allUsers ? Registry.LocalMachine : Registry.CurrentUser;
            using (RegistryKey key = k.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                return (string) key.GetValue(appName, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
        }

        public static bool ExistsStartupKey(string appName, bool allUsers = false)
        {
            return !string.IsNullOrEmpty(GetStartupKey(appName, allUsers));
        }

        public static bool IsUserAdministrator()
        {
            //bool value to hold our return value
            bool isAdmin;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
            catch (UnauthorizedAccessException ex)
            {
                isAdmin = false;
            }
            catch (Exception ex)
            {
                isAdmin = false;
            }
            return isAdmin;
        }
    }
}