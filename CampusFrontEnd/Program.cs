using Serilog.Events;
using Serilog;
using CampusBackEnd.API;
using CampusFrontEnd.App;
using CampusBackEnd.DataModels;

namespace CampusFrontEnd
{
    internal class Program
    {
        static void Main()
        {
            var api = new API();

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs\\log-.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            .CreateLogger();

            var app = new App.App(api);
            app.RunApp();
        }
    }
}

