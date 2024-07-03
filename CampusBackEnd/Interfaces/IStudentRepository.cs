using CampusBackEnd.DataModels;

namespace CampusBackEnd.Interfaces
{
    internal interface IStudentRepository
    {
        public List<Student> GetStudents();

        public void AddStudent(string firstName, string lastName, DateTime birthDate);

        public Student AddGrade(int courseID, double grade, string comment, Student student);

        public double CalculateCourseAverage(List<Grade> courseGrades);

        public double CalculateGeneralAverage(Student student);
    }
}
