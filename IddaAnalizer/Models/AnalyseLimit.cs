using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class AnalyseLimit
    {
        public AnalyseLimit()
        {

        }

        public int AnalyseLimitId { get; set; }

        public string LimitType { get; set; }

        public int MaxValue { get; set; }

        public int MinValue { get; set; }

        public void SetValues(int maxValue, int minValue)
        {
            this.MaxValue = maxValue;
            this.MinValue = minValue;
        }

    }
}
