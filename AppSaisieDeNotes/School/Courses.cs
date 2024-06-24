namespace CampusApp.School
{
    internal class Courses
    {
        private int _lastCourseID;

        public List<Lesson> CoursesList { get; private set; }

        internal Courses()
        {
            CoursesList = new();
            _lastCourseID = 0;
        }

        internal Courses(List<Lesson> coursesList)
        {
            CoursesList = coursesList;

            if (coursesList.Count() == 0)
            {
                _lastCourseID = 0;
            }
            else
            {
                _lastCourseID = coursesList.Last().LessonID;
            }
        }

        internal string GetCourseByID(int ID)
        {
            foreach (Lesson course in CoursesList)
            {
                if (course.LessonID == ID)
                {
                    return course.Name;
                }
            }

            return "";
        }

        // Add a new course to the program
        internal void AddLesson(string nom)
        {
            // several lessons can have the same name;
            // they will just not have the same ID
            _lastCourseID++;
            var newLesson = new Lesson(nom, _lastCourseID);
            CoursesList.Add(newLesson);
        }

        // Remove a course by its ID
        internal void RemoveLesson(Lesson course, List<Student> studentsList)
        {
            foreach (Student eleve in studentsList)
            {
                eleve.RemoveCourse(course.LessonID);
            }
            CoursesList.Remove(course);
        }
    }
}
