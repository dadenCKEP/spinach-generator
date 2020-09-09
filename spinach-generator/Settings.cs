using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace spinach_generator
{
    public partial class Settings : Form
    {
        bool priventClosing = false;
        public Settings()
        {
            InitializeComponent();
        }

        private void button_selectDir_Click(object sender, EventArgs e)
        {
            // フォルダ選択
            folderBrowserDialog1.ShowDialog();
            textBox_dirPath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void button_create_Click(object sender, EventArgs e)
        {
            // バリデーション
            // 空欄がないかどうか
            if (textBox_dirPath.Text == "" || textBox_nippouDir.Text == "" || textBox_shuhouDir.Text == "")
            {
                MessageBox.Show("全ての欄を埋める必要があります。");
                priventClosing = true;
            }

            // 格納
            Program.nippouSettings.nippouBasePath = textBox_dirPath.Text;
            Program.nippouSettings.nippouDirName = textBox_nippouDir.Text;
            Program.nippouSettings.nippouBasePath = textBox_shuhouDir.Text;

            // ファイル書き出し
            using (StreamWriter sw = new StreamWriter("./config.json", false))
            {
                sw.Write(JsonSerializer.Serialize(Program.nippouSettings));
            }

            // 問題ない場合はOKを送出
            DialogResult = DialogResult.OK;
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (priventClosing)
            {
                priventClosing = false;
                e.Cancel = true;
            }
        }
    }
}
