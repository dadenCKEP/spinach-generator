using System;
using System.Collections.Generic;
using System.IO;

namespace spinach_generator
{
    class Utility
    {
        /// <summary>
        /// 日付に対応した日報のファイルパスを生成して返す
        /// </summary>
        public static string NippouPath(DateTime date)
        {
            return Program.nippouSettings.NippouBasePath + "\\" + Program.nippouSettings.NippouDirName + "\\" + date.ToString("日報_yyyy_MM_dd") + ".md";
        }

        /// <summary>
        /// 指定ファイルから startHeader の行から endHeader の行（または EOF）直前までの内容を返す。
        /// 見出し行自体は含まない。末尾の空行はトリムする。
        /// </summary>
        /// <param name="filePath">読み取るMarkdownファイルのパス</param>
        /// <param name="startHeader">対象セクションの見出し文字列</param>
        /// <param name="endHeader">次セクションの見出し文字列（null なら EOF まで）</param>
        public static string ExtractSection(string filePath, string startHeader, string endHeader = null)
        {
            var lines = new List<string>();
            bool inSection = false;

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (!inSection)
                    {
                        if (line == startHeader) inSection = true;
                    }
                    else
                    {
                        if (endHeader != null && line == endHeader) break;
                        lines.Add(line);
                    }
                }
            }

            // 末尾の空行を除去
            while (lines.Count > 0 && string.IsNullOrWhiteSpace(lines[lines.Count - 1]))
                lines.RemoveAt(lines.Count - 1);

            return string.Join(Environment.NewLine, lines);
        }

        /// <summary>
        /// VSCodeで指定フォルダをワークスペースとして開き、同時に指定ファイルも開く。
        /// VSCodeが見つからない場合は関連付けアプリで開く。
        /// </summary>
        public static void OpenInVSCode(string folderPath, string filePath)
        {
            string[] candidatePaths = new[]
            {
                "code",
                System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    @"Programs\Microsoft VS Code\bin\code.cmd"),
                @"C:\Program Files\Microsoft VS Code\bin\code.cmd",
                @"C:\Program Files (x86)\Microsoft VS Code\bin\code.cmd",
            };

            foreach (string codePath in candidatePaths)
            {
                try
                {
                    var psi = new System.Diagnostics.ProcessStartInfo
                    {
                        FileName = codePath,
                        Arguments = $"--reuse-window \"{folderPath}\" \"{filePath}\"",
                        UseShellExecute = false,   // シェル経由をやめる
                        CreateNoWindow = true,     // コンソールウィンドウを出さない
                    };
                    System.Diagnostics.Process.Start(psi);
                    return;
                }
                catch { }
            }

            // フォールバック：関連付けアプリで開く
            System.Diagnostics.Process.Start(filePath);
        }
    }
}
