namespace PlexSeriesNameFormatFixer.Helpers
{
    public class PathHelper
    {
        public List<string> GetAllSubdirectories(string targetDirectory)
        {
            List<string> dirs = [];
            string[] subdirectoryEntries = Directory.GetDirectories(targetDirectory);
            dirs.AddRange(subdirectoryEntries);

            foreach (string subdirectory in subdirectoryEntries)
                dirs.AddRange(GetAllSubdirectories(subdirectory));

            return dirs;
        }

        public List<string> GetFiles(string targetDirectory)
        {
            List<string> paths = [];
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            paths.AddRange(fileEntries);

            return paths;
        }
    }
}
