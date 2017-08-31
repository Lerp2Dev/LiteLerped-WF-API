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
                    foreach (var form in Application.OpenForms.OfType<LocalizedForm>())
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

        public LanguageManager(MenuStrip menu, LerpedLanguage def)
            : this(menu, def.ToString().ToLower())
        { }

        internal LanguageManager(MenuStrip menu, string def) //string baseName, Action<LerpedLanguage> act
        {
            //this.baseName = baseName;

            //Create a menu for the ActiveForm

            /*Switch = (lang) =>
            {
                //culture = CultureInfo.CreateSpecificCulture(lang.ToString().ToLower());
                act(lang);
            };*/

            GlobalUICulture = new CultureInfo(def);
            AttachMenu(menu);
        }

        public static void AttachMenu(MenuStrip menu)
        {
            ToolStripDropDownItem item = new ToolStripMenuItem("Language"); // <--- Esto lo tengo que cargar de algún sitio...

            foreach (LerpedLanguage lang in Enum.GetValues(typeof(LerpedLanguage)))
            {
                ToolStripDropDownItem langItem = new ToolStripMenuItem(LanguageName(lang));
                langItem.Tag = lang.ToString().ToLower();
                langItem.Click += (sender, e) =>
                {
                    GlobalUICulture = new CultureInfo((string) ((ToolStripDropDownItem) sender).Tag);
                };
                item.DropDownItems.Add(langItem);
            }

            menu.Items.Add(item);
        }

        internal static string LanguageName(LerpedLanguage lang)
        {
            switch (lang)
            {
                case LerpedLanguage.EN:
                    return "Inglés111";

                case LerpedLanguage.ES:
                    return "Español222";
            }
            return "";
        }

        /*public string GetString(string str)
        {
            return ResMan.GetString(str, culture);
        }*/
    }

    #endregion "Languages"
}