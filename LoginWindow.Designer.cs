
namespace ClientViTrader
{
    partial class LoginWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginWindow));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.userNameLog = new System.Windows.Forms.TextBox();
            this.passwordLog = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.passwordReg = new System.Windows.Forms.TextBox();
            this.userNameReg = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.emailAddress = new System.Windows.Forms.TextBox();
            this.regButton = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.passwordReg2 = new System.Windows.Forms.TextBox();
            this.logUserNameErr = new System.Windows.Forms.ErrorProvider(this.components);
            this.logPasswordErr = new System.Windows.Forms.ErrorProvider(this.components);
            this.regUserNameErr = new System.Windows.Forms.ErrorProvider(this.components);
            this.regPasswordErr = new System.Windows.Forms.ErrorProvider(this.components);
            this.emailAddressErr = new System.Windows.Forms.ErrorProvider(this.components);
            this.regPasswordErr2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logUserNameErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.logPasswordErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regUserNameErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regPasswordErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailAddressErr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.regPasswordErr2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.groupBox1.Controls.Add(this.logButton);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.userNameLog);
            this.groupBox1.Controls.Add(this.passwordLog);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // logButton
            // 
            resources.ApplyResources(this.logButton, "logButton");
            this.logButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.logButton.Name = "logButton";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.logButton_Click);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.Color.Gainsboro;
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.Color.Gainsboro;
            this.label1.Name = "label1";
            // 
            // userNameLog
            // 
            this.userNameLog.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.userNameLog, "userNameLog");
            this.userNameLog.Name = "userNameLog";
            this.userNameLog.Validating += new System.ComponentModel.CancelEventHandler(this.userNameLog_Validating);
            this.userNameLog.Validated += new System.EventHandler(this.userNameLog_Validated);
            // 
            // passwordLog
            // 
            this.passwordLog.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.passwordLog, "passwordLog");
            this.passwordLog.Name = "passwordLog";
            this.passwordLog.UseSystemPasswordChar = true;
            this.passwordLog.Validating += new System.ComponentModel.CancelEventHandler(this.passwordLog_Validating);
            this.passwordLog.Validated += new System.EventHandler(this.passwordLog_Validated);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.Color.Gainsboro;
            this.label3.Name = "label3";
            // 
            // passwordReg
            // 
            this.passwordReg.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.passwordReg, "passwordReg");
            this.passwordReg.Name = "passwordReg";
            this.passwordReg.UseSystemPasswordChar = true;
            this.passwordReg.Validating += new System.ComponentModel.CancelEventHandler(this.passwordReg_Validating);
            this.passwordReg.Validated += new System.EventHandler(this.passwordReg_Validated);
            // 
            // userNameReg
            // 
            this.userNameReg.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.userNameReg, "userNameReg");
            this.userNameReg.Name = "userNameReg";
            this.userNameReg.Validating += new System.ComponentModel.CancelEventHandler(this.userNameReg_Validating);
            this.userNameReg.Validated += new System.EventHandler(this.userNameReg_Validated);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.Color.Gainsboro;
            this.label4.Name = "label4";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.Color.Gainsboro;
            this.label5.Name = "label5";
            // 
            // emailAddress
            // 
            this.emailAddress.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.emailAddress, "emailAddress");
            this.emailAddress.Name = "emailAddress";
            this.emailAddress.Validating += new System.ComponentModel.CancelEventHandler(this.emailAddress_Validating);
            this.emailAddress.Validated += new System.EventHandler(this.emailAddress_Validated);
            // 
            // regButton
            // 
            resources.ApplyResources(this.regButton, "regButton");
            this.regButton.ForeColor = System.Drawing.Color.Gainsboro;
            this.regButton.Name = "regButton";
            this.regButton.UseVisualStyleBackColor = true;
            this.regButton.Click += new System.EventHandler(this.regButton_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.passwordReg2);
            this.groupBox2.Controls.Add(this.regButton);
            this.groupBox2.Controls.Add(this.emailAddress);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.userNameReg);
            this.groupBox2.Controls.Add(this.passwordReg);
            this.groupBox2.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.ForeColor = System.Drawing.Color.Gainsboro;
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.Color.Gainsboro;
            this.label6.Name = "label6";
            // 
            // passwordReg2
            // 
            this.passwordReg2.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.passwordReg2, "passwordReg2");
            this.passwordReg2.Name = "passwordReg2";
            this.passwordReg2.UseSystemPasswordChar = true;
            this.passwordReg2.Validated += new System.EventHandler(this.passwordReg2_Validated);
            // 
            // logUserNameErr
            // 
            this.logUserNameErr.ContainerControl = this;
            // 
            // logPasswordErr
            // 
            this.logPasswordErr.ContainerControl = this;
            // 
            // regUserNameErr
            // 
            this.regUserNameErr.ContainerControl = this;
            // 
            // regPasswordErr
            // 
            this.regPasswordErr.ContainerControl = this;
            // 
            // emailAddressErr
            // 
            this.emailAddressErr.ContainerControl = this;
            // 
            // regPasswordErr2
            // 
            this.regPasswordErr2.ContainerControl = this;
            // 
            // LoginWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "LoginWindow";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginWindow_FormClosing);
            this.Load += new System.EventHandler(this.LoginWindow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logUserNameErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.logPasswordErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regUserNameErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regPasswordErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emailAddressErr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.regPasswordErr2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox userNameLog;
        private System.Windows.Forms.TextBox passwordLog;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox passwordReg;
        private System.Windows.Forms.TextBox userNameReg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox emailAddress;
        private System.Windows.Forms.Button regButton;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ErrorProvider logUserNameErr;
        private System.Windows.Forms.ErrorProvider logPasswordErr;
        private System.Windows.Forms.ErrorProvider regUserNameErr;
        private System.Windows.Forms.ErrorProvider regPasswordErr;
        private System.Windows.Forms.ErrorProvider emailAddressErr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox passwordReg2;
        private System.Windows.Forms.ErrorProvider regPasswordErr2;
    }
}

