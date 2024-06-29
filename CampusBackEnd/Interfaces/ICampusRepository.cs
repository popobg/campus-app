using CampusBackEnd.DataStorage;

namespace CampusBackEnd.Interfaces
{
    internal interface ICampusRepository: ICourseRepository, IStudentRepository
    {
        //int GetNewID(List<Student> students);

        //int GetNewID(List<Course> courses);

        Course AddNewItem(string fieldName);

        void AddNewItem(string firstName, string lastName, DateTime birthDate);
    }
}
