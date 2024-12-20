using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ClassLibrary1
{
    public static class TraceToFile
    {
        public static void WriteLine(string fileName, string line)
        {
            WriteLine(Directory.GetCurrentDirectory(), fileName, line);
        }

        public static void WriteLine(string directoryPath, string fileName, string line)
        {
            try
            {
                using (StreamWriter sw = File.AppendText(Path.Combine(directoryPath, fileName)))
                {
                    sw.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} => {line}");
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
