using CampusBackEnd.DataModels;

namespace CampusBackEnd.Interfaces
{
    internal interface ICampusRepository: ICourseRepository, IStudentRepository {}
}
