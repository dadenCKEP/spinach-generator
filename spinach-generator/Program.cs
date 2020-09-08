using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace spinach_generator
{
    public class NippouSettings
    {
        public string nippouBasePath { get; set; }
        public string nippouDirName { get; set; }
        public string shuhouDirName { get; set; }
        public string userName { get; set; }
    }

    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        /// 

        // 個人設定
        public static NippouSettings nippouSettings = new NippouSettings();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 設定を読み込む
            string config_path = "./config.json";
            if (!File.Exists(config_path))
            {
                // なさそうなら専用フォームで作成
                Settings settings = new Settings();
                DialogResult result = settings.ShowDialog();
            }

            // 読み込み
            using (StreamReader todayNippou = new StreamReader(config_path))
            {
                string buffer = todayNippou.ReadToEnd();
                nippouSettings = JsonSerializer.Deserialize<NippouSettings>(buffer);
            }


            Application.Run(new MainForm());
        }
    }
}
