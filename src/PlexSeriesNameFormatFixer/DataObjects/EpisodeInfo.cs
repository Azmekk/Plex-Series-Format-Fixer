using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlexSeriesNameFormatFixer.DataObjects
{
    internal class EpisodeInfo(string name, int season, int episode)
    {
        public int Season { get; set; } = season;
        public int Episode { get; set; } = episode;
        public string Name { get; set; } = name;
    }
}
