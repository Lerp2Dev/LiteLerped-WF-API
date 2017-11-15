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
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
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

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private bool IsActive(IntPtr handle)
        {
            IntPtr activeHandle = GetForegroundWindow();
            return (activeHandle == handle);
        }

        public bool Actived
        {
            get
            {
                return IsActive(Handle);
            }
        }

        public delegate void CustomEventHandler(object sender, ActiveState state);

        public event CustomEventHandler ActiveChanged;

        public LerpedForm()
        {
            //Console.WriteLine(GetType().GetFields(BindingFlags.Public | BindingFlags.Static)[0].GetValue(null) == null); //.ForEach((x) => { Console.WriteLine("[0]: {1}", GetType().Name, x.Name); });
            //Console.WriteLine("[{0}] CC: " + ((Form) GetType().GetFields(BindingFlags.Public | BindingFlags.Static).GetValue(null)).Controls.Count, GetType().Name);

            //Set Localizable to true (no se puede es private)

            Shown += (sender, e) =>
            {
                if (!DesignMode)
                    LerpedManager.ManageMenu(this);
            };

            Activated += (sender, e) =>
            {
                if (Visible && ActiveChanged != null)
                    ActiveChanged(this, new ActiveState(true));
            };

            Deactivate += (sender, e) =>
            {
                if (Visible && ActiveChanged != null)
                    ActiveChanged(this, new ActiveState(false));
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

        public void LoadCallback()
        {
            //Console.WriteLine(GetType().GetFields(BindingFlags.Public | BindingFlags.Static)[0].GetValue(null) == null);
            //Console.WriteLine("[{0}] CC: " + ((Form) GetType().GetField("instance", BindingFlags.Public | BindingFlags.Static).GetValue(null)).Controls.Count, GetType().Name);
            foreach (Control c in ((Form) GetType().GetFields(BindingFlags.Public | BindingFlags.Static)[0].GetValue(null)).Controls)
                Console.WriteLine(c.Name);
        }
    }

    public class ActiveState : EventArgs
    {
        public bool IsActive;

        private ActiveState()
        {
        }

        public ActiveState(bool isAct)
        {
            IsActive = isAct;
        }
    }
}