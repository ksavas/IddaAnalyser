using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class Team
    {
        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
        public Team()
        {

        }
        public Team(string teamName, string adaptedTeamName)
        {
            this.TeamName = teamName;
            this.AdaptedTeamName = adaptedTeamName;

        }
        public int TeamId { get; set; }

        public string TeamName { get; set; }
        public string AdaptedTeamName { get; set; }

    }
}
