using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacysScrapperAPI.Helpers
{
    class PythonCaller
    {
        public dynamic RunScraper(string url)
        {
            ProcessStartInfo start = new ProcessStartInfo
            {
                FileName = FindFileInParentDirectories("scraper.exe"),
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

        static string FindFileInParentDirectories(string fileName)
        {
            // Start from the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Search in all parent directories
            while (!string.IsNullOrEmpty(currentDirectory))
            {
                // Check if the file exists in the current directory
                string filePath = Path.Combine(currentDirectory, fileName);
                if (File.Exists(filePath))
                {
                    return filePath; // Return the found file path
                }

                // Search in all child directories of the current directory
                string[] directories = Directory.GetDirectories(currentDirectory);
                foreach (string directory in directories)
                {
                    string foundPath = SearchInDirectory(directory, fileName);
                    if (!string.IsNullOrEmpty(foundPath))
                    {
                        return foundPath; // Return the found file path
                    }
                }

                // Move to the parent directory
                currentDirectory = Directory.GetParent(currentDirectory)?.FullName;
            }

            return null; // Return null if the file is not found
        }

        static string SearchInDirectory(string directory, string fileName)
        {
            // Check if the file exists in the current directory
            string filePath = Path.Combine(directory, fileName);
            if (File.Exists(filePath))
            {
                return filePath; // Return the found file path
            }

            // Search in all child directories of the current directory
            string[] directories = Directory.GetDirectories(directory);
            foreach (string subdirectory in directories)
            {
                string foundPath = SearchInDirectory(subdirectory, fileName);
                if (!string.IsNullOrEmpty(foundPath))
                {
                    return foundPath; // Return the found file path
                }
            }

            return null; // Return null if the file is not found
        }
    }
}
