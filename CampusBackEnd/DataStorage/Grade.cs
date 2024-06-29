using Newtonsoft.Json;

namespace CampusBackEnd.DataStorage
{
    public struct Grade
    {
        public double Mark { get; private set; }

        public string Comment { get; private set; }

        [JsonConstructor]
        internal Grade(double mark, string comment)
        {
            Mark = mark;
            Comment = comment;
        }
    }
}
