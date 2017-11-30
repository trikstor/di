using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace TextReader
{
    public class NormalFormConverter
    {
        private Process Process { get; }
        private readonly string TempFileName = "tempFile.txt";
        
        public NormalFormConverter()
        {
            Process = new Process();
            Process.StartInfo.FileName = "mystem.exe";
            Process.StartInfo.Arguments = $"-i {TempFileName}";
            Process.StartInfo.UseShellExecute = false;
            Process.StartInfo.RedirectStandardOutput = true;
        }
        
        public IEnumerable<string> NormalizeWords(IEnumerable<string> words)
        {
            File.WriteAllLines(TempFileName, words);
            
            const string pattern = @"{\.?}";
            var regex = new Regex(pattern);
            
            Process.Start();
            
            using (var textReader = new StreamReader(Process.StandardOutput.BaseStream, Encoding.UTF8))
            {
                string currStr;
                while ((currStr = textReader.ReadLine()) != null)
                {
                    foreach (var word in currStr.Split(
                        new[] {' ', '.', ',', ':', ';', '!', '?', '\t', '–'},
                        StringSplitOptions.RemoveEmptyEntries))
                    {
                        var match = regex.Match(word);
                        yield return match.Groups[0].ToString();
                    }
                }
            }
            Process.WaitForExit();
        }
    }
}