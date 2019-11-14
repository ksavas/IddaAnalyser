using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class UsefulMethods
    {
        public UsefulMethods()
        {

        }

        //-----

        //HashSet<string> leagueNames = new HashSet<string>();
        //HashSet<string> matchTimes = new HashSet<string>();
        //HashSet<string> TeamNames = new HashSet<string>();
        //HashSet<string> odds = new HashSet<string>();
        //List<League> dbLeagues;
        //List<Odds> dbOdds;
        //List<Team> dbTeams;
        //List<Time> dbTimes;
        //List<MatchResult> dbMatchResults;
        //List<Match> newMatches;
        //Dictionary<string, HashSet<string>> excelMatchResults = new Dictionary<string, HashSet<string>>();
        //public void CreateLastAnalysedMatches(int rowCount, string fileLocation)
        //{
        //    // using (var db = new MatchModel())
        //    // {
        //    //     ratioNames = new List<RatioType>(db.SelectedResults.OrderBy(x => x.SelectedResultId).Select(c => c.Result).Cast<RatioType>().ToList());
        //    // }
        //
        //    ExcelFileControls(fileLocation, 1);
        //    List<RatioCombination> ratioCombinations = new List<RatioCombination>();
        //    for (int i = 2; i < rowCount + 1; i++)
        //        SetDbObjects(i);
        //
        //    InsertDbObjects();
        //    newMatches = new List<Match>();
        //    using (var db = new MatchModel())
        //    {
        //        dbLeagues = db.Leagues.ToList();
        //        dbOdds = db.Odds.ToList();
        //        dbTeams = db.Teams.ToList();
        //        dbTimes = db.Times.ToList();
        //        dbMatchResults = db.MatchResults.ToList();
        //        for (int i = 2; i < rowCount + 1; i++)
        //            CreateMatches(i, db);
        //
        //        db.SaveChanges();
        //    }
        //
        //    excel.Quit();
        //}
        //private void InsertDbObjects()
        //{
        //    using (var db = new MatchModel())
        //    {
        //        Odds dbOdds;
        //        List<Odds> newOdds = new List<Odds>();
        //        foreach (var item in odds)
        //        {
        //            dbOdds = new Odds();
        //            dbOdds.StringOdds = item;
        //            newOdds.Add(dbOdds);
        //        }
        //        db.Odds.AddRange(newOdds);
        //        db.SaveChanges();
        //        Team team;
        //        List<Team> newTeams = new List<Team>();
        //        foreach (var TeamName in TeamNames)
        //        {
        //            team = new Team();
        //            team.TeamName = TeamName;
        //            team.AdaptedTeamName = TeamName.Replace(" ", "").ToLower();
        //            newTeams.Add(team);
        //        }
        //        db.Teams.AddRange(newTeams);
        //        db.SaveChanges();
        //
        //        League league;
        //        foreach (var item in leagueNames)
        //        {
        //            string name = item.Replace(" ", "").ToLower();
        //            name = name.Trim();
        //            if (db.Leagues.FirstOrDefault(x => x.LeagueName.Equals(name)) == null)
        //            {
        //                league = new League();
        //                league.LeagueName = name;
        //                db.Leagues.Add(league);
        //                db.SaveChanges();
        //            }
        //        }
        //        db.SaveChanges();
        //        Time time;
        //        List<Time> newTimes = new List<Time>();
        //        foreach (var item in matchTimes)
        //        {
        //            time = new Time();
        //            time.StrTime = item;
        //            newTimes.Add(time);
        //        }
        //        db.Times.AddRange(newTimes);
        //        db.SaveChanges();
        //
        //        MatchResult matchResult;
        //        foreach (var fhItem in excelMatchResults)
        //        {
        //            string[] fh = fhItem.Key.Split('-');
        //            int fhHome = int.Parse(fh[0]);
        //            int fhAway = int.Parse(fh[1]);
        //            foreach (var msItem in fhItem.Value)
        //            {
        //                string[] ms = msItem.Split('-');
        //                int msHome = int.Parse(ms[0]);
        //                int msAway = int.Parse(ms[1]);
        //                if (db.MatchResults.FirstOrDefault(x => x.FirstHalfHomeScore == fhHome && x.FirstHalfAwayScore == fhAway && x.MatchHomeScore == msHome && x.MatchAwayScore == msAway) == null)
        //                {
        //                    matchResult = new MatchResult();
        //                    matchResult.FirstHalfHomeScore = fhHome;
        //                    matchResult.FirstHalfAwayScore = fhAway;
        //                    matchResult.MatchHomeScore = msHome;
        //                    matchResult.MatchAwayScore = msAway;
        //                    db.MatchResults.Add(matchResult);
        //                    db.SaveChanges();
        //                    CheckGeneralResults(db, matchResult);
        //                }
        //            }
        //        }
        //
        //    }
        //}
        //private void CheckGeneralResults(MatchModel db, MatchResult newMatchResult)
        //{
        //    int MsResultType = 0;
        //    int FhResultType = 0;
        //    int MgResultType = 0;
        //    int TgResultType = 0;
        //    int FhMsResultType = 0;
        //    int FhUpDownResultType = 0;
        //    int UpDown15ResultType = 0;
        //    int UpDown25ResultType = 0;
        //    int UpDown35ResultType = 0;
        //
        //    if (newMatchResult.MatchHomeScore > newMatchResult.MatchAwayScore)
        //        MsResultType = (int)RatioType.MS1;
        //    else if (newMatchResult.MatchHomeScore == newMatchResult.MatchAwayScore)
        //        MsResultType = (int)RatioType.MSX;
        //    else if (newMatchResult.MatchHomeScore < newMatchResult.MatchAwayScore)
        //        MsResultType = (int)RatioType.MS2;
        //
        //    if (newMatchResult.FirstHalfHomeScore > newMatchResult.FirstHalfAwayScore)
        //        FhResultType = (int)RatioType.FH1;
        //    else if (newMatchResult.FirstHalfHomeScore == newMatchResult.FirstHalfAwayScore)
        //        FhResultType = (int)RatioType.FHX;
        //    else if (newMatchResult.FirstHalfHomeScore < newMatchResult.FirstHalfAwayScore)
        //        FhResultType = (int)RatioType.FH2;
        //
        //    if (newMatchResult.MatchHomeScore == 0 || newMatchResult.MatchAwayScore == 0)
        //        MgResultType = (int)RatioType.MGNOTEXIST;
        //    else
        //        MgResultType = (int)RatioType.MGEXIST;
        //
        //    if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 1.5)
        //        UpDown15ResultType = (int)RatioType.DOWN15;
        //    else
        //        UpDown15ResultType = (int)RatioType.UP15;
        //
        //    if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 2.5)
        //        UpDown25ResultType = (int)RatioType.DOWN25;
        //    else
        //        UpDown25ResultType = (int)RatioType.UP25;
        //
        //    if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 3.5)
        //        UpDown35ResultType = (int)RatioType.DOWN35;
        //    else
        //        UpDown35ResultType = (int)RatioType.UP35;
        //
        //    if (newMatchResult.FirstHalfHomeScore + newMatchResult.FirstHalfAwayScore < 1.5)
        //        FhUpDownResultType = (int)RatioType.DOWNFH15;
        //    else
        //        FhUpDownResultType = (int)RatioType.UPFH15;
        //
        //    if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 2)
        //        TgResultType = (int)RatioType.TG01;
        //    else if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 4)
        //        TgResultType = (int)RatioType.TG23;
        //    else if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 7)
        //        TgResultType = (int)RatioType.TG46;
        //    else
        //        TgResultType = (int)RatioType.TG7;
        //
        //    if (FhResultType == (int)RatioType.FH1 && MsResultType == (int)RatioType.MS1)
        //        FhMsResultType = (int)RatioType.FH1MS1;
        //    else if (FhResultType == (int)RatioType.FHX && MsResultType == (int)RatioType.MS1)
        //        FhMsResultType = (int)RatioType.FHXMS1;
        //    else if (FhResultType == (int)RatioType.FH2 && MsResultType == (int)RatioType.MS1)
        //        FhMsResultType = (int)RatioType.FH2MS1;
        //    else if (FhResultType == (int)RatioType.FH1 && MsResultType == (int)RatioType.MSX)
        //        FhMsResultType = (int)RatioType.FH1MSX;
        //    else if (FhResultType == (int)RatioType.FHX && MsResultType == (int)RatioType.MSX)
        //        FhMsResultType = (int)RatioType.FHXMSX;
        //    else if (FhResultType == (int)RatioType.FH2 && MsResultType == (int)RatioType.MSX)
        //        FhMsResultType = (int)RatioType.FH2MSX;
        //    else if (FhResultType == (int)RatioType.FH1 && MsResultType == (int)RatioType.MS2)
        //        FhMsResultType = (int)RatioType.FH1MS2;
        //    else if (FhResultType == (int)RatioType.FHX && MsResultType == (int)RatioType.MS2)
        //        FhMsResultType = (int)RatioType.FHXMS2;
        //    else if (FhResultType == (int)RatioType.FH2 && MsResultType == (int)RatioType.MS2)
        //        FhMsResultType = (int)RatioType.FH2MS2;
        //
        //
        //    GeneralResult generalResult = db.GeneralResults
        //        .FirstOrDefault(
        //        x => x.FhMsResultType == FhMsResultType
        //        && x.TgResultType == TgResultType
        //        && x.UpDown35ResultType == UpDown35ResultType
        //        && x.UpDown25ResultType == UpDown25ResultType
        //        && x.UpDown15ResultType == UpDown15ResultType
        //        && x.FhUpDownResultType == FhUpDownResultType
        //        && x.FhResultType == FhResultType
        //        && x.MsResultType == MsResultType
        //        && x.MgResultType == MgResultType);
        //
        //    if (generalResult == null)
        //    {
        //        generalResult = new GeneralResult();
        //        generalResult.MsResultType = MsResultType;
        //        generalResult.FhResultType = FhResultType;
        //        generalResult.TgResultType = TgResultType;
        //        generalResult.MgResultType = MgResultType;
        //        generalResult.FhUpDownResultType = FhUpDownResultType;
        //        generalResult.UpDown15ResultType = UpDown15ResultType;
        //        generalResult.UpDown25ResultType = UpDown25ResultType;
        //        generalResult.UpDown35ResultType = UpDown35ResultType;
        //        generalResult.FhMsResultType = FhMsResultType;
        //        generalResult.AddMatchResult(newMatchResult);
        //        db.SaveChanges();
        //    }
        //    else
        //    {
        //        if (!generalResult.MatchResults.Select(x => x.MatchResultId).Contains(newMatchResult.MatchResultId))
        //        {
        //            generalResult.AddMatchResult(newMatchResult);
        //            db.SaveChanges();
        //        }
        //
        //    }
        //
        //}
        //private void SetDbObjects(int index)
        //{
        //    SetMatchTimes(index);
        //    SetLeagues(index);
        //    SetTeams(index);
        //    SetMatchResults(index);
        //    SetOdds(index);
        //}
        //private void SetMatchTimes(int index)
        //{
        //    matchTimes.Add(ExcelRangeValues[index, 1].ToString());
        //}
        //private void SetLeagues(int index)
        //{
        //    leagueNames.Add(ExcelRangeValues[index, 2].ToString());
        //}
        //private void SetMatchResults(int index)
        //{
        //    string fh = ExcelRangeValues[index, 6].ToString().Trim();
        //    string ms = ExcelRangeValues[index, 7].ToString().Trim();
        //
        //    if (!excelMatchResults.Keys.Contains(fh))
        //        excelMatchResults.Add(fh, new HashSet<string>());
        //
        //    excelMatchResults[fh].Add(ms);
        //}
        //private void SetTeams(int index)
        //{
        //    TeamNames.Add(ExcelRangeValues[index, 4].ToString());
        //    TeamNames.Add(ExcelRangeValues[index, 5].ToString());
        //}
        //private void SetOdds(int index)
        //{
        //    List<string> ratioAndValue = new List<string>();
        //    List<string> allRatioValues = new List<string>();
        //    for (int i = 0; i < ratioNames.Count; i++)
        //    {
        //        ratioAndValue.Clear();
        //        ratioAndValue.Add(ratioNames[i].ToString());
        //        ratioAndValue.Add(GetOdds(ratioNames[i], index));
        //        allRatioValues.Add(string.Join(":", ratioAndValue));
        //    }
        //    odds.Add(string.Join("|", allRatioValues));
        //}
        //private void CreateMatches(int index, MatchModel db)
        //{
        //    List<string> ratioAndValue = new List<string>();
        //    List<string> allRatioValues = new List<string>();
        //    for (int i = 0; i < ratioNames.Count; i++)
        //    {
        //        ratioAndValue.Clear();
        //        ratioAndValue.Add(ratioNames[i].ToString());
        //        ratioAndValue.Add(GetOdds(ratioNames[i], index));
        //        allRatioValues.Add(string.Join(":", ratioAndValue));
        //    }
        //    string strOdds = string.Join("|", allRatioValues);
        //    string strTime = ExcelRangeValues[index, 1].ToString();
        //    string leagueName = ExcelRangeValues[index, 2].ToString();
        //    leagueName = leagueName.Trim().ToLower();
        //    string homeTeamName = ExcelRangeValues[index, 4].ToString();
        //    homeTeamName = homeTeamName.Replace(" ", "").ToLower();
        //    string AwayTeamName = ExcelRangeValues[index, 5].ToString();
        //    AwayTeamName = AwayTeamName.Replace(" ", "").ToLower();
        //    string[] fh = ExcelRangeValues[index, 6].ToString().Trim().Split('-');
        //    string[] ms = ExcelRangeValues[index, 7].ToString().Trim().Split('-');
        //    int fhHome = int.Parse(fh[0]);
        //    int fhAway = int.Parse(fh[1]);
        //    int msHome = int.Parse(ms[0]);
        //    int msAway = int.Parse(ms[1]);
        //    Odds odds = dbOdds.First(x => x.StringOdds.Equals(strOdds));
        //    League league = dbLeagues.First(x => x.LeagueName.Equals(leagueName));
        //    Team homeTeam = dbTeams.First(x => x.AdaptedTeamName.Equals(homeTeamName));
        //    Team awayTeam = dbTeams.First(x => x.AdaptedTeamName.Equals(AwayTeamName));
        //    MatchResult matchResult = dbMatchResults.First(x => x.FirstHalfHomeScore == fhHome && x.FirstHalfAwayScore == fhAway && x.MatchHomeScore == msHome && x.MatchAwayScore == msAway);
        //    Time time = dbTimes.First(x => x.StrTime.Equals(strTime));
        //    Date date = db.Dates.First();
        //    Match match = new Match();
        //    match.SetMatch(date, time, homeTeam, awayTeam, league, matchResult, odds);
        //    if (index % 1000 == 0)
        //        db.SaveChanges();
        //}
        //private string GetOdds(RatioType ratioType, int index)
        //{
        //    string returnOdd = "";
        //    switch (ratioType)
        //    {
        //        case RatioType.MS1:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, ms1Col]);
        //            break;
        //        case RatioType.MSX:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, msxCol]);
        //            break;
        //        case RatioType.MS2:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, ms2Col]);
        //            break;
        //        case RatioType.FH1:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fh1Col]);
        //            break;
        //        case RatioType.FHX:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fhxCol]);
        //            break;
        //        case RatioType.FH2:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fh2Col]);
        //            break;
        //        case RatioType.H1:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, h1Col]);
        //            break;
        //        case RatioType.HX:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, hxCol]);
        //            break;
        //        case RatioType.H2:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, h2Col]);
        //            break;
        //        case RatioType.TG01:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, tg01Col]);
        //            break;
        //        case RatioType.TG23:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, tg23Col]);
        //            break;
        //        case RatioType.TG46:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, tg46Col]);
        //            break;
        //        case RatioType.TG7:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, tg7Col]);
        //            break;
        //        case RatioType.DC1X:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, dc1xCol]);
        //            break;
        //        case RatioType.DC12:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, dc12Col]);
        //            break;
        //        case RatioType.DCX2:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, dcx2Col]);
        //            break;
        //        case RatioType.DOWNFH15:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, downFh15Col]);
        //            break;
        //        case RatioType.UPFH15:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, upFh15Col]);
        //            break;
        //        case RatioType.DOWN15:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, down15Col]);
        //            break;
        //        case RatioType.UP15:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, up15Col]);
        //            break;
        //        case RatioType.DOWN25:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, down25Col]);
        //            break;
        //        case RatioType.UP25:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, up25Col]);
        //            break;
        //        case RatioType.DOWN35:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, down35Col]);
        //            break;
        //        case RatioType.UP35:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, up35Col]);
        //            break;
        //        case RatioType.MGEXIST:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, mgExistCol]);
        //            break;
        //        case RatioType.MGNOTEXIST:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, mgNotExistCol]);
        //            break;
        //        case RatioType.FH1MS1:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fh1Ms1Col]);
        //            break;
        //        case RatioType.FH1MSX:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fh1MsxCol]);
        //            break;
        //        case RatioType.FH1MS2:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fh1Ms2Col]);
        //            break;
        //        case RatioType.FHXMS1:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fhxMs1Col]);
        //            break;
        //        case RatioType.FHXMSX:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fhxMsxCol]);
        //            break;
        //        case RatioType.FHXMS2:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fhxMs2Col]);
        //            break;
        //        case RatioType.FH2MS1:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fh2Ms1Col]);
        //            break;
        //        case RatioType.FH2MSX:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fh2MsxCol]);
        //            break;
        //        case RatioType.FH2MS2:
        //            returnOdd = AdaptRatioStr(ExcelRangeValues[index, fh2Ms2Col]);
        //            break;
        //        default:
        //            returnOdd = "";
        //            break;
        //    }
        //    return returnOdd.Replace(" ", "");
        //}
        //-----
        private void StoreResultControl()
        {
            //using (var db = new MatchModel())
            //{
            //    foreach (var resultTypeItem in importResults[analysedMatchId].allResults)
            //    {
            //        foreach (var similarityResultItem in resultTypeItem.Value)
            //        {
            //            Similarity similarity = db.Similarities.First(x => x.SelectedSimilarities == similarityResultItem.Key);
            //            foreach (var selectedResultItem in similarityResultItem.Value)
            //            {
            //                int selectedResult = selectedResultItem;
            //                if (selectedResult == (int)RatioType.MS1 || selectedResult == (int)RatioType.MSX || selectedResult == (int)RatioType.MS2)
            //                {
            //                    if (generalResult.MsResult.Type == (int)selectedResult)
            //                        similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    else
            //                        similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                }
            //                else if (selectedResult == (int)RatioType.FH1 || selectedResult == (int)RatioType.FHX || selectedResult == (int)RatioType.FH2)
            //                {
            //                    if (generalResult.FhResult.Type == (int)selectedResult)
            //                        similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    else
            //                        similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                }
            //                else if (selectedResult == (int)RatioType.TG01 || selectedResult == (int)RatioType.TG23 || selectedResult == (int)RatioType.TG46 || selectedResult == (int)RatioType.TG7)
            //                {
            //                    if (generalResult.TgResult.Type == (int)selectedResult)
            //                        similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    else
            //                        similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                }
            //                else if (selectedResult == (int)RatioType.DC1X || selectedResult == (int)RatioType.DC12 || selectedResult == (int)RatioType.DCX2)
            //                {
            //                    if (selectedResult == (int)RatioType.DC1X)
            //                    {
            //                        if (generalResult.MsResult.Type == (int)RatioType.MS1 || generalResult.MsResult.Type == (int)RatioType.MSX)
            //                            similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                        else
            //                            similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    }
            //                    else if (selectedResult == (int)RatioType.DC12)
            //                    {
            //                        if (generalResult.MsResult.Type == (int)RatioType.MS1 || generalResult.MsResult.Type == (int)RatioType.MS2)
            //                            similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                        else
            //                            similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    }
            //                    else if (selectedResult == (int)RatioType.DCX2)
            //                    {
            //                        if (generalResult.MsResult.Type == (int)RatioType.MSX || generalResult.MsResult.Type == (int)RatioType.MS2)
            //                            similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                        else
            //                            similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    }
            //                }
            //                else if (selectedResult == (int)RatioType.DOWNFH15 || selectedResult == (int)RatioType.UPFH15)
            //                {
            //                    if (generalResult.FhUpDownResult.Type == (int)selectedResult)
            //                        similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    else
            //                        similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                }
            //
            //                else if (selectedResult == (int)RatioType.DOWN15 || selectedResult == (int)RatioType.UP15)
            //                {
            //                    if (generalResult.UpDown15Result.Type == (int)selectedResult)
            //                        similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    else
            //                        similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                }
            //                else if (selectedResult == (int)RatioType.DOWN25 || selectedResult == (int)RatioType.UP25)
            //                {
            //                    if (generalResult.UpDown25Result.Type == (int)selectedResult)
            //                        similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    else
            //                        similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                }
            //                else if (selectedResult == (int)RatioType.DOWN35 || selectedResult == (int)RatioType.UP35)
            //                {
            //                    if (generalResult.UpDown35Result.Type == (int)selectedResult)
            //                        similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    else
            //                        similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                }
            //                else if (selectedResult == (int)RatioType.MGEXIST || selectedResult == (int)RatioType.MGNOTEXIST)
            //                {
            //                    if (generalResult.MgResult.Type == (int)selectedResult)
            //                        similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    else
            //                        similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                }
            //                else if (selectedResult == (int)RatioType.FHDC1X || selectedResult == (int)RatioType.FHDC12 || selectedResult == (int)RatioType.FHDCX2)
            //                {
            //                    if (selectedResult == (int)RatioType.FHDC1X)
            //                    {
            //                        if (generalResult.FhResult.Type == (int)RatioType.FH1 || generalResult.FhResult.Type == (int)RatioType.FHX)
            //                            similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                        else
            //                            similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    }
            //                    else if (selectedResult == (int)RatioType.FHDC12)
            //                    {
            //                        if (generalResult.FhResult.Type == (int)RatioType.FH1 || generalResult.FhResult.Type == (int)RatioType.FH2)
            //                            similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                        else
            //                            similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    }
            //                    else if (selectedResult == (int)RatioType.FHDCX2)
            //                    {
            //                        if (generalResult.FhResult.Type == (int)RatioType.FHX || generalResult.FhResult.Type == (int)RatioType.FH2)
            //                            similarity.IncreseTrues(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                        else
            //                            similarity.IncreseFalses(resultTypeItem.Key, (RatioType)selectedResult, leagueId);
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    db.SaveChanges();
            //}
        }

        private void CheckGeneralResults(MatchModel db, List<MatchResult> newMatchResults)
        {
            int MsResultType = 0;
            int FhResultType = 0;
            int MgResultType = 0;
            int TgResultType = 0;
            int FhMsResultType = 0;
            int FhUpDownResultType = 0;
            int UpDown15ResultType = 0;
            int UpDown25ResultType = 0;
            int UpDown35ResultType = 0;
            foreach (var newMatchResult in newMatchResults)
            {
                if (newMatchResult.MatchHomeScore > newMatchResult.MatchAwayScore)
                    MsResultType = (int)SpecialSim.MS1;
                else if (newMatchResult.MatchHomeScore == newMatchResult.MatchAwayScore)
                    MsResultType = (int)SpecialSim.MSX;
                else if (newMatchResult.MatchHomeScore < newMatchResult.MatchAwayScore)
                    MsResultType = (int)SpecialSim.MS2;

                if (newMatchResult.FirstHalfHomeScore > newMatchResult.FirstHalfAwayScore)
                    FhResultType = (int)SpecialSim.FH1;
                else if (newMatchResult.FirstHalfHomeScore == newMatchResult.FirstHalfAwayScore)
                    FhResultType = (int)SpecialSim.FHX;
                else if (newMatchResult.FirstHalfHomeScore < newMatchResult.FirstHalfAwayScore)
                    FhResultType = (int)SpecialSim.FH2;

                if (newMatchResult.MatchHomeScore == 0 || newMatchResult.MatchAwayScore == 0)
                    MgResultType = (int)SpecialSim.MGNOTEXIST;
                else
                    MgResultType = (int)SpecialSim.MGEXIST;

                if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 1.5)
                    UpDown15ResultType = (int)SpecialSim.DOWN15;
                else
                    UpDown15ResultType = (int)SpecialSim.UP15;

                if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 2.5)
                    UpDown25ResultType = (int)SpecialSim.DOWN25;
                else
                    UpDown25ResultType = (int)SpecialSim.UP25;

                if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 3.5)
                    UpDown35ResultType = (int)SpecialSim.DOWN35;
                else
                    UpDown35ResultType = (int)SpecialSim.UP35;

                if (newMatchResult.FirstHalfHomeScore + newMatchResult.FirstHalfAwayScore < 1.5)
                    FhUpDownResultType = (int)SpecialSim.DOWNFH15;
                else
                    FhUpDownResultType = (int)SpecialSim.UPFH15;

                if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 2)
                    TgResultType = (int)SpecialSim.TG01;
                else if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 4)
                    TgResultType = (int)SpecialSim.TG23;
                else if (newMatchResult.MatchHomeScore + newMatchResult.MatchAwayScore < 7)
                    TgResultType = (int)SpecialSim.TG46;
                else
                    TgResultType = (int)SpecialSim.TG7;

                if (FhResultType == (int)SpecialSim.FH1 && MsResultType == (int)SpecialSim.MS1)
                    FhMsResultType = (int)SpecialSim.FH1MS1;
                else if (FhResultType == (int)SpecialSim.FHX && MsResultType == (int)SpecialSim.MS1)
                    FhMsResultType = (int)SpecialSim.FHXMS1;
                else if (FhResultType == (int)SpecialSim.FH2 && MsResultType == (int)SpecialSim.MS1)
                    FhMsResultType = (int)SpecialSim.FH2MS1;
                else if (FhResultType == (int)SpecialSim.FH1 && MsResultType == (int)SpecialSim.MSX)
                    FhMsResultType = (int)SpecialSim.FH1MSX;
                else if (FhResultType == (int)SpecialSim.FHX && MsResultType == (int)SpecialSim.MSX)
                    FhMsResultType = (int)SpecialSim.FHXMSX;
                else if (FhResultType == (int)SpecialSim.FH2 && MsResultType == (int)SpecialSim.MSX)
                    FhMsResultType = (int)SpecialSim.FH2MSX;
                else if (FhResultType == (int)SpecialSim.FH1 && MsResultType == (int)SpecialSim.MS2)
                    FhMsResultType = (int)SpecialSim.FH1MS2;
                else if (FhResultType == (int)SpecialSim.FHX && MsResultType == (int)SpecialSim.MS2)
                    FhMsResultType = (int)SpecialSim.FHXMS2;
                else if (FhResultType == (int)SpecialSim.FH2 && MsResultType == (int)SpecialSim.MS2)
                    FhMsResultType = (int)SpecialSim.FH2MS2;


                GeneralResult generalResult = db.GeneralResults
                    .FirstOrDefault(
                    x => x.FhMsResultType == FhMsResultType
                    && x.TgResultType == TgResultType
                    && x.UpDown35ResultType == UpDown35ResultType
                    && x.UpDown25ResultType == UpDown25ResultType
                    && x.UpDown15ResultType == UpDown15ResultType
                    && x.FhUpDownResultType == FhUpDownResultType
                    && x.FhResultType == FhResultType
                    && x.MsResultType == MsResultType
                    && x.MgResultType == MgResultType);

                if (generalResult == null)
                {
                    generalResult = new GeneralResult();

                    generalResult.AddMatchResult(newMatchResult);
                    db.SaveChanges();
                }
                else
                {
                    if (!generalResult.MatchResults.Select(x => x.MatchResultId).Contains(newMatchResult.MatchResultId))
                    {
                        generalResult.AddMatchResult(newMatchResult);
                        db.SaveChanges();
                    }

                }
            }
        }


        private void GetUniqsSimilarity()
        {
            //similarities = new HashSet<Similarity>(similarities.GroupBy(x => new {
            //    x.Ms1,
            //    x.Msx,
            //    x.Ms2,
            //    x.Fh1,
            //    x.Fhx,
            //    x.Fh2,
            //    x.Dc1x,
            //    x.Dc12,
            //    x.Dcx2,
            //    x.H1,
            //    x.Hx,
            //    x.H2,
            //    x.Tg01,
            //    x.Tg23,
            //    x.Tg46,
            //    x.Tg7,
            //    x.FhUpDown,
            //    x.DownFh15,
            //    x.UpFh15,
            //    x.UpDown15,
            //    x.Down15,
            //    x.Up15,
            //    x.UpDown25,
            //    x.Down25,
            //    x.Up25,
            //    x.UpDown35,
            //    x.Down35,
            //    x.Up35,
            //    x.MgExist,
            //    x.MgNotExist,
            //    x.Fh1Ms1,
            //    x.Fh1Msx,
            //    x.Fh1Ms2,
            //    x.FhxMs1,
            //    x.FhxMsx,
            //    x.FhxMs2,
            //    x.Fh2Ms1,
            //    x.Fh2Msx,
            //    x.Fh2Ms2,
            //}).Select(group => group.First()));
            //dbSimilarities = db.Similarities.ToList();
        }
        private void InsertDbSelectedResult()
        {
            //using (var db = new MatchModel())
            //{
            //    SelectedResult selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.MS1;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.MSX;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.MS2;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.FH1;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.FHX;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.FH2;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.DC1X;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.DC12;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.DCX2;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.FHDC1X;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.FHDC12;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.FHDCX2;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.MGEXIST;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.MGNOTEXIST;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.TG01;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.TG23;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.TG46;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.TG7;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.DOWNFH15;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.UPFH15;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.DOWN15;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.UP15;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.DOWN25;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.UP25;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.DOWN35;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //    selectedResult = new SelectedResult();
            //    selectedResult.Type = (int)RatioType.UP35;
            //    db.SelectedResults.Add(selectedResult);
            //    db.SaveChanges();
            //}
        }

        private void Compare2Lists()
        {
            //List<Similarity> newSimilarities = allUniqueSimilarities.Where(x => !dbSimilarities.Any(similarity => x.Ms1 == similarity.Ms1
            //    && x.Msx == similarity.Msx
            //    && x.Ms2 == similarity.Ms2
            //    && x.Fh1 == similarity.Fh1
            //    && x.Fhx == similarity.Fhx
            //    && x.Fh2 == similarity.Fh2
            //    && x.Dc1x == similarity.Dc1x
            //    && x.Dc12 == similarity.Dc12
            //    && x.Dcx2 == similarity.Dcx2
            //    && x.H1 == similarity.H1
            //    && x.Hx == similarity.Hx
            //    && x.H2 == similarity.H2
            //    && x.Tg01 == similarity.Tg01
            //    && x.Tg23 == similarity.Tg23
            //    && x.Tg46 == similarity.Tg46
            //    && x.Tg7 == similarity.Tg7
            //    && x.FhUpDown == similarity.FhUpDown
            //    && x.DownFh15 == similarity.DownFh15
            //    && x.UpFh15 == similarity.UpFh15
            //    && x.UpDown15 == similarity.UpDown15
            //    && x.Down15 == similarity.Down15
            //    && x.Up15 == similarity.Up15
            //    && x.UpDown25 == similarity.UpDown25
            //    && x.Down25 == similarity.Down25
            //    && x.Up25 == similarity.Up25
            //    && x.UpDown35 == similarity.UpDown35
            //    && x.Down35 == similarity.Down35
            //    && x.Up35 == similarity.Up35
            //    && x.MgExist == similarity.MgExist
            //    && x.MgNotExist == similarity.MgNotExist
            //    && x.Fh1Ms1 == similarity.Fh1Ms1
            //    && x.FhxMs1 == similarity.FhxMs1
            //    && x.Fh2Ms1 == similarity.Fh2Ms1
            //    && x.Fh1Msx == similarity.Fh1Msx
            //    && x.FhxMsx == similarity.FhxMsx
            //    && x.Fh2Msx == similarity.Fh2Msx
            //    && x.Fh1Ms2 == similarity.Fh1Ms2
            //    && x.FhxMs2 == similarity.FhxMs2
            //    && x.Fh2Ms2 == similarity.Fh2Ms2)).ToList();
        }

        private void CheckCollectiveResults()
        {
            using (var db = new MatchModel())
            {
                int t = 0;
               // var duplicates = db.Similarities
               //     .GroupBy(x => new
               //     {
               //         x.Ms1,
               //         x.Msx,
               //         x.Ms2,
               //         x.Fh1,
               //         x.Fhx,
               //         x.Fh2,
               //         x.Dc1x,
               //         x.Dc12,
               //         x.Dcx2,
               //         x.H1,
               //         x.Hx,
               //         x.H2,
               //         x.Tg01,
               //         x.Tg23,
               //         x.Tg46,
               //         x.Tg7,
               //         x.FhUpDown,
               //         x.DownFh15,
               //         x.UpFh15,
               //         x.UpDown15,
               //         x.Down15,
               //         x.Up15,
               //         x.UpDown25,
               //         x.Down25,
               //         x.Up25,
               //         x.UpDown35,
               //         x.Down35,
               //         x.Up35,
               //         x.MgExist,
               //         x.MgNotExist,
               //         x.Fh1Ms1,
               //         x.Fh1Msx,
               //         x.Fh1Ms2,
               //         x.FhxMs1,
               //         x.FhxMsx,
               //         x.FhxMs2,
               //         x.Fh2Ms1,
               //         x.Fh2Msx,
               //         x.Fh2Ms2,
               //     }).Where(x => x.Count() > 1).Select(x => new { x.Key });
               //
               // t = duplicates.ToList().Count;
             //   var dduplicates = db.CollectiveResults
             //       .GroupBy(x => new
             //       {
             //           x.EarlyGeneralResultSimilarityId,
             //           x.GeneralResultSimilarityId,
             //           x.SameLeagueSimilarityId,
             //           x.GivenResult
             //       }).Where(x => x.Count() > 1).Select(x => new { x.Key });
             //
             //   t = dduplicates.ToList().Count;


               //var ddduplicates = db.LeagueCorrectnesses
               //    .GroupBy(x => new
               //    {
               //        x.LeagueId,
               //        x.CollectiveResultId,
               //    }).Where(x => x.Count() > 1).Select(x => new { x.Key });
               //
               //t = ddduplicates.ToList().Count;
            }
        }


        private void SpecificResults()
        {

            //using (var db = new MatchModel())
            //{
            //    grdCollectiveResult.Rows.Clear();
            //    currentResultSimilarities.Clear();
            //    currentResultSimilarities = currentResults.GetCollectiveResultSimilarities(ratioType,db);
            //    foreach (var resultSimilarity in currentResultSimilarities)
            //    {
            //        int t = -1;
            //        foreach (var ratioSimilarity in resultSimilarity.Value)
            //        {
            //            int totalTrues = 0;
            //            int totalFalses = 0;
            //            int totalSLTrues = 0;
            //            int totalSLFalses = 0;
            //            foreach (var similarity in ratioSimilarity.Value)
            //            {
            //                switch (resultSimilarity.Key)
            //                {
            //                    case ResultType.GeneralRatio:
            //                        foreach (var item in similarity.GeneralCollectiveResults.Where(x => x.GivenResult == (int)ratioType))
            //                        {
            //                            totalTrues += item.GetTotalTrues();
            //                            totalFalses += item.GetTotalFalses();
            //                        }
            //                        break;
            //                    case ResultType.SameLeague:
            //                        foreach (var item in similarity.SameLeagueCollectiveResults.Where(x => x.GivenResult == (int)ratioType))
            //                        {
            //                            //int leagueId = db.Leagues.First(x => x.LeagueName == currentAnalysedMatch.LeagueName).LeagueId;
            //                            totalTrues += item.GetTotalTrues();
            //                            totalFalses += item.GetTotalFalses();
            //                            totalSLTrues += item.GetLeagueTrues(currentResults.leagueId);
            //                            totalSLFalses += item.GetLeagueFalses(currentResults.leagueId);
            //                        }
            //                        break;
            //                    case ResultType.EarlyGeneralRatio:
            //                        foreach (var item in similarity.EarlyGeneralCollectiveResults.Where(x => x.GivenResult == (int)ratioType))
            //                        {
            //                            totalTrues += item.GetTotalTrues();
            //                            totalFalses += item.GetTotalFalses();
            //                        }
            //                        break;
            //                    default:
            //                        break;
            //                }
            //            }
            //            t = grdCollectiveResult.Rows.Add();
            //            grdCollectiveResult.Rows[t].Cells[0].Value = (ratioType).ToString();
            //            grdCollectiveResult.Rows[t].Cells[1].Value = resultSimilarity.Key.ToString();
            //            grdCollectiveResult.Rows[t].Cells[2].Value = ((RatioType)ratioSimilarity.Key).ToString();
            //            grdCollectiveResult.Rows[t].Cells[3].Value = totalTrues.ToString();
            //            grdCollectiveResult.Rows[t].Cells[4].Value = totalFalses.ToString();
            //            grdCollectiveResult.Rows[t].Cells[5].Value = totalSLTrues.ToString();
            //            grdCollectiveResult.Rows[t].Cells[6].Value = totalSLFalses.ToString();
            //        }
            //    }
            //}
        }


        private void TestCodes()
        {



            // using (var db = new MatchModel())
            // {
            //       int t = 0;
            //       var duplicates = db.GeneralResults
            //           .GroupBy(x => new {
            //               x.FhMsResultId,  
            //                 x.FhResultId ,  
            //                 x.FhUpDownResultId ,  
            //                 x.MgResultId ,  
            //                 x.MsResultId ,  
            //                 x.TgResultId ,  
            //                 x.UpDown15ResultId ,  
            //                 x.UpDown25ResultId ,  
            //                 x.UpDown35ResultId 
            //           }).Where(x => x.Count() > 1).Select(x => new { x.Key });
            //
            //       t = duplicates.ToList().Count;
            //
            //     t = db.RatioCombinations.ToList().Count;
            //
            //     var dups = db.Leagues.GroupBy(x => x.LeagueName).Where(x => x.Count()>1).Select(x => new { x.Key });
            //     t = dups.ToList().Count;
            //     var dps = db.MatchResults.GroupBy(x => new{x.FirstHalfAwayScore,x.FirstHalfHomeScore,x.MatchAwayScore,x.MatchHomeScore }).Where(x => x.Count() > 1).Select(x => new { x.Key });
            //     t = dps.ToList().Count;
            //     var d = db.GeneralResults.GroupBy(x => new { x.FhMsResultId,x.FhResultId,x.FhUpDownResultId,x.MgResultId,x.MsResultId,x.TgResultId,x.UpDown15ResultId,x.UpDown25ResultId,x.UpDown35ResultId}).Where(x => x.Count() > 1).Select(x => new { x.Key });
            //     t = d.ToList().Count;
            //     var ds = db.LeagueGeneralResults.GroupBy(x => new { x.RatioCombinationId, x.LeagueId }).Where(x => x.Count() > 1).Select(x => new { x.Key });
            //     t = ds.ToList().Count;
            // }
        }

        private void Checkers()
        {  //
           // using (var db = new MatchModel())
           // {
           //     int t = 0;
           //     var duplicates = db.RatioCombinations
           //         .GroupBy(x => new {
           //             x.Ms1Ratio,
           //             x.MsxRatio,
           //             x.Ms2Ratio,
           //             x.Fh1Ratio,
           //             x.FhxRatio,
           //             x.Fh2Ratio,
           //             x.H1Ratio,
           //             x.HxRatio,
           //             x.H2Ratio,
           //             x.MgExistRatio,
           //             x.MgNotExistRatio,
           //             x.Dc12Ratio,
           //             x.Dc1xRatio,
           //             x.Dcx2Ratio,
           //             x.DownFh15Ratio,
           //             x.UpFh15Ratio,
           //             x.Down15Ratio,
           //             x.Up15Ratio,
           //             x.Down25Ratio,
           //             x.Up25Ratio,
           //             x.Down35Ratio,
           //             x.Up35Ratio,
           //             x.Tg01Ratio,
           //             x.Tg23Ratio,
           //             x.Tg46Ratio,
           //             x.Tg7Ratio,
           //             x.Fh1Ms1Ratio,
           //             x.Fh1MsxRatio,
           //             x.Fh1Ms2Ratio,
           //             x.FhxMs1Ratio,
           //             x.FhxMsxRatio,
           //             x.FhxMs2Ratio,
           //             x.Fh2Ms1Ratio,
           //             x.Fh2MsxRatio,
           //             x.Fh2Ms2Ratio
           //         }).Where(x => x.Count() > 1).Select(x => new { x.Key });
           //
           //     t = duplicates.ToList().Count;
           //
           //     var dups = db.Leagues.GroupBy(x => x.LeagueName).Where(x => x.Count() > 1).Select(x => new { x.Key });
           //     t = dups.ToList().Count;
           //     var dps = db.MatchResults.GroupBy(x => new { x.FirstHalfAwayScore, x.FirstHalfHomeScore, x.MatchAwayScore, x.MatchHomeScore }).Where(x => x.Count() > 1).Select(x => new { x.Key });
           //     t = dps.ToList().Count;
           //     var ds = db.LeagueGeneralResults.GroupBy(x => new { x.RatioCombinationId, x.LeagueId }).Where(x => x.Count() > 1).Select(x => new { x.Key });
           //     t = ds.ToList().Count;
           // }
        }

        private void ProfitMargins()
        {

            //using (var db = new MatchModel())
            //{
            //    List<RatioCombination> ratioCombinations = db.RatioCombinations.ToList();
            //    List<PercentageProfitMargin> ratioCombinations1 = db.PercentageProfitMargins.ToList();
            //  //  db.PercentageProfitMargins.RemoveRange(db.PercentageProfitMargins);
            //  //  db.SaveChanges();
            //    Dictionary<PercentageProfitMargin,HashSet<RatioCombination>> percentageProfitMargins = new Dictionary<PercentageProfitMargin, HashSet<RatioCombination>>();
            //    PercentageProfitMargin percentageProfitMargin;
            //    foreach (var ratioCombination in ratioCombinations)
            //    {
            //        percentageProfitMargin = new PercentageProfitMargin();
            //
            //        percentageProfitMargin.MsProfitMargin =(int) ((100 / ratioCombination.Ms1Ratio) + (100 / ratioCombination.MsxRatio) + (100 / ratioCombination.Ms2Ratio));
            //        percentageProfitMargin.MsProfitMargin = percentageProfitMargin.MsProfitMargin - 100;
            //        percentageProfitMargin.Ms1Percentage = (int) (((100 * (100 / ratioCombination.Ms1Ratio)) / (100+ percentageProfitMargin.MsProfitMargin)));
            //        percentageProfitMargin.MsxPercentage = (int) (((100 * (100 / ratioCombination.MsxRatio)) / (100+ percentageProfitMargin.MsProfitMargin)));
            //        percentageProfitMargin.Ms2Percentage = (int) (((100 * (100 / ratioCombination.Ms2Ratio)) / (100+ percentageProfitMargin.MsProfitMargin)));
            //        percentageProfitMargin.MsProfitMargin = SetProfitMargin(percentageProfitMargin.MsProfitMargin);
            //        percentageProfitMargin.Ms1Percentage = SetProfitMargin(percentageProfitMargin.Ms1Percentage);
            //        percentageProfitMargin.MsxPercentage = SetProfitMargin(percentageProfitMargin.MsxPercentage);
            //        percentageProfitMargin.Ms2Percentage = SetProfitMargin(percentageProfitMargin.Ms2Percentage);
            //
            //        percentageProfitMargin.FhProfitMargin = (int)((100 / ratioCombination.Fh1Ratio) + (100 / ratioCombination.FhxRatio) + (100 / ratioCombination.Fh2Ratio));
            //        percentageProfitMargin.FhProfitMargin = percentageProfitMargin.FhProfitMargin - 100;
            //        percentageProfitMargin.Fh1Percentage = (int) (((100 * (100 / ratioCombination.Fh1Ratio)) / (100+ percentageProfitMargin.FhProfitMargin)));
            //        percentageProfitMargin.FhxPercentage = (int) (((100 * (100 / ratioCombination.FhxRatio)) / (100+ percentageProfitMargin.FhProfitMargin)));
            //        percentageProfitMargin.Fh2Percentage = (int) (((100 * (100 / ratioCombination.Fh2Ratio)) / (100+ percentageProfitMargin.FhProfitMargin)));
            //        percentageProfitMargin.FhProfitMargin = SetProfitMargin(percentageProfitMargin.FhProfitMargin);
            //        percentageProfitMargin.Fh1Percentage = SetProfitMargin(percentageProfitMargin.Fh1Percentage);
            //        percentageProfitMargin.FhxPercentage = SetProfitMargin(percentageProfitMargin.FhxPercentage);
            //        percentageProfitMargin.Fh2Percentage = SetProfitMargin(percentageProfitMargin.Fh2Percentage);
            //
            //        percentageProfitMargin.DcProfitMargin = (int)((100 / ratioCombination.Dc12Ratio) + (100 / ratioCombination.Dc1xRatio) + (100 / ratioCombination.Dcx2Ratio));
            //        percentageProfitMargin.DcProfitMargin = percentageProfitMargin.DcProfitMargin - 100;
            //        percentageProfitMargin.Dc12Percentage = (int) (((100 * (100 / ratioCombination.Dc12Ratio)) / (100+ percentageProfitMargin.DcProfitMargin)));
            //        percentageProfitMargin.Dc1xPercentage = (int) (((100 * (100 / ratioCombination.Dc1xRatio)) / (100+ percentageProfitMargin.DcProfitMargin)));
            //        percentageProfitMargin.Dcx2Percentage = (int) (((100 * (100 / ratioCombination.Dcx2Ratio)) / (100+ percentageProfitMargin.DcProfitMargin)));
            //        percentageProfitMargin.DcProfitMargin = SetProfitMargin(percentageProfitMargin.DcProfitMargin);
            //        percentageProfitMargin.Dc12Percentage = SetProfitMargin(percentageProfitMargin.Dc12Percentage);
            //        percentageProfitMargin.Dc1xPercentage = SetProfitMargin(percentageProfitMargin.Dc1xPercentage);
            //        percentageProfitMargin.Dcx2Percentage = SetProfitMargin(percentageProfitMargin.Dcx2Percentage);
            //
            //
            //        percentageProfitMargin.HndProfitMargin = (int)((100 / ratioCombination.H1Ratio) + (100 / ratioCombination.HxRatio) + (100 / ratioCombination.H2Ratio));
            //        percentageProfitMargin.HndProfitMargin = percentageProfitMargin.HndProfitMargin - 100;
            //        percentageProfitMargin.H1Percentage = (int) (((100 * (100 / ratioCombination.H1Ratio)) / (100+ percentageProfitMargin.HndProfitMargin)));
            //        percentageProfitMargin.HxPercentage = (int) (((100 * (100 / ratioCombination.HxRatio)) / (100+ percentageProfitMargin.HndProfitMargin)));
            //        percentageProfitMargin.H2Percentage = (int) (((100 * (100 / ratioCombination.H2Ratio)) / (100+ percentageProfitMargin.HndProfitMargin)));
            //
            //        percentageProfitMargin.HndProfitMargin = SetProfitMargin(percentageProfitMargin.HndProfitMargin);
            //        percentageProfitMargin.H1Percentage = SetProfitMargin(percentageProfitMargin.H1Percentage);
            //        percentageProfitMargin.HxPercentage = SetProfitMargin(percentageProfitMargin.HxPercentage);
            //        percentageProfitMargin.H2Percentage = SetProfitMargin(percentageProfitMargin.H2Percentage);
            //
            //        percentageProfitMargin.MgProfitMargin = (int)((100 / ratioCombination.MgExistRatio) + (100 / ratioCombination.MgExistRatio));
            //        percentageProfitMargin.MgProfitMargin = percentageProfitMargin.MgProfitMargin - 100;
            //        percentageProfitMargin.MgExistPercentage = (int) (((100 * (100 / ratioCombination.MgExistRatio)) / (100+ percentageProfitMargin.MgProfitMargin)));
            //        percentageProfitMargin.MgNotExistPercentage = (int) (((100 * (100 / ratioCombination.MgNotExistRatio)) / (100+ percentageProfitMargin.MgProfitMargin)));
            //
            //        percentageProfitMargin.MgProfitMargin = SetProfitMargin(percentageProfitMargin.MgProfitMargin);
            //        percentageProfitMargin.MgExistPercentage = SetProfitMargin(percentageProfitMargin.MgExistPercentage);
            //        percentageProfitMargin.MgNotExistPercentage = SetProfitMargin(percentageProfitMargin.MgNotExistPercentage);
            //
            //        percentageProfitMargin.TgProfitMargin = (int)((100 / ratioCombination.Tg01Ratio) + (100 / ratioCombination.Tg23Ratio) + (100 / ratioCombination.Tg46Ratio) + (100 / ratioCombination.Tg7Ratio));
            //        percentageProfitMargin.TgProfitMargin = percentageProfitMargin.TgProfitMargin - 100;
            //        percentageProfitMargin.Tg01Percentage = (int) (((100 * (100 / ratioCombination.Tg01Ratio)) / (100+ percentageProfitMargin.TgProfitMargin)));
            //        percentageProfitMargin.Tg23Percentage = (int) (((100 * (100 / ratioCombination.Tg23Ratio)) / (100+ percentageProfitMargin.TgProfitMargin)));
            //        percentageProfitMargin.Tg46Percentage = (int) (((100 * (100 / ratioCombination.Tg46Ratio)) / (100+ percentageProfitMargin.TgProfitMargin)));
            //        percentageProfitMargin.Tg7Percentage = (int) (((100 * (100 / ratioCombination.Tg7Ratio)) / (100+ percentageProfitMargin.TgProfitMargin)));
            //
            //        percentageProfitMargin.TgProfitMargin = SetProfitMargin(percentageProfitMargin.TgProfitMargin);
            //        percentageProfitMargin.Tg01Percentage = SetProfitMargin(percentageProfitMargin.Tg01Percentage);
            //        percentageProfitMargin.Tg23Percentage = SetProfitMargin(percentageProfitMargin.Tg23Percentage);
            //        percentageProfitMargin.Tg46Percentage = SetProfitMargin(percentageProfitMargin.Tg46Percentage);
            //        percentageProfitMargin.Tg7Percentage = SetProfitMargin(percentageProfitMargin.Tg7Percentage);
            //
            //        percentageProfitMargin.FhUpDownProfitMargin = (int)((100 / ratioCombination.DownFh15Ratio) + (100 / ratioCombination.UpFh15Ratio));
            //        percentageProfitMargin.FhUpDownProfitMargin = percentageProfitMargin.FhUpDownProfitMargin - 100;
            //        percentageProfitMargin.DownFh15Percentage = (int) (((100 * (100 / ratioCombination.DownFh15Ratio)) / (100+ percentageProfitMargin.FhUpDownProfitMargin)));
            //        percentageProfitMargin.UpFh15Percentage = (int) (((100 * (100 / ratioCombination.UpFh15Ratio)) / (100+ percentageProfitMargin.FhUpDownProfitMargin)));
            //
            //
            //        percentageProfitMargin.FhUpDownProfitMargin = SetProfitMargin(percentageProfitMargin.FhUpDownProfitMargin);
            //        percentageProfitMargin.DownFh15Percentage = SetProfitMargin(percentageProfitMargin.DownFh15Percentage);
            //        percentageProfitMargin.UpFh15Percentage = SetProfitMargin(percentageProfitMargin.UpFh15Percentage);
            //
            //        percentageProfitMargin.UpDown15ProfitMargin = (int)((100 / ratioCombination.Down15Ratio) + (100 / ratioCombination.Up15Ratio));
            //        percentageProfitMargin.UpDown15ProfitMargin = percentageProfitMargin.UpDown15ProfitMargin - 100;
            //        percentageProfitMargin.Down15Percentage = (int) (((100 * (100 / ratioCombination.Down15Ratio)) / (100+ percentageProfitMargin.UpDown15ProfitMargin)));
            //        percentageProfitMargin.Up15Percentage = (int) (((100 * (100 / ratioCombination.Up15Ratio)) / (100+ percentageProfitMargin.UpDown15ProfitMargin)));
            //
            //        percentageProfitMargin.UpDown15ProfitMargin = SetProfitMargin(percentageProfitMargin.UpDown15ProfitMargin);
            //        percentageProfitMargin.Down15Percentage = SetProfitMargin(percentageProfitMargin.Down15Percentage);
            //        percentageProfitMargin.Up15Percentage = SetProfitMargin(percentageProfitMargin.Up15Percentage);
            //
            //        percentageProfitMargin.UpDown25ProfitMargin = (int)((100 / ratioCombination.Down25Ratio) + (100 / ratioCombination.Up25Ratio));
            //        percentageProfitMargin.UpDown25ProfitMargin = percentageProfitMargin.UpDown25ProfitMargin - 100;
            //        percentageProfitMargin.Down25Percentage = (int) (((100 * (100 / ratioCombination.Down25Ratio)) / (100+ percentageProfitMargin.UpDown25ProfitMargin)));
            //        percentageProfitMargin.Up25Percentage = (int) (((100 * (100 / ratioCombination.Up25Ratio)) / (100+ percentageProfitMargin.UpDown25ProfitMargin)));
            //
            //        percentageProfitMargin.UpDown25ProfitMargin = SetProfitMargin(percentageProfitMargin.UpDown25ProfitMargin);
            //        percentageProfitMargin.Down25Percentage = SetProfitMargin(percentageProfitMargin.Down25Percentage);
            //        percentageProfitMargin.Up25Percentage = SetProfitMargin(percentageProfitMargin.Up25Percentage);
            //
            //        percentageProfitMargin.UpDown35ProfitMargin = (int)((100 / ratioCombination.Down35Ratio) + (100 / ratioCombination.Up35Ratio));
            //        percentageProfitMargin.UpDown35ProfitMargin = percentageProfitMargin.UpDown35ProfitMargin - 100;
            //        percentageProfitMargin.Down35Percentage = (int) (((100 * (100 / ratioCombination.Down35Ratio)) / (100+ percentageProfitMargin.UpDown35ProfitMargin)));
            //        percentageProfitMargin.Up35Percentage = (int) (((100 * (100 / ratioCombination.Up35Ratio)) / (100+ percentageProfitMargin.UpDown35ProfitMargin)));
            //
            //        percentageProfitMargin.UpDown35ProfitMargin = SetProfitMargin(percentageProfitMargin.UpDown35ProfitMargin);
            //        percentageProfitMargin.Down35Percentage = SetProfitMargin(percentageProfitMargin.Down35Percentage);
            //        percentageProfitMargin.Up35Percentage = SetProfitMargin(percentageProfitMargin.Up35Percentage);
            //
            //        percentageProfitMargin.FhMsProfitMargin = (int)((100 / ratioCombination.Fh1Ms1Ratio) 
            //            + (100 / ratioCombination.Fh1Ms1Ratio)
            //            + (100 / ratioCombination.FhxMs1Ratio)
            //            + (100 / ratioCombination.Fh2Ms1Ratio)
            //            + (100 / ratioCombination.Fh1MsxRatio)
            //            + (100 / ratioCombination.FhxMsxRatio)
            //            + (100 / ratioCombination.Fh2MsxRatio)
            //            + (100 / ratioCombination.Fh1Ms2Ratio)
            //            + (100 / ratioCombination.FhxMs2Ratio)
            //            + (100 / ratioCombination.Fh2Ms2Ratio));
            //        percentageProfitMargin.FhMsProfitMargin = percentageProfitMargin.FhMsProfitMargin - 100;
            //        percentageProfitMargin.Fh1Ms1Percentage = (int) (((100 * (100 / ratioCombination.Fh1Ms1Ratio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //        percentageProfitMargin.FhxMs1Percentage = (int) (((100 * (100 / ratioCombination.FhxMs1Ratio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //        percentageProfitMargin.Fh2Ms1Percentage = (int) (((100 * (100 / ratioCombination.Fh2Ms1Ratio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //        percentageProfitMargin.Fh1MsxPercentage = (int) (((100 * (100 / ratioCombination.Fh1MsxRatio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //        percentageProfitMargin.FhxMsxPercentage = (int) (((100 * (100 / ratioCombination.FhxMsxRatio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //        percentageProfitMargin.Fh2MsxPercentage = (int) (((100 * (100 / ratioCombination.Fh2MsxRatio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //        percentageProfitMargin.Fh1Ms2Percentage = (int) (((100 * (100 / ratioCombination.Fh1Ms2Ratio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //        percentageProfitMargin.FhxMs2Percentage = (int) (((100 * (100 / ratioCombination.FhxMs2Ratio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //        percentageProfitMargin.Fh2Ms2Percentage = (int) (((100 * (100 / ratioCombination.Fh2Ms2Ratio)) / (100+ percentageProfitMargin.FhMsProfitMargin)));
            //
            //        percentageProfitMargin.FhMsProfitMargin = SetProfitMargin(percentageProfitMargin.FhMsProfitMargin);
            //        percentageProfitMargin.Fh1Ms1Percentage =SetProfitMargin(percentageProfitMargin.Fh1Ms1Percentage);
            //        percentageProfitMargin.FhxMs1Percentage = SetProfitMargin(percentageProfitMargin.FhxMs1Percentage);
            //        percentageProfitMargin.Fh2Ms1Percentage = SetProfitMargin(percentageProfitMargin.Fh2Ms1Percentage);
            //        percentageProfitMargin.Fh1MsxPercentage = SetProfitMargin(percentageProfitMargin.Fh1MsxPercentage);
            //        percentageProfitMargin.FhxMsxPercentage =  SetProfitMargin(percentageProfitMargin.FhxMsxPercentage);
            //        percentageProfitMargin.Fh2MsxPercentage =  SetProfitMargin(percentageProfitMargin.Fh2MsxPercentage);
            //        percentageProfitMargin.Fh1Ms2Percentage = SetProfitMargin(percentageProfitMargin.Fh1Ms2Percentage);
            //        percentageProfitMargin.FhxMs2Percentage = SetProfitMargin(percentageProfitMargin.FhxMs2Percentage);
            //        percentageProfitMargin.Fh2Ms2Percentage = SetProfitMargin(percentageProfitMargin.Fh2Ms2Percentage);
            //
            //
            //        PercentageProfitMargin percentageProfit = percentageProfitMargins
            //            .Keys.FirstOrDefault(x => 
            //            x.Ms1Percentage == percentageProfitMargin.Ms1Percentage &&
            //            x.MsxPercentage == percentageProfitMargin.MsxPercentage &&
            //            x.Ms2Percentage == percentageProfitMargin.Ms2Percentage &&
            //            x.MsProfitMargin == percentageProfitMargin.MsProfitMargin &&
            //            x.Fh1Percentage == percentageProfitMargin.Fh1Percentage &&
            //            x.FhxPercentage == percentageProfitMargin.FhxPercentage &&
            //            x.Fh2Percentage == percentageProfitMargin.Fh2Percentage &&
            //            x.FhProfitMargin == percentageProfitMargin.FhProfitMargin &&
            //            x.H1Percentage == percentageProfitMargin.H1Percentage &&
            //            x.HxPercentage == percentageProfitMargin.HxPercentage &&
            //            x.H2Percentage == percentageProfitMargin.H2Percentage &&
            //            x.HndProfitMargin == percentageProfitMargin.HndProfitMargin &&
            //            x.Dc12Percentage == percentageProfitMargin.Dc12Percentage &&
            //            x.Dc1xPercentage == percentageProfitMargin.Dc1xPercentage &&
            //            x.Dcx2Percentage == percentageProfitMargin.Dcx2Percentage &&
            //            x.DcProfitMargin == percentageProfitMargin.DcProfitMargin &&
            //            x.MgExistPercentage == percentageProfitMargin.MgExistPercentage &&
            //            x.MgNotExistPercentage == percentageProfitMargin.MgNotExistPercentage &&
            //            x.MgProfitMargin == percentageProfitMargin.MgProfitMargin &&
            //            x.Tg01Percentage == percentageProfitMargin.Tg01Percentage &&
            //            x.Tg23Percentage == percentageProfitMargin.Tg23Percentage &&
            //            x.Tg46Percentage == percentageProfitMargin.Tg46Percentage &&
            //            x.Tg7Percentage == percentageProfitMargin.Tg7Percentage &&
            //            x.TgProfitMargin == percentageProfitMargin.TgProfitMargin &&
            //            x.DownFh15Percentage == percentageProfitMargin.DownFh15Percentage &&
            //            x.UpFh15Percentage == percentageProfitMargin.UpFh15Percentage &&
            //            x.FhUpDownProfitMargin == percentageProfitMargin.FhUpDownProfitMargin &&
            //            x.Down15Percentage == percentageProfitMargin.Down15Percentage &&
            //            x.Up15Percentage == percentageProfitMargin.Up15Percentage &&
            //            x.UpDown15ProfitMargin == percentageProfitMargin.UpDown15ProfitMargin &&
            //            x.Down25Percentage == percentageProfitMargin.Down25Percentage &&
            //            x.Up25Percentage == percentageProfitMargin.Up25Percentage &&
            //            x.UpDown25ProfitMargin == percentageProfitMargin.UpDown25ProfitMargin &&
            //            x.Down35Percentage == percentageProfitMargin.Down35Percentage &&
            //            x.Up35Percentage == percentageProfitMargin.Up35Percentage &&
            //            x.UpDown35ProfitMargin == percentageProfitMargin.UpDown35ProfitMargin &&
            //            x.Fh1Ms1Percentage == percentageProfitMargin.Fh1Ms1Percentage &&
            //            x.FhxMs1Percentage == percentageProfitMargin.FhxMs1Percentage &&
            //            x.Fh2Ms1Percentage == percentageProfitMargin.Fh2Ms1Percentage &&
            //            x.Fh1MsxPercentage == percentageProfitMargin.Fh1MsxPercentage &&
            //            x.FhxMsxPercentage == percentageProfitMargin.FhxMsxPercentage &&
            //            x.Fh2MsxPercentage == percentageProfitMargin.Fh2MsxPercentage &&
            //            x.Fh1Ms2Percentage == percentageProfitMargin.Fh1Ms2Percentage &&
            //            x.FhxMs2Percentage == percentageProfitMargin.FhxMs2Percentage &&
            //            x.Fh2Ms2Percentage == percentageProfitMargin.Fh2Ms2Percentage &&
            //            x.FhMsProfitMargin == percentageProfitMargin.FhMsProfitMargin);
            //        if(percentageProfit == null)
            //            percentageProfitMargins.Add(percentageProfitMargin, new HashSet<RatioCombination>() { ratioCombination });
            //        else
            //            percentageProfitMargins[percentageProfit].Add(ratioCombination);
            //
            //    }
            //    int i = 0;
            //    foreach (var percentageProfitMarginItem in percentageProfitMargins)
            //    {
            //         i++;
            //        foreach (var ratioCombination in percentageProfitMarginItem.Value)
            //        {
            //            percentageProfitMarginItem.Key.AddRatioCombination(ratioCombination);
            //        }
            //        if(i%1000==0)
            //            db.SaveChanges();
            //    }
            //    db.SaveChanges();
            //}
        }

    }
}
