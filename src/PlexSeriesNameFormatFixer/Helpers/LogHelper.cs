using Spectre.Console;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexSeriesNameFormatFixer.Helpers
{
    public class LogHelper : IDisposable
    {
        private readonly static string LogName = $"Rename_Log_{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssZ}.txt";
        private string LogFilePath { get; set; }
        
        private FileStream LogFileFilestream { get; set; }
        private StreamWriter LogFileStreamWriter { get; set; }

        public LogHelper()
        {
            LogFilePath = CreateLogFile();
            LogFileFilestream = new(LogFilePath, FileMode.Append, FileAccess.Write);
            LogFileStreamWriter = new(LogFileFilestream);
        }

        private int WrittenLines { get; set; } = 0;

        private static string CreateLogFile()
        {
            var logsDirectory = Path.Join(PathHelper.ExecutablePath, "Logs");
            Directory.CreateDirectory(logsDirectory);

            var logPath = Path.Join(logsDirectory, LogName);
            if(!File.Exists(logPath))
            {
                File.Create(logPath).Close();
            }

            return logPath;
        }

        public void LogVideoNameChange(string oldName, string newName)
        {
            if(WrittenLines == 0)
            {
                AppendToFile("Renamed files:");
            }

            AppendToFile(oldName + " -> " + newName);
        }

        private void AppendToFile(string line)
        {
            LogFileStreamWriter.WriteLine(line);
            WrittenLines++;

            LogFileStreamWriter.Flush();
        }

        public void Dispose()
        {
            if(WrittenLines == 0)
            {
                AppendToFile("No changes.");
            }

            LogFileStreamWriter.Dispose();
            LogFileFilestream.Dispose();

            AnsiConsole.MarkupLine($"[darkorange]Output log file: {LogFilePath}[/]");

            GC.SuppressFinalize(this);
        }
    }
}
