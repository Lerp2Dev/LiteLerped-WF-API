using Lerp2Web;
using Lerp2Web.Properties;
using LiteLerped_WF_API.Classes;
using LiteLerped_WF_API.Controls;
using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace LiteLerped_WF_API
{ //Este namespace tiene que ser distinto
    public partial class frmCredentials : LerpedForm
    {
        public const string GoogleKey = "6LexLsMSAAAAABUuI6bvUYfxaumgcu0vGiEFotDA",
                            ValidNicknameChars = "ABCDEFGHIJKLMNÑOPQRSTUVWXYZabcdefghijklmnñopqrstuvwxyz0123456789.-_";

        public string AuthKey,
                      InstanceKey;

        public int UserId,
                   CreationDate;

        public bool doingAutologin;
        internal Timer checkAuth;

        public ExTextBox a_txtUsername
        {
            get
            {
                return txtUsername;
            }
        }

        public ExTextBox a_txtPassword
        {
            get
            {
                return txtPassword;
            }
        }

        public CheckBox a_chkRemember
        {
            get
            {
                return chkRemember;
            }
        }

        internal static Action<bool> doLogin;

        public frmCredentials()
        {
            Shown += (sender, e) =>
            {
                Program.lang = new LanguageManager(ManagedSettings.CurrentLanguage);
            };

            InitializeComponent();
        }

        private void LerpedCredentials_Load(object sender, EventArgs e)
        {
            ResizeForm(tabControl1.SelectedIndex);

            //Make the autologin here...

            if (doingAutologin)
            {
                Console.WriteLine("Auto login ready!!");
                Login(doLogin, true); //Este no funciona muy bien...
            }

            //typeof(Program).Namespace + ".Properties.Lang.Credentials",
            //Program.lang.Switch((LerpedLanguage) Enum.Parse(typeof(LerpedLanguage), ManagedSettings.DefaultLanguage));

            pictureBox1.Key = GoogleKey;

            //Border color checks

            Func<string, Func<bool>> passwordCheck = (pass) =>
            {
                return () =>
                       {
                           //Console.WriteLine("IsUpper: {0}\nIsLower: {1}\nIsDigit: {2}\nIsSymbol: {3}\nLength: {4}\nIsNotEmpty: {5}\nGoodLength: {6}\n", str.Any(c => char.IsUpper(c)), str.Any(c => char.IsLower(c)), str.Any(c => char.IsDigit(c)), str.Any(c => !char.IsLetterOrDigit(c)), str.Length, !string.IsNullOrWhiteSpace(str), str.Length >= 8 && str.Length <= 32);
                           return !(!string.IsNullOrWhiteSpace(pass) && pass.Length >= Lerp2Web.Lerp2Web.MIN_PASSWORD_LENGTH && pass.Length <= Lerp2Web.Lerp2Web.MAX_PASSWORD_LENGTH && pass.Any(c => char.IsUpper(c)) && pass.Any(c => char.IsLower(c)) && pass.Any(c => char.IsDigit(c)) && pass.Any(c => char.IsLetterOrDigit(c)));
                       };
            };

            Func<bool> usernameCheck = () =>
                       {
                           string str = txtRegisterUsername.Text;
                           return string.IsNullOrWhiteSpace(str) || !(str.Length >= Lerp2Web.Lerp2Web.MIN_USERNAME_LENGTH && str.Length <= Lerp2Web.Lerp2Web.MAX_USERNAME_LENGTH && Regex.IsMatch(str, @"^[a-zA-Z0-9\.\-_]*$")); // || already taken!
                       };

            txtRegisterUsername.ErrorFunc = usernameCheck;

            txtRegisterPassword.ErrorFunc = passwordCheck(txtRegisterPassword.Text);

            txtUsername.ErrorFunc = usernameCheck;

            txtPassword.ErrorFunc = passwordCheck(txtPassword.Text);

            txtRepeatPassword.ErrorFunc = () =>
            {
                return txtRepeatPassword.Text != txtRegisterPassword.Text;
            };

            txtEmail.ErrorFunc = () =>
            {
                string str = txtEmail.Text;
                return string.IsNullOrEmpty(str) || !Regex.IsMatch(str, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
            };

            txtRepeatEmail.ErrorFunc = () =>
            {
                return txtEmail.Text != txtRepeatEmail.Text;
            };

            txtSolution.ErrorFunc = () =>
            {
                return string.IsNullOrWhiteSpace(txtSolution.Text);
            };
        }

        public static void SetLoginCallback<T>(T frm) where T : Form
        {
            doLogin = (autoLogin) =>
            {
                //El frmMain no se puede usar aqui

                //Aqui se deberia saber a partir del localized form si se va a ejecutar el Auth, y si se ejecuta entonces hacer un main.Hide()

                if (autoLogin)
                    frm.Show();
                else
                    Program.cred.Show();

                Program.cred.checkAuth = new Timer();
                Program.cred.checkAuth.Tick += Program.cred.CheckAuth_Tick;
                Program.cred.checkAuth.Interval = 300000; //5 mins = 300000
                Program.cred.checkAuth.Start();

                //Append here language menu
                //Append also the logout menu

                //Application.OpenForms["frmCredentials"].Hide();
            };
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedIndex = ((TabControl) sender).SelectedIndex;
            NotifyLabel.CurOrder = selectedIndex;
            if (selectedIndex == 1) //new SmoothCaptcha.SmoothCaptcha(new CaptchaConfiguration() { Width = 300, Height = 57 }).Image;
                pictureBox1.DownloadReCaptcha();
            ResizeForm(selectedIndex);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Login(doLogin, false);
        }

        private void CheckAuth_Tick(object sender, EventArgs e)
        {
            if (!Program.web.ValidatingAuth(() =>
            {
                ActiveForm.Hide();
                Program.cred.Show();
            }))
                checkAuth.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Register();
        }

        private void Login(Action<bool> act, bool autoLogin)
        {
            /*if (txtUsername.CheckError() ||
                txtPassword.CheckError())
                return;*/
            //Mala idea mezclar los campos de fuera, aunque creo que txtUsername, txt... y chk son del frmCredentials
            bool isRemembered = chkRemember.Enabled;
            if (isRemembered && !doingAutologin)
                API.config.Save();
            //Now, do the login...
            if (Program.web.CreateAuth(txtUsername.Text, txtPassword.Text.CreateMD5()) != -1)
                act(autoLogin);
        }

        private void txtUsername_Leave(object sender, EventArgs e)
        {
            ExTextBox username = ((ExTextBox) sender);
            ManagedSettings.LoginUsername = username.Text;
        }

        private void txtPassword_Leave(object sender, EventArgs e)
        {
            ExTextBox password = ((ExTextBox) sender);
            ManagedSettings.LoginPassword = password.Text;
        }

        private void chkRemember_CheckedChanged(object sender, EventArgs e)
        {
            bool isRemembered = ((CheckBox) sender).Checked;
            if (!string.IsNullOrWhiteSpace(txtUsername.Text))
                txtUsername.Enabled = !isRemembered;
            if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                txtPassword.Enabled = !isRemembered;
        }

        private void Register()
        {
            if (txtRegisterUsername.CheckError() ||
                txtRegisterPassword.CheckError() ||
                txtRepeatPassword.CheckError() ||
                txtEmail.CheckError() ||
                txtRepeatEmail.CheckError() ||
                txtSolution.CheckError())
                return;
            if (!pictureBox1.SolveCaptcha(txtSolution.Text))
            {
                NotifyLabel.SendError("Invalid captcha given!");
                txtSolution.ForeColor = txtSolution.ErrorBorderColor;
                pictureBox1.RegenCaptcha();
                return;
            }
            //Interpretar este resultado como un JObject y si hay errores pos no proseguir... Y si es todo correcto mostrar un messagebox alertando de que todo ha ido bien
            JObject obj = Lerp2Web.Lerp2Web.JPost(new ActionRequest("register")
            {
                { "username", txtRegisterUsername.Text },
                { "password", txtRegisterPassword.Text },
                { "email", txtEmail.Text }
            });
            if (obj["success"].ToObject<int>() == (int) PHPSuccess.Register)
                MessageBox.Show(string.Format("A new account '{0}' has been succesfully registered!", txtRegisterUsername.Text));
        }

        public void Logout()
        {
        }

        private void ResizeForm(int selectedIndex)
        {
            switch (selectedIndex)
            {
                case 0:
                    Size = new Size(250, 250);
                    tabControl1.Size = new Size(234, 250 - 64);
                    break;

                case 1:
                    Size = new Size(360, 460);
                    tabControl1.Size = new Size(360 - 16, 460 - 64);
                    break;

                default:
                    return;
            }
        }
    }
}