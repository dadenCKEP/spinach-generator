using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        // なさそうなら専用フォームで聞く

        // 


        Application.Run(new Form1());
        }
}
}
