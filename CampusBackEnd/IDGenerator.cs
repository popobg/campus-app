using CampusBackEnd.DataStorage;
using CampusBackEnd.Interfaces;

namespace CampusBackEnd
{
    internal static class IDGenerator
    {
        internal static int GenerateID<T>(List<T> list) where T : IObjectWithID
        {
            if (list.Count == 0) return 1;
            return list.Last().ID + 1;

            //if(list.Last() is Student student)
            //{
            //    return student.ID + 1;
            //}
            //else if (list.Last() is Course course)
            //{
            //    return course.ID + 1;
            //}

            //throw new Exception("There is no student or course in the given list.");
        }
    }
}
