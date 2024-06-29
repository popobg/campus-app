using Newtonsoft.Json;

namespace CampusBackEnd.DataStorage
{
    public struct Student
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public readonly DateTime BirthDate { get; }

        public readonly int StudentID { get; }

        public readonly Dictionary<int, List<Grade>> SchoolReport { get; }

        internal Student(string lastName, string firstName, DateTime birthDate, int studentID)
        {
            this.LastName = lastName;
            this.FirstName = firstName;
            this.BirthDate = birthDate;
            this.StudentID = studentID;
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
