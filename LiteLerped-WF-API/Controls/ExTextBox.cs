using Lerp2Web;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace LiteLerped_WF_API.Controls
{
    [Designer(typeof(ExTextBoxDesigner))]
    public class ExTextBox : UserControl
    {
        public TextBox textBox;

        public ExTextBox()
        {
            textBox = new TextBox()
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(-1, -1),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right
            };
            Control container = new ContainerControl()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(-1)
            };
            container.Controls.Add(textBox);
            this.Controls.Add(container);

            textBox.KeyUp += (obj, sender) =>
            {
                //if (DateTime.UtcNow.IsValid(LastTimePressed))
                //{
                if (ErrorFunc != null)
                {
                    if (ErrorFunc())
                        BackColor = ErrorBorderColor;
                    else
                        BackColor = ValidBorderColor;
                }
                //}
                LastTimePressed = TimeHelpers.UnixTimestamp;
            };

            DefaultBorderColor = SystemColors.ControlDark;
            FocusedBorderColor = Color.FromArgb(129, 190, 247);
            ValidBorderColor = Color.Green;
            ErrorBorderColor = Color.Red;
            BackColor = DefaultBorderColor;
            Padding = new Padding(1);
            Size = textBox.Size;
        }

        private ulong LastTimePressed;

        public Func<bool> ErrorFunc;

        public Color DefaultBorderColor { get; set; }
        public Color FocusedBorderColor { get; set; }
        public Color ValidBorderColor { get; set; }
        public Color ErrorBorderColor { get; set; }

        public override string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public char PasswordChar
        {
            get { return textBox.PasswordChar; }
            set { textBox.PasswordChar = value; }
        }

        public bool CheckError()
        {
            bool ret = ErrorFunc != null && ErrorFunc();
            BackColor = ret ? ErrorBorderColor : (ErrorFunc != null ? ValidBorderColor : DefaultBackColor);
            return ret;
        }

        protected override void OnEnter(EventArgs e)
        {
            BackColor = FocusedBorderColor;
            base.OnEnter(e);
        }

        protected override void OnLeave(EventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
                BackColor = DefaultBorderColor;
            else if (ErrorFunc != null && ErrorFunc())
                BackColor = ErrorBorderColor;
            else if (ErrorFunc != null && !ErrorFunc())
                BackColor = ValidBorderColor;
            base.OnLeave(e);
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(x, y, width, textBox.PreferredHeight, specified);
        }
    }

    public class ExTextBoxDesigner : ControlDesigner
    {
        public override void Initialize(IComponent comp)
        {
            base.Initialize(comp);
            var uc = (ExTextBox) comp;
            EnableDesignMode(uc.textBox, "textBox");
        }
    }
}