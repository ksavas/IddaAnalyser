using System.Collections.Generic;

namespace IddaAnalyser
{
    public class Date
    {
        public virtual ICollection<Match> Matches { get; set; }
        public Date()
        {
        }
        public Date(int day, int month, int year)
        {
            this.Day = day;
            this.Month = month;
            this.Year = year;
        }
        public int DateId { get; set; }

        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

    }
}
