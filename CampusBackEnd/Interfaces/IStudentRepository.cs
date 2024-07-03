using CampusBackEnd.DataStorage;

namespace CampusBackEnd.Interfaces
{
    internal interface IStudentRepository
    {
        public List<Student> GetStudents();

        public void AddNewStudent(string firstName, string lastName, DateTime birthDate);

        public Student AddNewGrade(int courseID, double grade, string comment, Student student);

        public double CalculateCourseAverage(List<Grade> courseGrades);

        public double CalculateGeneralAverage(Student student);
    }
}
