using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class PartialOdd
    {
        public PartialOdd()
        {

        }
        public PartialOdd(int partialOddType, string oddValues, string fullOddIds)
        {
            this.PartialOddType = partialOddType;
            this.OddValues = oddValues;
            this.FullOddIds = fullOddIds;
        }

        public int PartialOddId { get; set; }
        public int PartialOddType { get; set; }
        public string OddValues { get; set; }
        public string FullOddIds { get; set; }
        public string ResultOrders{get;set;}
    }
}
