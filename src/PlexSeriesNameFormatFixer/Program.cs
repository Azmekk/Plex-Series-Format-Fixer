using PlexSeriesNameFormatFixer.DataObjects;
using PlexSeriesNameFormatFixer.Helpers;
using Spectre.Console;

string path = AnsiConsole.Prompt(new TextPrompt<string>("[red]Folder path (leave empty to use relative):[/]"));

if(string.IsNullOrEmpty(path))
{
    path = "./";
}

path = Path.GetFullPath(path);

PathHelper pathHelper = new();

List<string> directories = [path];

AnsiConsole.Status()
    .Spinner(Spinner.Known.Star)
    .Start("[yellow]Locating subdirectories...[/]", ctx =>
    {
        directories.AddRange(pathHelper.GetAllSubdirectories(path));
    });

AnsiConsole.MarkupLine("[green]Located subdirectories.[/]");

using FileStream fs = File.Create(Path.Join(path, $"Rename_Log_{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssZ}.txt"));
using StreamWriter logWriter = new(fs);
logWriter.WriteLine("The following files were renamed: ");

List<string> files = [];
EpisodeHelper episodeHelper = new();

AnsiConsole.Status()
    .Spinner(Spinner.Known.Star)
    .Start("[yellow]Renaming files...[/]", ctx =>
    {
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
                    logWriter.WriteLine($"{oldName} -> {epInfo.Name}");
                }
            }
        }
    });

AnsiConsole.MarkupLine("[green]Renamed files.[/]");