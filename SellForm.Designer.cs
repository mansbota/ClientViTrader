
namespace ClientViTrader
{
    partial class SellForm
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
            this.coinLabel = new System.Windows.Forms.Label();
            this.usdLabel = new System.Windows.Forms.Label();
            this.coinTextBox = new System.Windows.Forms.TextBox();
            this.usdTextBox = new System.Windows.Forms.TextBox();
            this.usdCheckBox = new System.Windows.Forms.CheckBox();
            this.okButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.coinTextBoxError = new System.Windows.Forms.ErrorProvider(this.components);
            this.usdTextBoxError = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.coinTextBoxError)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.usdTextBoxError)).BeginInit();
            this.SuspendLayout();
            // 
            // coinLabel
            // 
            this.coinLabel.AutoSize = true;
            this.coinLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.coinLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.coinLabel.Location = new System.Drawing.Point(73, 10);
            this.coinLabel.Name = "coinLabel";
            this.coinLabel.Size = new System.Drawing.Size(99, 20);
            this.coinLabel.TabIndex = 0;
            this.coinLabel.Text = "Coin Amount";
            // 
            // usdLabel
            // 
            this.usdLabel.AutoSize = true;
            this.usdLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.usdLabel.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.usdLabel.Location = new System.Drawing.Point(73, 69);
            this.usdLabel.Name = "usdLabel";
            this.usdLabel.Size = new System.Drawing.Size(98, 20);
            this.usdLabel.TabIndex = 1;
            this.usdLabel.Text = "USD Amount";
            // 
            // coinTextBox
            // 
            this.coinTextBox.Location = new System.Drawing.Point(95, 36);
            this.coinTextBox.Name = "coinTextBox";
            this.coinTextBox.Size = new System.Drawing.Size(224, 25);
            this.coinTextBox.TabIndex = 2;
            // 
            // usdTextBox
            // 
            this.usdTextBox.Location = new System.Drawing.Point(95, 95);
            this.usdTextBox.Name = "usdTextBox";
            this.usdTextBox.Size = new System.Drawing.Size(224, 25);
            this.usdTextBox.TabIndex = 3;
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
            // SellForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(38)))));
            this.ClientSize = new System.Drawing.Size(415, 227);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.usdCheckBox);
            this.Controls.Add(this.usdTextBox);
            this.Controls.Add(this.coinTextBox);
            this.Controls.Add(this.usdLabel);
            this.Controls.Add(this.coinLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SellForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sell";
            this.Load += new System.EventHandler(this.SellForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.coinTextBoxError)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.usdTextBoxError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label coinLabel;
        private System.Windows.Forms.Label usdLabel;
        private System.Windows.Forms.TextBox coinTextBox;
        private System.Windows.Forms.TextBox usdTextBox;
        private System.Windows.Forms.CheckBox usdCheckBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ErrorProvider coinTextBoxError;
        private System.Windows.Forms.ErrorProvider usdTextBoxError;
    }
}