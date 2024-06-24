namespace CampusApp
{
    internal class Student
    {
        // name can be modified (modifying menu not implemented yet)
        internal string LastName { get; set; }

        internal string FirstName { get; set; }

        internal DateTime BirthDate { get; }

        internal int StudentID { get; }

        // Key : course ID, Value : list of Grade object
        internal Dictionary<int, List<Grade>> SchoolReport { get; }

        internal double GeneralMean { get => CalculateGeneralMean(); }

        internal Student(string firstName, string lastName, DateTime birthDate, int studentID)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BirthDate = birthDate;
            this.StudentID = studentID;
            // empty
            this.SchoolReport = new();
        }

        // METHODES
        // Add a course and grade to the school_report
        // comment can be an empty string
        internal void AddGrade(int courseID, double mark, string comment)
        {
            // Existing course (recognized by ID)?
            if (this.SchoolReport.ContainsKey(courseID))
            {
                this.SchoolReport[courseID].Add(new Grade(mark, comment));
                return;
            }

            this.SchoolReport.Add(courseID, [new Grade(mark, comment)]);
        }

        // Remove course by its ID
        internal void RemoveCourse(int courseID)
        {
            // the student didn't follow this course
            if (!this.SchoolReport.ContainsKey(courseID))
            {
                return;
            }

            this.SchoolReport.Remove(courseID);
        }

        //internal void ModifyGrade(string discipline, double mark)
        //{
        //    if (string.IsNullOrEmpty(discipline) || !(_grades.ContainsKey(discipline)) || mark < 0 || mark > 20)
        //    {
        //        return;
        //    }

        //    this._grades[discipline] = mark;
        //}

        internal double CalculateMeanCourse(List<Grade> grades)
        {
            double total = 0;

            foreach(Grade grade in grades)
            {
                total += grade.Mark;
            }

            return total / grades.Count();
        }

        private double CalculateGeneralMean()
        {
            // no grades
            if (this.SchoolReport.Count() == 0)
            {
                return -1;
            }

            // Contains the means of each course
            List<double> MeansList = new();

            // Contains the lists of grades for each discipline 
            List<Grade>[] allGrades = this.SchoolReport.Values.ToArray();

            // Calculate the mean of each list of grades,
            // so the mean of each discipline
            // add it to the list of means
            foreach (List<Grade> grades in allGrades)
            {
                MeansList.Add(CalculateMeanCourse(grades));
            }

            // Round mean, 1 decimal place
            // ex: 12.2 = 12.0, 12.3 = 12.5, 12.6 = 13
            return Math.Round(MeansList.Average(), 1, MidpointRounding.AwayFromZero);
        }
    }
}
