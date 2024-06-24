namespace CampusApp.School
{
    internal struct Lesson
    {
        internal string Name { get; set; }

        internal readonly int LessonID { get; }

        internal Lesson(string nom, int id)
        {
            Name = nom;
            LessonID = id;
        }
    }
}
