using CampusBackEnd.API;
using CampusBackEnd.DataModels;
using Serilog;

namespace CampusFrontEnd.App
{
    internal class App
    {
        private readonly API _api;

        internal App(API api)
        {
            this._api = api;
        }

        // Contains the while loop that checks if the user ask to quit the app;
        // redirects to the correct display function according to the user choice
        internal void RunApp()
        {
            int choice = MenuChoices.MAIN_MENU;

            while (choice != MenuChoices.QUIT_APP)
            {
                switch (choice)
                {
                    case MenuChoices.MAIN_MENU:
                        choice = Menus.DisplayMainMenu();
                        break;

                    case MenuChoices.STUDENTS_MENU:
                        choice = Menus.DisplayStudentsMenu();
                        break;

                    case MenuChoices.COURSES_MENU:
                        choice = Menus.DisplayCoursesMenu();
                        break;

                    case MenuChoices.LIST_STUDENTS:
                        StudentsMenu.DisplayListStudents(this._api.GetStudents());
                        choice = MenuChoices.STUDENTS_MENU;
                        break;

                    case MenuChoices.ADD_STUDENT:
                        StudentsMenu.CreateNewStudent(this._api);
                        choice = MenuChoices.STUDENTS_MENU;
                        break;

                    case MenuChoices.DISPLAY_STUDENT:
                        try
                        {
                            Student studentToDisplay = this.ChooseStudent("De quel élève souhaitez-vous consulter les informations (tapez son index) ?");
                            StudentsMenu.DisplayStudentInfo(this._api, studentToDisplay);
                            choice = MenuChoices.STUDENTS_MENU;
                            break;
                        }
                        catch (Exception)
                        {
                            choice = MenuChoices.STUDENTS_MENU;
                            break;
                        }

                    case MenuChoices.ADD_GRADE:
                        try
                        {
                            Student studentToAssess = this.ChooseStudent("A quel élève voulez-vous ajouter une note (tapez son index) ?");
                            Course courseToAssess = this.ChooseCourse("A quel cours voulez-vous ajouter une note (tapez son index) ?");
                            StudentsMenu.ManageAddingGrade(_api, studentToAssess, courseToAssess);
                            choice = MenuChoices.STUDENTS_MENU;
                            break;
                        }
                        catch (Exception)
                        {
                            choice = MenuChoices.STUDENTS_MENU;
                            break;
                        }

                    case MenuChoices.LIST_COURSES:
                        CoursesMenu.DisplayListCourses(this._api.GetCourses());
                        choice = MenuChoices.COURSES_MENU;
                        break;

                    case MenuChoices.ADD_COURSE:
                        CoursesMenu.ManageAddingCourse(this._api);
                        choice = MenuChoices.COURSES_MENU;
                        break;

                    case MenuChoices.REMOVE_COURSE:
                        try
                        {
                            Course courseToRemove = this.ChooseCourse("Quel cours voulez-vous supprimer (tapez son index) ?");
                            CoursesMenu.ManageRemovingCourse(this._api, courseToRemove);
                            choice = MenuChoices.COURSES_MENU;
                            break;
                        }
                        catch (Exception)
                        {
                            choice = MenuChoices.COURSES_MENU;
                            break;
                        }

                    default:
                        break;
                }
            }

            Console.WriteLine("Fermeture de l'application...");
        }

        internal Student ChooseStudent(string message)
        {
            Console.Clear();
            var students = this._api.GetStudents();

            if (students.Count == 0)
            {
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.WriteLine("Il n'y a pas encore d'élèves dans le campus.\nVous ne pouvez donc consulter les informations d'aucun élève.");
                Console.WriteLine();
                Console.WriteLine(MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);

                Log.Error("Tentative d'accès à un élève inexistant");

                throw new Exception();
            }

            StudentsMenu.DisplayListStudents(students, 0);

            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");

            string input = Console.ReadLine();

            while (!CheckInput.CheckInt(input, 1, students.Count()))
            {
                Console.WriteLine("Le chiffre n'est pas reconnu. Veuillez saisir le numéro d'un élève.");
                Console.Write($">{" ",4}");
                input = Console.ReadLine();
            }

            int inputChoice = Convert.ToInt32(input);

            return students[inputChoice - 1];
        }

        internal Course ChooseCourse(string message)
        {
            Console.Clear();

            var courses = this._api.GetCourses();

            if (courses.Count == 0)
            {
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.WriteLine("Il n'y a pas encore de cours disponibles sur le campus.");
                Console.WriteLine(MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);

                Log.Error("Tentative d'accès à un cours inexistant");

                throw new Exception();
            }

            CoursesMenu.DisplayListCourses(courses, 0);

            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");

            string input = Console.ReadLine();

            while (!CheckInput.CheckInt(input, 1, courses.Count()))
            {
                Console.WriteLine("Le chiffre n'est pas reconnu. Veuillez saisir le numéro d'un cours.");
                Console.Write($">{" ",4}");
                input = Console.ReadLine();
            }

            int inputChoice = Convert.ToInt32(input);

            return courses[inputChoice - 1];
        }

        internal static string ConfirmationLoop(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");

            string confirmation = Console.ReadLine().ToLower();

            while (!CheckInput.ConfirmationCheck(confirmation))
            {
                Console.WriteLine(message);
                Console.Write($">{" ",4}");
                confirmation = Console.ReadLine().ToLower();
            }

            return confirmation;
        }
    }
}
