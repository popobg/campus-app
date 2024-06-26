﻿using Serilog.Events;
using Serilog;
using CampusBackEnd.API;
using CampusFrontEnd.App;

namespace CampusFrontEnd
{
    internal class Program
    {
        static void RunApp(API api)
        {
            var studentsMenu = new StudentsMenu(api);
            var coursesMenu = new CoursesMenu(api);
            var menu = new Menu(api, studentsMenu, coursesMenu);
            
            studentsMenu.Menu = menu;
            coursesMenu.Menu = menu;

            menu.ManageMenus();
        }

        static void Main()
        {
            // backend initialization
            var api = new API();

            // log initialization
            // RollingInterval.Day ensures that a new log file
            // is created every day
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.File("logs\\log-.txt", LogEventLevel.Information, rollingInterval: RollingInterval.Day)
            .CreateLogger();

            RunApp(api);
        }
    }
}

