using CampusBackEnd.Interfaces;
using Newtonsoft.Json;

namespace CampusBackEnd.DataStorage
{
    public struct Student: IObjectWithID
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public readonly DateTime BirthDate { get; }

        public int ID { get; }

        public readonly Dictionary<int, List<Grade>> SchoolReport { get; }

        internal Student(string firstName, string lastName, DateTime birthDate, int studentID)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.ID = studentID;
            this.SchoolReport = new();
        }

        [JsonConstructor]
        internal Student(string firstName, string lastName, DateTime birthDate, int studentID, Dictionary<int, List<Grade>> schoolReport)
            : this(firstName, lastName, birthDate, studentID)
        {
            SchoolReport = schoolReport;
        }
    }
}
