using Newtonsoft.Json;

namespace CampusApp.App
{
    internal static class FileHandler
    {
        internal static App Deserialize() 
        {
            string jsonString = File.ReadAllText("..\\..\\..\\..\\campus_app_data.json");

            // If the file is empty, initialize the App class
            // without attribute's values
            if (String.IsNullOrEmpty(jsonString))
            {
                return new App();
            }

            return JsonConvert.DeserializeObject<App>(jsonString);
        }

        internal static void Serialize(App app)
        {
            // serializing the data (students list and courses list)
            string serializedData = JsonConvert.SerializeObject(app, Formatting.Indented);

            // Writting in file
            File.WriteAllText("C:\\Users\\pauli\\dev\\dotnet-formation\\project\\campus-app\\campus_app_data.json", serializedData);
        }
    }
}
