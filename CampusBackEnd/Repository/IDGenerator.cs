using CampusBackEnd.DataStorage;

namespace CampusBackEnd.Repository
{
    internal static class IDGenerator
    {
        internal static int GenerateID<T>(List<T> list)
        {
            if (list.Count == 0) return 1;

            if (list.Last() is Student student)
            {
                return student.StudentID + 1;
            }
            else if (list.Last() is Course course) 
            {
                return course.CourseID + 1;
            }

            throw new ArgumentException("Can't find any student or course ID");
        }
    }
}
