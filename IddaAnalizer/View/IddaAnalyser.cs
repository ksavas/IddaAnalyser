
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;
using mshtml;
using System.Data.SQLite;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Text;

namespace IddaAnalyser
{
    public partial class IddaAnalyser : Form
    {
        public IddaAnalyser()
        {
            using (var db = new MatchModel())
            {
                List<Date> dbDates = db.Dates.ToList();
                var t = dbDates.OrderBy(x => x.Year).ThenBy(x => x.Month).ThenBy(x => x.Day);
                Date date = t.First();
                Date date1 = t.Last();
            }
            InitializeComponent();
            FitformSize();
            StoreTabInit();

        }


        private void FitformSize()
        {
            
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            int w = Width >= screen.Width ? screen.Width : (screen.Width + Width) / 2;
            int h = Height >= screen.Height ? screen.Height : (screen.Height + Height) / 2;
            this.Location = new Point((screen.Width - w) / 2, (screen.Height - h) / 2);
            this.Size = new Size(w, h);
        }

        private void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControl.SelectedIndex)
            {
                case 0:
                    StoreTabInit();
                    break;
                case 1:
                    AnalyseRatioInit();
                    break;
                default:
                    break;
            }
        }


    }
}
