using Newtonsoft.Json;

namespace CampusApp.School
{
    internal class Courses
    {
        [JsonProperty("lastid")]
        private int _lastCourseID;

        [JsonProperty]
        internal List<Course> CoursesList { get; private set; }

        internal Courses()
        {
            CoursesList = new();
            _lastCourseID = 0;
        }

        [JsonConstructor]
        internal Courses(List<Course> coursesList)
        {
            CoursesList = coursesList;
            _lastCourseID = coursesList.Last().LessonID;
        }

        internal string GetCourseByID(int ID)
        {
            foreach (Course course in CoursesList)
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
            var newLesson = new Course(nom, _lastCourseID);
            CoursesList.Add(newLesson);
        }

        // Remove a course by its ID
        internal void RemoveLesson(Course course, List<Student> studentsList)
        {
            foreach (Student eleve in studentsList)
            {
                eleve.RemoveCourse(course.LessonID);
            }
            CoursesList.Remove(course);
        }
    }
}
