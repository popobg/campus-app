using CampusBackEnd.Interfaces;
using CampusBackEnd.Repository;
using CampusBackEnd.DataModels;

namespace CampusBackEnd.API
{
    /// <summary>
    /// Exposes public methods that can be called by the FrontEnd project.
    /// Link between backend (database management and operations) and frontend (UI).
    /// </summary>
    public class API
    {
        private ICampusRepository _campusRepository;

        public API()
        {
            this._campusRepository = new CampusRepository();
        }

        // STUDENTS
        public List<Student> GetStudents()
        {
            return this._campusRepository.GetStudents();
        }

        public void AddStudents(string firstName, string lastName, DateTime birthDate)
        {
            this._campusRepository.AddStudent(firstName, lastName, birthDate);
        }

        public Student AddGrade(int courseID, double grade, string comment, Student student)
        {
            return this._campusRepository.AddGrade(courseID, grade, comment, student);
        }

        public double CalculateCourseAverage(List<Grade> courseGrades)
        {
            return this._campusRepository.CalculateCourseAverage(courseGrades);
        }

        public double CalculateGeneralAverage(Student student)
        {
            return this._campusRepository.CalculateGeneralAverage(student);
        }

        // COURSES
        public List<Course> GetCourses()
        {
            return this._campusRepository.GetCourses();
        }

        public Course GetCourse(int ID)
        {
            return this._campusRepository.GetCourse(ID);
        }

        public Course AddCourse(string name)
        {
            return this._campusRepository.AddCourse(name);
        }

        public void RemoveCourse(Course course)
        {
            this._campusRepository.RemoveCourse(course);
        }
    }
}
