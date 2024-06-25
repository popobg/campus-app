using Newtonsoft.Json;

namespace CampusApp.School
{
    internal class Campus
    {
        // ID of the last student on the list
        private int _lastStudentID;

        [JsonProperty]
        internal List<Student> StudentsList { get; }

        internal Campus()
        {
            StudentsList = new();
            _lastStudentID = 0;
        }

        [JsonConstructor]
        internal Campus(List<Student> students)
        {
            StudentsList = students;
            _lastStudentID = students.Last().StudentID;
        }

        // "Créer un nouvel élève"
        internal void AddStudent(string firstName, string lastName, DateTime birthDate)
        {
            // no need to check for a duplicate because two students with the same name and birthday is possible
            _lastStudentID++;
            // unique ID for each student
            var eleve = new Student(firstName, lastName, birthDate, _lastStudentID);
            StudentsList.Add(eleve);
        }
    }
}
