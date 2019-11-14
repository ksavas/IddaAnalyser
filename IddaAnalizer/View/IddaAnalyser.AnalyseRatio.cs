using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;

namespace IddaAnalyser
{
    partial class IddaAnalyser
    {
        Dictionary<AnalyseResultType, DataGridView> dictGridViews;
        List<AnalyseLimitContanier> analyseLimits;

        Dictionary<string, AnalyseLimitContanier> analyseLimitMap;

        AnalysedMatch currentAnalysedMatch;
        EmbedValueController embedValueController = EmbedValueController.GetEmbedValueController;

        HashSet<int> pickedMatchIds;

        List<DataGridView> simGrids = new List<DataGridView>();
        List<AnalysedMatch> analysedMatches = new List<AnalysedMatch>();
        // Analyse collections
        Dictionary<string, List<string>> currentDictPartialOdds;
        Dictionary<string, HashSet<int>> currentDictStrPartialOdds;
        Dictionary<string, Dictionary<int, int>> oddCombResultCounts;
        Dictionary<string, Dictionary<int, int>> partialOddResultCounts;
        Dictionary<int, Dictionary<AnalyseResultType, Dictionary<string, Dictionary<int, int>>>> allResults;
        
        // OddComb collections
        private Dictionary<int, SortedSet<int>> oddCombFullOddIds;
        Dictionary<int, Dictionary<HashSet<string>, int>> oddCombHashIds;
        Dictionary<int, HashSet<string>> oddCombinationIdResults;
        //PartialOdd collections
        List<List<string>> allStrPartialOddPerms;
        Dictionary<string, HashSet<int>> partialOddFullOddIds;
        //FullOdd collections
        Dictionary<int, Dictionary<int, HashSet<string>>> fullOddResults;
        Dictionary<int, int> fullOddMatchCount;
        Dictionary<string, HashSet<int>> fullOddValueOddCombIds;
        // db
        private void DbControlsAsync()
        {
            //DbOddCombinationControls();
            //DbPartialOddPermControls();
            //DbFullOddControls();
            //DbPartialOddControls();
            Task t1 = Task.Factory.StartNew(() => DbOddCombinationControls());
            Task t2 = Task.Factory.StartNew(() => DbPartialOddPermControls());
            Task t3 = Task.Factory.StartNew(() => DbFullOddControls());
            Task t4 = Task.Factory.StartNew(() => DbPartialOddControls());
            Task.WaitAll(t1, t2,t3,t4);
        }
        Dictionary<int, string> oddCombTest;
        Dictionary<int, string> fullOddTest;

