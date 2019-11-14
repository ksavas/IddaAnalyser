using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class OddCombination
    {
        public OddCombination()
        {

        }
        public OddCombination(string value, string resultOrders,string fullOddIds, int matchCount)
        {
            this.Value = value;
            this.ResultOrders = resultOrders;
            this.FullOddIds = fullOddIds;
            this.MatchCount = matchCount;
        }
        public int OddCombinationId { get; set; }

        public string Value { get; set; }
        public string ResultOrders { get; set; }
        public string FullOddIds { get; set; }

        public int MatchCount { get; set; }

    }
}
