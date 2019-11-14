using System.Collections.Generic;
namespace IddaAnalyser
{
    public class League
    {
        public virtual ICollection<Match> Matches { get; set; }
        public League()
        {
            Matches = new HashSet<Match>();
        }
        public League(string LeagueName)
        {
            this.LeagueName = LeagueName;
        }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
    }
}
