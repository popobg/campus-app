using Newtonsoft.Json;

namespace CampusApp.School
{
    internal struct Grade
    {
        [JsonProperty]
        internal double Mark { get; set; }

        [JsonProperty]
        internal string Comment { get; set; }

        [JsonConstructor]
        internal Grade(double mark, string comment)
        {
            Mark = mark;
            Comment = comment;
        }
    }
}
