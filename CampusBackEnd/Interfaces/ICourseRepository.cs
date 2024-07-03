using CampusBackEnd.DataModels;

namespace CampusBackEnd.Interfaces
{
    internal interface ICourseRepository
    {
        public List<Course> GetCourses();

        public Course GetCourse(int courseID);

        public Course AddCourse(string fieldName);

        public void RemoveCourse(Course course);
    }
}
