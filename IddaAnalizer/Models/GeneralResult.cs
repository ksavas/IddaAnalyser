using System.Collections.Generic;
namespace IddaAnalyser
{
    public class GeneralResult
    {
        public virtual ICollection<MatchResult> MatchResults { get; set; }



        public GeneralResult()
        {
            MatchResults = new HashSet<MatchResult>();
        }

        public int GeneralResultId { get; set; }

        public int MsResultType { get; set; }
        public int FhResultType { get; set; }
        public int FhUpDownResultType { get; set; }
        public int UpDown15ResultType { get; set; }
        public int UpDown25ResultType { get; set; }
        public int UpDown35ResultType { get; set; }
        public int TgResultType { get; set; }
        public int FhMsResultType { get; set; }
        public int MgResultType { get; set; }

        public int FirstDcResultType { get; set; }
        public int SecondDcResultType { get; set; }

        public int FirstFhDcResultType { get; set; }
        public int SecondFhDcResultType { get; set; }

        public HashSet<Result> Results
        {
            get
            {
                return GetResults();
            }
        }
        public HashSet<int> testtt
        {
            get
            {
                return new HashSet<int>();
            }
        }
        public HashSet<Result> GetResults()
        {
            return new HashSet<Result>
            {
                (Result)MsResultType,
                (Result)FhResultType,
                (Result)TgResultType,
                (Result)MgResultType,
                (Result)FhUpDownResultType,
                (Result)UpDown15ResultType,
                (Result)UpDown25ResultType,
                (Result)UpDown35ResultType,
                (Result)FirstDcResultType,
                (Result)SecondDcResultType,
                (Result)FirstFhDcResultType,
                (Result)SecondFhDcResultType
            };
        }
        public void AddMatchResult(MatchResult matchResult)
        {
            matchResult.GeneralResult = this;
            matchResult.GeneralResultId = GeneralResultId;
            this.MatchResults.Add(matchResult);
        }

    }
}
