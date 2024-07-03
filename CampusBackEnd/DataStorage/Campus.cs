namespace CampusBackEnd.DataStorage
{
    public struct Campus
    {
        public List<Student> Students { get; internal set; }

        public List<Course> Courses { get; internal set; }

        public Campus(List<Student> students, List<Course> courses)
        {
            this.Students = students;
            this.Courses = courses;
        }
    }
}
