using Lerp2Web;
using LiteLerped_WF_API.Classes;
using LiteLerped_WF_API.Classes.Extensions;
using LiteLerped_WF_API.Controls.Extensions;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
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
        protected ComponentResourceManager resManager;

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
                    this.ApplyResources(this, value);

                    this.culture = value;
                    this.OnCultureChanged();
                }
            }
        }

        public LocalizedForm()
        {
            this.resManager = new ComponentResourceManager(this.GetType());
            this.culture = CultureInfo.CurrentUICulture;

            MenuStrip menu = this.Controls.OfType<MenuStrip>().SingleOrDefault();
            //Console.WriteLine("Null:: " + (this.Controls.Cast<Control>().SingleOrDefault(x => x.Name == "menuStrip1") == null));
            //typeof(LocalizedForm).GetAllDerivedClasses().ForEach(x => Console.WriteLine(x.TryGetFormByType<LocalizedForm>() == null ? "Null::Null" : x.TryGetFormByType<LocalizedForm>().Name));
            if (menu != null)
                LanguageManager.AttachMenu(menu);
        }

        private void ApplyResources(Control parent, CultureInfo culture)
        {
            this.resManager.ApplyResources(parent, parent.Name, culture);

            foreach (Control ctl in parent.IterateAllChildren()) //parent.GetAll(typeof(Label)))
            {
                //this.ApplyResources(ctl, culture);
                this.resManager.ApplyResources(ctl, ctl.Name, culture);
            }
        }

        protected void OnCultureChanged()
        {
            this.CultureChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}