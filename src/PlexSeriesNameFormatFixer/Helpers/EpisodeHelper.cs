using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlexSeriesNameFormatFixer.DataObjects;

namespace PlexSeriesNameFormatFixer.Helpers
{
    internal class EpisodeHelper
    {
        private string[] Patterns { get; set; }
        public EpisodeHelper()
        {
            string exePath = AppDomain.CurrentDomain.BaseDirectory;
            string jsonFilePath = Path.Combine(exePath, "patterns.json");

            string jsonContent = File.ReadAllText(jsonFilePath);

            PatternData? patternData = JsonConvert.DeserializeObject<PatternData>(jsonContent);
            Patterns = patternData?.Patterns ?? [];
        }

        public EpisodeInfo GetNormalizedEpisode(string episodeName)
        {
            string output = "";
            int season = 0;
            int episode = 0;

            foreach (string pattern in Patterns)
            {
                if (!Regex.IsMatch(episodeName, pattern))
                {
                    continue;
                }

                output = Regex.Replace(episodeName, pattern, match =>
                {
                    season = int.Parse(match.Groups[1].Value);
                    episode = int.Parse(match.Groups[2].Value);

                    return $"S{season:D2}E{episode:D2}";
                });
            }

            return new EpisodeInfo(output, season, episode);
        }
    }
}
