using CampusBackEnd.DataStorage;
using CampusBackEnd.Interfaces;

namespace CampusBackEnd
{
    internal static class IDGenerator
    {
        internal static int GenerateID(List<IObjectWithID> list)
        {
            if (list.Count == 0) return 1;

            // check if the last element of the list is of type Student
            // and convert it into a Student instance

            return list.Last().ID + 1;
        }
    }
}
