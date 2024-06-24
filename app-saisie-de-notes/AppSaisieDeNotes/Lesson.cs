namespace CampusApp
{
    internal struct Lesson
    {
        internal string Name { get; set; }

        internal readonly int LessonID { get; }

        internal Lesson(string nom, int id)
        {
            this.Name = nom;
            this.LessonID = id;
        }
    }
}
