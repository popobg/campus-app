using Newtonsoft.Json;

namespace CampusBackEnd.DataStorage
{
    public struct Course
    {
        public readonly string FieldName { get; }

        public readonly int CourseID { get; }

        [JsonConstructor]
        internal Course(string fieldName, int courseID)
        {
            this.FieldName = fieldName;
            this.CourseID = courseID;
        }
    }
}
