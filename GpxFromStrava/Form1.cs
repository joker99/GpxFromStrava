using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GpxFromStrava
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnGetGpx_Click(object sender, EventArgs e)
        {

        }


        private static string GetFireFoxCookiePath()
        {
            string profilePath = Environment.GetFolderPath(
                             Environment.SpecialFolder.ApplicationData);
            profilePath += @"\Mozilla\Firefox\Profiles\";

            try
            {
                string[] dir = Directory.GetDirectories("*.default");
                if (dir.Length != 1)
                    return string.Empty;

                profilePath += dir[0] + @"\" + "cookies.sqlite";
            }
            catch (Exception)
            {
                return string.Empty;
            }

            if (!File.Exists(profilePath))
                return string.Empty;

            return profilePath;
        }

        /*
        private static bool GetCookie_FireFox(string strHost, string strField, ref string Value)
        {
            Value = string.Empty;
            bool fRtn = false;
            string strPath, strTemp, strDb;
            strTemp = string.Empty;

            // Check to see if FireFox Installed
            strPath = GetFireFoxCookiePath();
            if (string.Empty == strPath) // Nope, perhaps another browser
                return false;

            try
            {
                // First copy the cookie jar so that we can read the cookies 
                // from unlocked copy while
                // FireFox is running
                strTemp = strPath + ".temp";
                strDb = "Data Source=" + strTemp + ";pooling=false";

                File.Copy(strPath, strTemp, true);

                // Now open the temporary cookie jar and extract Value from the cookie if
                // we find it.
                using (SQLiteConnection conn = new SQLiteConnection(strDb))
                {
                    using (SQLiteCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = "SELECT value FROM moz_cookies WHERE host LIKE '%" +
                            strHost + "%' AND name LIKE '%" + strField + "%';";

                        conn.Open();
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Value = reader.GetString(0);
                                if (!Value.Equals(string.Empty))
                                {
                                    fRtn = true;
                                    break;
                                }
                            }
                        }
                        conn.Close();
                    }
                }
            }
            catch (Exception)
            {
                Value = string.Empty;
                fRtn = false;
            }

            // All done clean up
            if (string.Empty != strTemp)
            {
                File.Delete(strTemp);
            }
            return fRtn;
        }
         * */
    }
}
