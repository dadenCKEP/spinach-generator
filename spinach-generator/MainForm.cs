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
                    nippouTemp = nippouTemp.Replace("<h_nextweek>", "").Replace("</h_nextweek>", "");
                    // nippouTemp = Regex.Replace(nippouTemp, "<h_nextweek>.*</h_nextweek>", "");
                    // nippouTemp = nippouTemp.Replace("<nextweek />", "");

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