        private void DbOddCombinationControls()
        {
            
            SqlConnection SqlConnection = new SqlConnection(EmbedValueController.connectionString);
            SqlCommand SqlCommand = new SqlCommand()
            {
                Connection = SqlConnection,
                CommandText = "Select * From OddCombinations;"
            };
            SqlConnection.Open();
            SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();
            oddCombTest = new Dictionary<int, string>();
            oddCombFullOddIds = new Dictionary<int, SortedSet<int>>();
            oddCombHashIds = new Dictionary<int, Dictionary<HashSet<string>, int>>();
            oddCombinationIdResults = new Dictionary<int, HashSet<string>>();
            while (SqlDataReader.Read())
            {
                int id = int.Parse(SqlDataReader["OddCombinationId"].ToString());
                string value = SqlDataReader["Value"].ToString();
                HashSet<string> lstStrResultOrders = new HashSet<string>(SqlDataReader["ResultOrders"]
                    .ToString()
                    .Split('|'));

                string strFullOdds = SqlDataReader["FullOddIds"].ToString();
                List<string> lstStrFullOddIds = strFullOdds
                    .ToString()
                    .Split('|')
                    .ToList();


                int matchCount = int.Parse(SqlDataReader["MatchCount"].ToString());

                if (!oddCombHashIds.ContainsKey(matchCount))
                    oddCombHashIds.Add(matchCount, new Dictionary<HashSet<string>, int>());

                SortedSet<int> tempFullOddIds = new SortedSet<int>();
                foreach (var tempFullOddId in lstStrFullOddIds)
                    tempFullOddIds.Add(int.Parse(tempFullOddId));

                oddCombTest.Add(id,value);
                oddCombHashIds[matchCount].Add(new HashSet<string>(value.Split('|')), id);
                oddCombinationIdResults.Add(id, lstStrResultOrders);
                oddCombFullOddIds.Add(id, tempFullOddIds);
            }
        }
        private void DbPartialOddPermControls()
        {
            SqlConnection SqlConnection = new SqlConnection(EmbedValueController.connectionString);
            SqlCommand SqlCommand = new SqlCommand()
            {
                Connection = SqlConnection,
                CommandText = " Select * From PartialOddPerms;"
            };
            SqlConnection.Open();
            SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();
            allStrPartialOddPerms = new List<List<string>>();
            while (SqlDataReader.Read())
                allStrPartialOddPerms.Add(SqlDataReader["StrValue"].ToString().Split('|').ToList());
        }
        private void DbFullOddControls()
        {
            SqlConnection SqlConnection = new SqlConnection(EmbedValueController.connectionString);
            SqlCommand SqlCommand = new SqlCommand()
            {
                Connection = SqlConnection,
                CommandText = @" SELECT t.FullOddId,t.Value,t.IntersectedResults,t.OddCombinationIds, COUNT(p.FullOddId) MatchCount
                    FROM FullOdds t LEFT JOIN
                         Matches p ON p.FullOddId = t.FullOddId
                    GROUP BY t.FullOddId,t.Value,t.IntersectedResults,t.OddCombinationIds;"
            };
            SqlConnection.Open();
            SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();
            fullOddTest = new Dictionary<int, string>();
            fullOddResults = new Dictionary<int, Dictionary<int, HashSet<string>>>();
            fullOddMatchCount = new Dictionary<int, int>();
            fullOddValueOddCombIds = new Dictionary<string, HashSet<int>>();
            while (SqlDataReader.Read())
            {
                int id = int.Parse(SqlDataReader["FullOddId"].ToString());
                int matchesCount = int.Parse(SqlDataReader["MatchCount"].ToString());

                string strOddCombinationIds = SqlDataReader["OddCombinationIds"].ToString();
                string value = SqlDataReader["Value"].ToString();

                HashSet<string> hashStrOddCombinationIds = new HashSet<string>(strOddCombinationIds.Split('|'));
                HashSet<string> intersectedResults = new HashSet<string>(SqlDataReader["IntersectedResults"].ToString().Split('|'));

                HashSet<int> oddCombinationIds = new HashSet<int>();
                if (strOddCombinationIds != null && strOddCombinationIds != "")
                    foreach (var strId in hashStrOddCombinationIds)
                        oddCombinationIds.Add(int.Parse(strId));

                if (!fullOddResults.ContainsKey(matchesCount))
                    fullOddResults.Add(matchesCount, new Dictionary<int, HashSet<string>>());

                fullOddTest.Add(id,value);
                fullOddResults[matchesCount].Add(id, intersectedResults);
                fullOddValueOddCombIds.Add(value, oddCombinationIds);
                fullOddMatchCount.Add(id, matchesCount);
            }
        }
        private void DbPartialOddControls()
        {
            SqlConnection SqlConnection = new SqlConnection(EmbedValueController.connectionString);
            SqlCommand SqlCommand = new SqlCommand()
            {
                Connection = SqlConnection,
                CommandText = "Select * From PartialOdds;"
            };
            SqlConnection.Open();
            SqlDataReader SqlDataReader = SqlCommand.ExecuteReader();
            partialOddFullOddIds = new Dictionary<string, HashSet<int>>();
            while (SqlDataReader.Read())
            {
                HashSet<int> tempFullOddIds = new HashSet<int>();
                foreach (var strFullOddId in SqlDataReader["FullOddIds"].ToString().Split('|'))
                    tempFullOddIds.Add(int.Parse(strFullOddId));

                partialOddFullOddIds.Add(SqlDataReader["OddValues"].ToString(), tempFullOddIds);
            }
        }
        private void BtnClearAnalyseRatio_Click(object sender, EventArgs e)
        {
            AnalyseRatioInit();
        }
        private void AnalyseRatioInit()
        {
            allResults = new Dictionary<int, Dictionary<AnalyseResultType, Dictionary<string, Dictionary<int, int>>>>();
            pickedMatchIds = new HashSet<int>();
            pnlCurtain.Location = new Point(0, 0);
            SetLimits();
            SetDictGridViews();
            DbControlsAsync();
            SetCurrentDictPartialOdds();
            SetSimGrids();
            SetUI();
        }

