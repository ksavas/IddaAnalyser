 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IddaAnalyser
{
    public class BackupController
    {
        public BackupController()
        {

        }

        private void SetOperations()
        {
            //SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-0ITDCMB\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            //string backupCommand = "BACKUP DATABASE [" + "Matches1" + "] TO DISK='" + "D:" + "\\" + "Business\\My useful projects\\İdda Analizer - Copy\\IddaAnalizer\\IddaAnalizer" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";
            //SqlCommand restoreCommand = new SqlCommand("RESTORE DATABASE[Matches1] FROM DISK='D:\\Business\\My useful projects\\İdda Analizer - Copy\\IddaAnalizer\\IddaAnalizer-2018-07-02--11-22-38.bak' WITH REPLACE", con);
            //using (SqlCommand command = new SqlCommand(backupCommand, con))
            //{
            //    if (con.State != ConnectionState.Open)
            //    {
            //        con.Open();
            //    }
            //    command.ExecuteNonQuery();
            //    con.Close();
            //}

            // USE master
            // GO
            // ALTER DATABASE[Matches1]
            // SET SINGLE_USER
            // WITH ROLLBACK IMMEDIATE
            // GO
            // RESTORE DATABASE[Matches1] FROM DISK = 'D:\Business\My useful projects\İdda Analizer - Copy\IddaAnalizer\IddaAnalizer_New_Empty_30-10-2018.bak' WITH REPLACE
            // GO

            //'D:Business\My useful projects\İdda Analizer - Copy\IddaAnalizer\IddaAnalizer_StrSimilarityEmpty.bak'

            //'D:Business\My useful projects\İdda Analizer - Copy\IddaAnalizer\IddaAnalizer_SimilarityCorrectness_Empty_13-11-2018.bak' !!!

            //BACKUP DATABASE [Matches1] TO DISK='D:Business\My useful projects\İdda Analizer - Copy\IddaAnalizer\IddaAnalizer_New_Empty_30-10-2018.bak'
        }

    }
}
