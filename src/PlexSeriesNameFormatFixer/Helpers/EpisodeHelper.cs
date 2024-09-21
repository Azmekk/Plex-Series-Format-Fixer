using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PlexSeriesNameFormatFixer.DataObjects;
using System.IO;

namespace PlexSeriesNameFormatFixer.Helpers
{
    public class EpisodeHelper
    {
        private string[] Patterns { get; set; }
        public EpisodeHelper()
        {
            string jsonFilePath = Path.Combine(PathHelper.ExecutablePath, "patterns.json");

            string jsonContent = File.ReadAllText(jsonFilePath);

            PatternData? patternData = JsonSerializer.Deserialize<PatternData>(jsonContent);
            Patterns = patternData?.Patterns ?? [];
        }

        public EpisodeInfo GetNormalizedEpisode(string episodeName)
        {
            string output = "";
            int season = 0;
            int episode = 0;

            foreach(string pattern in Patterns)
            {
                if(!Regex.IsMatch(episodeName, pattern))
                {
                    continue;
                }

                output = Regex.Replace(episodeName, pattern, match =>
                {
                    season = int.Parse(match.Groups["season"].Value);
                    episode = int.Parse(match.Groups["episode"].Value);

                    return $"S{season:D2}E{episode:D2}";
                });
                break;
            }

            return new EpisodeInfo(output, season, episode);
        }
    }
}
