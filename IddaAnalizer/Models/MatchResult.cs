using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace IddaAnalyser
{
    public class MatchResult
    {
        public int MatchResultId { get; set; }
        public virtual ICollection<Match> Matches { get; set; }
        public MatchResult()
        {
            Matches = new HashSet<Match>();
        }


        public int FirstHalfHomeScore
        {
            get;
            set;
        }

        public int FirstHalfAwayScore
        {
            get;
            set;
        }

        public int MatchHomeScore
        {
            get;
            set;
        }
        public int MatchAwayScore
        {
            get;
            set;
        }
        public virtual GeneralResult GeneralResult { get; set; }
        public int? GeneralResultId { get; set; }

    }
}
