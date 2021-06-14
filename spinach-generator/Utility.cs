using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spinach_generator
{
    class Utility
    {
        /// <summary>
        /// 日付に対応した日報のファイルパスを生成して返す
        /// </summary>
        /// <param name="date">日報の日付</param>
        /// <returns>日付に対応した日報のファイルパス</returns>
        public static string NippouPath(DateTime date)
        {
            return Program.nippouSettings.nippouBasePath + "\\" + Program.nippouSettings.nippouDirName + "\\" + date.ToString("日報_yyyy_MM_dd") + ".md"; ;
        }

        /// <summary>
        /// 日付に対応した週報のファイルパスを生成して返す
        /// </summary>
        /// <param name="date">週報の日付</param>
        /// <returns>日付に対応した週報のファイルパス</returns>
        public static string ShuhouPath(DateTime date)
        {
            return Program.nippouSettings.nippouBasePath + "\\" + Program.nippouSettings.shuhouDirName + "\\" + date.ToString("週報_yyyy_MM_dd") + ".md"; ;
        }

    }
}
