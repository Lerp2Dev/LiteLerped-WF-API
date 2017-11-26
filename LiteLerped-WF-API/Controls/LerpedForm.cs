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
        //private List<Tuple<Type, string, Control>> controls = new List<Tuple<Type, string, Control>>(); //De hecho si estas 2 cosas funcionan las puedo unir

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
                    ResourceSet resourceSet = new ComponentResourceManager(GetType()).GetResourceSet(value, true, true);
                    IEnumerable<DictionaryEntry> entries = resourceSet
                        .Cast<DictionaryEntry>()
                        .Where(x => x.Key.ToString().Contains(".Text")) //Buscar solamente .Text no se pq me cuela el $this del PCStats si solo es un .Name
                        .Select(x => { x.Key = x.Key.ToString().Replace(">", "").Split('.')[0]; return x; });

                    foreach (DictionaryEntry entry in entries)
                    {
                        if (!entry.Value.GetType().Equals(typeof(string))) return;

                        string Key = entry.Key.ToString(),
                               Value = (string) entry.Value;

                        ControlHandler.SetText(Value, Key); //Aqui lo unico que puedo hacer es buscar directamente la traducción
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
            //Set Localizable to true (no se puede es private)

            Shown += (sender, e) =>
            {
                if (!DesignMode)
                    LerpedManager.ManageMenu(this);

                foreach (Control cc in Controls)
                    ControlHandler.Add(cc);
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

public static class ControlHandler
{
    private static ControlData dataHandler = new ControlData();

    public static void Add(Control cont)
    {
        string t = cont.GetType().Name;
        switch (t)
        {
            case "MenuStrip":
            case "ToolStrip":
                dataHandler.AddHandledData(cont);

                var items = ((ToolStrip) cont).Items.Cast<ToolStripItem>();

                dataHandler.AddUnhandledData(items, ControlDataType.Tool);
                foreach (var it in items)
                    dataHandler.AddUnhandledData(it.GetItems(), ControlDataType.Menu);
                break;

            default:
                dataHandler.AddHandledData(cont);
                break;
        }
    }

    /// <summary>
    /// Recursively get SubMenu Items. Includes Separators.
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public static IEnumerable<ToolStripItem> GetItems(this ToolStripItem item)
    {
        if (item is ToolStripMenuItem)
        {
            foreach (ToolStripItem tsi in (item as ToolStripMenuItem).DropDownItems)
            {
                if (tsi is ToolStripMenuItem)
                {
                    if ((tsi as ToolStripMenuItem).HasDropDownItems)
                    {
                        foreach (ToolStripItem subItem in GetItems((tsi as ToolStripMenuItem)))
                            yield return subItem;
                    }
                    yield return (tsi as ToolStripMenuItem);
                }
                else if (tsi is ToolStripSeparator)
                {
                    yield return (tsi as ToolStripSeparator);
                }
            }
        }
        else if (item is ToolStripSeparator)
        {
            yield return (item as ToolStripSeparator);
        }
    }

    public static void SetText(string text, string name) //string altName = ""
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            try
            {
                var data = ControlData.data[name];
                if (data.Item2)
                    ((ControlHandledData) data.Item1).Text = text;
                else
                    ((ControlUnhandledData) data.Item1).Text = text;
            }
            catch
            {
                if (name == "$this")
                    Console.WriteLine("We can't handle $this keyword.");
                else
                    Console.WriteLine("Unsupported lang key providen: '{0}'", name);
            }
        }
        else
            Console.WriteLine("Tried to change a control without a key but with text: '{0}'", text);
    }
}

public class ControlData
{
    internal static Dictionary<string, Tuple<ControlData, bool>> data = new Dictionary<string, Tuple<ControlData, bool>>();

    public void AddUnhandledData(IEnumerable<object> childs, ControlDataType type)
    {
        foreach (object child in childs)
        {
            ControlUnhandledData uhn = new ControlUnhandledData(child, type);
            Console.WriteLine("Adding child '{0}'", uhn.Name);

            try
            {
                if (!data.ContainsKey(uhn.Name))
                    data.Add(string.IsNullOrWhiteSpace(uhn.Name) ? uhn.AltName : uhn.Name, new Tuple<ControlData, bool>(uhn, false));
                else
                    Console.WriteLine("Tried to add repeated toolstrip key with name: '{0}'", uhn.Name);
            }
            catch
            {
                Console.WriteLine("[FAST] Item with the same key already exists!"); //Revisar esta mierda
            }
        }
    }

    public void AddHandledData(Control control)
    {
        data.Add(control.Name, new Tuple<ControlData, bool>(new ControlHandledData(control), true));
    }

    public ControlData()
    {
    }
}

public class ControlHandledData : ControlData
{
    public Control control;

    public string Text
    {
        set
        {
            control.Text = value;
        }
    }

    public ControlHandledData(Control cont)
    {
        control = cont;
    }
}

public enum ControlDataType
{
    Menu, Tool
}

public class ControlUnhandledData : ControlData
{
    public ToolStripMenuItem menuItem;
    public ToolStripItem toolItem;

    private ControlDataType type;

    public string Name
    {
        get
        {
            if (type == ControlDataType.Menu)
                return menuItem.Name;
            else
                return toolItem.Name;
        }
    }

    public string AltName
    {
        get
        {
            try
            {
                if (type == ControlDataType.Tool)
                    return string.Format("{0}_{1}", toolItem.GetCurrentParent().Name, toolItem.GetCurrentParent().Items.Cast<ToolStripItem>().ToList().IndexOf(toolItem));
                else
                    return string.Format("{0}_{1}", toolItem.GetCurrentParent().Name, toolItem.GetCurrentParent().Items.Cast<ToolStripMenuItem>().ToList().IndexOf(menuItem));
            }
            catch
            {
                return "exception";
            }
        }
    }

    public string Text
    {
        set
        {
            if (type == ControlDataType.Menu)
                menuItem.Text = value;
            else
                toolItem.Text = value;
        }
    }

    public ControlUnhandledData(object obj, ControlDataType ty)
    {
        type = ty;
        switch (ty)
        {
            case ControlDataType.Menu:
                menuItem = (ToolStripMenuItem) obj;
                break;

            case ControlDataType.Tool:
                toolItem = (ToolStripItem) obj;
                break;
        }
    }
}