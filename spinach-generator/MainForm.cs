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
using System.Text.RegularExpressions;

namespace spinach_generator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // GUI上の日付表示の更新
            label_date.Text = DateTime.Now.ToString("yyyy/MM/dd");

            // 日報・週報を格納するフォルダがないなら作成
            string tmp = Program.nippouSettings.nippouBasePath + "\\" + Program.nippouSettings.nippouDirName;
            if (!Directory.Exists(tmp))
                Directory.CreateDirectory(tmp);

            tmp = Program.nippouSettings.nippouBasePath + "\\" + Program.nippouSettings.shuhouDirName;
            if (!Directory.Exists(tmp))
                Directory.CreateDirectory(tmp);
        }

        /// <summary>
        /// 日報生成ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nippou_Click(object sender, EventArgs e)
        {
            // 本日の日報がなければ生成
            string nippou_path = Utility.NippouPath(DateTime.Now);
            if (!File.Exists(nippou_path))
            {
                // 今日の日報を作成する前に一番若い日付を探す
                List<string> existNippouFiles = new List<string>();
                existNippouFiles.AddRange(Directory.GetFiles(Program.nippouSettings.nippouBasePath + "\\" + Program.nippouSettings.nippouDirName + "\\", "*.md", SearchOption.TopDirectoryOnly));
                existNippouFiles.Sort();

                // 作成する
                using (StreamWriter todayNippou = new StreamWriter(nippou_path, false))
                {
                    // テンプレートを一旦書き出す
                    string nippouTemp = Program.nippouTemplate.template;

                    // <date>を置き換える
                    nippouTemp = nippouTemp.Replace("<date />", DateTime.Now.ToString("yyyy-MM-dd"));

                    // <name>を置き換える
                    nippouTemp = nippouTemp.Replace("<name />", Program.nippouSettings.userName);

                    // すでにある日報の中で一番新しいものを探し、
                    // その日報の「翌営業日の作業予定」から本日の作業を生成する
                    string yesterday_nippou_tomorrow = "";
                    if (existNippouFiles.Count > 0)
                    {
                        string path_nippou_yesterday = existNippouFiles[existNippouFiles.Count - 1];
                        using (StreamReader yesterdayNippou = new StreamReader(path_nippou_yesterday))
                        {
                            string buffer = yesterdayNippou.ReadLine();
                            while (buffer != null)
                            {
                                buffer = yesterdayNippou.ReadLine();
                                if (buffer == Program.nippouTemplate.h_tomorrow) break;
                            }

                            buffer = yesterdayNippou.ReadLine();
                            while (buffer != null && buffer != Program.nippouTemplate.h_thisweek)
                            {
                                yesterday_nippou_tomorrow += buffer + "\r\n";
                                buffer = yesterdayNippou.ReadLine();
                            }
                        }
                    }
                    nippouTemp = nippouTemp.Replace("<today />", yesterday_nippou_tomorrow);

                    // 不要なタグを消す
                    nippouTemp = nippouTemp.Replace("<h_today>", "").Replace("</h_today>", "");
                    nippouTemp = nippouTemp.Replace("<h_tomorrow>", "").Replace("</h_tomorrow>", "");
                    nippouTemp = nippouTemp.Replace("<h_other>", "").Replace("</h_other>", "");
                    nippouTemp = nippouTemp.Replace("<tomorrow />", "");
                    // nippouTemp = nippouTemp.Replace("<h_thisweek>", "").Replace("</h_thisweek>", "");
                    nippouTemp = Regex.Replace(nippouTemp, "<h_thisweek>.*</h_thisweek>", "");
                    nippouTemp = nippouTemp.Replace("<thisweek />", "");
                    // nippouTemp = nippouTemp.Replace("<h_nextweek>", "").Replace("</h_nextweek>", "");
                    nippouTemp = Regex.Replace(nippouTemp, "<h_nextweek>.*</h_nextweek>", "");
                    nippouTemp = nippouTemp.Replace("<nextweek />", "");

                    // 書き出す
                    todayNippou.Write(nippouTemp);
                }
            }

            // 日報を拡張子に関連付けられたソフトウェアで開く
            System.Diagnostics.Process.Start(nippou_path);

            // 終了
            this.Close();
        }

        /// <summary>
        /// 週報生成ボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_shuhou_Click(object sender, EventArgs e)
        {
            // 今週の週報がないなら作成
            string shuhou_path = Utility.ShuhouPath(DateTime.Now);
            if (!File.Exists(shuhou_path))
            {
                // 今日の日報を作成する前に一番若い日付を探す
                List<string> existNippouFiles = new List<string>();
                existNippouFiles.AddRange(Directory.GetFiles(Program.nippouSettings.nippouBasePath + "\\" + Program.nippouSettings.nippouDirName + "\\", "*.md", SearchOption.TopDirectoryOnly));
                existNippouFiles.Sort();

                // 今週は何日間あったかを計算
                int ThisWeekLength = (int)DateTime.Now.DayOfWeek - 1;   // 本日分は入れない
                if (ThisWeekLength == 0) ThisWeekLength = 7;    // 日曜日なら0でなく7に
                DateTime ThisMonday = DateTime.Now.AddDays(1 - ThisWeekLength); // 月曜日のDateTimeを求める

                using (StreamWriter todayShuuho = new StreamWriter(shuhou_path, false))
                {
                    // テンプレートを一旦書き出す
                    string shuhoTemp = Program.nippouTemplate.template;

                    // <date>を置き換える
                    shuhoTemp = shuhoTemp.Replace("<date />", DateTime.Now.ToString("yyyy-MM-dd"));

                    // <name>を置き換える
                    shuhoTemp = shuhoTemp.Replace("<name />", Program.nippouSettings.userName);

                    // すでにある日報の中で一番新しいものを探し、
                    // その日報の「翌営業日の作業予定」から本日の作業を生成する
                    string yesterday_nippou_tomorrow = "";
                    if (existNippouFiles.Count > 0)
                    {
                        string path_nippou_yesterday = existNippouFiles[existNippouFiles.Count - 1];
                        using (StreamReader yesterdayNippou = new StreamReader(path_nippou_yesterday))
                        {
                            string buffer = yesterdayNippou.ReadLine();
                            while (buffer != null)
                            {
                                buffer = yesterdayNippou.ReadLine();
                                if (buffer == Program.nippouTemplate.h_tomorrow) break;
                            }

                            buffer = yesterdayNippou.ReadLine();
                            while (buffer != null && buffer != Program.nippouTemplate.h_thisweek)
                            {
                                yesterday_nippou_tomorrow += buffer + "\r\n";
                                buffer = yesterdayNippou.ReadLine();
                            }
                        }
                    }
                    shuhoTemp = shuhoTemp.Replace("<today />", yesterday_nippou_tomorrow);


                    // 昨日のやつを捜査して翌営業日の作業予定以降を入れる
                    string nippou_buffer = "";
                    for (int i = 0; i < ThisWeekLength; i++)
                    {
                        // 今週のi日目の日報を参照
                        string tmp_shuhou_path = Utility.NippouPath(ThisMonday.AddDays(i));
                        if (File.Exists(tmp_shuhou_path))
                        {
                            using (StreamReader tmpNippou = new StreamReader(tmp_shuhou_path))
                            {
                                // 日付を入れる
                                nippou_buffer += ThisMonday.AddDays(i).ToString("yyyy-MM-dd(ddd)") + "\r\n";
                                // 一行ずつ読んで本日の作業の部分を書き写す
                                string buffer;
                                while ((buffer = tmpNippou.ReadLine()) != null)
                                {
                                    if (buffer == Program.nippouTemplate.h_today) break;
                                }
                                while ((buffer = tmpNippou.ReadLine()) != null)
                                {
                                    if (buffer == Program.nippouTemplate.h_other) break;
                                    nippou_buffer += buffer + "\r\n";
                                }

                                // 今日の分を書き終わったら次週の予定を挿入
                                // 今日の「翌営業日の作業」を使用する
                                if (i == ThisWeekLength - 1)
                                {
                                    string nippou_tomorrow_buffer = "";
                                    while ((buffer = tmpNippou.ReadLine()) != null)
                                    {
                                        if (buffer == Program.nippouTemplate.h_other) break;
                                        nippou_tomorrow_buffer += buffer + "\r\n";
                                    }
                                    shuhoTemp = shuhoTemp.Replace("<tomorrow />", nippou_tomorrow_buffer);
                                }
                            }
                        }
                    }
                    shuhoTemp = shuhoTemp.Replace("<thisweek />", nippou_buffer);

                    // 不要なタグを消す
                    shuhoTemp = shuhoTemp.Replace("<h_today>", "").Replace("</h_today>", "");
                    shuhoTemp = shuhoTemp.Replace("<h_tomorrow>", "").Replace("</h_tomorrow>", "");
                    shuhoTemp = shuhoTemp.Replace("<tomorrow />", "");
                    shuhoTemp = shuhoTemp.Replace("<h_other>", "").Replace("</h_other>", "");
                    shuhoTemp = shuhoTemp.Replace("<h_thisweek>", "").Replace("</h_thisweek>", "");
                    shuhoTemp = shuhoTemp.Replace("<h_nextweek>", "").Replace("</h_nextweek>", "");
                    shuhoTemp = shuhoTemp.Replace("<nextweek />", "");

                    // 書き出す
                    todayShuuho.Write(shuhoTemp);
                }
            }

            // 週報を拡張子に関連付けられたソフトウェアで開く
            System.Diagnostics.Process.Start(shuhou_path);

            // 終了
            this.Close();
        }

        /// <summary>
        /// 日報をクリップボードにコピーボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_nippo_cp_Click(object sender, EventArgs e)
        {
            string path_nippou = Utility.NippouPath(DateTime.Now); ;
            // 日報が既にあるなら開く
            if (File.Exists(path_nippou))
            {
                // あればクリップボードに入れる
                StreamReader todayNippou = new StreamReader(path_nippou);
                Clipboard.SetText(todayNippou.ReadToEnd());
                // 完了表示
                MessageBox.Show("クリップボードにコピーしました");
            }
            else
            {
                // なければ警告表示
                MessageBox.Show("本日の日報がありませんでした");
            }

            // 終了
            this.Close();
        }

        /// <summary>
        /// 週報をクリップボードにコピーボタン
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_shuho_cp_Click(object sender, EventArgs e)
        {
            string path_shuhou = Utility.ShuhouPath(DateTime.Now);
            // 日報が既にあるなら開く
            if (File.Exists(path_shuhou))
            {
                // あればクリップボードに入れる
                StreamReader todayShuuho = new StreamReader(path_shuhou);
                Clipboard.SetText(todayShuuho.ReadToEnd());
                // 完了表示
                MessageBox.Show("クリップボードにコピーしました");
            }
            else
            {
                // なければ警告表示
                MessageBox.Show("本日の週報がありませんでした");
            }

            // 終了
            this.Close();
        }
    }
}
