using System.Collections.Generic;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System;

namespace IddaAnalyser
{
    public class ExcelReader
    {
        string path = "";
        _Application excel = new _Excel.Application();
        protected Workbook wb;
        protected Worksheet ws;
        protected int columnCount;
        object[,] ExcelRangeValues;

        int fhHomeScore;
        int fhAwayScore;

        int msHomeScore;
        int msAwayScore;

        int day = 0;
        int month = 0;
        int year = 0;

        string homeTeamName = "";
        string awayTeamName = "";
        string leagueName = "";
        int matchCode = 0;
        string matchTime = "";
        int matchHour = -1;
        int matchMinute = -1;
        int rowCount = -1;

        int currentRow = -1;

        SortedDictionary<int, Odd> OddItems;

        List<MatchResult> matchResults;
        List<GeneralResult> generalResults;
        List<AnalysedMatch> analysedMatches;
        AnalysedMatch currentAnalysedMatch;

        EmbedValueController embedValueController = EmbedValueController.GetEmbedValueController;
        SortedDictionary<int, int> excelColumns;

        public ExcelReader()
        {
            matchResults = new List<MatchResult>();
            generalResults = new List<GeneralResult>();
            SetExcelColumnNames();
        }

        private void SetExcelColumnNames()
        {
            using (var db = new MatchModel())
            {
                excelColumns = new SortedDictionary<int, int>();

                List<ExcelColumn> dbExcelColumns = db.ExcelColumns.ToList();
                foreach (var dbExcelColumn in dbExcelColumns)
                    excelColumns.Add(dbExcelColumn.ColumntType,dbExcelColumn.ColumntNumber);
            }
        }

        public int GetCurrentRow()
        {
            return this.currentRow;
        }
        public List<AnalysedMatch> GeneralRatioExcelControl(string fileLocation)
        {
            ExcelFileControls(@fileLocation, 2);
            ObjectControls(rowCount);
            GenerateAnalysedMatches();
            excel.Quit();
            return analysedMatches;
        }
        private void ExcelFileControls(string path, int workSheet)
        {
            this.path = path;
            this.wb = excel.Workbooks.Open(path);
            this.ws = wb.Worksheets[workSheet];
            _Excel.Range DataRange = ws.UsedRange;
            this.ExcelRangeValues = (object[,])DataRange.Value2;
            this.columnCount = DataRange.Columns.Count;
        }

        private void ObjectControls(int rowCount)
        {
            analysedMatches = new List<AnalysedMatch>();
            day = 0;
            month = 0;
            year = 0;
            matchTime = "";
            matchHour = -1;
            matchMinute = -1;
            leagueName = "";
            matchCode = 0;
            this.rowCount = ExcelRangeValues.GetLength(0) + 1;
        }
        private void GenerateAnalysedMatches()
        {
            try
            {
                analysedMatches = new List<AnalysedMatch>();
                currentRow = 1;
                while (currentRow < rowCount)
                {
                    if (!AnalysedMatchDateControls())
                    {
                        if (CheckAnalysedMatchConditions())
                        {
                            AnalysedMatchGeneralControls();
                            AnalysedMatchValueControls();
                            analysedMatches.Add(currentAnalysedMatch);
                        }
                    }
                    currentRow++;
                }
            }
            catch(Exception e)
            {
                excel.Quit();
                analysedMatches = new List<AnalysedMatch>();
                return;
            }
            
        }
        private bool AnalysedMatchDateControls()
        {
            if (ExcelRangeValues[currentRow, 1].ToString().Contains("."))
            {
                string[] date = ExcelRangeValues[currentRow, 1].ToString().Split('.');

                day = int.Parse(date[0]);
                month = int.Parse(date[1]);
                year = int.Parse(date[2]);
                return true;
            }
            return false;
        }
        private bool CheckAnalysedMatchConditions()
        {
            if (int.Parse(ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.MatchCode]].ToString()) != int.Parse(ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.FhMsMatchCodeCol]].ToString()))
                return false;

            if (ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.FhScore]] != null && ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.MsScore]] != null)
            {
                if (ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.FhScore]].ToString().Contains('-') && ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.MsScore]].ToString().Contains('-'))
                {
                    string[] fhScores = ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.FhScore]].ToString().Split('-');
                    string[] msScores = ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.MsScore]].ToString().Split('-');
                    if (msScores[0] != "" && msScores[1] != "" && fhScores[0] != "" && fhScores[1] != "")
                    {
                        msHomeScore = int.Parse(msScores[0]);
                        msAwayScore = int.Parse(msScores[1]);
                        fhHomeScore = int.Parse(fhScores[0]);
                        fhAwayScore = int.Parse(fhScores[1]);
                    }
                    else
                    {
                        msHomeScore = -1;
                        msAwayScore = -1;
                        fhHomeScore = -1;
                        fhAwayScore = -1;
                    }
                }
            }
            else
            {
                msHomeScore = -1;
                msAwayScore = -1;
                fhHomeScore = -1;
                fhAwayScore = -1;
            }

            return true;
        }
        private void AnalysedMatchGeneralControls()
        {
            currentAnalysedMatch = new AnalysedMatch(day, month, year);
            AnalysedMatchTimeControls();
            LeagueNameControls();
            MatchCodeTeamNameControls();
        }
        private void AnalysedMatchTimeControls()
        {
            matchTime = ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.MatchTime]].ToString();
            string[] aTime = matchTime.Split(':');
            matchHour = int.Parse(aTime[0]);
            matchMinute = int.Parse(aTime[1]);
        }
        private void LeagueNameControls()
        {
            leagueName = ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.League]].ToString();
            leagueName = leagueName.Trim().Replace(" ", "").ToLower();
        }
        private void MatchCodeTeamNameControls()
        {
            matchCode = int.Parse(ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.MatchCode]].ToString());
            homeTeamName = ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.HomeTeamName]].ToString();
            awayTeamName = ExcelRangeValues[currentRow, excelColumns[(int)ExcelColumnType.AwayTeamName]].ToString();
        }
        private void AnalysedMatchValueControls()
        {
            currentAnalysedMatch.MsHomeScore = msHomeScore;
            currentAnalysedMatch.FhHomeScore = fhHomeScore;
            currentAnalysedMatch.MsAwayScore = msAwayScore;
            currentAnalysedMatch.FhAwayScore = fhAwayScore;

            currentAnalysedMatch.MatchCode = matchCode;
            currentAnalysedMatch.LeagueName = leagueName;
            currentAnalysedMatch.HomeTeamName = homeTeamName;
            currentAnalysedMatch.AwayTeamName = awayTeamName;
            currentAnalysedMatch.MatchTime = matchTime;
            currentAnalysedMatch.Hour = matchHour;
            currentAnalysedMatch.Minute = matchMinute;

            AnalysedMatchOddsControls();
        }
        private void AnalysedMatchOddsControls()
        {
            OddItems = embedValueController.GetNewOddItems;
            foreach (var OddItem in OddItems)
                OddItem.Value.SetOddValue(GetOdds(currentRow, OddItem.Key));

            currentAnalysedMatch.FullOdd = embedValueController.GetStrFullOdds(OddItems);
        }
        private double GetOdds(int currentRow, int currentOdd)
        {
            return AdaptOddStr(ExcelRangeValues[currentRow, excelColumns[currentOdd]]);
        }
        private double AdaptOddStr(object oddObject)
        {
            if (oddObject == null || oddObject.Equals("-"))
                return embedValueController.ConvertOddValueToDouble("-1");

            return embedValueController.ConvertOddValueToDouble(oddObject.ToString());
        }
    }
}

