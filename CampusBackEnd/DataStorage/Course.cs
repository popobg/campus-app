using CampusBackEnd.Interfaces;
using Newtonsoft.Json;

namespace CampusBackEnd.DataStorage
{
    public struct Course: IObjectWithID
    {
        public readonly string FieldName { get; }

        public int ID { get; set; }

        [JsonConstructor]
        internal Course(string fieldName, int courseID)
        {
            this.FieldName = fieldName;
            this.ID = courseID;
        }
    }
}