using Lerp2Web;
using LiteLerped_WF_API.Controls;
using System;
using System.Drawing;

using System.Linq;
using System.Windows.Forms;

namespace LiteLerped_WF_API.Classes
{
    public class LerpedManager
    {
        public static void ManageMenu(LerpedForm frm)
        {
            MenuStrip menu = null;
            if (HasValidMenu(frm, out menu))
                AttachMenu(menu);
            else
                CreateMenu(frm);
        }

        private static void CreateMenu(LerpedForm frm)
        {
            MenuStrip menu = new MenuStrip() { Dock = DockStyle.Top };
            int height = menu.Height; //Get the height of a default MenuStrip

            //Increase form height
            frm.Size += new Size(0, height);

            //Increase top value of the form Controls
            foreach (Control control in frm.Controls)
                control.Location = new Point(control.Location.X, control.Location.Y + height);

            //Attach & add it to the form
            AttachMenu(menu);
            frm.Controls.Add(menu);
        }

        private static void AttachMenu(MenuStrip menu)
        {
            //Attach language menu
            string s_Name = "m_LangMan";
            if (!menu.Items.Cast<ToolStripMenuItem>().Any(x => x.Name == s_Name))
            {
                ToolStripDropDownItem languageItem = new ToolStripMenuItem("Language"); // <--- Esto lo tengo que cargar de algún sitio...
                languageItem.Name = s_Name;

                foreach (LerpedLanguage lang in Enum.GetValues(typeof(LerpedLanguage)))
                {
                    ToolStripDropDownItem langItem = new ToolStripMenuItem(LanguageManager.LanguageName(lang));
                    langItem.Tag = lang.ToString().ToLower();
                    langItem.Click += LanguageManager.LangItem_Click;
                    languageItem.DropDownItems.Add(langItem);
                }

                menu.Items.Add(languageItem);
            }

            //Atach options Menu

            string cap = "Options";
            ToolStripDropDownItem _optItem = menu.Items.Cast<ToolStripMenuItem>().SingleOrDefault(x => x.Text == cap);

            if (_optItem != null)
                _optItem.DropDownItems.Add(new ToolStripSeparator());

            ToolStripDropDownItem optionItem = _optItem == null ? new ToolStripMenuItem(cap) : _optItem;

            ToolStripDropDownItem logoutOption = new ToolStripMenuItem("Logout");
            logoutOption.Click += (sender, e) =>
            {
                Program.cred.Logout();
            };

            optionItem.DropDownItems.Add(logoutOption);

            menu.Items.Add(optionItem);
        }

        private static bool HasValidMenu(LerpedForm frm, out MenuStrip menu)
        {
            Console.WriteLine("Attaching a menu to " + frm.Name);

            try
            {
                menu = frm.Controls.OfType<MenuStrip>().SingleOrDefault(x => x.Dock == DockStyle.Top);
                return menu != null;
            }
            catch
            {
                menu = null;
                return false;
            }
        }
    }

    public class LerpedConfigKeys : ConfigKeys
    {
    }
}