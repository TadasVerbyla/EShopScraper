﻿using System.Diagnostics;
using System.Reflection;

namespace MacysScrapperAPI.Helpers
{
    // Helper class used for calling python scripts and reading their return values
    class PythonCaller
    {
        // Specific caller method for scraper script
        public dynamic RunScraper(string url)
        {

            string exePath = ExtractExe("MacysScraperAPI.PythonBinaries.scraper.exe");
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = exePath,
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

        // Extracts embeded exe into a temporary folder
        private string ExtractExe(string exeName)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), exeName);
            Assembly assembly = Assembly.GetExecutingAssembly();
            using (Stream resourceStream = assembly.GetManifestResourceStream(exeName))
            {
                if (resourceStream == null)
                {
                    throw new Exception("scraper.exe not found.");
                }
                using (FileStream fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
                {
                    resourceStream.CopyTo(fileStream);
                }
            }
            return tempPath;
        }
    }
}