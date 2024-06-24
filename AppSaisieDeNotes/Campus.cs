namespace CampusApp
{
    internal class Campus
    {
        // ID of the last student on the list
        private int _lastStudentID;

        public List<Student> StudentsList { get; }

        internal Campus()
        {
            this.StudentsList = new();
            this._lastStudentID = 0;
        }

        internal Campus(List<Student> Eleve)
        {
            this.StudentsList = Eleve;
            this._lastStudentID = Eleve.Last().StudentID;
        }

        // "Créer un nouvel élève"
        internal void AddStudent(string firstName, string lastName, DateTime birthDate)
        {
            // no need to check for a duplicate because two students with the same name and birthday is possible
            this._lastStudentID++;
            // unique ID for each student
            var eleve = new Student(firstName, lastName, birthDate, this._lastStudentID);
            this.StudentsList.Add(eleve);
        }
    }
}
