using CampusBackEnd.DataModels;
using CampusBackEnd.Interfaces;

namespace CampusBackEnd
{
    internal static class IDGenerator
    {
        internal static int GenerateID<T>(List<T> list) where T : IObjectWithID
        {
            if (list.Count == 0) return 1;
            return list.Last().ID + 1;
        }
    }
}
