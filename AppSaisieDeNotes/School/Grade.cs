using Newtonsoft.Json;

namespace CampusApp.School
{
    internal struct Grade
    {
        private double _mark;
        private string _comment;

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
