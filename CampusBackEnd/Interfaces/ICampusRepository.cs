using CampusBackEnd.DataStorage;

namespace CampusBackEnd.Interfaces
{
    internal interface ICampusRepository: ICourseRepository, IStudentRepository {}
}
