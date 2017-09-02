using LiteLerped_WF_API.Classes;
using LiteLerped_WF_API.Controls;

namespace LiteLerped_WF_API
{
    public partial class frmCredentials
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCredentials));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLogin = new System.Windows.Forms.TabPage();
            this.chkRemember = new System.Windows.Forms.CheckBox();
            this.lblLogin = new LiteLerped_WF_API.Controls.NotifyLabel();
            this.button1 = new System.Windows.Forms.Button();
            this.txtPassword = new LiteLerped_WF_API.Controls.ExTextBox();
            this.txtUsername = new LiteLerped_WF_API.Controls.ExTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tabRegister = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new LiteLerped_WF_API.Controls.Captcha();
            this.lblRegister = new LiteLerped_WF_API.Controls.NotifyLabel();
            this.txtSolution = new LiteLerped_WF_API.Controls.ExTextBox();
            this.txtRepeatEmail = new LiteLerped_WF_API.Controls.ExTextBox();
            this.txtEmail = new LiteLerped_WF_API.Controls.ExTextBox();
            this.txtRepeatPassword = new LiteLerped_WF_API.Controls.ExTextBox();
            this.txtRegisterPassword = new LiteLerped_WF_API.Controls.ExTextBox();
            this.txtRegisterUsername = new LiteLerped_WF_API.Controls.ExTextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.languageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.spanishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frenchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.germanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1.SuspendLayout();
            this.tabLogin.SuspendLayout();
            this.lblLogin.SuspendLayout();
            this.tabRegister.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.lblRegister.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabLogin);
            this.tabControl1.Controls.Add(this.tabRegister);
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabLogin
            // 
            this.tabLogin.Controls.Add(this.chkRemember);
            this.tabLogin.Controls.Add(this.lblLogin);
            this.tabLogin.Controls.Add(this.button1);
            this.tabLogin.Controls.Add(this.txtPassword);
            this.tabLogin.Controls.Add(this.txtUsername);
            this.tabLogin.Controls.Add(this.label2);
            this.tabLogin.Controls.Add(this.label1);
            resources.ApplyResources(this.tabLogin, "tabLogin");
            this.tabLogin.Name = "tabLogin";
            this.tabLogin.UseVisualStyleBackColor = true;
            // 
            // chkRemember
            // 
            resources.ApplyResources(this.chkRemember, "chkRemember");
            this.chkRemember.Name = "chkRemember";
            this.chkRemember.UseVisualStyleBackColor = true;
            this.chkRemember.CheckedChanged += new System.EventHandler(this.chkRemember_CheckedChanged);
            // 
            // lblLogin
            // 
            resources.ApplyResources(this.lblLogin, "lblLogin");
            this.lblLogin.MyOrder = 0;
            this.lblLogin.Name = "lblLogin";
            this.lblLogin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtPassword.DefaultBorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtPassword.ErrorBorderColor = System.Drawing.Color.Red;
            this.txtPassword.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ValidBorderColor = System.Drawing.Color.Green;
            this.txtPassword.Leave += new System.EventHandler(this.txtPassword_Leave);
            // 
            // txtUsername
            // 
            this.txtUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            this.txtUsername.DefaultBorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtUsername.ErrorBorderColor = System.Drawing.Color.Red;
            this.txtUsername.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PasswordChar = '\0';
            this.txtUsername.ValidBorderColor = System.Drawing.Color.Green;
            this.txtUsername.Leave += new System.EventHandler(this.txtUsername_Leave);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tabRegister
            // 
            this.tabRegister.Controls.Add(this.label11);
            this.tabRegister.Controls.Add(this.label9);
            this.tabRegister.Controls.Add(this.button2);
            this.tabRegister.Controls.Add(this.label10);
            this.tabRegister.Controls.Add(this.label7);
            this.tabRegister.Controls.Add(this.label8);
            this.tabRegister.Controls.Add(this.label5);
            this.tabRegister.Controls.Add(this.label6);
            this.tabRegister.Controls.Add(this.pictureBox1);
            this.tabRegister.Controls.Add(this.lblRegister);
            this.tabRegister.Controls.Add(this.txtSolution);
            this.tabRegister.Controls.Add(this.txtRepeatEmail);
            this.tabRegister.Controls.Add(this.txtEmail);
            this.tabRegister.Controls.Add(this.txtRepeatPassword);
            this.tabRegister.Controls.Add(this.txtRegisterPassword);
            this.tabRegister.Controls.Add(this.txtRegisterUsername);
            resources.ApplyResources(this.tabRegister, "tabRegister");
            this.tabRegister.Name = "tabRegister";
            this.tabRegister.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // lblRegister
            // 
            resources.ApplyResources(this.lblRegister, "lblRegister");
            this.lblRegister.MyOrder = 0;
            this.lblRegister.Name = "lblRegister";
            this.lblRegister.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtSolution
            // 
            this.txtSolution.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtSolution.DefaultBorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtSolution.ErrorBorderColor = System.Drawing.Color.Red;
            this.txtSolution.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            resources.ApplyResources(this.txtSolution, "txtSolution");
            this.txtSolution.Name = "txtSolution";
            this.txtSolution.PasswordChar = '\0';
            this.txtSolution.ValidBorderColor = System.Drawing.Color.Green;
            // 
            // txtRepeatEmail
            // 
            this.txtRepeatEmail.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtRepeatEmail.DefaultBorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtRepeatEmail.ErrorBorderColor = System.Drawing.Color.Red;
            this.txtRepeatEmail.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            resources.ApplyResources(this.txtRepeatEmail, "txtRepeatEmail");
            this.txtRepeatEmail.Name = "txtRepeatEmail";
            this.txtRepeatEmail.PasswordChar = '\0';
            this.txtRepeatEmail.ValidBorderColor = System.Drawing.Color.Green;
            // 
            // txtEmail
            // 
            this.txtEmail.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtEmail.DefaultBorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtEmail.ErrorBorderColor = System.Drawing.Color.Red;
            this.txtEmail.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            resources.ApplyResources(this.txtEmail, "txtEmail");
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PasswordChar = '\0';
            this.txtEmail.ValidBorderColor = System.Drawing.Color.Green;
            // 
            // txtRepeatPassword
            // 
            this.txtRepeatPassword.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtRepeatPassword.DefaultBorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtRepeatPassword.ErrorBorderColor = System.Drawing.Color.Red;
            this.txtRepeatPassword.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            resources.ApplyResources(this.txtRepeatPassword, "txtRepeatPassword");
            this.txtRepeatPassword.Name = "txtRepeatPassword";
            this.txtRepeatPassword.PasswordChar = '*';
            this.txtRepeatPassword.ValidBorderColor = System.Drawing.Color.Green;
            // 
            // txtRegisterPassword
            // 
            this.txtRegisterPassword.BackColor = System.Drawing.SystemColors.ControlDark;
            this.txtRegisterPassword.DefaultBorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtRegisterPassword.ErrorBorderColor = System.Drawing.Color.Red;
            this.txtRegisterPassword.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            resources.ApplyResources(this.txtRegisterPassword, "txtRegisterPassword");
            this.txtRegisterPassword.Name = "txtRegisterPassword";
            this.txtRegisterPassword.PasswordChar = '*';
            this.txtRegisterPassword.ValidBorderColor = System.Drawing.Color.Green;
            // 
            // txtRegisterUsername
            // 
            this.txtRegisterUsername.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            this.txtRegisterUsername.DefaultBorderColor = System.Drawing.SystemColors.ControlDark;
            this.txtRegisterUsername.ErrorBorderColor = System.Drawing.Color.Red;
            this.txtRegisterUsername.FocusedBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(129)))), ((int)(((byte)(190)))), ((int)(((byte)(247)))));
            resources.ApplyResources(this.txtRegisterUsername, "txtRegisterUsername");
            this.txtRegisterUsername.Name = "txtRegisterUsername";
            this.txtRegisterUsername.PasswordChar = '\0';
            this.txtRegisterUsername.ValidBorderColor = System.Drawing.Color.Green;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.languageToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // languageToolStripMenuItem
            // 
            this.languageToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishToolStripMenuItem,
            this.spanishToolStripMenuItem,
            this.frenchToolStripMenuItem,
            this.germanToolStripMenuItem});
            this.languageToolStripMenuItem.Name = "languageToolStripMenuItem";
            resources.ApplyResources(this.languageToolStripMenuItem, "languageToolStripMenuItem");
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            resources.ApplyResources(this.englishToolStripMenuItem, "englishToolStripMenuItem");
            this.englishToolStripMenuItem.Tag = "en";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click_1);
            // 
            // spanishToolStripMenuItem
            // 
            this.spanishToolStripMenuItem.Name = "spanishToolStripMenuItem";
            resources.ApplyResources(this.spanishToolStripMenuItem, "spanishToolStripMenuItem");
            this.spanishToolStripMenuItem.Tag = "es";
            this.spanishToolStripMenuItem.Click += new System.EventHandler(this.spanishToolStripMenuItem_Click);
            // 
            // frenchToolStripMenuItem
            // 
            this.frenchToolStripMenuItem.Name = "frenchToolStripMenuItem";
            resources.ApplyResources(this.frenchToolStripMenuItem, "frenchToolStripMenuItem");
            this.frenchToolStripMenuItem.Tag = "fr";
            this.frenchToolStripMenuItem.Click += new System.EventHandler(this.frenchToolStripMenuItem_Click);
            // 
            // germanToolStripMenuItem
            // 
            this.germanToolStripMenuItem.Name = "germanToolStripMenuItem";
            resources.ApplyResources(this.germanToolStripMenuItem, "germanToolStripMenuItem");
            this.germanToolStripMenuItem.Tag = "de";
            this.germanToolStripMenuItem.Click += new System.EventHandler(this.germanToolStripMenuItem_Click);
            // 
            // frmCredentials
            // 
            this.AcceptButton = this.button1;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCredentials";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCredentials_FormClosing);
            this.Load += new System.EventHandler(this.LerpedCredentials_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabLogin.ResumeLayout(false);
            this.tabLogin.PerformLayout();
            this.lblLogin.ResumeLayout(false);
            this.tabRegister.ResumeLayout(false);
            this.tabRegister.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.lblRegister.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabLogin;
        private NotifyLabel lblLogin;
        private System.Windows.Forms.Button button1;
        private ExTextBox txtPassword;
        private ExTextBox txtUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabRegister;
        public  System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button2;
        private ExTextBox txtRepeatEmail;
        private System.Windows.Forms.Label label10;
        private ExTextBox txtEmail;
        private ExTextBox txtRepeatPassword;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private NotifyLabel lblRegister;
        private ExTextBox txtRegisterPassword;
        private ExTextBox txtRegisterUsername;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private ExTextBox txtSolution;
        private System.Windows.Forms.Label label9;
        private Captcha pictureBox1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkRemember;
        private System.Windows.Forms.ToolStripMenuItem languageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem englishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem spanishToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frenchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem germanToolStripMenuItem;
    }
}