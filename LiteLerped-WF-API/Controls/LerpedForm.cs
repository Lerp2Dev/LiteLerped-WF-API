using Lerp2Web;
using Lerp2Web.Properties;
using LiteLerped_WF_API.Classes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows.Forms;

namespace LiteLerped_WF_API.Controls
{
    /// <summary>
    /// Class LerpedForm.
    /// This forms will server the Launguage & Auth system!
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public class LerpedForm : Form
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

        public LerpedForm()
        {
            //this.culture = CultureInfo.CurrentUICulture;

            //Set Localizable to true (no se puede es private)

            Shown += (sender, e) =>
            {
                if (!DesignMode)
                    LerpedManager.ManageMenu(this);
            };
            FormClosing += (sender, e) =>
            {
                ConfigCore.Exiting(() =>
                {
                    ManagedSettings.Save();
                });
            };
        }

        protected void OnCultureChanged()
        {
            this.CultureChanged?.Invoke(this, EventArgs.Empty);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(LerpedForm));
            this.SuspendLayout();
            //
            // LerpedForm
            //
            resources.ApplyResources(this, "$this");
            this.Name = "LerpedForm";
            this.ResumeLayout(false);
        }

        public bool ChildReallyVisible(Control child)
        {
            Point pos = this.PointToClient(child.PointToScreen(Point.Empty));
            if (this.GetChildAtPoint(pos) == child) return true;
            if (this.GetChildAtPoint(new Point(pos.X + child.Width - 1, pos.Y)) == child) return true;
            // two more to test
            //...
            return false;
        }
    }
}