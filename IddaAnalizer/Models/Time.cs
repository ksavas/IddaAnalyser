using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class Time
    {
        public virtual ICollection<Match> Matches { get; set; }
        public Time()
        {
            Matches = new HashSet<Match>();
        }
        public Time(string strTime):this()
        {
            this.StrTime = strTime;
            string[] aTime = strTime.Split(':');
            this.Hour = int.Parse(aTime[0]);
            this.Minute = int.Parse(aTime[1]);
        }
        public int TimeId { get; set; }
        public string StrTime { get; set; }
        public int Hour { get; set; }
        public int Minute { get; set; }
    }
}
