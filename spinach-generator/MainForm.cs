using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace spinach_generator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // GUI上の日付表示の更新
            label_date.Text = DateTime.Now.ToString("yyyy/MM/dd");

            // 日報を格納するフォルダがないなら作成
            string tmp = Program.nippouSettings.NippouBasePath + "\\" + Program.nippouSettings.NippouDirName;
            if (!Directory.Exists(tmp))
                Directory.CreateDirectory(tmp);
        }

        /// <summary>
        /// 日報生成ボタン
        /// </summary>
        private void button_nippou_Click(object sender, EventArgs e)
        {
            // 本日の日報がなければ生成
            string nippou_path = Utility.NippouPath(DateTime.Now);
            if (!File.Exists(nippou_path))
            {
                // 既存の日報ファイルを日付順に取得
                List<string> existNippouFiles = new List<string>();
                existNippouFiles.AddRange(Directory.GetFiles(Program.nippouSettings.NippouBasePath + "\\" + Program.nippouSettings.NippouDirName + "\\", "*.md", SearchOption.TopDirectoryOnly));
                existNippouFiles.Sort();

                using (StreamWriter todayNippou = new StreamWriter(nippou_path, false))
                {
                    string nippouTemp = Program.nippouTemplate.template;

                    // <date /> <name /> を置き換える
                    nippouTemp = nippouTemp.Replace("<date />", DateTime.Now.ToString("yyyy-MM-dd"));
                    nippouTemp = nippouTemp.Replace("<name />", Program.nippouSettings.UserName);

                    // 前日日報から引き継ぐ内容
                    string inherited_today = "";  // 前日の「翌日の予定」→ 今日の「本日の活動報告」
                    string inherited_other = "";  // 前日の「その他」       → 今日の「その他」

                    if (existNippouFiles.Count > 0)
                    {
                        string path_prev = existNippouFiles[existNippouFiles.Count - 1];
                        inherited_today = Utility.ExtractSection(path_prev, Program.nippouTemplate.h_tomorrow, Program.nippouTemplate.h_other);
                        inherited_other = Utility.ExtractSection(path_prev, Program.nippouTemplate.h_other, null);
                    }

                    nippouTemp = nippouTemp.Replace("<today />", inherited_today);
                    nippouTemp = nippouTemp.Replace("<other />", inherited_other);

                    // 不要なタグを消す
                    nippouTemp = nippouTemp.Replace("<h_today>", "").Replace("</h_today>", "");
                    nippouTemp = nippouTemp.Replace("<h_tomorrow>", "").Replace("</h_tomorrow>", "");
                    nippouTemp = nippouTemp.Replace("<h_other>", "").Replace("</h_other>", "");
                    nippouTemp = nippouTemp.Replace("<tomorrow />", "");

                    todayNippou.Write(nippouTemp);
                }
            }

            // VSCodeで日報フォルダをワークスペースとして開き、当日ファイルも開く
            string nippouDir = Program.nippouSettings.NippouBasePath + "\\" + Program.nippouSettings.NippouDirName;
            Utility.OpenInVSCode(nippouDir, nippou_path);

            this.Close();
        }

        /// <summary>
        /// 日報をクリップボードにコピーボタン
        /// </summary>
        private void button_nippo_cp_Click(object sender, EventArgs e)
        {
            string path_nippou = Utility.NippouPath(DateTime.Now);
            if (File.Exists(path_nippou))
            {
                StreamReader todayNippou = new StreamReader(path_nippou);
                Clipboard.SetText(todayNippou.ReadToEnd());
                MessageBox.Show("クリップボードにコピーしました");
            }
            else
            {
                MessageBox.Show("本日の日報がありませんでした");
            }

            this.Close();
        }
    }
}
