using CampusBackEnd.Interfaces;

namespace CampusBackEnd.DataModels
{
    public struct Student: IObjectWithID
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public int ID { get; set; }

        public Dictionary<int, List<Grade>> SchoolReport { get; set; }

        internal Student(string firstName, string lastName, DateTime birthDate, int studentID)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.ID = studentID;
            this.SchoolReport = new();
        }

        internal Student(string firstName, string lastName, DateTime birthDate, int studentID, Dictionary<int, List<Grade>> schoolReport)
            : this(firstName, lastName, birthDate, studentID)
        {
            SchoolReport = schoolReport;
        }
    }
}
