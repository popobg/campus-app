using CampusBackEnd.Interfaces;
using CampusBackEnd.DataStorage;

namespace CampusBackEnd.Repository
{
    internal class CampusRepository: ICampusRepository
    {
        private const string _jsonPath = "campus.json";
        private Campus _campus;

        // rajouter une interface avec les méthodes pour choper le JSON
        // éventuellement une méthode de SaveJSON appelée chaque fois qu'une action de modification est effectuée sur le JSON
        internal CampusRepository()
        {
            _campus = JSONSerializer.LoadJSON(_jsonPath);
        }

        // STUDENTS
        public List<Student> GetStudents()
        {
            return this._campus.Students;
        }

        // pas sûre de le laisser là lui --> peut-être plutôt dans le FrontEnd
        public Student GetStudent(int ID)
        {
            foreach (Student student in this._campus.Students)
            {
                if (student.StudentID == ID) return student;
            }

            throw new Exception("Il n'y a pas d'élève à cet ID.");
        }

        public Student AddNewGrade(int courseID, double grade, string comment, Student student)
        {
            if (student.SchoolReport.ContainsKey(courseID))
            {
                // add a new grade to a course that already has grades
                student.SchoolReport[courseID].Add(new Grade(grade, comment));
            }
            else
            {
                // add new courseID (key) and grade to the dictionary
                student.SchoolReport.Add(courseID, [new Grade(grade, comment)]);
            }

            JSONSerializer.SaveJSON(_campus, _jsonPath);

            return student;
        }

        public double CalculateCourseAverage(List<Grade> courseGrades)
        {
            double total = 0;

            foreach (Grade grade in courseGrades)
            {
                total += grade.Mark;
            }

            return total / courseGrades.Count;
        }

        public double CalculateGeneralAverage(Student student)
        {
            if (student.SchoolReport.Count() == 0)
            {
                // no grades, no average
                return -1;
            }

            List<double> averages = new();

            List<Grade>[] allGrades = student.SchoolReport.Values.ToArray();

            foreach(List<Grade> courseGrades in allGrades)
            {
                averages.Add(this.CalculateCourseAverage(courseGrades));
            }

            return Math.Round(averages.Average(), 1, MidpointRounding.AwayFromZero);
        }

        // COURSES
        public List<Course> GetCourses()
        {
            return this._campus.Courses;
        }

        // pas sûre de le laisser là lui --> peut-être plutôt dans le FrontEnd
        public Course GetCourse(int ID)
        {
            foreach (Course course in this._campus.Courses)
            {
                if (course.CourseID == ID) return course;
            }

            throw new Exception("Il n'y a pas de cours à cet ID.");
        }

        public void RemoveCourse(Course course)
        {
            int courseID = course.CourseID;

            foreach(Student student in this._campus.Students)
            {
                if (!student.SchoolReport.ContainsKey(courseID))
                {
                    continue;
                }

                student.SchoolReport.Remove(courseID);
            }

            this._campus.Courses.Remove(course);

            JSONSerializer.SaveJSON(_campus, _jsonPath);
        }

        // methods using polymorphism
        public void AddNewItem(string firstName, string lastName, DateTime birthDate)
        {
            Student newStudent = new Student(firstName, lastName, birthDate, IDGenerator.GenerateID(this._campus.Students));

            this._campus.Students.Add(newStudent);

            JSONSerializer.SaveJSON(_campus, _jsonPath);
        }

        public Course AddNewItem(string fieldName)
        {
            Course newCourse = new Course(fieldName, IDGenerator.GenerateID(this._campus.Courses));

            this._campus.Courses.Add(newCourse);

            JSONSerializer.SaveJSON(_campus, _jsonPath);

            return newCourse;
        }
    }
}
