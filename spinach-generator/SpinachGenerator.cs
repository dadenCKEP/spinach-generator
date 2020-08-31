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

namespace spinach_generator
{
    public partial class Form1 : Form
    {
        // マイドキュメントのパス
        string nippou_base_path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);

        public Form1()
        {
            InitializeComponent();

            // 日付表示の更新
            label_date.Text = DateTime.Now.ToString("yyyy/MM/dd");

            // フォルダがないなら作成
            if (!Directory.Exists(nippou_base_path + "\\日報")) Directory.CreateDirectory(nippou_base_path + "\\日報");
        }

        private void button_nippou_Click(object sender, EventArgs e)
        {
            string nippou_path = nippou_base_path + "\\日報\\" + DateTime.Now.ToString("日報_yyyy_MM_dd") + ".md";
            // 日報が既にあるなら開く
            if (File.Exists(nippou_path))
            {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(nippou_path);
            }
            else
            {
                // 一番若い日付を探す
                List<string> existNippouFiles = new List<string>();
                existNippouFiles.AddRange(Directory.GetFiles(nippou_base_path + "\\日報\\", "*.md", SearchOption.TopDirectoryOnly));
                existNippouFiles.Sort();
                string nippou_yesterday_path = existNippouFiles[existNippouFiles.Count - 1];

                // 作成する
                using (StreamWriter todayNippou = new StreamWriter(nippou_path, false))
                {
                    // 日付とかテンプレートを挿入
                    todayNippou.WriteLine("# 日報 " + DateTime.Now.ToString("yyyy-MM-dd(ddd)"));
                    todayNippou.WriteLine("## 作業");
                    // 昨日のやつを捜査して翌営業日の作業予定以降を入れる
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
                    todayNippou.WriteLine("");

                    todayNippou.WriteLine("## 翌営業日の作業予定\n");
                }

                System.Diagnostics.Process.Start(nippou_path);
            }
        }

        private void button_shuhou_Click(object sender, EventArgs e)
        {
            // 週報が既にあるなら開く
            // ないならテンプレートから作成
            // 今週分の日報の内容をすべて挿入
            // 予定は最新の分だけ挿入
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
            // あればクリップボードに入れる
            // 完了表示
            // なければ警告表示
        }
    }
}
