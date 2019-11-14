using System.Collections.Generic;
namespace IddaAnalyser
{
    public class FullOdd
    {
        public virtual ICollection<Match> Matches { get; set; }
        public FullOdd()
        {
            Matches = new HashSet<Match>();
        }
        public FullOdd(string value)
        {
            this.Value = value;
        }

        public string OddCombinationIds { get; set; }
        public byte[] RawOddCombinationIds { get; set; }
        public string OrderedGeneralResultIds { get; set; }
        public string ResultOrders { get; set; }

        public string IntersectedResults { get; set; }

        public int FullOddId { get; set; }

        public string Value { get; set; }
        public string PartialOddIds { get; set; }
        public void SetValue(string value)
        {
            this.Value = value;
        }
    }
}
