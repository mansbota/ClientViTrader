using LiveChartsCore.Defaults;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientViTrader.Utils
{
    public class OHLCData : IEnumerable<FinancialPoint>
    {
        public List<FinancialPoint> data;

        public long Count { get => data.Count; }

        public OHLCData(List<double[]> data)
        {
            this.data = new List<FinancialPoint>(data.Count);

            foreach (var point in data)
            {
                this.data.Add(
                    new FinancialPoint(DateTimeOffset
                        .FromUnixTimeMilliseconds((long)point[0]).DateTime, point[2], point[1], point[4], point[3]));
            }
        }

        public FinancialPoint this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }

        public IEnumerator<FinancialPoint> GetEnumerator()
        {
            return data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
