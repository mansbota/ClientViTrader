
namespace ClientViTrader
{
    partial class BuyForm
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
            this.components = new System.ComponentModel.Container();
            this.coinTextBox = new System.Windows.Forms.TextBox();
            this.coinLabel = new System.Windows.Forms.Label();
            this.usdTextBox = new System.Windows.Forms.TextBox();
            this.usdLabel = new System.Windows.Forms.Label();
            this.usdCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.coinTextBoxError = new System.Windows.Forms.ErrorProvider(this.components);
            this.usdTextBoxError = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.coinTextBoxError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usdTextBoxError)).BeginInit();
            this.SuspendLayout();
            // 
            // coinTextBox
            // 
            this.coinTextBox.Location = new System.Drawing.Point(95, 36);
            this.coinTextBox.Name = "coinTextBox";
            this.coinTextBox.Size = new System.Drawing.Size(224, 25);
            this.coinTextBox.TabIndex = 0;
            this.coinTextBox.Validated += new System.EventHandler(this.coinTextBox_Validated);
            // 
            // coinLabel
            // 
            this.coinLabel.AutoSize = true;
            this.coinLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.coinLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.coinLabel.Location = new System.Drawing.Point(73, 10);
            this.coinLabel.Name = "coinLabel";
            this.coinLabel.Size = new System.Drawing.Size(99, 20);
            this.coinLabel.TabIndex = 1;
            this.coinLabel.Text = "Coin Amount";
            // 
            // usdTextBox
            // 
            this.usdTextBox.Location = new System.Drawing.Point(95, 95);
            this.usdTextBox.Name = "usdTextBox";
            this.usdTextBox.Size = new System.Drawing.Size(224, 25);
            this.usdTextBox.TabIndex = 2;
            this.usdTextBox.Validated += new System.EventHandler(this.usdTextBox_Validated);
            // 
            // usdLabel
            // 
            this.usdLabel.AutoSize = true;
            this.usdLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.usdLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.usdLabel.Location = new System.Drawing.Point(73, 69);
            this.usdLabel.Name = "usdLabel";
            this.usdLabel.Size = new System.Drawing.Size(98, 20);
            this.usdLabel.TabIndex = 3;
            this.usdLabel.Text = "USD Amount";
            // 
            // usdCheckBox
            // 
            this.usdCheckBox.AutoSize = true;
            this.usdCheckBox.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.usdCheckBox.Location = new System.Drawing.Point(73, 128);
            this.usdCheckBox.Name = "usdCheckBox";
            this.usdCheckBox.Size = new System.Drawing.Size(127, 21);
            this.usdCheckBox.TabIndex = 4;
            this.usdCheckBox.Text = "Use USD Amount";
            this.usdCheckBox.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(124, 175);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 26);
            this.okButton.TabIndex = 5;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(205, 175);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 26);
            this.cancelButton.TabIndex = 6;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // coinTextBoxError
            // 
            this.coinTextBoxError.ContainerControl = this;
            // 
            // usdTextBoxError
            // 
            this.usdTextBoxError.ContainerControl = this;
            // 
            // BuyForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(415, 228);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.usdCheckBox);
            this.Controls.Add(this.usdLabel);
            this.Controls.Add(this.usdTextBox);
            this.Controls.Add(this.coinLabel);
            this.Controls.Add(this.coinTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "BuyForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Buy";
            this.Load += new System.EventHandler(this.BuyForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.coinTextBoxError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usdTextBoxError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox coinTextBox;
        private System.Windows.Forms.Label coinLabel;
        private System.Windows.Forms.TextBox usdTextBox;
        private System.Windows.Forms.Label usdLabel;
        private System.Windows.Forms.CheckBox usdCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ErrorProvider coinTextBoxError;
        private System.Windows.Forms.ErrorProvider usdTextBoxError;
    }
}