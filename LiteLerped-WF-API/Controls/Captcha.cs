using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace LiteLerped_WF_API.Controls
{
    [Designer(typeof(CaptchaDesigner))]
    public class Captcha : UserControl, ISupportInitialize
    {
        public PictureBox pictureBox;
        public Button btnRefresh;

        public string Challenge, Key;

        private ResourceManager rMan;
        private bool onceLoad;

        public Captcha()
        {
            rMan = new ResourceManager(typeof(Program).Namespace + ".Resources.Credentials", Assembly.GetExecutingAssembly());
            Control container = new ContainerControl()
            {
                Dock = DockStyle.Fill,
                BackColor = SystemColors.ControlLightLight
            };
            //Console.WriteLine(container.ClientRectangle.Width);
            pictureBox = new PictureBox()
            {
                Name = "pcbCaptcha",
                BorderStyle = BorderStyle.None,
                Location = new Point(1, 1),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom |
                         AnchorStyles.Left | AnchorStyles.Right
            };
            btnRefresh = new Button()
            {
                Name = "btnCaptcha",
                Size = new Size(24, 24),
                BackColor = SystemColors.ControlLight,
                BackgroundImage = (Bitmap) rMan.GetObject("Refresh")
            };
            container.Controls.Add(pictureBox);
            container.Controls.Add(btnRefresh);
            this.Controls.Add(container);

            btnRefresh.Click += RegenCaptcha;
            pictureBox.Paint += (obj, sender) =>
            {
                if (!onceLoad)
                { //Quizás vaya un poco justo.
                    Rectangle rect = ((PictureBox) obj).Parent.ClientRectangle;
                    pictureBox.Size = new Size(container.Width - 25, container.Height);
                    btnRefresh.Location = new Point(container.Width - 25, 0);
                    onceLoad = true;
                }
            };
            /*Load += (a, b) =>
            {
                Console.WriteLine("aaaa");
            };*/
        }

        public void DownloadReCaptcha()
        {
            if (string.IsNullOrEmpty(Key)) throw new NotDefinedCaptchaKeyException();
            try
            {
                using (WebClient client = new WebClient())
                {
                    string response = client.DownloadString(string.Format("http://api.recaptcha.net/challenge?k={0}", Key));

                    Match match = Regex.Match(response, "challenge : '(.+?)'");

                    if (match.Captures.Count == 0)
                    {
                        Challenge = null;
                        return;
                    }

                    Challenge = match.Groups[1].Value;

                    WebRequest request = WebRequest.Create(string.Format("http://www.google.com/recaptcha/api/image?c={0}", Challenge));
                    using (WebResponse webResp = request.GetResponse())
                    using (Stream responseStream = webResp.GetResponseStream())
                        pictureBox.Image = new Bitmap(responseStream);
                }
            }
            catch
            {
                Challenge = null;
            }
        }

        private string GetCaptchaResponse(string userResponse)
        {
            if (string.IsNullOrEmpty(Key)) throw new NotDefinedCaptchaKeyException();
            string url = string.Format("https://www.google.com/recaptcha/api/noscript?k={0}&recaptcha_challenge_field={1}&recaptcha_response_field={2}", Key, Challenge, Uri.EscapeDataString(userResponse));
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(url);
            try
            {
                return doc.DocumentNode.Descendants("textarea").FirstOrDefault().InnerText;
            }
            catch
            {
                return "";
            }
        }

        private string VerifyCaptcha(string gResponse)
        {
            if (string.IsNullOrEmpty(Key)) throw new NotDefinedCaptchaKeyException();
            using (WebClient client = new WebClient())
            {
                string GoogleReply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", Key, gResponse));

                //PHPResponses captchaResponse = JsonConvert.DeserializeObject<PHPResponses>(GoogleReply);

                return JsonConvert.DeserializeObject<JObject>(GoogleReply)["success"].ToObject<string>(); //captchaResponse.Success;
            }
        }

        public bool SolveCaptcha(string userResponse)
        {
            string res = GetCaptchaResponse(userResponse);
            return VerifyCaptcha(res) == "true" || !string.IsNullOrWhiteSpace(res);
        }

        public void RegenCaptcha()
        {
            RegenCaptcha(null, null);
        }

        private void RegenCaptcha(object obj, EventArgs sender)
        {
            DownloadReCaptcha();
        }

        void ISupportInitialize.BeginInit()
        {
            //throw new NotImplementedException();
        }

        void ISupportInitialize.EndInit()
        {
            //throw new NotImplementedException();
        }
    }

    public class CaptchaDesigner : ControlDesigner
    {
        public override void Initialize(IComponent comp)
        {
            base.Initialize(comp);
            var uc = (Captcha) comp;
            EnableDesignMode(uc.pictureBox, "pictureBox");
        }
    }

    public class NotDefinedCaptchaKeyException : Exception
    {
    }
}