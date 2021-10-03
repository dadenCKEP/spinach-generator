using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.Text.Encodings.Web;

namespace spinach_generator
{
    public class NippouSettings
    {
        /// <summary>
        /// 日報フォルダの配置先パス
        /// </summary>
        public string nippouBasePath { get; set; }

        /// <summary>
        /// 日報ディレクトリ名
        /// </summary>
        public string nippouDirName { get; set; }

        /// <summary>
        /// 週報ディレクトリ名
        /// </summary>
        public string shuhouDirName { get; set; }

        /// <summary>
        /// 日報に載せる名前
        /// </summary>
        public string userName { get; set; }
    }

    static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        /// 

        // 個人設定の初期化
        public static NippouSettings nippouSettings = new NippouSettings();

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 個人設定をファイルから読み込む
            string config_path = "./config.json";
            if (!File.Exists(config_path))
            {
                // 個人設定ファイルがなさそうなら専用フォームで作成
                Settings settings = new Settings();
                DialogResult result = settings.ShowDialog();
                // 個人設定が作成されずに閉じられたらソフトウェアを終了
                if (result == DialogResult.Cancel) return;
            }
            else
            {
                // 個人設定ファイルの読み込み
                using (StreamReader todayNippou = new StreamReader(config_path))
                {
                    // 文字列ベースでファイルを読み込んでJSONシリアライザで処理
                    string buffer = todayNippou.ReadToEnd();
                    var options = new JsonSerializerOptions
                    {
                        // Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                        WriteIndented = true
                    };
                    try
                    {
                        nippouSettings = JsonSerializer.Deserialize<NippouSettings>(buffer, options);
                    }
                    catch (Exception)
                    {
                        // 設定ファイルが破損している場合はメッセージを表示して終了
                        MessageBox.Show("設定ファイルが破損しています。削除して再起動することで再設定できます。");
                        return;
                    }
                }
            }


            // 個人設定をファイルから読み込む
            string template_path = "./template.txt";
            if (!File.Exists(template_path))
            {
                // TODO: テンプレートをでっち上げたい
                MessageBox.Show("テンプレートファイルが破損しています。再セットアップが必要です。");
                return;
            }
            else
            {
                // テンプレートを読み込む
                using (StreamReader template = new StreamReader(config_path))
                {
                    // 文字列ベースでファイルを読み込む
                    string buffer = template.ReadToEnd();

                    // TODO: ファイルから構造を解釈する
                }
            }

            Application.Run(new MainForm());
        }
    }
}
