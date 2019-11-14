using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace IddaAnalyser
{
    public class StoreController
    {
        ImportReport ImportReport;

        //FullOddUpdate
        Dictionary<int, string[]> newFullOddIdValues;
        HashSet<int> newMatchIds;
        Dictionary<int, int> fullOddMatchCounts;
        Dictionary<int, HashSet<Result>> fullOddResults;
        Dictionary<int, string[]> allFullOddValues;
        Dictionary<int, HashSet<int>> updatedFullOddOddCombIds;
        
        //OddCombUpdate
        Dictionary<string, int> oddCombValueIds;
        Dictionary<string, HashSet<Result>> processedOddCombResults;
        Dictionary<string, SortedSet<int>> processedOddCombFullOddIds;
        Dictionary<string, HashSet<Result>> newOddCombResults;
        Dictionary<string, SortedSet<int>> newOddCombFullOddIds;
        HashSet<int> removedIds;
        HashSet<string> updatedOddCombvalues;

        List<OddCombination> newOddCombinations;

        //-----
        List<PartialOddType> partialOddTypes = Enum.GetValues(typeof(PartialOddType)).Cast<PartialOddType>().ToList();
        private void PartialOddUpdateControls(BackgroundWorker backgroundWorker)
        {
            SetStoreStatus("Partial Odd Controls");
            using (var db = new MatchModel())
            {
                Dictionary<string, int> currentPartialOddValueIds = new Dictionary<string, int>();  
                Dictionary<PartialOddType, Dictionary<string, SortedSet<int>>> currentPartialOdds = new Dictionary<PartialOddType, Dictionary<string, SortedSet<int>>>();
                Dictionary<PartialOddType, Dictionary<string, SortedSet<int>>> newPartialOdds = new Dictionary<PartialOddType, Dictionary<string, SortedSet<int>>>();
                HashSet<int> updatedIds = new HashSet<int>();

                List<PartialOdd> dbPartialOdds = db.PartialOdds.ToList();
                
                foreach (var partialOddType in partialOddTypes)
                {
                    currentPartialOdds.Add(partialOddType, new Dictionary<string, SortedSet<int>>());
                    newPartialOdds.Add(partialOddType, new Dictionary<string, SortedSet<int>>());
                }
                    
                foreach (var dbPartialOdd in dbPartialOdds)
                {
                    List<string> strFullOddIds = dbPartialOdd.FullOddIds.Split('|').ToList();
                    List<int> intFullOddIDs = new List<int>();
                    foreach (var item in strFullOddIds)
                        intFullOddIDs.Add(int.Parse(item));

                    currentPartialOddValueIds.Add(dbPartialOdd.OddValues,dbPartialOdd.PartialOddId);
                    currentPartialOdds[(PartialOddType)dbPartialOdd.PartialOddType].Add(dbPartialOdd.OddValues,new SortedSet<int>(intFullOddIDs));
                }
                backgroundWorker.ReportProgress(35);
                List<FullOdd> newFullOdds = db.FullOdds.AsParallel().Where(x => newFullOddIdValues.Keys.Contains(x.FullOddId)).ToList();
                foreach (var newFullOdd in newFullOdds)
                {
                    foreach (var tempPartialOdd in GetTempPartialOdds(newFullOdd.Value))
                    {
                        string str = string.Join("|", tempPartialOdd.Value);
                        if (!currentPartialOdds[tempPartialOdd.Key].ContainsKey(str))
                        {
                            if(!newPartialOdds[tempPartialOdd.Key].ContainsKey(str))
                                newPartialOdds[tempPartialOdd.Key].Add(str,new SortedSet<int>());

                            newPartialOdds[tempPartialOdd.Key][str].Add(newFullOdd.FullOddId);
                        }
                        else
                        {
                            currentPartialOdds[tempPartialOdd.Key][str].Add(newFullOdd.FullOddId);
                            updatedIds.Add(currentPartialOddValueIds[str]);
                        }
                            
                    }
                }
                backgroundWorker.ReportProgress(40);

                foreach (var uptadedPartialOdd in dbPartialOdds.Where(x => updatedIds.Contains(x.PartialOddId)))
                    uptadedPartialOdd.FullOddIds = string.Join("|", currentPartialOdds[(PartialOddType)uptadedPartialOdd.PartialOddType][uptadedPartialOdd.OddValues]);
                db.SaveChanges();

                List<PartialOdd> newDbPartialOdds = new List<PartialOdd>();
                foreach (var partialOddType in newPartialOdds)
                {
                    int intPartialOddType = (int)partialOddType.Key;
                    foreach (var oddValues in partialOddType.Value)
                        newDbPartialOdds.Add(new PartialOdd(intPartialOddType,oddValues.Key,string.Join("|",oddValues.Value)));
                }
                db.PartialOdds.AddRange(newDbPartialOdds);
                db.SaveChanges();
                ImportReport.NewPartialOddCount = newDbPartialOdds.Count;
                ImportReport.UpdatedPartialOddCount = updatedIds.Count;
                backgroundWorker.ReportProgress(45);
            }
        }
        Dictionary<PartialOddType, List<string>> GetTempPartialOdds(string value)
        {
            Dictionary<PartialOddType, List<string>> tempPartialOdds = new Dictionary<PartialOddType, List<string>>();
            foreach (var partialOddType in partialOddTypes)
                tempPartialOdds.Add(partialOddType, new List<string>());

            List<string> lstFullOdds = value.Split('|').ToList();
            foreach (var lstFullOdd in lstFullOdds)
            {
                if (lstFullOdd.StartsWith("MS1:") || lstFullOdd.StartsWith("MSX:") || lstFullOdd.StartsWith("MS2:"))
                    tempPartialOdds[PartialOddType.ALLMS].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("FH1:") || lstFullOdd.StartsWith("FHX:") || lstFullOdd.StartsWith("FH2:"))
                    tempPartialOdds[PartialOddType.ALLFH].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("H1:") || lstFullOdd.StartsWith("HX:") || lstFullOdd.StartsWith("H2:"))
                    tempPartialOdds[PartialOddType.ALLHND].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("DC1X:") || lstFullOdd.StartsWith("DC12:") || lstFullOdd.StartsWith("DCX2:"))
                    tempPartialOdds[PartialOddType.ALLDC].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("TG01:") || lstFullOdd.StartsWith("TG23:") || lstFullOdd.StartsWith("TG46:") || lstFullOdd.StartsWith("TG7:"))
                    tempPartialOdds[PartialOddType.ALLTG].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("MGEXIST:") || lstFullOdd.StartsWith("MGNOTEXIST:"))
                    tempPartialOdds[PartialOddType.ALLMG].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("DOWNFH15:") || lstFullOdd.StartsWith("UPFH15:"))
                    tempPartialOdds[PartialOddType.FHUPDOWN].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("DOWN15:") || lstFullOdd.StartsWith("UP15:"))
                    tempPartialOdds[PartialOddType.UPDOWN15].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("DOWN25:") || lstFullOdd.StartsWith("UP25:"))
                    tempPartialOdds[PartialOddType.UPDOWN25].Add(lstFullOdd);
                else if (lstFullOdd.StartsWith("DOWN35:") || lstFullOdd.StartsWith("UP35:"))
                    tempPartialOdds[PartialOddType.UPDOWN35].Add(lstFullOdd);
                else
                    tempPartialOdds[PartialOddType.ALLFHMS].Add(lstFullOdd);
            }

            return tempPartialOdds;

        }
        //-----
        private void OddCombinationUpdateControls(BackgroundWorker backgroundWorker)
        {
            SetStoreStatus("Odd Combination Controls");
            FillDbOddCombinationCollections(backgroundWorker);
            FillMatchFullOddCollections(backgroundWorker);
            SetProcessedOddCombinations(backgroundWorker);
            RemoveUselessOddCombinations(backgroundWorker);
            UpdateCurrentOddCombs(backgroundWorker);
            SetNewOddCombinations();
            InsertNewOddCombinations(backgroundWorker);
        }
        private void FillDbOddCombinationCollections(BackgroundWorker backgroundWorker)
        {
            oddCombValueIds = new Dictionary<string, int>();
            SQLiteConnection SQLiteConnection = new SQLiteConnection(EmbedValueController.connectionString);
            SQLiteCommand SQLiteCommand = new SQLiteCommand()
            {
                Connection = SQLiteConnection,
                CommandText = "Select * From OddCombinations"
            };

            SQLiteConnection.Open();
            SQLiteDataReader SQLiteDataReader = SQLiteCommand.ExecuteReader();
            while (SQLiteDataReader.Read())
                oddCombValueIds.Add(SQLiteDataReader["Value"].ToString(), int.Parse(SQLiteDataReader["OddCombinationId"].ToString()));

            SQLiteDataReader.Close();
            SQLiteConnection.Close();
            backgroundWorker.ReportProgress(50);
        }
        private void FillMatchFullOddCollections(BackgroundWorker backgroundWorker)
        {
            allFullOddValues = new Dictionary<int, string[]>();
            fullOddResults = new Dictionary<int, HashSet<Result>>();
            fullOddMatchCounts = new Dictionary<int, int>();
            using (var db = new MatchModel())
            {
                List<FullOdd> dbFullOdds = db.FullOdds.AsNoTracking().ToList();
                foreach (var dbFullOdd in dbFullOdds)
                {
                    allFullOddValues.Add(dbFullOdd.FullOddId, dbFullOdd.Value.Split('|'));
                    fullOddMatchCounts.Add(dbFullOdd.FullOddId,dbFullOdd.Matches.Count);
                    fullOddResults.Add(dbFullOdd.FullOddId, dbFullOdd.Matches.Skip(1)
                            .Aggregate(
                                    new HashSet<Result>(dbFullOdd.Matches.First().MatchResult.GeneralResult.Results),
                                    (h, e) =>
                                    {
                                        h.IntersectWith(e.MatchResult.GeneralResult.Results); return h;
                                    }
                                ));
                }
            }
            backgroundWorker.ReportProgress(60);
        }
        private void SetProcessedOddCombinations(BackgroundWorker backgroundWorker)
        {
            object myLock = new object();
            HashSet<string> tempSimilarOdds = new HashSet<string>();

            Dictionary<string, HashSet<int>> oddFullOddds = new Dictionary<string, HashSet<int>>();

            processedOddCombFullOddIds = new Dictionary<string, SortedSet<int>>();
            processedOddCombResults = new Dictionary<string, HashSet<Result>>();


            foreach (var allFullOddValueItem in allFullOddValues)
            {
                foreach (var strOdd in allFullOddValueItem.Value)
                {
                    if (!oddFullOddds.ContainsKey(strOdd))
                        oddFullOddds.Add(strOdd, new HashSet<int>());

                    oddFullOddds[strOdd].Add(allFullOddValueItem.Key);
                }
            }

            newFullOddIdValues.AsParallel().ForAll(firstFullOdd =>
            {
                allFullOddValues.AsParallel().Where(x => x.Key != firstFullOdd.Key).ForAll(secondFullOdd =>
                {
                    HashSet<string> similarOdds = new HashSet<string>();
                    foreach (var titem in firstFullOdd.Value.Zip(secondFullOdd.Value, (x, y) => new string[] { x, y }).Where(x => x[0] == x[1]))
                        similarOdds.Add(titem[0]);

                    if (similarOdds.Count > 0)
                    {
                        lock (myLock)
                            tempSimilarOdds.Add(string.Join("|", similarOdds));
                    }

                });
            });

            tempSimilarOdds.AsParallel().ForAll(tempSimilarOdd =>
            {
                HashSet<Result> intersectedResults = new HashSet<Result>();
                string[] aTemp = tempSimilarOdd.Split('|');
                SortedSet<int> ids = aTemp.Skip(1)
                            .Aggregate(
                                    new SortedSet<int>(oddFullOddds[aTemp.First()]),
                                    (h, e) =>
                                    {
                                        h.IntersectWith(oddFullOddds[e]); return h;
                                    }
                                );

                if (ids.Count > 0)
                {
                    intersectedResults = ids.Skip(1)
                            .Aggregate(
                                    new HashSet<Result>(fullOddResults[ids.First()]),
                                    (h, e) =>
                                    {
                                        h.IntersectWith(fullOddResults[e]); return h;
                                    }
                                );

                }

                lock (myLock)
                {
                    processedOddCombFullOddIds.Add(tempSimilarOdd, ids);
                    processedOddCombResults.Add(tempSimilarOdd, intersectedResults);
                }
            });
            backgroundWorker.ReportProgress(65);
        }
        private void RemoveUselessOddCombinations(BackgroundWorker backgroundWorker)
        {
            removedIds = new HashSet<int>();
            HashSet<string> currentRemovedValues = new HashSet<string>();


            foreach (var item in processedOddCombResults.Where(x => x.Value.Count == 0 && oddCombValueIds.Keys.Contains(x.Key)))
                removedIds.Add(oddCombValueIds[item.Key]);

            foreach (var item in processedOddCombResults.Where(x => x.Value.Count ==0))
                currentRemovedValues.Add(item.Key);

            foreach (var currentRemovedValue in currentRemovedValues)
            {
                processedOddCombResults.Remove(currentRemovedValue);
                processedOddCombFullOddIds.Remove(currentRemovedValue);
            }

            using (var db = new MatchModel())
            {
                db.OddCombinations.RemoveRange(db.OddCombinations.AsParallel().Where(x => removedIds.Contains(x.OddCombinationId)));
                db.SaveChanges();
            }
            ImportReport.RemovedOddCombinationCount = removedIds.Count;
            backgroundWorker.ReportProgress(70);
        }
        private void UpdateCurrentOddCombs(BackgroundWorker backgroundWorker)
        {
            updatedFullOddOddCombIds = new Dictionary<int, HashSet<int>>();
            updatedOddCombvalues = new HashSet<string>();
            using (var db = new MatchModel())
            {
                List<OddCombination> oddCombs = db.OddCombinations.AsParallel().Where(x => processedOddCombResults.Keys.Contains(x.Value)).ToList();
                int counter = 0;
                foreach (var oddComb in oddCombs)
                {
                    counter++;
                    updatedOddCombvalues.Add(oddComb.Value);
                    int count = 0;
                    foreach (var fullOddId in processedOddCombFullOddIds[oddComb.Value])
                    {
                        count = count + fullOddMatchCounts[fullOddId];
                        if (!updatedFullOddOddCombIds.ContainsKey(fullOddId))
                            updatedFullOddOddCombIds.Add(fullOddId, new HashSet<int>());

                        updatedFullOddOddCombIds[fullOddId].Add(oddComb.OddCombinationId);
                    }
                    oddComb.ResultOrders = string.Join("|", processedOddCombResults[oddComb.Value]);
                    oddComb.FullOddIds = string.Join("|", processedOddCombFullOddIds[oddComb.Value]);
                    oddComb.MatchCount = count;

                    if (counter % 10000 == 0)
                        db.SaveChanges();
                }
                db.SaveChanges();
                ImportReport.UpdatedOddCombinationCount = counter;
                backgroundWorker.ReportProgress(75);
            }

           // try
           // {
           // 
           //     DataTable dataTable = new DataTable();
           //     dataTable.Columns.Add("OddCombinationId");
           //     dataTable.Columns.Add("ResultOrders");
           //     dataTable.Columns.Add("FullOddIds");
           //     dataTable.Columns.Add("MatchCount");
           //     updatedOddCombvalues = new HashSet<string>();
           //     foreach (var item in processedOddCombResults.Where(x => oddCombValueIds.Keys.Contains(x.Key)))
           //     {
           //         updatedOddCombvalues.Add(item.Key);
           //         int id = oddCombValueIds[item.Key];
           //         int count = 0;
           //         foreach (var fullOddId in processedOddCombFullOddIds[item.Key])
           //         {
           //             count = count + fullOddMatchCounts[fullOddId];
           //             if (!updatedFullOddOddCombIds.ContainsKey(fullOddId))
           //                 updatedFullOddOddCombIds.Add(fullOddId, new HashSet<int>());
           // 
           //             updatedFullOddOddCombIds[fullOddId].Add(id);
           //         }
           //         dataTable.Rows.Add(id, string.Join("|", item.Value), string.Join("|", processedOddCombFullOddIds[item.Key]), count);
           //     }
           //     using (SQLiteConnection con = new SQLiteConnection(EmbedValueController.connectionString))
           //     {
           //         using (SQLiteCommand cmd = new SQLiteCommand("Update_OddCombinations"))
           //         {
           //             cmd.CommandType = CommandType.StoredProcedure;
           //             cmd.Connection = con;
           //             cmd.Parameters.AddWithValue("@updateElement", dataTable);
           //             con.Open();
           //             cmd.ExecuteNonQuery();
           //             con.Close();
           //         }
           //     }
           //     ImportReport.UpdatedOddCombinationCount = dataTable.Rows.Count;
           // 
           // }
           // catch (Exception e)
           // {
           //     string sada = e.ToString();
           //     throw;
           // }
            
        }
        private void SetNewOddCombinations()
        {
            newOddCombResults = new Dictionary<string, HashSet<Result>>();
            newOddCombFullOddIds = new Dictionary<string, SortedSet<int>>();

            foreach (var item in processedOddCombResults.Where(x => !updatedOddCombvalues.Contains(x.Key)))
            {
                newOddCombResults.Add(item.Key, item.Value);
                newOddCombFullOddIds.Add(item.Key, processedOddCombFullOddIds[item.Key]);
            }
        }
        private void InsertNewOddCombinations(BackgroundWorker backgroundWorker)
        {
            try
            {
                using (var db = new MatchModel())
                {
                    newOddCombinations = new List<OddCombination>();
                    foreach (var newOddCombResultItem in newOddCombResults)
                    {
                        int count = 0;
                        foreach (var fullOddId in processedOddCombFullOddIds[newOddCombResultItem.Key])
                            count = count + fullOddMatchCounts[fullOddId];

                        newOddCombinations.Add(new OddCombination(newOddCombResultItem.Key, string.Join("|", newOddCombResultItem.Value), string.Join("|", newOddCombFullOddIds[newOddCombResultItem.Key]), count));
                    }

                    db.OddCombinations.AddRange(newOddCombinations);
                    db.SaveChanges();
                    ImportReport.NewOddCombinationCount = newOddCombinations.Count;

                    foreach (var newOddCombination in newOddCombinations)
                    {
                        HashSet<int> ids = new HashSet<int>();
                        foreach (var item in newOddCombination.FullOddIds.Split('|'))
                        {
                            int id = int.Parse(item);

                            if (!updatedFullOddOddCombIds.ContainsKey(id))
                                updatedFullOddOddCombIds.Add(id, new HashSet<int>());

                            updatedFullOddOddCombIds[id].Add(newOddCombination.OddCombinationId);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string str = e.ToString();
                throw;
            }

            backgroundWorker.ReportProgress(80);
        }
        //-----
        private void FullOddUpdateControls(BackgroundWorker backgroundWorker)
        {
            SetStoreStatus("Full Odd Controls");
            try
            {
                using (var db = new MatchModel())
                {
                    List<FullOdd> dbFullOdds = db.FullOdds.AsParallel().ToList();
                    foreach (var dbFullOdd in dbFullOdds)
                    {
                        if (updatedFullOddOddCombIds.Keys.Contains(dbFullOdd.FullOddId))
                            dbFullOdd.OddCombinationIds = string.Join("|", updatedFullOddOddCombIds[dbFullOdd.FullOddId]);
                        else if (dbFullOdd.OddCombinationIds != null && dbFullOdd.OddCombinationIds != "")
                        {
                            SortedSet<int> oddCombIds = new SortedSet<int>();
                            foreach (var item in dbFullOdd.OddCombinationIds.Split('|'))
                                oddCombIds.Add(int.Parse(item));

                            dbFullOdd.OddCombinationIds = string.Join("|", oddCombIds.Except(removedIds));
                        }
                        dbFullOdd.IntersectedResults = string.Join("|", fullOddResults[dbFullOdd.FullOddId]);
                    }
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                string sds = e.ToString();
                throw;
            }

            backgroundWorker.ReportProgress(90);
        }

        public StoreController()
        {

        }
        List<AnalysedMatch> analysedMatches;
        Label lblStoreStatus;
        
        private void SetStoreStatus(string state)
        {
            lblStoreStatus.Invoke((MethodInvoker)delegate { lblStoreStatus.Text = state; });
        }
        public ImportReport StoreMatches(BackgroundWorker backgroundWorker, int day, int month, int year, Label lblStoreStatus)
        {
            this.lblStoreStatus = lblStoreStatus;
            ImportReport = new ImportReport();
            SetAnalysedMatches(day, month, year);
            ResultControls();
            LeagueControls();
            TeamControls();
            DateTimeControls(day,month,year);
            FullOddControls();
            MatchControls(backgroundWorker);
            PartialOddUpdateControls(backgroundWorker);
            OddCombinationUpdateControls(backgroundWorker);
            FullOddUpdateControls(backgroundWorker);
            RemoveAnalysedMatches(day,month,year);
            backgroundWorker.ReportProgress(100);
            return ImportReport;
        }
        private void SetAnalysedMatches(int day, int month, int year)
        {
            using (var db = new MatchModel())
            {
                analysedMatches = new List<AnalysedMatch>(db.AnalysedMatches
                                    .Where(x =>
                                    x.MatchDay == day &&
                                    x.MatchMonth == month &&
                                    x.MatchYear == year &&
                                    x.FhHomeScore != -1 &&
                                    x.FhAwayScore != -1 &&
                                    x.MsHomeScore != -1 &&
                                    x.MsAwayScore != -1 &&
                                    x.FhHomeScore <= x.MsHomeScore &&
                                    x.FhAwayScore <= x.MsAwayScore));
            }
        }
        //-----
        private void ResultControls()
        {
            SetStoreStatus("New Result Controls");
            using (var db = new MatchModel())
            {
                CheckGeneralResults(db, StoreNewMatchResults(db));
            }
        }
        private List<MatchResult> StoreNewMatchResults(MatchModel db)
        {
            List<MatchResult> newMatchResults = new List<MatchResult>();
            List<MatchResult> insertedMatchResults = new List<MatchResult>();
            foreach (var analysedMatch in analysedMatches)
            {
                MatchResult matchResult = new MatchResult
                {
                    MatchHomeScore = analysedMatch.MsHomeScore,
                    MatchAwayScore = analysedMatch.MsAwayScore,
                    FirstHalfHomeScore = analysedMatch.FhHomeScore,
                    FirstHalfAwayScore = analysedMatch.FhAwayScore
                };

                MatchResult newMR = newMatchResults
                    .FirstOrDefault(x =>
                    x.MatchAwayScore == matchResult.MatchAwayScore
                    && x.MatchHomeScore == matchResult.MatchHomeScore
                    && x.FirstHalfHomeScore == matchResult.FirstHalfHomeScore
                    && x.FirstHalfAwayScore == matchResult.FirstHalfAwayScore);

                if (newMR == null)
                    newMatchResults.Add(matchResult);
            }
            foreach (var newMatchResult in newMatchResults)
            {
                MatchResult matchResult = db.MatchResults
                    .FirstOrDefault(x =>
                    x.MatchHomeScore == newMatchResult.MatchHomeScore
                    && x.MatchAwayScore == newMatchResult.MatchAwayScore
                    && x.FirstHalfHomeScore == newMatchResult.FirstHalfHomeScore
                    && x.FirstHalfAwayScore == newMatchResult.FirstHalfAwayScore);

                if (matchResult == null)
                {
                    db.MatchResults.Add(newMatchResult);
                    db.SaveChanges();
                    insertedMatchResults.Add(newMatchResult);
                }
            }
            ImportReport.NewMatchResultCount = newMatchResults.Count;
            return insertedMatchResults;
        }
        private void CheckGeneralResults(MatchModel db, List<MatchResult> newMatchResults)
        {
            int count = 0;
            foreach (var newMatchResult in newMatchResults)
            {
                GeneralResult generalResult = new GeneralResult();
                generalResult.MsResultType = GetMsResultType(newMatchResult.MatchHomeScore, newMatchResult.MatchAwayScore);
                generalResult.FhResultType = GetFhResultType(newMatchResult.FirstHalfHomeScore, newMatchResult.FirstHalfAwayScore);
                generalResult.TgResultType = GetTgResultType(newMatchResult.MatchHomeScore, newMatchResult.MatchAwayScore);
                generalResult.MgResultType = GetMgResultType(newMatchResult.MatchHomeScore, newMatchResult.MatchAwayScore);
                generalResult.FhUpDownResultType = GetUpDownResultType(newMatchResult.FirstHalfHomeScore, newMatchResult.FirstHalfAwayScore, 1.5, SpecialSim.FHUPDOWN);
                generalResult.UpDown15ResultType = GetUpDownResultType(newMatchResult.MatchHomeScore, newMatchResult.MatchAwayScore, 1.5, SpecialSim.UPDOWN15);
                generalResult.UpDown25ResultType = GetUpDownResultType(newMatchResult.MatchHomeScore, newMatchResult.MatchAwayScore, 2.5, SpecialSim.UPDOWN25);
                generalResult.UpDown35ResultType = GetUpDownResultType(newMatchResult.MatchHomeScore, newMatchResult.MatchAwayScore, 3.5, SpecialSim.UPDOWN35);
                generalResult.FhMsResultType = GetFhMsResultType(generalResult.MsResultType, generalResult.FhResultType);
                generalResult.FirstDcResultType = GetFirstDcResultType((Result)generalResult.MsResultType);
                generalResult.SecondDcResultType = GetSecondDcResultType((Result)generalResult.MsResultType);
                generalResult.FirstFhDcResultType = GetFirstFhDcResultType((Result)generalResult.FhResultType);
                generalResult.SecondFhDcResultType = GetSecondFhDcResultType((Result)generalResult.FhResultType);

                if (db.GeneralResults
                    .FirstOrDefault(
                    x => x.FhMsResultType == generalResult.FhMsResultType
                    && x.TgResultType == generalResult.TgResultType
                    && x.UpDown35ResultType == generalResult.UpDown35ResultType
                    && x.UpDown25ResultType == generalResult.UpDown25ResultType
                    && x.UpDown15ResultType == generalResult.UpDown15ResultType
                    && x.FhUpDownResultType == generalResult.FhUpDownResultType
                    && x.FhResultType == generalResult.FhResultType
                    && x.MsResultType == generalResult.MsResultType
                    && x.MgResultType == generalResult.MgResultType) == null)
                {
                    count++;
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
            ImportReport.NewGeneralResultCount = count;
        }
        private int GetMsResultType(int msHomeScore, int msAwayScore)
        {
            if (msHomeScore > msAwayScore)
                return (int)Result.MS1;
            else if (msHomeScore == msAwayScore)
                return (int)Result.MSX;
            else if (msHomeScore < msAwayScore)
                return (int)Result.MS2;
            else return -1;
        }
        private int GetFhResultType(int fhHomeScore, int fhAwayScore)
        {
            if (fhHomeScore > fhAwayScore)
                return (int)Result.FH1;
            else if (fhHomeScore == fhAwayScore)
                return (int)Result.FHX;
            else if (fhHomeScore < fhAwayScore)
                return (int)Result.FH2;
            else return -1;
        }
        private int GetMgResultType(int msHomeScore, int msAwayScore)
        {
            if (msHomeScore > 0 && msAwayScore > 0)
                return (int)Result.MGEXIST;
            else
                return (int)Result.MGNOTEXIST;
        }
        private int GetUpDownResultType(int homeScore, int awayScore, double limit, SpecialSim selectedSim)
        {
            if (homeScore + awayScore > limit)
            {
                if (selectedSim == SpecialSim.FHUPDOWN)
                    return (int)Result.UPFH15;
                else if (selectedSim == SpecialSim.UPDOWN15)
                    return (int)Result.UP15;
                else if (selectedSim == SpecialSim.UPDOWN25)
                    return (int)Result.UP25;
                else if (selectedSim == SpecialSim.UPDOWN35)
                    return (int)Result.UP35;
                else return -1;
            }
            else
            {
                if (selectedSim == SpecialSim.FHUPDOWN)
                    return (int)Result.DOWNFH15;
                else if (selectedSim == SpecialSim.UPDOWN15)
                    return (int)Result.DOWN15;
                else if (selectedSim == SpecialSim.UPDOWN25)
                    return (int)Result.DOWN25;
                else if (selectedSim == SpecialSim.UPDOWN35)
                    return (int)Result.DOWN35;
                else return -1;
            }
        }
        private int GetTgResultType(int msHomeScore, int msAwayScore)
        {
            if (msHomeScore + msAwayScore < 2)
                return (int)Result.TG01;
            else if (msHomeScore + msAwayScore < 4)
                return (int)Result.TG23;
            else if (msHomeScore + msAwayScore < 7)
                return (int)Result.TG46;
            else
                return (int)Result.TG7;
        }
        private int GetFhMsResultType(int MsResultType, int FhResultType)
        {
            if (FhResultType == (int)Result.FH1 && MsResultType == (int)Result.MS1)
                return (int)Result.FH1MS1;
            else if (FhResultType == (int)Result.FHX && MsResultType == (int)Result.MS1)
                return (int)Result.FHXMS1;
            else if (FhResultType == (int)Result.FH2 && MsResultType == (int)Result.MS1)
                return (int)Result.FH2MS1;
            else if (FhResultType == (int)Result.FH1 && MsResultType == (int)Result.MSX)
                return (int)Result.FH1MSX;
            else if (FhResultType == (int)Result.FHX && MsResultType == (int)Result.MSX)
                return (int)Result.FHXMSX;
            else if (FhResultType == (int)Result.FH2 && MsResultType == (int)Result.MSX)
                return (int)Result.FH2MSX;
            else if (FhResultType == (int)Result.FH1 && MsResultType == (int)Result.MS2)
                return (int)Result.FH1MS2;
            else if (FhResultType == (int)Result.FHX && MsResultType == (int)Result.MS2)
                return (int)Result.FHXMS2;
            else if (FhResultType == (int)Result.FH2 && MsResultType == (int)Result.MS2)
                return (int)Result.FH2MS2;
            else return -1;
        }
        private int GetFirstDcResultType(Result result)
        {
            switch (result)
            {
                case Result.MS1:
                    return (int)Result.DC1X;
                case Result.MSX:
                    return (int)Result.DC1X;
                case Result.MS2:
                    return (int)Result.DC12;
                default: return -1;
            }
        }
        private int GetFirstFhDcResultType(Result result)
        {
            switch (result)
            {
                case Result.FH1:
                    return (int)Result.FHDC1X;
                case Result.FHX:
                    return (int)Result.FHDC1X;
                case Result.FH2:
                    return (int)Result.FHDC12;
                default: return -1;
            }
        }
        private int GetSecondFhDcResultType(Result result)
        {
            switch (result)
            {
                case Result.FH1:
                    return (int)Result.FHDC12;
                case Result.FHX:
                    return (int)Result.FHDCX2;
                case Result.FH2:
                    return (int)Result.FHDCX2;
                default: return -1;
            }
        }
        private int GetSecondDcResultType(Result result)
        {
            switch (result)
            {
                case Result.MS1:
                    return (int)Result.DC12;
                case Result.MSX:
                    return (int)Result.DCX2;
                case Result.MS2:
                    return (int)Result.DCX2;
                default: return -1;
            }
        }
        //-----
        private void LeagueControls()
        {
            SetStoreStatus("New League Controls");
            using (var db = new MatchModel())
            {
                HashSet<string> currentLeagueNames = new HashSet<string>(analysedMatches.Select(x => x.LeagueName).ToList());
                List<string> dbLeagueNames = db.Leagues.Select(x => x.LeagueName).ToList();
                HashSet<string> newLeagueNames = new HashSet<string>(currentLeagueNames.Except(dbLeagueNames));
                List<League> newLeagues = new List<League>();
                foreach (var newLeagueName in newLeagueNames)
                    newLeagues.Add(new League(newLeagueName));

                db.Leagues.AddRange(newLeagues);
                db.SaveChanges();
                ImportReport.NewLeagueCount = newLeagues.Count;
            }
        }
        private void TeamControls()
        {
            SetStoreStatus("New Team Controls");
            HashSet<string> teamNames = new HashSet<string>(analysedMatches.Select(x => x.HomeTeamName).ToList());
            teamNames.UnionWith(analysedMatches.Select(x => x.AwayTeamName).ToList());

            Dictionary<string, string> adaptedTeamNames = new Dictionary<string, string>();

            foreach (var teamName in teamNames)
            {
                string adaptedTeamName = teamName.Replace(" ", "").ToLower();
                adaptedTeamNames.Add(teamName,adaptedTeamName);
            }


            using (var db = new MatchModel())
            {
                List<string> dbTeamNames = db.Teams.Select(x => x.AdaptedTeamName).ToList();
                List<string> newTeamNames = adaptedTeamNames.Values.Except(dbTeamNames).ToList();
                List<Team> newTeams = new List<Team>();
                foreach (var newAdaptedTeamName in newTeamNames)
                {
                    string teamName = adaptedTeamNames.First(x => x.Value == newAdaptedTeamName).Key;
                    newTeams.Add(new Team(teamName, newAdaptedTeamName));
                }
                db.Teams.AddRange(newTeams);
                db.SaveChanges();
                ImportReport.NewTeamCount = newTeamNames.Count;
            }
        }
        private void DateTimeControls(int day, int month,int year)
        {
            SetStoreStatus("Date/Time Controls");
            using (var db = new MatchModel())
            {
                Date date = db.Dates.FirstOrDefault(x => x.Day == day && x.Year == year && x.Month == month);
                if(date == null)
                {
                    db.Dates.Add(new Date(day, month, year));
                    db.SaveChanges();
                }
                HashSet<string> times = new HashSet<string>(analysedMatches.Select(x => x.MatchTime).ToList());
                List<string> dbTimes = db.Times.Select(x => x.StrTime).ToList();
                List<string> newTimes = times.Except(dbTimes).ToList();

                if(newTimes.Count > 0)
                {
                    foreach (var newTime in newTimes)
                        db.Times.Add(new Time(newTime));

                    db.SaveChanges();
                }
            }
            ImportReport.ImportDate = day + "." + month + "." + year;
        }
        private void FullOddControls()
        {
            SetStoreStatus("FullOdd Controls");
            List<FullOdd> newFullOdds = new List<FullOdd>();
            using (var db = new MatchModel())
            {
                List<string> newOddValues = analysedMatches.Select(x => x.FullOdd).Except(db.FullOdds.AsParallel().Select(x => x.Value)).ToList();
                foreach (var newOddValue in newOddValues)
                    newFullOdds.Add(new FullOdd(newOddValue));

                db.FullOdds.AddRange(newFullOdds);
                db.SaveChanges();
            }
            newFullOddIdValues = new Dictionary<int, string[]>();
            foreach (var newFullOdd in newFullOdds)
                newFullOddIdValues.Add(newFullOdd.FullOddId,newFullOdd.Value.Split('|'));

            ImportReport.NewFullOddCount = newFullOdds.Count;
        }        
        private void MatchControls(BackgroundWorker backgroundWorker)
        {
            List<Match> addedMatches = new List<Match>();
            using (var db = new MatchModel())
            {
                db.Database.CommandTimeout = Int32.MaxValue;
                int val = 0;
                List<FullOdd> dbFullOdds = db.FullOdds.ToList();
                List<MatchResult> dbMatchResults = db.MatchResults.ToList();
                List<Team> dbTeams = db.Teams.ToList();
                List<League> dbLeagues = db.Leagues.ToList();
                List<Date> dates = db.Dates.ToList();
                List<Time> dbTimes = db.Times.ToList();
                string adaptedHomeTeamName;
                string adaptedAwayTeamName;
                int counter = 0;
                foreach (var analysedMatch in analysedMatches)
                {
                    SetStoreStatus("Storing : " + analysedMatch.HomeTeamName + "-" + analysedMatch.AwayTeamName);
                    FullOdd fullOdd = dbFullOdds.First(x => x.Value==analysedMatch.FullOdd);
                    League league = dbLeagues.First(x => x.LeagueName == analysedMatch.LeagueName);
                    adaptedHomeTeamName = analysedMatch.HomeTeamName.Replace(" ", "").ToLower();
                    adaptedAwayTeamName = analysedMatch.AwayTeamName.Replace(" ", "").ToLower();
                    Team homeTeam = dbTeams.First(x => x.AdaptedTeamName==adaptedHomeTeamName);
                    Team awayTeam = dbTeams.First(x => x.AdaptedTeamName==adaptedAwayTeamName);
                    Time time = dbTimes.First(x => x.StrTime==analysedMatch.MatchTime);
                    MatchResult matchResult = dbMatchResults
                        .First(x =>
                        x.FirstHalfHomeScore == analysedMatch.FhHomeScore &&
                        x.FirstHalfAwayScore == analysedMatch.FhAwayScore &&
                        x.MatchHomeScore == analysedMatch.MsHomeScore &&
                        x.MatchAwayScore == analysedMatch.MsAwayScore);
                    Date date = dates.First(x => x.Day == analysedMatch.MatchDay && x.Month == analysedMatch.MatchMonth && x.Year == analysedMatch.MatchYear);
                    Match match = new Match();
                    match.SetMatch(date, time, homeTeam, awayTeam, league, matchResult, fullOdd, analysedMatch.MatchCode);
                    val = 10 + ((20 * ++counter) / analysedMatches.Count);
                    addedMatches.Add(match);
                    backgroundWorker.ReportProgress(val);
                }
                db.SaveChanges();
            }
            newMatchIds = new HashSet<int>(addedMatches.Select(x => x.MatchId));
            ImportReport.NewMatchCount = newMatchIds.Count; 
        }
        private void RemoveAnalysedMatches(int day, int month, int year)
        {
            SetStoreStatus("Removing Analysed Matches");
            using (var db = new MatchModel())
            {
                db.AnalysedMatches.RemoveRange(db.AnalysedMatches.Where(x => x.MatchDay == day && x.MatchMonth == month && x.MatchYear == year));
                db.SaveChanges();
            }
        }
    }
}
