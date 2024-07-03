using CampusBackEnd;
using CampusBackEnd.DataModels;

namespace CampusApp.Tests.Backend.Tests
{
    public class IDGeneratorTest
    {
        // attributes
        private List<Student> studentsEmpty = new();
        private List<Student> students = [new Student("Popo", "bg", DateTime.ParseExact("23/11/1997", "dd/MM/yyyy", null), 2)];
        private List<Course> coursesEmpty = new();
        private List<Course> courses = [new Course() { Name = "Français", ID = 2 }];

        [Fact]
        public void IDGeneratorEmptyListStudents()
        {
            int result = IDGenerator.GenerateID(studentsEmpty);

            Assert.Equal(1, result);
        }

        [Fact]
        public void IDGeneratorEmptyListCourses()
        {
            int result = IDGenerator.GenerateID(coursesEmpty);

            Assert.Equal(1, result);
        }

        [Fact]
        public void IDGeneratorStudents()
        {
            int result = IDGenerator.GenerateID(students);

            Assert.Equal(3, result);
        }

        [Fact]
        public void IDGeneratorCourses()
        {
            int result = IDGenerator.GenerateID(courses);

            Assert.Equal(3, result);
        }
    }
}