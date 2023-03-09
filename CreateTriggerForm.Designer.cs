
namespace ClientViTrader
{
    partial class CreateTriggerForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.radioButtonUnder = new System.Windows.Forms.RadioButton();
            this.valueTextBox = new System.Windows.Forms.TextBox();
            this.radioButtonOver = new System.Windows.Forms.RadioButton();
            this.addButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.valueTextBoxError = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.valueTextBoxError)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(44, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Trigger value:";
            // 
            // radioButtonUnder
            // 
            this.radioButtonUnder.AutoSize = true;
            this.radioButtonUnder.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonUnder.Location = new System.Drawing.Point(73, 56);
            this.radioButtonUnder.Name = "radioButtonUnder";
            this.radioButtonUnder.Size = new System.Drawing.Size(146, 21);
            this.radioButtonUnder.TabIndex = 1;
            this.radioButtonUnder.TabStop = true;
            this.radioButtonUnder.Text = "Triggers when under";
            this.radioButtonUnder.UseVisualStyleBackColor = true;
            // 
            // valueTextBox
            // 
            this.valueTextBox.Location = new System.Drawing.Point(158, 23);
            this.valueTextBox.Name = "valueTextBox";
            this.valueTextBox.Size = new System.Drawing.Size(124, 25);
            this.valueTextBox.TabIndex = 2;
            this.valueTextBox.Validated += new System.EventHandler(this.valueTextBox_Validated);
            // 
            // radioButtonOver
            // 
            this.radioButtonOver.AutoSize = true;
            this.radioButtonOver.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.radioButtonOver.Location = new System.Drawing.Point(73, 85);
            this.radioButtonOver.Name = "radioButtonOver";
            this.radioButtonOver.Size = new System.Drawing.Size(138, 21);
            this.radioButtonOver.TabIndex = 3;
            this.radioButtonOver.TabStop = true;
            this.radioButtonOver.Text = "Triggers when over";
            this.radioButtonOver.UseVisualStyleBackColor = true;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(89, 124);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(75, 26);
            this.addButton.TabIndex = 4;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(171, 124);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 26);
            this.cancelButton.TabIndex = 5;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // valueTextBoxError
            // 
            this.valueTextBoxError.ContainerControl = this;
            // 
            // CreateTriggerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(347, 177);
            this.ControlBox = false;
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.radioButtonOver);
            this.Controls.Add(this.valueTextBox);
            this.Controls.Add(this.radioButtonUnder);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CreateTriggerForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add Trigger";
            this.Load += new System.EventHandler(this.CreateTriggerForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.valueTextBoxError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonUnder;
        private System.Windows.Forms.TextBox valueTextBox;
        private System.Windows.Forms.RadioButton radioButtonOver;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.ErrorProvider valueTextBoxError;
    }
}