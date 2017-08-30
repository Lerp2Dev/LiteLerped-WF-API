using Lerp2Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace LiteLerped_WF_API.Controls
{
    [Designer(typeof(NotifyLabelDesigner))]
    public class NotifyLabel : UserControl, ICloneable, IDisposable
    {
        public static Dictionary<int, NotifyLabel> lbl = new Dictionary<int, NotifyLabel>();
        public static int CurOrder = 0;
        public static bool PlaySounds = true;
        internal NotifyLabel _cloned;

        public static NotifyLabel FindLabel(int key)
        {
            if (lbl.ContainsKey(key))
                return lbl[key];
            return null;
        }

        private int _order;

        public int MyOrder
        {
            get
            {
                return _order;
            }
            set
            {
                if (_order != value)
                {
                    if (lbl.ContainsKey(value))
                        lbl.Swap(_order, value);
                    else
                        lbl.Add(value, this);
                }
                _order = value;
            }
        }

        public Label lblNotify;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Bindable(true)]
        public override string Text
        {
            get { return lblNotify.Text; }
            set { lblNotify.Text = value; }
        }

        public ContentAlignment TextAlign
        {
            get { return lblNotify.TextAlign; }
            set { lblNotify.TextAlign = value; }
        }

        public NotifyLabel()
            : base()
        {
            lblNotify = new Label()
            {
                Name = "lblNotify",
                BackColor = SystemColors.ControlLightLight,
                Dock = DockStyle.Fill
            };
            this.Controls.Add(lblNotify);
        }

        public void StoreDetails()
        {
            _cloned = (NotifyLabel) Clone();
        }

        public void RestoreDetails()
        {
            PropertyInfo[] properties = _cloned.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                object obj = property.GetValue(_cloned, null);
                if (!obj.Equals(GetType().GetProperty(property.Name)))
                    GetType().GetProperty(property.Name).SetValue(this, obj);
            }
        }

        public static void SendInfo(string str)
        {
            SystemSounds.Question.Play();
            SendMsg(str, Color.AliceBlue);
        }

        public static void SendWarning(string str)
        {
            SystemSounds.Exclamation.Play();
            SendMsg(str, Color.Yellow);
        }

        public static void SendError(string str)
        {
            SystemSounds.Asterisk.Play();
            SendMsg(str, Color.Red);
        }

        private static void SendMsg(string str, Color? col = null)
        {
            if (col == null) col = SystemColors.ControlDark;

            string backupText = FindLabel(CurOrder).Text;

            NotifyLabel lblNotify = FindLabel(CurOrder);

            lblNotify.StoreDetails();

            lblNotify.Text = str;
            lblNotify.ForeColor = (Color) col;
            //Change font size, weight??

            //And later... restore it! (After 5 secs)
            Task.Delay(5000).ContinueWith(t => lblNotify.RestoreDetails());
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }

    public class NotifyLabelDesigner : ControlDesigner
    {
        public override void Initialize(IComponent comp)
        {
            base.Initialize(comp);
            var uc = (NotifyLabel) comp;
            EnableDesignMode(uc.lblNotify, "label");
        }
    }
}