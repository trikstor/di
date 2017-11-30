using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReader
{
    public class NormalFormConverter
    {
        private Process Process { get; }
        private readonly string TempFileName = "tempFile.txt";
        public NormalFormConverter(string mystemPath)
        {
            Process = new Process
            {
                StartInfo =
                {
                    FileName = mystemPath,
                    Arguments = $"-n {TempFileName}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
        }
        
        public IEnumerable<string> NormalizeWords(IEnumerable<string> words)
        {
            File.WriteAllLines(TempFileName, words);           
            Process.Start();

            using (var textReader = new StreamReader(Process.StandardOutput.BaseStream, Encoding.UTF8))
            {
                string currStr;
                while ((currStr = textReader.ReadLine())!= null)
                {
                    currStr = currStr.Replace("?", "");
                    var startIndex = currStr.IndexOf('{') + 1;
                    var endIndex = currStr.Contains('|') ? currStr.IndexOf('|') : currStr.IndexOf('}');

                    yield return currStr.Substring(startIndex, endIndex - startIndex);
                }
            }
            Process.WaitForExit();
        }
    }
}