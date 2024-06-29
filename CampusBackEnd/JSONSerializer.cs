using CampusBackEnd.DataStorage;
using Newtonsoft.Json;

namespace CampusBackEnd
{
    internal static class JSONSerializer
    {
        public static Campus LoadJSON(string jsonpath)
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

        public static void SaveJSON(Campus campus, string jsonpath)
        {
            // serializing the data (students list and courses list)
            string serializedData = JsonConvert.SerializeObject(campus, Formatting.Indented);

            // Writting in file
            File.WriteAllText(jsonpath, serializedData);
        }
    }

}

