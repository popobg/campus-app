using CampusBackEnd.DataModels;
using Newtonsoft.Json;

namespace CampusBackEnd
{
    internal static class JSONSerializer
    {
        public static Campus Deserialize(string jsonpath)
        {
            try
            {
                string jsonString = File.ReadAllText(jsonpath);
                return JsonConvert.DeserializeObject<Campus>(jsonString);
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException || ex is JsonSerializationException)
                {
                    return new Campus()
                    {
                        Students = new List<Student>(),
                        Courses = new List<Course>()
                    };
                }
                else
                {
                    throw;
                }
            }

        }

        public static void Serialize(Campus campus, string jsonpath)
        {
            // serializing the data (students list and courses list)
            string serializedData = JsonConvert.SerializeObject(campus, Formatting.Indented);

            // Writting in file
            File.WriteAllText(jsonpath, serializedData);
        }
    }

}

