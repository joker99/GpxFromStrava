using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GpxFromStrava
{
    public partial class Form1 : Form
    {
        private static string StravaDomain = "app.strava.com";
        public Form1()
        {
            InitializeComponent();
        }

        private async void btnGetGpx_Click(object sender, EventArgs e)
        {
            if (txtActivityId.Text == string.Empty)
                return;
            try
            {
                var rawData = await DownloadRawData("http://app.strava.com/stream/" + txtActivityId.Text + "?streams[]=latlng");
                var gpxData = ConvertJsonToGpx(rawData);
                saveFileDialog.FileName = txtActivityId.Text + ".gpx";
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    File.WriteAllText(saveFileDialog.FileName, gpxData);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private static string GPS_PREFIX =
@"<?xml version='1.0' encoding='UTF-8'?>
<gpx xmlns='http://www.topografix.com/GPX/1/1' creator='GpxFromStrava' version='1.1' xmlns:xsi='http://www.w3.org/2001/XMLSchema-instance' xsi:schemaLocation='http://www.topografix.com/GPX/1/1 http://www.topografix.com/GPX/1/1/gpx.xsd'>
  <trk>
    <trkseg>";

        private static string GPS_POSTFIX = 
@"    </trkseg>
  </trk>
</gpx>";

        private static string ConvertJsonToGpx(string json)
        {
            // replaces
            string gpx = json;
            string[] key = {"]}","{\"latlng\":[","[","],","," };
            string[] value = {",",string.Empty,"<trkpt lat=\"","\"/>","\" lon=\"" };
            for (var i = 0; i < key.Length; i++)
            {
                gpx = gpx.Replace(key[i], value[i]);
            }
            return GPS_PREFIX + gpx + GPS_POSTFIX;

        }

        private static async Task<string> DownloadRawData(string url)
        {
            string cookieHeader = GetAllCookies_FireFox(StravaDomain);
            WebClient webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Cookie, cookieHeader);
            var res = await webClient.DownloadDataTaskAsync(new Uri(url));
            return Encoding.UTF8.GetString(res);
        }


        private static string GetFireFoxCookiePath()
        {
            string profilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Mozilla\Firefox\Profiles\");
            string[] dir = Directory.GetDirectories(profilePath, "*.default");
            if (dir.Length != 1)
                return string.Empty;

            var cookiePath = dir[0] + @"\" + "cookies.sqlite";
            if (!File.Exists(cookiePath))
                return string.Empty;
            return cookiePath;
        }

        //Adapted from http://www.codeproject.com/Articles/330142/Cookie-Quest-A-Quest-to-Read-Cookies-from-Four-Pop
        private static string GetAllCookies_FireFox(string strHost)
        {
            string strPath, strTemp, strDb;
            strTemp = string.Empty;

            // Check to see if FireFox Installed
            strPath = GetFireFoxCookiePath();
            if (string.Empty == strPath) // Nope, perhaps another browser
                return null;

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
                        cmd.CommandText = "SELECT name, value FROM moz_cookies WHERE host LIKE '%" +
                            strHost + "%';";

                        conn.Open();
                        CookieContainer cookies = new CookieContainer();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cookies.Add(new Cookie(reader.GetString(0), reader.GetString(1), "/", StravaDomain));
                            }
                        }
                        return cookies.GetCookieHeader(new Uri("https://" + StravaDomain));
                    }
                }
            }
            finally
            {
                // All done clean up
                if (string.Empty != strTemp)
                {
                    File.Delete(strTemp);
                }
            }
        }
    }
}