        private void SetLimits()
        {
            analyseLimitMap = new Dictionary<string, AnalyseLimitContanier>();
            analyseLimits = new List<AnalyseLimitContanier>();
            using (var db = new MatchModel())
            {
                List<AnalyseLimit> dbAnalyseLimits = db.AnalyseLimits.AsNoTracking().ToList();
                foreach (var dbAnalyseLimit in dbAnalyseLimits)
                {
                    TextBox maxTextBox;
                    TextBox minTextBox;
                    switch (dbAnalyseLimit.LimitType)
                    {
                        case "oddCombMatchCount":
                            maxTextBox = txtOddCombinationMatchMax;
                            minTextBox = txtOddCombinationMatchMin;
                            break;
                        case "oddCombResultMatchCount":
                            maxTextBox = txtOddCombinationResultMatchMax;
                            minTextBox = txtOddCombinationResultMatchMin;
                            break;
                        case "oddCombFullOddCount":
                            maxTextBox = txtOddCombinationFullOddMax;
                            minTextBox = txtOddCombinationFullOddMin;
                            break;
                        case "oddCombResultFullOddCount":
                            maxTextBox = txtOddCombinationResultFullOddMax;
                            minTextBox = txtOddCombinationResultFullOddMin;
                            break;
                        case "partialOddMatchCount":
                            maxTextBox = txtPartialOddMatchMax;
                            minTextBox = txtPartialOddMatchMin;
                            break;
                        case "partialOddResultMatchCount":
                            maxTextBox = txtPartialOddResultMatchMax;
                            minTextBox = txtPartialOddResultMatchMin;
                            break;
                        case "partialOddFullOddCount":
                            maxTextBox = txtPartialOddFullOddMax;
                            minTextBox = txtPartialOddFullOddMin;
                            break;
                        case "partialOddResultFullOddCount":
                            maxTextBox = txtPartialOddResultFullOddMax;
                            minTextBox = txtPartialOddResultFullOddMin;
                            break;
                        default:
                            maxTextBox = new TextBox();
                            minTextBox = new TextBox();
                            break;
                    }
                    AnalyseLimitContanier analyseLimitContanier = new AnalyseLimitContanier(dbAnalyseLimit.AnalyseLimitId, dbAnalyseLimit.LimitType, dbAnalyseLimit.MaxValue, dbAnalyseLimit.MinValue, maxTextBox, minTextBox);
                    analyseLimits.Add(analyseLimitContanier);
                    analyseLimitMap.Add(dbAnalyseLimit.LimitType,analyseLimitContanier);
                }
            }
        }
        private void SetDictGridViews()
        {
            dictGridViews = new Dictionary<AnalyseResultType, DataGridView>()
            {
                {AnalyseResultType.OddCombination,grdOddCombination  },
                {AnalyseResultType.PartialOdd,grdPartialOdd  },
            };
        }
        private void SetCurrentDictPartialOdds()
        {
            currentDictPartialOdds = new Dictionary<string, List<string>>();
            foreach (var partialOdd in Enum.GetValues(typeof(PartialOddType)).Cast<PartialOddType>())
                currentDictPartialOdds.Add(partialOdd.ToString(), new List<string>());
        }
        private void SetSimGrids()
        {
            simGrids = new List<DataGridView>()
            {
                grdPartialOdd,
                grdOddCombination,
                grdIntersection
            };
        }
        private void SetUI()
        {
            ResetTxtSearch();
            ResetAnalyseMatchResultLabels();
            treeViewAnalyseMatches.Nodes.Clear();
            ClearGridViews();
            SetTextBoxes();
            pnlCurtain.Location = new Point(pnlCurtain.Location.X,pnlCurtain.Location.Y-pnlCurtain.Size.Height);
            grdAnalysedFullOdds.DefaultCellStyle.Font = new Font(grdAnalysedFullOdds.DefaultCellStyle.Font, FontStyle.Bold);
        }
        private void ResetAnalyseMatchResultLabels()
        {
            lblAnalyseMatchName.Text = "";
            lblAnalyseMatchMsScore.Text = "";
            lblAnalyseMatchFhScore.Text = "";
            chkGetMatchResults.Checked = false;
        }
        private void ClearGridViews()
        {
            grdPartialOdd.Rows.Clear();
            grdOddCombination.Rows.Clear();
            grdPickedMatches.Rows.Clear();
            grdIntersection.Rows.Clear();
        }

