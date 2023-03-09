using ClientViTrader.Utils;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientViTrader
{
    public partial class VisualizeForm : Form
    {
        private PriceData data;

        public VisualizeForm(PriceData data)
        {
            InitializeComponent();
            this.data = data;
        }

        private void VisualizeForm_Load(object sender, EventArgs e)
        {
            double[] stochRsi = data.CalculateStochasticRsi();
            double[] prices = new double[stochRsi.Length];

            for (int i = 0; i < prices.Length; i++)
                prices[i] = data.prices[i][1];

            stochRsi = stochRsi[14..];
            prices = prices[14..];

            ISeries[] series = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = prices,
                    Fill = null,
                    GeometrySize = 0
                }
            };

            priceChart.Series = series;
            priceChart.Draw();

            ISeries[] series2 = new ISeries[]
            {
                new LineSeries<double>
                {
                    Values = stochRsi,
                    Fill = null,
                    LineSmoothness = 0,
                    GeometrySize = 0
                }
            };

            indicatorChart.Series = series2;
            indicatorChart.Draw();
        }
    }
}
