using Newtonsoft.Json;

namespace CampusBackEnd.DataModels
{
    public struct Campus
    {
        public List<Student> Students { get; set; }

        public List<Course> Courses { get; set; }
    }
}
