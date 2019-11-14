using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class ImportReport
    {
        public ImportReport()
        {

        }
        public int ImportReportId { get; set; }
        public string Path { get; set; }
        public string ImportDate { get; set; }
        public int NewMatchCount { get; set; }
        public int NewLeagueCount { get; set; }
        public int NewTeamCount { get; set; }
        public int NewFullOddCount { get; set; }
        public int NewMatchResultCount { get; set; }
        public int NewGeneralResultCount { get; set; }
        public int RemovedOddCombinationCount { get; set; }
        public int NewOddCombinationCount { get; set; }
        public int UpdatedOddCombinationCount { get; set; }
        public int NewPartialOddCount { get; set; }
        public int UpdatedPartialOddCount { get; set; }
    }
}
