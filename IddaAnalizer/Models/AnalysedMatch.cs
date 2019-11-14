namespace IddaAnalyser
{
    public class AnalysedMatch
    {
        public AnalysedMatch()
        {
        }

        public AnalysedMatch(int day, int month, int year)
        {
            MatchDay = day;
            MatchMonth = month;
            MatchYear = year;
        }

        public int AnalysedMatchId { get; set; }

        public int MatchCode { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string LeagueName { get; set; }

        public int MsHomeScore { get; set; }
        public int MsAwayScore { get; set; }
        public int FhHomeScore { get; set; }
        public int FhAwayScore { get; set; }

        public int MatchDay { get; set; }
        public int MatchMonth { get; set; }
        public int MatchYear { get; set; }

        public string MatchTime { get; set; }

        public int Hour { get; set; }
        public int Minute { get; set; }

        public string FullOdd { get; set; }

    }
}
