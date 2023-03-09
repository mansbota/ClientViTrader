using System;
using System.Collections.Generic;

namespace ClientViTrader.Utils
{
    public class PriceData
    {
        public List<double[]> prices { get; set; }
        public List<double[]> market_caps { get; set; }
        public List<double[]> total_volumes { get; set; }

        public static int RSI_PERIOD { get => 14; }

        public double[] CalculateRsi()
        {
            double[] change = new double[prices.Count];
            double[] gain = new double[prices.Count];
            double[] loss = new double[prices.Count];
            double[] avgGain = new double[prices.Count];
            double[] avgLoss = new double[prices.Count];
            double[] rs = new double[prices.Count];
            double[] rsi = new double[prices.Count];

            for (int i = 1; i < prices.Count; i++)
            {
                change[i] = prices[i][1] - prices[i - 1][1];

                if (change[i] > 0)
                    gain[i] = change[i];
                else
                    loss[i] = Math.Abs(change[i]);

                if (i == RSI_PERIOD)
                {
                    double gains = 0;
                    double losses = 0;

                    for (int j = 1; j <= RSI_PERIOD; j++)
                    {
                        gains += gain[j];
                        losses += loss[j];
                    }

                    avgGain[i] = gains / RSI_PERIOD;
                    avgLoss[i] = losses / RSI_PERIOD;
                    rs[i] = avgGain[i] / avgLoss[i];
                    rsi[i] = (100 - (100 / (1 + rs[i])));
                }
                else if (i > RSI_PERIOD)
                {
                    avgGain[i] = ((avgGain[i - 1] * RSI_PERIOD - 1) + gain[i]) / RSI_PERIOD;
                    avgLoss[i] = ((avgLoss[i - 1] * RSI_PERIOD - 1) + loss[i]) / RSI_PERIOD;
                    rs[i] = avgGain[i] / avgLoss[i];
                    rsi[i] = (100 - (100 / (1 + rs[i])));
                }
            }

            return rsi;
        }

        public double[] CalculateStochasticRsi()
        {
            double[] rsi = CalculateRsi();
            double[] highestValues = new double[rsi.Length];
            double[] lowestValues = new double[rsi.Length];
            double[] stochasticRsi = new double[rsi.Length];

            for (int i = 0; i < rsi.Length; i++)
            {
                if (i < RSI_PERIOD)
                {
                    double highestValue = 0;
                    double lowestValue = 100;

                    for (int j = i; j >= 0; j--)
                    {
                        if (rsi[j] > highestValue)
                            highestValue = rsi[j];

                        if (rsi[j] < lowestValue)
                            lowestValue = rsi[j];
                    }

                    highestValues[i] = highestValue;
                    lowestValues[i] = lowestValue;
                }
                else
                {
                    double highestValue = 0;
                    double lowestValue = 100;

                    for (int j = 0; j < RSI_PERIOD; j++)
                    {
                        if (rsi[i - j] > highestValue)
                            highestValue = rsi[i - j];

                        if (rsi[i - j] < lowestValue)
                            lowestValue = rsi[i - j];
                    }

                    highestValues[i] = highestValue;
                    lowestValues[i] = lowestValue;
                }

                stochasticRsi[i] = (rsi[i] - lowestValues[i]) / (highestValues[i] - lowestValues[i]) * 100;
            }

            for (int i = 0; i < 5; i++)
                stochasticRsi[i] = 50;

            return stochasticRsi;
        }
    }
}
