using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spinach_generator
{
    public partial class MainForm : Form
    {
        // マイドキュメントのパス
        string nippou_base_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public MainForm()
        {
            InitializeComponent();

            // 日付表示の更新
            label_date.Text = DateTime.Now.ToString("yyyy/MM/dd");

            // フォルダがないなら作成
            if (!Directory.Exists(nippou_base_path + "\\日報")) Directory.CreateDirectory(nippou_base_path + "\\日報");
            if (!Directory.Exists(nippou_base_path + "\\週報")) Directory.CreateDirectory(nippou_base_path + "\\週報");
        }

        private void button_nippou_Click(object sender, EventArgs e)
        {
            string nippou_path = nippou_base_path + "\\日報\\" + DateTime.Now.ToString("日報_yyyy_MM_dd") + ".md";
            // 日報が既にあるなら開く
            if (File.Exists(nippou_path))
            {
                System.Diagnostics.Process.Start(nippou_path);
            }
            else
            {
                // 今日の日報を作成する前に一番若い日付を探す
                List<string> existNippouFiles = new List<string>();
                existNippouFiles.AddRange(Directory.GetFiles(nippou_base_path + "\\日報\\", "*.md", SearchOption.TopDirectoryOnly));
                existNippouFiles.Sort();

                // 作成する
                using (StreamWriter todayNippou = new StreamWriter(nippou_path, false))
                {
                    // 日付とかテンプレートを挿入
                    todayNippou.WriteLine("# 日報 " + DateTime.Now.ToString("yyyy-MM-dd(ddd)"));
                    todayNippou.WriteLine("## 作業");
                    // 昨日のやつを捜査して翌営業日の作業予定以降を入れる
                    if (existNippouFiles.Count > 0)
                    {
                        string nippou_yesterday_path = existNippouFiles[existNippouFiles.Count - 1];
                        using (StreamReader yesterdayNippou = new StreamReader(nippou_yesterday_path))
                        {
                            string buffer = yesterdayNippou.ReadLine();
                            while (buffer != null)
                            {
                                buffer = yesterdayNippou.ReadLine();
                                if (buffer == "## 翌営業日の作業予定") break;
                            }

                            buffer = yesterdayNippou.ReadLine();
                            while (buffer != null)
                            {
                                todayNippou.WriteLine(buffer);
                                buffer = yesterdayNippou.ReadLine();
                            }
                        }
                    }
                    todayNippou.WriteLine("");

                    todayNippou.WriteLine("## 翌営業日の作業予定\n");
                }

                System.Diagnostics.Process.Start(nippou_path);
            }
        }

        private void button_shuhou_Click(object sender, EventArgs e)
        {
            // 本日分の日報がない場合は一応警告を出す
            string nippou_path = nippou_base_path + "\\日報\\" + DateTime.Now.ToString("日報_yyyy_MM_dd") + ".md";
            if (!File.Exists(nippou_path))
            {
                DialogResult result = MessageBox.Show("本日分の日報がありません。続けますか？", "注意", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel) return;
            }

            // 週報が既にあるなら開く
            string shuhou_path = nippou_base_path + "\\週報\\" + DateTime.Now.ToString("週報_yyyy_MM_dd") + ".md";
            if (File.Exists(shuhou_path))
            {
                System.Diagnostics.Process.Start(shuhou_path);
            }
            else
            {
                // ないならテンプレートから作成
                using (StreamWriter todayShuuho = new StreamWriter(shuhou_path, false))
                {
                    // 日付とかテンプレートを挿入
                    todayShuuho.WriteLine("週報 " + DateTime.Now.AddDays(-(int)DateTime.Now.DayOfWeek + 1).ToString("(yyyy-MM-dd ～ ") + DateTime.Now.ToString("yyyy-MM-dd)"));
                    todayShuuho.WriteLine("1. 総括\n");
                    todayShuuho.WriteLine("2. 今週の主な活動報告\n");
                    // 昨日のやつを捜査して翌営業日の作業予定以降を入れる
                    for (int i = 0; i < (int)DateTime.Now.DayOfWeek; i++)
                    {
                        // 01234 → -4 -3 -2 -1 0
                        // dayofweek-1で出せる？
                        string tmp_shuhou_path = nippou_base_path + "\\日報\\" + DateTime.Now.AddDays(i - (int)DateTime.Now.DayOfWeek + 1).ToString("日報_yyyy_MM_dd") + ".md";
                        if (File.Exists(tmp_shuhou_path))
                        {
                            using (StreamReader yesterdayNippou = new StreamReader(tmp_shuhou_path))
                            {
                                // まず2行すてる
                                yesterdayNippou.ReadLine();
                                yesterdayNippou.ReadLine();

                                string buffer = yesterdayNippou.ReadLine();
                                while (buffer != null)
                                {
                                    todayShuuho.WriteLine(buffer);
                                    buffer = yesterdayNippou.ReadLine();
                                    if (buffer == "## 翌営業日の作業予定") break;
                                }
                                if (i == (int)DateTime.Now.DayOfWeek - 1)
                                {
                                    todayShuuho.WriteLine("3. 次週の予定\n");
                                    buffer = yesterdayNippou.ReadLine();
                                    while (buffer != null)
                                    {
                                        todayShuuho.WriteLine(buffer);
                                        buffer = yesterdayNippou.ReadLine();
                                    }
                                }
                            }
                        }
                    }

                    todayShuuho.WriteLine("4. 問題点\n\n5. 提案・提言\n\n6. その他\n\n7. 出張・イベント予定\n\n以上\n");
                }

                System.Diagnostics.Process.Start(shuhou_path);
            }
        }

        private void button_nippo_cp_Click(object sender, EventArgs e)
        {
            string nippou_path = nippou_base_path + "\\日報\\" + DateTime.Now.ToString("日報_yyyy_MM_dd") + ".md";
            // 日報が既にあるなら開く
            if (File.Exists(nippou_path))
            {
                // あればクリップボードに入れる
                StreamReader todayNippou = new StreamReader(nippou_path);
                Clipboard.SetText(todayNippou.ReadToEnd());
                // 完了表示
                MessageBox.Show("クリップボードにコピーしました");
            }
            else
            {
                // なければ警告表示
                MessageBox.Show("本日の日報がありませんでした");
            }
        }

        private void button_shuho_cp_Click(object sender, EventArgs e)
        {
            string shuuho_path = nippou_base_path + "\\週報\\" + DateTime.Now.ToString("週報_yyyy_MM_dd") + ".md";
            // 日報が既にあるなら開く
            if (File.Exists(shuuho_path))
            {
                // あればクリップボードに入れる
                StreamReader todayShuuho = new StreamReader(shuuho_path);
                Clipboard.SetText(todayShuuho.ReadToEnd());
                // 完了表示
                MessageBox.Show("クリップボードにコピーしました");
            }
            else
            {
                // なければ警告表示
                MessageBox.Show("本日の週報がありませんでした");
            }
        }
    }
}
