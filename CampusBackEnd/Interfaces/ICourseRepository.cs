using CampusBackEnd.DataStorage;

namespace CampusBackEnd.Interfaces
{
    internal interface ICourseRepository
    {
        public List<Course> GetCourses();

        public Course GetCourse(int courseID);

        public Course AddNewCourse(string fieldName);

        public void RemoveCourse(Course course);
    }
}
