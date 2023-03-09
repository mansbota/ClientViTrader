
namespace ClientViTrader
{
    partial class VisualizeForm
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
            this.priceChart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            this.indicatorChart = new LiveChartsCore.SkiaSharpView.WinForms.CartesianChart();
            this.SuspendLayout();
            // 
            // priceChart
            // 
            this.priceChart.Location = new System.Drawing.Point(12, 14);
            this.priceChart.Name = "priceChart";
            this.priceChart.Size = new System.Drawing.Size(1274, 448);
            this.priceChart.TabIndex = 0;
            // 
            // indicatorChart
            // 
            this.indicatorChart.Location = new System.Drawing.Point(12, 469);
            this.indicatorChart.Name = "indicatorChart";
            this.indicatorChart.Size = new System.Drawing.Size(1274, 296);
            this.indicatorChart.TabIndex = 1;
            // 
            // VisualizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(1298, 780);
            this.Controls.Add(this.indicatorChart);
            this.Controls.Add(this.priceChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "VisualizeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Visualize";
            this.Load += new System.EventHandler(this.VisualizeForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart priceChart;
        private LiveChartsCore.SkiaSharpView.WinForms.CartesianChart indicatorChart;
    }
}