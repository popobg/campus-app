using CampusBackEnd.Interfaces;
using Newtonsoft.Json;

namespace CampusBackEnd.DataModels
{
    public struct Course: IObjectWithID
    {
        public string Name { get; set; }

        public int ID { get; set; }
    }
}