namespace IddaAnalyser
{
    public class Match
    {
        public Match()
        {

        }

        public int MatchId { get; set; }

        public virtual Date Date { get; set; }
        public int DateId { get; set; }
        public virtual Time Time { get; set; }
        public int TimeId { get; set; }
        public virtual Team HomeTeam { get; set; }
        public int HomeTeamId { get; set; }
        public virtual Team AwayTeam { get; set; }
        public int AwayTeamId { get; set; }
        public virtual League League { get; set; }
        public int LeagueId { get; set; }
        public virtual MatchResult MatchResult { get; set; }
        public int MatchResultId { get; set; }

        public virtual FullOdd FullOdd { get; set; }
        public int FullOddId { get; set; }

        public int MatchCode { get; set; }
        public int MatchHour { get; set; }
        public int MatchMinute { get; set; }
        public int MatchDay { get; set; }
        public int MatchMonth { get; set; }
        public int MatchYear { get; set; }

        public int GeneralResultId { get; set; }

        public void SetMatch(Date date, Time time,Team homeTeam, Team awayTeam, League league, MatchResult matchResult, FullOdd fullOdd,int matchCode)
        {
            this.MatchCode = matchCode;

            this.Date = date;
            this.DateId = date.DateId;
            date.Matches.Add(this);

            this.Time = time;
            this.TimeId = TimeId;
            time.Matches.Add(this);

            this.HomeTeam = homeTeam;
            this.HomeTeamId = homeTeam.TeamId;
            homeTeam.HomeMatches.Add(this);

            this.AwayTeam = awayTeam;
            this.AwayTeamId = AwayTeamId;
            awayTeam.AwayMatches.Add(this);

            this.League = league;
            this.LeagueId = league.LeagueId;
            league.Matches.Add(this);

            this.MatchResult = matchResult;
            this.MatchResultId = matchResult.MatchResultId;
            matchResult.Matches.Add(this);

            this.FullOdd = fullOdd;
            this.FullOddId = fullOdd.FullOddId;
            fullOdd.Matches.Add(this);

            this.HomeTeamName = homeTeam.TeamName;
            this.AwayTeamName = awayTeam.TeamName;
            this.FhScore = matchResult.FirstHalfHomeScore + " - " + matchResult.FirstHalfAwayScore;
            this.MsScore = matchResult.MatchHomeScore + " - " + matchResult.MatchAwayScore;
        }
        public void SetFullOdd(FullOdd fullOdd)
        {
            this.FullOdd = fullOdd;
            this.FullOddId = fullOdd.FullOddId;
            fullOdd.Matches.Add(this);
        }


        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string FhScore { get; set; }
        public string MsScore { get; set; }

    }
}
