using Newtonsoft.Json;

namespace CampusBackEnd.DataStorage
{
    public readonly struct Grade
    {
        public double Mark { get; }

        public string Comment { get; }

        [JsonConstructor]
        internal Grade(double mark, string comment)
        {
            Mark = mark;
            Comment = comment;
        }
    }
}