        //-----
        private void ChkGetMatchResults_CheckedChanged(object sender, EventArgs e)
        {
            pnlAnalyseMatchResults.Visible = chkGetMatchResults.Checked;
        }
        //-----
        private void BtnGetPlayedMatches_Click(object sender, EventArgs e)
        {
            GetMatches(true);
        }
        private void BtnGetUnPlayedMatches_Click(object sender, EventArgs e)
        {
            GetMatches(false);
        }
        private void GetMatches(bool isPlayed)
        {
            SetAnalysedMatches(isPlayed);
            SetTreeViewAnalysedMatches();
        }
        private void SetAnalysedMatches(bool isPlayed)
        {
            using (var db = new MatchModel())
            {
                analysedMatches.Clear();
                if (isPlayed)
                    analysedMatches = db.AnalysedMatches.Where(x => x.FhHomeScore != -1 && x.FhAwayScore != -1 && x.MsHomeScore != -1 && x.MsAwayScore != -1).ToList();
                else
                    analysedMatches = db.AnalysedMatches.Where(x => x.FhHomeScore == -1 && x.FhAwayScore == -1 && x.MsHomeScore == -1 && x.MsAwayScore == -1).ToList();
            }
        }
        private void SetTreeViewAnalysedMatches()
        {
            var dailyMatches = analysedMatches
                .GroupBy(x => new
                {
                    x.MatchYear,
                    x.MatchMonth,
                    x.MatchDay,
                })
                .OrderBy(x => x.Key.MatchYear)
                .ThenBy(x => x.Key.MatchMonth)
                .ThenBy(x => x.Key.MatchDay);

            treeViewAnalyseMatches.Nodes.Clear();
            foreach (var dailyMatch in dailyMatches)
            {
                string day = dailyMatch.First().MatchDay.ToString();
                string month = dailyMatch.First().MatchMonth.ToString();
                string year = dailyMatch.First().MatchYear.ToString();
                TreeNode currentNode = treeViewAnalyseMatches.Nodes.Add(day + "." + month + "." + year);

                var g = dailyMatch.OrderBy(x => x.MatchTime);

                foreach (var item in g)
                        currentNode.Nodes.Add(item.AnalysedMatchId.ToString(),item.LeagueName + ", " + item.MatchCode.ToString()+", "+item.HomeTeamName+" - "+item.AwayTeamName + ", " + item.MatchTime);
                
            }
        }
        private void TreeViewAnalyseMatches_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Level == 0)
                return;
            CurrentAnalyseMatchControls(int.Parse(e.Node.Name));
        }
        //-----
        private void CurrentAnalyseMatchControls(int analysedMatchId)
        {
            SetCurrentAnalysedMatch(analysedMatchId);
            AnalyseControlsAsync();
            PickedMatchControls();
            SetAnalysedMatchUI();
        }
        //-----
        private void SetCurrentAnalysedMatch(int analysedMatchId)
        {
            currentAnalysedMatch = analysedMatches.First(x => x.AnalysedMatchId == analysedMatchId);
        }
        private void SetAnalysedMatchUI()
        {
            SetMatchInfoLabels();
            FillGrdAnalysedFullOdds();
        }
        private void SetMatchInfoLabels()
        {
            lblAnalyseMatchMsScore.Text = currentAnalysedMatch.MsHomeScore + " - " + currentAnalysedMatch.MsAwayScore;
            lblAnalyseMatchFhScore.Text = currentAnalysedMatch.FhHomeScore + " - " + currentAnalysedMatch.FhAwayScore;
            lblAnalyseMatchName.Text = currentAnalysedMatch.LeagueName + ", " + currentAnalysedMatch.MatchCode + ", " + currentAnalysedMatch.HomeTeamName + " - " + currentAnalysedMatch.AwayTeamName + ", " + currentAnalysedMatch.MatchTime;
        }
        private void FillGrdAnalysedFullOdds()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            List<string> lstFullOdds = currentAnalysedMatch.FullOdd.Split('|').ToList();
            foreach (var lstFullOdd in lstFullOdds)
            {
                string[] aValue = lstFullOdd.Split(':');
                values.Add(aValue[0],aValue[1]);
            }
            grdAnalysedFullOdds.Rows.Clear();
            int index = grdAnalysedFullOdds.Rows.Add();
            grdAnalysedFullOdds.Rows[index].Cells[0].Value = values["MS1"];
            grdAnalysedFullOdds.Rows[index].Cells[1].Value = values["MSX"];
            grdAnalysedFullOdds.Rows[index].Cells[2].Value = values["MS2"];
            grdAnalysedFullOdds.Rows[index].Cells[3].Value = values["FH1"];
            grdAnalysedFullOdds.Rows[index].Cells[4].Value = values["FHX"];
            grdAnalysedFullOdds.Rows[index].Cells[5].Value = values["FH2"];
            grdAnalysedFullOdds.Rows[index].Cells[6].Value = values["H1"];
            grdAnalysedFullOdds.Rows[index].Cells[7].Value = values["HX"];
            grdAnalysedFullOdds.Rows[index].Cells[8].Value = values["H2"];
            grdAnalysedFullOdds.Rows[index].Cells[9].Value = values["DC1X"];
            grdAnalysedFullOdds.Rows[index].Cells[10].Value = values["DC12"];
            grdAnalysedFullOdds.Rows[index].Cells[11].Value = values["DCX2"];
            grdAnalysedFullOdds.Rows[index].Cells[12].Value = values["MGEXIST"];
            grdAnalysedFullOdds.Rows[index].Cells[13].Value = values["MGNOTEXIST"];
            grdAnalysedFullOdds.Rows[index].Cells[14].Value = values["DOWNFH15"];
            grdAnalysedFullOdds.Rows[index].Cells[15].Value = values["UPFH15"];
            grdAnalysedFullOdds.Rows[index].Cells[16].Value = values["DOWN15"];
            grdAnalysedFullOdds.Rows[index].Cells[17].Value = values["UP15"];
            grdAnalysedFullOdds.Rows[index].Cells[18].Value = values["DOWN25"];
            grdAnalysedFullOdds.Rows[index].Cells[19].Value = values["UP25"];
            grdAnalysedFullOdds.Rows[index].Cells[20].Value = values["DOWN35"];
            grdAnalysedFullOdds.Rows[index].Cells[21].Value = values["UP35"];
            grdAnalysedFullOdds.Rows[index].Cells[22].Value = values["TG01"];
            grdAnalysedFullOdds.Rows[index].Cells[23].Value = values["TG23"];
            grdAnalysedFullOdds.Rows[index].Cells[24].Value = values["TG46"];
            grdAnalysedFullOdds.Rows[index].Cells[25].Value = values["TG7"];
            grdAnalysedFullOdds.Rows[index].Cells[26].Value = values["FH1MS1"];
            grdAnalysedFullOdds.Rows[index].Cells[27].Value = values["FH1MSX"];
            grdAnalysedFullOdds.Rows[index].Cells[28].Value = values["FH1MS2"];
            grdAnalysedFullOdds.Rows[index].Cells[29].Value = values["FHXMS1"];
            grdAnalysedFullOdds.Rows[index].Cells[30].Value = values["FHXMSX"];
            grdAnalysedFullOdds.Rows[index].Cells[31].Value = values["FHXMS2"];
            grdAnalysedFullOdds.Rows[index].Cells[32].Value = values["FH2MS1"];
            grdAnalysedFullOdds.Rows[index].Cells[33].Value = values["FH2MSX"];
            grdAnalysedFullOdds.Rows[index].Cells[34].Value = values["FH2MS2"];
        }
        private void PickedMatchControls()
        {
            if (pickedMatchIds.Contains(currentAnalysedMatch.AnalysedMatchId))
            {
                btnPickAnalysedMatch.Enabled = false;
                btnUnPick.Enabled = true;
            }
            else
            {
                btnPickAnalysedMatch.Enabled = true;
                btnUnPick.Enabled = false;
            }
        }
        //-----

        private void AnalyseControlsAsync()
        {
            if (!allResults.ContainsKey(currentAnalysedMatch.AnalysedMatchId))
            {
                Task t1 = Task.Factory.StartNew(() => OddCombinationControls(GetOddCombIds()));
                Task t2 = Task.Factory.StartNew(() => PartialOddControls());
                Task.WaitAll(t1, t2);
                SetAllResults();
            }
            else
            {
                oddCombResultCounts = allResults[currentAnalysedMatch.AnalysedMatchId][AnalyseResultType.OddCombination];
                partialOddResultCounts = allResults[currentAnalysedMatch.AnalysedMatchId][AnalyseResultType.PartialOdd];
            }
            ShowResults();
        }

        HashSet<int> GetOddCombIds()
        {
            Dictionary<HashSet<string>, int> rangedOddCombs = oddCombHashIds
                    .AsParallel()
                    .Where(x => x.Key > analyseLimitMap["oddCombMatchCount"].textValues["Min"] && x.Key < analyseLimitMap["oddCombMatchCount"].textValues["Max"] ).SelectMany(x => x.Value)
                    .ToDictionary(x => x.Key, y => y.Value);

            HashSet<int> oddCombIds = new HashSet<int>();
            if (fullOddValueOddCombIds.ContainsKey(currentAnalysedMatch.FullOdd))
                oddCombIds = new HashSet<int>(rangedOddCombs.AsParallel().Where(x =>  fullOddValueOddCombIds[currentAnalysedMatch.FullOdd].Contains(x.Value)).Select(x => x.Value));
            else
            {
                HashSet<string> hashCurrentFullOdd = new HashSet<string>(currentAnalysedMatch.FullOdd.Split('|'));
                oddCombIds = new HashSet<int>(rangedOddCombs.AsParallel().Where(x => x.Key.IsSubsetOf(hashCurrentFullOdd)).Select(x => x.Value));
            }
            return oddCombIds;
        }
        private void OddCombinationControls(HashSet<int> oddCombIds)
        {
            oddCombResultCounts = new Dictionary<string, Dictionary<int, int>>();
            Dictionary<int, HashSet<string>> temp = oddCombinationIdResults.AsParallel().Where(x => oddCombIds.Contains(x.Key)).ToDictionary(x => x.Key, x => x.Value);
            object myLock = new object();
            foreach (var oddCombItem in temp)
            {
                string oddCombVal = oddCombTest[oddCombItem.Key];
                SortedSet<int> tempFullOddIds = oddCombFullOddIds[oddCombItem.Key];
                HashSet<string> sadas = new HashSet<string>(fullOddTest.Where(x => tempFullOddIds.Contains(x.Key)).Select(x => x.Value));
                if (tempFullOddIds.Count < analyseLimitMap["oddCombFullOddCount"].textValues["Max"] && tempFullOddIds.Count > analyseLimitMap["oddCombFullOddCount"].textValues["Min"])
                {
                    foreach (var result in oddCombItem.Value)
                    {

                            if (!oddCombResultCounts.ContainsKey(result))
                                oddCombResultCounts.Add(result, new Dictionary<int, int>());

                            foreach (var tempFullOddId in tempFullOddIds)
                            {
                                if (!oddCombResultCounts[result].ContainsKey(tempFullOddId))
                                    oddCombResultCounts[result].Add(tempFullOddId, fullOddMatchCount[tempFullOddId]);
                            }
                        
                    }
                }
            }
        }
        private void PartialOddControls()
        {
            CurrentDictPartialOddsControls();
            CurrentDictStrPartialOddsControls();
            SetPartialOddResultCounts();
        }
        private void CurrentDictPartialOddsControls()
        {
            foreach (var currentDictPartialOdd in currentDictPartialOdds)
                currentDictPartialOdd.Value.Clear();

            foreach (var strOdd in currentAnalysedMatch.FullOdd.Split('|'))
            {
                if (strOdd.StartsWith("MS1:") || strOdd.StartsWith("MSX:") || strOdd.StartsWith("MS2:"))
                    currentDictPartialOdds[PartialOddType.ALLMS.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("FH1:") || strOdd.StartsWith("FHX:") || strOdd.StartsWith("FH2:"))
                    currentDictPartialOdds[PartialOddType.ALLFH.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("H1:") || strOdd.StartsWith("HX:") || strOdd.StartsWith("H2:"))
                    currentDictPartialOdds[PartialOddType.ALLHND.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("DC1X:") || strOdd.StartsWith("DC12:") || strOdd.StartsWith("DCX2:"))
                    currentDictPartialOdds[PartialOddType.ALLDC.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("TG01:") || strOdd.StartsWith("TG23:") || strOdd.StartsWith("TG46:") || strOdd.StartsWith("TG7:"))
                    currentDictPartialOdds[PartialOddType.ALLTG.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("MGEXIST:") || strOdd.StartsWith("MGNOTEXIST:"))
                    currentDictPartialOdds[PartialOddType.ALLMG.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("DOWNFH15:") || strOdd.StartsWith("UPFH15:"))
                    currentDictPartialOdds[PartialOddType.FHUPDOWN.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("DOWN15:") || strOdd.StartsWith("UP15:"))
                    currentDictPartialOdds[PartialOddType.UPDOWN15.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("DOWN25:") || strOdd.StartsWith("UP25:"))
                    currentDictPartialOdds[PartialOddType.UPDOWN25.ToString()].Add(strOdd);
                else if (strOdd.StartsWith("DOWN35:") || strOdd.StartsWith("UP35:"))
                    currentDictPartialOdds[PartialOddType.UPDOWN35.ToString()].Add(strOdd);
                else
                    currentDictPartialOdds[PartialOddType.ALLFHMS.ToString()].Add(strOdd);
            }
        }
        private void CurrentDictStrPartialOddsControls()
        {
            currentDictStrPartialOdds = new Dictionary<string, HashSet<int>>();
            foreach (var currentDictPartialOdd in currentDictPartialOdds)
            {
                string strValue = string.Join("|", currentDictPartialOdd.Value);
                if (partialOddFullOddIds.ContainsKey(strValue))
                    currentDictStrPartialOdds.Add(currentDictPartialOdd.Key, partialOddFullOddIds[strValue]);
                else
                    currentDictStrPartialOdds.Add(currentDictPartialOdd.Key, new HashSet<int>());

            }
        }

        private void SetPartialOddResultCounts()
        {
            Dictionary<int, HashSet<string>> tempFullOddResults = fullOddResults.Where(x => x.Key < analyseLimitMap["partialOddMatchCount"].textValues["Max"] && x.Key > analyseLimitMap["partialOddMatchCount"].textValues["Min"]).SelectMany(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            partialOddResultCounts = new Dictionary<string, Dictionary<int, int>>();
            foreach (var StrPartialOddPerms in allStrPartialOddPerms)
            {
                HashSet<int> tempIds = GetTempIds(StrPartialOddPerms);
                if (tempIds.Count < analyseLimitMap["partialOddFullOddCount"].textValues["Max"] && tempIds.Count > analyseLimitMap["partialOddFullOddCount"].textValues["Min"] && tempIds.IsSubsetOf(tempFullOddResults.Keys))
                    FillPartialOddResultCounts(GetTempResults(tempIds, tempFullOddResults),tempIds);
            }
        }
        HashSet<int> GetTempIds(List<string> StrPartialOddPerms)
        {
            return StrPartialOddPerms.Skip(1)
                           .Aggregate(
                                   new HashSet<int>(currentDictStrPartialOdds[StrPartialOddPerms.First()]),
                                   (h, e) =>
                                   {
                                       h.IntersectWith(currentDictStrPartialOdds[e]); return h;
                                   }
                               );
        }
        HashSet<string> GetTempResults(HashSet<int> tempIds, Dictionary<int, HashSet<string>> tempFullOddResults)
        {
            return tempIds.Skip(1)
                           .Aggregate(
                                   new HashSet<string>(tempFullOddResults[tempIds.First()]),
                                   (h, e) =>
                                   {
                                       h.IntersectWith(tempFullOddResults[e]); return h;
                                   }
                               );
        }
        private void FillPartialOddResultCounts(HashSet<string> tempResults, HashSet<int> tempIds)
        {
            if (!(tempResults.Count == 1 && tempResults.First() == ""))
            {
                foreach (var result in tempResults)
                {
                    if (!partialOddResultCounts.ContainsKey(result))
                        partialOddResultCounts.Add(result, new Dictionary<int, int>());

                    foreach (var tempId in tempIds)
                    {
                        if (!partialOddResultCounts[result].ContainsKey(tempId))
                            partialOddResultCounts[result].Add(tempId, fullOddMatchCount[tempId]);
                    }
                }
            }
        }
        private void SetAllResults()
        {
            allResults.Add(currentAnalysedMatch.AnalysedMatchId, new Dictionary<AnalyseResultType, Dictionary<string, Dictionary<int, int>>>()
            {
                { AnalyseResultType.OddCombination,new Dictionary<string, Dictionary<int, int>>()},
                { AnalyseResultType.PartialOdd,new Dictionary<string, Dictionary<int, int>>()},
            });
            foreach (var oddCombResultCountItem in oddCombResultCounts)
            {
                if (oddCombResultCountItem.Value.Count > analyseLimitMap["oddCombResultFullOddCount"].textValues["Min"] 
                    && oddCombResultCountItem.Value.Count < analyseLimitMap["oddCombResultFullOddCount"].textValues["Max"]
                    && oddCombResultCountItem.Value.Sum(x => x.Value) < analyseLimitMap["oddCombResultMatchCount"].textValues["Max"]
                    && oddCombResultCountItem.Value.Sum(x => x.Value) > analyseLimitMap["oddCombResultMatchCount"].textValues["Min"])
                    allResults[currentAnalysedMatch.AnalysedMatchId][AnalyseResultType.OddCombination].Add(oddCombResultCountItem.Key, oddCombResultCountItem.Value);
            }
            foreach (var partialOddResultCountItem in partialOddResultCounts)
            {
                if (partialOddResultCountItem.Value.Count > analyseLimitMap["partialOddResultFullOddCount"].textValues["Min"] 
                    && partialOddResultCountItem.Value.Count < analyseLimitMap["partialOddResultFullOddCount"].textValues["Max"]
                    && partialOddResultCountItem.Value.Sum(x => x.Value) < analyseLimitMap["partialOddResultMatchCount"].textValues["Max"]
                    && partialOddResultCountItem.Value.Sum(x => x.Value) > analyseLimitMap["partialOddResultMatchCount"].textValues["Min"])
                    allResults[currentAnalysedMatch.AnalysedMatchId][AnalyseResultType.PartialOdd].Add(partialOddResultCountItem.Key, partialOddResultCountItem.Value);
            }


        }
        private void ShowResults()
        {
            SetGrdIntersects(new HashSet<string>(allResults[currentAnalysedMatch.AnalysedMatchId][AnalyseResultType.PartialOdd].Keys.Intersect(allResults[currentAnalysedMatch.AnalysedMatchId][AnalyseResultType.OddCombination].Keys)));
            SetGrdOddCombination();
        }
        private void SetGrdIntersects(HashSet<string> intersects)
        {
            grdIntersection.Rows.Clear();
            foreach (var intersect in intersects)
                grdIntersection.Rows[grdIntersection.Rows.Add()].Cells[0].Value = intersect;
        }
        private void SetGrdOddCombination()
        {
            foreach (var analyseResultType in allResults[currentAnalysedMatch.AnalysedMatchId])
            {
                DataGridView dataGridView = dictGridViews[analyseResultType.Key];
                dataGridView.Rows.Clear();
                foreach (var result in analyseResultType.Value.OrderByDescending(x => x.Value.Sum(y => y.Value)).ThenByDescending(x => x.Value.Count))
                {
                    int index = dataGridView.Rows.Add();
                    dataGridView.Rows[index].Cells[0].Value = result.Key;
                    dataGridView.Rows[index].Cells[1].Value = result.Value.Sum(x => x.Value);
                    dataGridView.Rows[index].Cells[2].Value = result.Value.Count;
                }
            }
        }
        //-----
        private void GrdPartialOdd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
                SameGivenResultControls(grdPartialOdd.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
        private void GrdOddCombination_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
                SameGivenResultControls(grdOddCombination.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
        private void GrdIntersection_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex > -1)
                SameGivenResultControls(grdIntersection.Rows[e.RowIndex].Cells[0].Value.ToString());
        }
        //-----
        private void SameGivenResultControls(string strGivenResult)
        {
            UnBoldGridViews();
            BoldSameResultGridViews(strGivenResult);
        }
        private void UnBoldGridViews()
        {
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle
            {
                Font = new Font(grdOddCombination.Font, FontStyle.Regular)
            };
            foreach (var simGrid in simGrids)
                foreach (DataGridViewRow row in simGrid.Rows)
                    row.DefaultCellStyle = dataGridViewCellStyle;
        }
        private void BoldSameResultGridViews(string strGivenResult)
        {
            DataGridViewCellStyle dataGridViewCellStyle = new DataGridViewCellStyle
            {
                Font = new Font(grdOddCombination.Font, FontStyle.Bold)
            };
            foreach (var simGrid in simGrids)
                foreach (DataGridViewRow row in simGrid.Rows)
                    if(row.Cells[0].Value.ToString().Equals(strGivenResult))
                        row.DefaultCellStyle = dataGridViewCellStyle;
        }

        //-----
        private void BtnPickAnalysedMatch_Click(object sender, EventArgs e)
        {
            if (!pickedMatchIds.Contains(currentAnalysedMatch.AnalysedMatchId))
                pickedMatchIds.Add(currentAnalysedMatch.AnalysedMatchId);

            RestoreGrdPickedMatches();
        }
        private void BtnUnPick_Click(object sender, EventArgs e)
        {
            if (pickedMatchIds.Contains(currentAnalysedMatch.AnalysedMatchId))
                pickedMatchIds.Remove(currentAnalysedMatch.AnalysedMatchId);

            RestoreGrdPickedMatches();
        }
        private void RestoreGrdPickedMatches()
        {
            grdPickedMatches.Rows.Clear();
            foreach (var pickedMatchId in pickedMatchIds)
            {
                int index = grdPickedMatches.Rows.Add();
                AnalysedMatch analysedMatch = analysedMatches.First(x => x.AnalysedMatchId == pickedMatchId);
                grdPickedMatches.Rows[index].Cells[0].Value = analysedMatch.MatchCode;
                grdPickedMatches.Rows[index].Cells[1].Value = analysedMatch.HomeTeamName+" - "+ analysedMatch.AwayTeamName;
                grdPickedMatches.Rows[index].Cells[2].Value = pickedMatchId;
            }
        }
        private void GrdPickedMatches_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            foreach (DataGridViewRow row in grdPickedMatches.Rows)
                row.DefaultCellStyle.Font = new Font(grdPickedMatches.DefaultCellStyle.Font,FontStyle.Regular);
            
            grdPickedMatches.Rows[e.RowIndex].DefaultCellStyle.Font = new Font(grdPickedMatches.DefaultCellStyle.Font, FontStyle.Bold);
            CurrentAnalyseMatchControls((int)grdPickedMatches.Rows[e.RowIndex].Cells[2].Value);
        }
        //-----
        private void BtnAnalyseAgain_Click(object sender, EventArgs e)
        {
            if (allResults.ContainsKey(currentAnalysedMatch.AnalysedMatchId))
                allResults.Remove(currentAnalysedMatch.AnalysedMatchId);

            AnalyseControlsAsync();
        }
        //-----
        private void BtnStoreLimits_Click(object sender, EventArgs e)
        {
            using (var db = new MatchModel())
            {
                List<AnalyseLimit> dbAnalyseLimits = db.AnalyseLimits.ToList();
                foreach (var analyseLimit in analyseLimits)
                    dbAnalyseLimits.First(x => x.AnalyseLimitId == analyseLimit.Id).SetValues(analyseLimit.textValues["Max"], analyseLimit.textValues["Min"]);

                db.SaveChanges();
            }
            SetLblNotification("Stored");
        }
        private void BtnResetLimits_Click(object sender, EventArgs e)
        {
            SetLimits();
            SetTextBoxes();
        }
        private void BtnZeroLimits_Click(object sender, EventArgs e)
        {
            foreach (var analyseLimit in analyseLimits)
                analyseLimit.ResetValues();

            SetTextBoxes();
            SetLblNotification("Applied");
        }
        private void SetLblNotification(string value)
        {
            lblNotification.Text = value;
            lblNotification.Visible = true;
            var t = new Timer();
            t.Interval = 2000;
            t.Tick += (s, e) =>
            {
                lblNotification.Hide();
                t.Stop();
            };
            t.Start();

        }
        //-----
        private void SetTextBoxes()
        {
            foreach (var analyseLimit in analyseLimits)
            {
                foreach (var textMapItem in analyseLimit.textMap)
                {
                    if (analyseLimit.textValues[textMapItem.Value] > 0 && analyseLimit.textValues[textMapItem.Value] < int.MaxValue)
                        textMapItem.Key.Text = analyseLimit.textValues[textMapItem.Value].ToString();
                    else
                        textMapItem.Key.Text = "";
                }
            }
        }
        private void TextNumControls(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
        private void LimitTextBoxControls(object sender, KeyPressEventArgs e)
        {
            TextNumControls(sender, e);
        }
        private void TxtLimitTextChanged(object sender, EventArgs e)
        {
            if(((TextBox)sender).Text != "")
                analyseLimits.First(x => x.textMap.Keys.Contains((TextBox)sender)).SetValue((TextBox)sender);
        }
        //-----
        private void ResetTxtSearch()
        {
            txtSearchMatchAnalyseRatio.Text = "Search - Match Code, Team Name";
        }
        private void TxtSearchGotFocus(object sender, EventArgs e)
        {
            if(txtSearchMatchAnalyseRatio.Text == "Search - Match Code, Team Name")
            {
                txtSearchMatchAnalyseRatio.Text = "";
                txtSearchMatchAnalyseRatio.ForeColor = Color.Black;
            }
        }
        private void TxtSearchLostFocus(object sender, EventArgs e)
        {
            if(txtSearchMatchAnalyseRatio.Text == "")
            {
                txtSearchMatchAnalyseRatio.Text = "Search - Match Code, Team Name";
                txtSearchMatchAnalyseRatio.ForeColor = Color.LightGray;
                ResetAnalysedMatchButtonFonts();
            }
        }
        private void ResetAnalysedMatchButtonFonts()
        {
            foreach (TreeNode dailyMatches in treeViewAnalyseMatches.Nodes)
            {
                foreach (TreeNode item in dailyMatches.Nodes)
                {
                    item.NodeFont = new Font(treeViewAnalyseMatches.Font, FontStyle.Regular);
                }
            }
        }
        private void TxtSearchMatchAnalyseRatio_TextChanged(object sender, EventArgs e)
        {
            string currentTextValue = txtSearchMatchAnalyseRatio.Text;

            if (currentTextValue == "Search - Match Code, Team Name")
                return;
            else if (currentTextValue == "")
                ResetAnalysedMatchButtonFonts();
            else
                SearchAnalysedMatch(currentTextValue);
        }
        private void SearchAnalysedMatch(string searchKeyword)
        {
            ResetAnalysedMatchButtonFonts();
            foreach (TreeNode dailyMatches in treeViewAnalyseMatches.Nodes)
            {
                foreach (TreeNode item in dailyMatches.Nodes)
                {
                    if (CultureInfo.CurrentCulture.CompareInfo.IndexOf(item.Text,searchKeyword,CompareOptions.IgnoreCase)>=0)
                        item.NodeFont = new Font(treeViewAnalyseMatches.Font, FontStyle.Bold);
                }
            }
        }
    }
}
