using Newtonsoft.Json;

namespace CampusApp.School
{
    internal struct Course
    {
        [JsonProperty]
        internal string Name;

        [JsonProperty]
        internal readonly int LessonID;

        internal Course(string nom, int id)
        {
            Name = nom;
            LessonID = id;
        }
    }
}
