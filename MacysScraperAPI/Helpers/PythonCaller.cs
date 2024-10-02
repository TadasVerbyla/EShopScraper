using System.Diagnostics;

namespace MacysScrapperAPI.Helpers
{
    // Helper class used for calling python scripts and reading their return values
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

        // Method to dynamically find the path to main project directory during execution
        private string GetDirectory()
        {
            var currentDirectory = AppContext.BaseDirectory;
            return Directory.GetParent(currentDirectory).Parent.Parent.Parent.FullName;
        }
    }
}