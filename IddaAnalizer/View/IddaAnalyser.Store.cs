using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Linq.Dynamic;
using System.Diagnostics;
using CefSharp;
using CefSharp.WinForms;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace IddaAnalyser
{

    partial class IddaAnalyser
    {
        ImportReport importReport;

        bool isImport;
        
        string fileLocation;

        List<AnalysedMatch> storeTabAnalysedMatches;
        ExcelReader excelReader;
        AnalysedMatchDbController analysedMatchDbController;

        int day = 0;
        int month = 0;
        int year = 0;

        int currentImportReportId;
        List<ImportReport> dbImportReports;
        string importReportPath;
        //-----
        private void StoreTabInit()
        {
            SetObjects();
            SetStoreUI();
            SetExcelColumns();
            SetCmbAnalysedMatchDates();
        }

        private void SetObjects()
        {
            day = 0;
            month = 0;
            year = 0;
            isImport = false;
            fileLocation = "";
            storeTabAnalysedMatches = new List<AnalysedMatch>();
            excelReader = new ExcelReader();
        }

        //-----
        private void BtnResetColNumbers_Click(object sender, EventArgs e)
        {
            using (var db = new MatchModel())
            {
                List<ExcelColumn> dbExcelColumns = db.ExcelColumns.ToList();
                foreach (var dbExcelColumn in dbExcelColumns)
                    dbExcelColumn.ColumntNumber = dbExcelColumn.RealColumntNumber;

                db.SaveChanges();
            }
            SetExcelColumns();
        }
        private void SetExcelColumns()
        {
            using (var db = new MatchModel())
            {
                List<ExcelColumn> ExcelColumns = db.ExcelColumns.ToList();
                grdExcelColumns.Rows.Clear();
                foreach (var excelColumn in ExcelColumns)
                {
                    int rowIndex = grdExcelColumns.Rows.Add();
                    grdExcelColumns.Rows[rowIndex].Cells[0].Value = excelColumn.ColumnName;
                    grdExcelColumns.Rows[rowIndex].Cells[1].Value = excelColumn.ColumntNumber;
                }
            }
        }
        private void BtnSaveExcelColumn_Click(object sender, EventArgs e)
        {
            SaveNewColumnNumbers();
            SetExcelColumns();
        }
        private void SaveNewColumnNumbers()
        {
            using (var db = new MatchModel())
            {
                List<ExcelColumn> ExcelColumns = db.ExcelColumns.ToList();
                foreach (DataGridViewRow row in grdExcelColumns.Rows)
                    ExcelColumns.First(x => x.ColumnName.Equals(row.Cells[0].Value.ToString())).ColumntNumber = int.Parse(row.Cells[1].Value.ToString());
                db.SaveChanges();
            }
        }
        //-----
        private void SetStoreUI()
        {
            SetStoredMatchCountLabel();
            SetButtonsForStoreTabInit();
        }
        private void SetButtonsForStoreTabInit()
        {
            btnImportDb.Enabled = false;
            btnStoreClear.Enabled = false;
            btnStoreDb.Enabled = true;
            btnRemoveDate.Enabled = false;
        }
        private void SetCmbAnalysedMatchDates()
        {
            using (var db = new MatchModel())
            {
                cmbAnalysedMatchDates.Items.Clear();
                var t = db.AnalysedMatches
                    .GroupBy(x => new { x.MatchDay, x.MatchMonth, x.MatchYear })
                    .OrderBy(x => x.Key.MatchYear)
                    .ThenBy(x => x.Key.MatchMonth)
                    .ThenBy(x => x.Key.MatchDay);

                foreach (var item in t)
                    cmbAnalysedMatchDates.Items.Add(item.First().MatchDay + "." + item.First().MatchMonth + "." + item.First().MatchYear);
            }
        }
        //-----
        private void BtnRemoveDate_Click(object sender, EventArgs e)
        {
            RemoveSelectedDate();
            StoreTabInit();
            SetStoreStatus("selected matches are removed..");
        }
        private void RemoveSelectedDate()
        {
            using (var db = new MatchModel())
            {
                storeTabAnalysedMatches = new List<AnalysedMatch>(db.AnalysedMatches.Where(x => x.MatchDay == day && x.MatchMonth == month && x.MatchYear == year).ToList());
                db.AnalysedMatches.RemoveRange(storeTabAnalysedMatches);
                db.SaveChanges();
            }
        }
        //-----
        private void CmbAnalysedMatchDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetSelectedDate();
            SelectedDateControls();
        }
        private void SetSelectedDate()
        {
            string[] date = cmbAnalysedMatchDates.Text.ToString().Split('.');
            day = int.Parse(date[0]);
            month = int.Parse(date[1]);
            year = int.Parse(date[2]);
        }
        private void SelectedDateControls()
        {
            using (var db = new MatchModel())
            {
                storeTabAnalysedMatches = new List<AnalysedMatch>(db.AnalysedMatches.Where(x => x.MatchDay == day && x.MatchMonth == month && x.MatchYear == year).ToList());
                SetStoredMatchCountLabel();
                btnImportDb.Enabled = true;
                btnRemoveDate.Enabled = true;
                btnStoreDb.Enabled = false;
                btnStoreClear.Enabled = true;
                ShowMatches();
            }
        }
        //-----
        
        private void ShowImportReportValues(ImportReport importReport)
        {
            lblImportDate.Text = importReport.ImportDate;
            lblImportMatch.Text = importReport.NewMatchCount.ToString();
            lblImportLeague.Text = importReport.NewLeagueCount.ToString();
            lblImportTeam.Text = importReport.NewTeamCount.ToString();
            lblImportFullOdd.Text = importReport.NewFullOddCount.ToString();
            lblImportMatchResult.Text = importReport.NewMatchResultCount.ToString();
            lblImportGeneralResult.Text = importReport.NewGeneralResultCount.ToString();
            lblRemovedOddComb.Text = importReport.RemovedOddCombinationCount.ToString();
            lblNewOddComb.Text = importReport.NewOddCombinationCount.ToString();
            lblUpdatedOddComb.Text = importReport.UpdatedOddCombinationCount.ToString();
            lblNewPartialOdd.Text = importReport.NewPartialOddCount.ToString();
            lblUpdatedPartialOdd.Text = importReport.UpdatedPartialOddCount.ToString();
        }

        //-----
        private void BtnStoreDb_Click(object sender, EventArgs e)
        {
            GetExcelFile();
            FetchMatches();
            StoreFetchedMatches();
            ShowMatches();
        }
        private void GetExcelFile()
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Excel File |*.xlsx";
            if (file.ShowDialog() == DialogResult.OK)
                fileLocation = file.FileName;
        }
        private void FetchMatches()
        {
            SetStoreStatus("Fetching..");
            FetchControls();
            FetchFinishControls();
        }
        private void FetchControls()
        {
            excelReader = new ExcelReader();
            storeTabAnalysedMatches = new List<AnalysedMatch>(excelReader.GeneralRatioExcelControl(fileLocation));
        }
        private void FetchFinishControls()
        {
            if (storeTabAnalysedMatches.Count > 0)
                SetStoreStatus("Matches Fetched..");
            else
            {
                StoreTabInit();
                SetStoreStatus("Error Occured at Row: " + excelReader.GetCurrentRow());
            }
        }
        //-----
        private void StoreFetchedMatches()
        {
            isImport = false;
            SetStoreStatus("Storing..");
            SetControlsBeforeBackGroundOperation();
            backgroundWorker1.RunWorkerAsync();
        }
        private void StoreBackGround()
        {
            analysedMatchDbController = new AnalysedMatchDbController(storeTabAnalysedMatches);
            analysedMatchDbController.ApplyMatchOperations(backgroundWorker1);
        }
        private void BackGroundStoreFinish(int count)
        {
            StoreTabInit();
            SetStoreStatus(count.ToString() + " matches stored db..");
        }
        //-----
        private void BtnImportDb_Click(object sender, EventArgs e)
        {
            if (storeTabAnalysedMatches.Count == 0)
                return;
            SetStoreStatus("Importing..");
            isImport = true;
            SetControlsBeforeBackGroundOperation();
            backgroundWorker1.RunWorkerAsync();
        }
        private void ImportBackGround()
        {
            if (day != 0 && month != 0 && year != 0)
            {
                StoreController storeController = new StoreController();
                importReport = storeController.StoreMatches(backgroundWorker1, day, month, year,lblStoreStatus);
            }
        }
        private void BackGroundImportFinish()
        {
            grdAnalysedMatches.Rows.Clear();
            StoreTabInit();
            ImportResulStateControls();
            ShowImportReportValues(importReport);
        }
        private void ImportResulStateControls()
        {
            if (importReport.NewMatchCount <= 0)
                SetStoreStatus("No Match Found to import Db..");
            else
                SetStoreStatus("Import Finished");
        }
        //-----
        private void BtnStoreClear_Click(object sender, EventArgs e)
        {
            StoreTabInit();
            SetStoreStatus("Idle");
        }  
        //-----
        private void BackgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            if (isImport)
                ImportBackGround();
            else
                StoreBackGround();
        }
        private void BackgroundWorker1_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)
        {
            prgBarStore.Value = e.ProgressPercentage;
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, System.ComponentModel.RunWorkerCompletedEventArgs e)
        {
            prgBarStore.Value = 0;
            if (isImport)
                BackGroundImportFinish();
            else
                BackGroundStoreFinish(storeTabAnalysedMatches.Count);

        }
        //-----
        private void SetControlsBeforeBackGroundOperation()
        {
            btnStoreDb.Enabled = false;
            btnStoreClear.Enabled = false;
            btnImportDb.Enabled = false;
            btnRemoveDate.Enabled = false;
        }
        //-----
        private void ShowMatches()
        {
            grdAnalysedMatches.Rows.Clear();
            int rowIndex = -1;
            foreach (var newAnalysedMatch in storeTabAnalysedMatches)
            {
                rowIndex = grdAnalysedMatches.Rows.Add();
                grdAnalysedMatches.Rows[rowIndex].Cells[0].Value = newAnalysedMatch.MatchTime;
                grdAnalysedMatches.Rows[rowIndex].Cells[1].Value = newAnalysedMatch.LeagueName;
                grdAnalysedMatches.Rows[rowIndex].Cells[3].Value = newAnalysedMatch.LeagueName;
                grdAnalysedMatches.Rows[rowIndex].Cells[4].Value = newAnalysedMatch.HomeTeamName + "-" + newAnalysedMatch.AwayTeamName;
                grdAnalysedMatches.Rows[rowIndex].Cells[5].Value = newAnalysedMatch.FhHomeScore + "-" + newAnalysedMatch.FhAwayScore;
                grdAnalysedMatches.Rows[rowIndex].Cells[6].Value = newAnalysedMatch.MsHomeScore + "-" + newAnalysedMatch.MsAwayScore;
                grdAnalysedMatches.Rows[rowIndex].Cells[7].Value = newAnalysedMatch.FullOdd.Replace("|", " ");
            }
        }
        private void SetStoreStatus(string status)
        {
            lblStoreStatus.Text = "status : " + status;
        }
        private void SetStoredMatchCountLabel()
        {
            lblStoredMatchCount.Text = "Count : " + storeTabAnalysedMatches.Count;
        }      
    }
}
