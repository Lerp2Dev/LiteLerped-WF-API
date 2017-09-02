using LiteLerped_WF_API.Controls;
using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
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
                    foreach (LocalizedForm form in Application.OpenForms.OfType<LocalizedForm>())
                        form.Culture = value;

                    Thread.CurrentThread.CurrentUICulture = value;
                }
            }
        }

        /*public CultureInfo culture;

        private ResourceManager _rMan;
        private string baseName;

        public ResourceManager ResMan
        {
            get
            {
                if (_rMan == null)
                    _rMan = new ResourceManager(baseName, Assembly.GetExecutingAssembly());
                return _rMan;
            }
        }*/

        public LanguageManager(LerpedLanguage def)
            : this(def.ToString().ToLower())
        { }

        internal LanguageManager(string def) //string baseName, Action<LerpedLanguage> act //MenuStrip menu,
        {
            GlobalUICulture = CultureInfo.GetCultureInfo(def);
        }

        public static void AttachMenu(MenuStrip menu)
        {
            string s_Name = "m_LangMan";
            if (menu.Items.Cast<ToolStripMenuItem>().Any(x => x.Name == s_Name))
                return;

            ToolStripDropDownItem item = new ToolStripMenuItem("Language"); // <--- Esto lo tengo que cargar de algún sitio...
            item.Name = s_Name;

            foreach (LerpedLanguage lang in Enum.GetValues(typeof(LerpedLanguage)))
            {
                foreach (LocalizedForm form in Application.OpenForms.OfType<LocalizedForm>())
                {
                    ToolStripDropDownItem langItem = new ToolStripMenuItem(LanguageName(lang));
                    langItem.Tag = lang.ToString().ToLower();
                    langItem.Click += (sender, e) =>
                    {
                        GlobalUICulture = new CultureInfo((string) ((ToolStripDropDownItem) sender).Tag);
                        //Thread.CurrentThread.CurrentUICulture = new CultureInfo((string) ((ToolStripDropDownItem) sender).Tag);
                        //form.Controls.Clear();
                        //form.InitializeComponent();
                    };
                    item.DropDownItems.Add(langItem);
                }
            }

            menu.Items.Add(item);
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

        public static void ChangeLanguage(string lang)
        {
            Console.WriteLine("Changing lang to " + lang);
            //Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(lang);
            //Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(lang);

            /*string[] resourceNames = Assembly.GetExecutingAssembly().GetManifestResourceNames();
            foreach (string resourceName in resourceNames)
            {
                Console.WriteLine("Res name: " + resourceName);
            }*/

            /*ResourceSet resourceSet = new ComponentResourceManager(typeof(frmCredentials)).GetResourceSet(CultureInfo.GetCultureInfo(lang), true, true);
            foreach (DictionaryEntry entry in resourceSet)
            {
                string resourceKey = entry.Key.ToString();
                object resource = entry.Value; //resourceSet.GetString(resourceKey);
                if (resource.GetType().Equals(typeof(string)))
                    Console.WriteLine("Key: {0}\nValue: {1}\n\n", resourceKey, (string) resource);
            }*/

            //foreach (Form frm in Application.OpenForms)
            //    LocalizeForm(frm);
        }
    }

    #endregion "Languages"
}