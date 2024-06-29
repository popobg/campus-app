using CampusBackEnd.DataStorage;

namespace CampusBackEnd.Interfaces
{
    internal interface ICourseRepository
    {
        public List<Course> GetCourses();

        public Course GetCourse(int ID);

        public void RemoveCourse(Course course);
    }
}
