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
        string nippo_base_path = "C:\\Users\\NakataKengo\\Documents\\";

        public Form1()
        {
            InitializeComponent();
            label_date.Text = DateTime.Now.ToString("yyyy/MM/dd");
        }

        private void button_nippou_Click(object sender, EventArgs e)
        {
            string nippo_path = nippo_base_path + "日報/" + DateTime.Now.ToString("日報_yyyy_MM_dd") + ".md";
            string nippo_yesterday_path = nippo_base_path + "日報/" + DateTime.Now.AddDays(-1).ToString("日報_yyyy_MM_dd") + ".md";
            // 日報が既にあるなら開く
            if (File.Exists(nippo_path))
            {
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(nippo_path);
            }
            else
            {
                // ないならテンプレートから作成
                // 前日の日報を探して予定を挿入
                System.Diagnostics.Process p = System.Diagnostics.Process.Start(nippo_yesterday_path);
                using (File.Create(nippo_path)) ;
                p = System.Diagnostics.Process.Start(nippo_path);
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
            // あればクリップボードに入れる
            // 完了表示
            // なければ警告表示
        }

        private void button_shuho_cp_Click(object sender, EventArgs e)
        {
            // あればクリップボードに入れる
            // 完了表示
            // なければ警告表示
        }
    }
}
