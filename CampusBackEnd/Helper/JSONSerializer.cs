using CampusBackEnd.DataModels;
using Newtonsoft.Json;

namespace CampusBackEnd.Helper
{
    internal static class JSONSerializer
    {
        /// <summary>
        /// Deserializes data from json file.
        /// </summary>
        /// <param name="jsonpath">Path of the .json file saving data</param>
        /// <returns>Campus object</returns>
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

        /// <summary>
        /// Saves data from the campus instance (list of students and courses)
        /// into the json file.
        /// </summary>
        /// <param name="campus">Campus object</param>
        /// <param name="jsonpath">Path of the .json file saving data</param>
        public static void Serialize(Campus campus, string jsonpath)
        {
            string serializedData = JsonConvert.SerializeObject(campus, Formatting.Indented);

            File.WriteAllText(jsonpath, serializedData);
        }
    }

}

