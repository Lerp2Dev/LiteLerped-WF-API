using Lerp2Web.Properties;
using LiteLerped_WF_API.Controls;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace LiteLerped_WF_API.Classes
{
    #region "Languages"

    //Esto va para la API (LiteLerped-WF-API)

    public enum LerpedLanguage { ES, EN }

    public class LanguageManager
    {
        public static CultureInfo GlobalUICulture
        {
            get { return Thread.CurrentThread.CurrentUICulture; }
            set
            {
                if (GlobalUICulture.Equals(value) == false)
                {
                    foreach (LerpedForm form in Application.OpenForms.OfType<LerpedForm>())
                        form.Culture = value;

                    Thread.CurrentThread.CurrentUICulture = value;
                }
            }
        }

        public LanguageManager(LerpedLanguage def)
            : this(def.ToString().ToLower())
        { }

        internal LanguageManager(string def) //string baseName, Action<LerpedLanguage> act //MenuStrip menu,
        {
            GlobalUICulture = CultureInfo.GetCultureInfo(def);
        }

        internal static string LanguageName(LerpedLanguage lang)
        {
            switch (lang)
            {
                case LerpedLanguage.EN:
                    return "English";

                case LerpedLanguage.ES:
                    return "Spanish";
            }
            return "";
        }

        internal static void LangItem_Click(object sender, EventArgs e)
        {
            string lang = (string) ((ToolStripDropDownItem) sender).Tag;
            GlobalUICulture = new CultureInfo(lang);
            ManagedSettings.CurrentLanguage = lang;
            ManagedSettings.Save();
        }
    }

    #endregion "Languages"
}