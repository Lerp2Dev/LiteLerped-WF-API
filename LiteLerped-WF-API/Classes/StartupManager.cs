using Microsoft.Win32;
using System;
using System.Security.Principal;

namespace LiteLerped_WF_API.Classes
{
    public static class StartupManager
    {
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

        public static object ExistsStartupKey(string appName, bool allUsers = false)
        {
            RegistryKey k = allUsers ? Registry.LocalMachine : Registry.CurrentUser;
            using (RegistryKey key = k.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                return key.GetValue(appName, "\"" + System.Reflection.Assembly.GetExecutingAssembly().Location + "\"");
            }
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
