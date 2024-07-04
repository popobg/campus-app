# CAMPUS APP
### First project of the .NET training course

A console app for teachers with a user interface that allows to manage courses and add students and grades.\
The interface is written in french.

## Technologies
### C#.NET
The program is written in C# using the framework .NET Core 8.0.

### Newtonsoft.JSON
The data is recorded on a JSON file.

The library Newtonsoft.JSON is used to manipulate data in JSON format.

### Serilog
User's activity on the app is recorded on a log file.

Serilog is a logging framework for .NET.\
It is a tool with fully-structured events that simplifies a lot the log management.

## App environment
The app works on MacOS, Linux and Windows 10+.

## App presentation
The program creates a json file, and a log file at the same place as the executable file.\
If you want to give your own data, call your json file "campus.json" and place it at the executable file location.

### Main menu
The start screen is a menu that gives you three options:
1. Display students menu
2. Display courses menu
3. Quit the app

In all the app, you can navigate through the menus by giving the option number you choose.

### Students menu
You can choose between the following options:
1. Display a list of registered students
2. Add a new student to the list (full name and age)
3. Display the information about a student (full name, age, school report and general average)
4. Add field's grade and comment related to a course in the school report of a student
5. Go back to the main menu

To read about a student, the student must already be recorded on the database.\
To add grades and comments related to a course, the course field must have already been recorded on the database.

### Courses menu
You can choose between the following options:
1. Display a list of the courses
2. Add a new course to the school program
3. Remove a course from the program
4. Go back to the main menu
