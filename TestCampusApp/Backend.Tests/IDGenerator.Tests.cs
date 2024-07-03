using CampusBackEnd;
using CampusBackEnd.DataStorage;

namespace CampusApp.Tests.Backend.Tests
{
    public class IDGeneratorTest
    {
        // attributes
        private List<int> numbers = [1, 2, 3, 4];
        private List<Student> studentsEmpty = new();
        private List<Student> students = [new Student("Popo", "bg", DateTime.ParseExact("23/11/1997", "dd/MM/yyyy", null), 2)];
        private List<Course> coursesEmpty = new();
        private List<Course> courses = [new Course("Français", 2)];

        [Fact]
        public void IDGeneratorWrongArgGiven()
        {
            bool exceptionThrown = false;
            try
            {
                int result = IDGenerator.GenerateID(numbers);
            }
            catch (Exception)
            {
                exceptionThrown = true;
            }

            Assert.True(exceptionThrown);
        }

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