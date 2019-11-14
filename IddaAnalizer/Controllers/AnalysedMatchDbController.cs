using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace IddaAnalyser
{
    public class AnalysedMatchDbController
    {
        List<AnalysedMatch> analysedMatches;
        public AnalysedMatchDbController(List<AnalysedMatch> analysedMatches)
        {
            this.analysedMatches = analysedMatches;
        }
        public void ApplyMatchOperations(BackgroundWorker backgroundWorker)
        {
            int day = 0;
            int month = 0;
            int year = 0;
            var dailyAnalysedMatches = analysedMatches.GroupBy(x => new { x.MatchDay, x.MatchMonth, x.MatchYear });
            using (var db = new MatchModel())
            {
                foreach (var dateItem in dailyAnalysedMatches)
                {
                    day =(int)dateItem.Key.MatchDay;
                    month =(int)dateItem.Key.MatchMonth;
                    year =(int)dateItem.Key.MatchYear;
                    OldAnalysedMatchControls(db,day,month,year);

                    foreach (var analysedMatch in dateItem)
                        db.AnalysedMatches.Add(analysedMatch);
                }
                db.SaveChanges();
            }
        }
        private void OldAnalysedMatchControls(MatchModel db, int day, int month, int year)
        {
            List<AnalysedMatch> storedMatches = db.AnalysedMatches.Where(x => x.MatchYear == year && x.MatchMonth == month && x.MatchDay == day).ToList();
            if (storedMatches != null)
            {
                if(storedMatches.Count > 0)
                {
                    db.AnalysedMatches.RemoveRange(storedMatches);
                    db.SaveChanges();
                }
            }
        }
    }
}
