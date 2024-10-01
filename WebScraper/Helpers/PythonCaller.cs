using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopScraper.Helpers
{
    class PythonCaller
    {
        public dynamic RunScraper(string url)
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = GetDirectory() + @"/PythonBinaries/scraper.exe",
                Arguments = $"--url {url}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
            };
            using (Process process = Process.Start(start))
            {
                using (StreamReader reader = process.StandardOutput)
                {
                    string result = reader.ReadToEnd();
                    return result;
                }
            }
        }

        private string GetDirectory()
        {
            var currentDirectory = AppContext.BaseDirectory;
            return Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
        }
    }
}
