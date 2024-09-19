using PlexSeriesNameFormatFixer.DataObjects;
using PlexSeriesNameFormatFixer.Helpers;
using System.Text;

string[] mediaExtensions = [".mp4", ".mov", ".avi", ".mkv", ".mp3", ".wav", ".flac", ".m4a", ".wmv", ".webm"];

Console.WriteLine("Detecting folders...");
string[] arguments = Environment.GetCommandLineArgs();

string path = "";
if(arguments.Length > 1)
{
    path = arguments[1];
}

if(string.IsNullOrEmpty(path))
{
    path = "./";
}

path = Path.GetFullPath(path);

PathHelper pathHelper = new();

List<string> directories = [path];
directories.AddRange(pathHelper.GetAllSubdirectories(path));

Console.WriteLine("Done.\nRenaming files...");
List<string> files = [];
EpisodeHelper episodeHelper = new();

foreach(var dir in directories)
{
    List<string> dirFiles = pathHelper.GetFiles(dir);

    foreach(string filePath in dirFiles)
    {
        string oldName = Path.GetFileName(filePath);
        EpisodeInfo epInfo = episodeHelper.GetNormalizedEpisode(oldName);

        if(!string.IsNullOrEmpty(epInfo.Name))
        {
            File.Move(filePath, Path.Join(dir, epInfo.Name));
            Console.WriteLine($"Renamed: {oldName} To: {epInfo.Name}");
        }
    }
}
Console.WriteLine("Done.");