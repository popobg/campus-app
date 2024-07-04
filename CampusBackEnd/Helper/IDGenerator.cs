using CampusBackEnd.Interfaces;

namespace CampusBackEnd.Helper
{
    internal static class IDGenerator
    {
        /// <summary>
        /// Generate a unique ID from the last ID of the given list.
        /// </summary>
        /// <typeparam name="T">List<Student> ou List<Course></typeparam>
        /// <param name="list">list of objects of type Student or Course</param>
        /// <returns></returns>
        internal static int GenerateID<T>(List<T> list) where T : IObjectWithID
        {
            if (list.Count == 0) return 1;
            return list.Last().ID + 1;
        }
    }
}
