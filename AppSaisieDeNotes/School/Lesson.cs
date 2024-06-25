using Newtonsoft.Json;

namespace CampusApp.School
{
    internal struct Lesson
    {
        [JsonProperty]
        internal string Name { get; set; }

        [JsonProperty]
        internal readonly int LessonID { get; }

        [JsonConstructor]
        internal Lesson(string nom, int id)
        {
            Name = nom;
            LessonID = id;
        }
    }
}
