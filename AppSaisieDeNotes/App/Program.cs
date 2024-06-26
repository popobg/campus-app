using Serilog.Events;
using Serilog;

namespace CampusApp.App
{
    internal class Program
    {
        static void Main(string[] args)
        {
            App app = JSONSerializer.Deserialize();

            // log initialization
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("..\\..\\..\\logs\\log-.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            .CreateLogger();

            app.ManageMenus();

            JSONSerializer.Serialize(app);
        }
    }
}
