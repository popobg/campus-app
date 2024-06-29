using CampusBackEnd.DataStorage;

namespace CampusBackEnd.Interfaces
{
    internal interface IStudentRepository
    {
        List<Student> GetStudents();

        Student GetStudent(int ID);

        Student AddNewGrade(int courseID, double grade, string comment, Student student);

        double CalculateCourseAverage(List<Grade> courseGrades);

        double CalculateGeneralAverage(Student student);
    }
}
