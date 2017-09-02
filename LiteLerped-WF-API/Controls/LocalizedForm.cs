using LiteLerped_WF_API.Classes;
using LiteLerped_WF_API.Controls.Extensions;
using LiteLerped_WF_API.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace LiteLerped_WF_API.Controls
{
    public class LocalizedForm : Form
    {
        /// <summary>
        /// Occurs when current UI culture is changed
        /// </summary>
        [Browsable(true)]
        [Description("Occurs when current UI culture is changed")]
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        [Category("Property Changed")]
        public event EventHandler CultureChanged;

        protected CultureInfo culture;
        //protected ComponentResourceManager resManager;

        /// <summary>
        /// Current culture of this form
        /// </summary>
        [Browsable(false)]
        [Description("Current culture of this form")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public CultureInfo Culture
        {
            get { return this.culture; }
            set
            {
                if (this.culture != value)
                {
                    //this.ApplyResources(this, value);

                    ResourceSet resourceSet = new ComponentResourceManager(GetType()).GetResourceSet(value, true, true);
                    IEnumerable<DictionaryEntry> entries = resourceSet
                        .Cast<DictionaryEntry>()
                        .Where(x => x.Key.ToString().Contains(".Text"))
                        .Select(x => { x.Key = x.Key.ToString().Replace(">", "").Split('.')[0]; return x; });

                    foreach (DictionaryEntry entry in entries)
                    {
                        if (!entry.Value.GetType().Equals(typeof(string))) return;

                        string Key = entry.Key.ToString(),
                               Value = (string) entry.Value;

                        try
                        {
                            Control c = Controls.Find(Key, true).SingleOrDefault();
                            c.Text = Value;
                        }
                        catch
                        {
                            Console.WriteLine("Control {0} is null in form {1}!", Key, GetType().Name);
                        }
                    }

                    this.culture = value;
                    this.OnCultureChanged();
                }
            }
        }

        public LocalizedForm()
        {
            //this.culture = CultureInfo.CurrentUICulture;

            //Set Localizable to true

            Shown += (sender, e) =>
            {
                MenuStrip menu = null;
                if (HasValidMenu(GetType().Name, out menu))
                    LanguageManager.AttachMenu(menu);
            };
        }

        protected void OnCultureChanged()
        {
            this.CultureChanged?.Invoke(this, EventArgs.Empty);
        }

        protected bool HasValidMenu(string name, out MenuStrip menu)
        {
            Console.WriteLine("Attaching a menu to " + name);

            try
            {
                menu = Controls.OfType<MenuStrip>().SingleOrDefault(x => x.Dock == DockStyle.Top);
                return menu != null;
            }
            catch
            {
                menu = null;
                return false;
            }
        }
    }
}