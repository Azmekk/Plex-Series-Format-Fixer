using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PlexSeriesNameFormatFixer.DataObjects
{
    public class PatternData
    {
        public PatternData()
        {
            //Empty 
        }

        [JsonPropertyName("patterns")]
        public string[] Patterns { get; set; }
    }
}
