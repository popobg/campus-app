using CampusBackEnd.Interfaces;
using Newtonsoft.Json;

namespace CampusBackEnd.DataStorage
{
    public readonly struct Course: IObjectWithID
    {
        public string FieldName { get; }

        public int ID { get; }

        [JsonConstructor]
        internal Course(string fieldName, int courseID)
        {
            this.FieldName = fieldName;
            this.ID = courseID;
        }
    }
}
