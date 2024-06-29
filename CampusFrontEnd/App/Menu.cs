using CampusBackEnd.API;
using CampusBackEnd.DataStorage;
using Serilog;

namespace CampusFrontEnd.App
{
    internal class Menu
    {
        internal API _api;
        private readonly StudentsMenu _studentsMenu;
        private readonly CoursesMenu _coursesMenu;

        internal Menu(API api, StudentsMenu studentsMenu, CoursesMenu coursesMenu)
        {
            this._api = api;
            this._studentsMenu = studentsMenu;
            this._coursesMenu = coursesMenu;
        }

        internal void ManageMenus(int choice = MenuChoices.MAIN_MENU)
        {
            switch (choice)
            {
                case MenuChoices.MAIN_MENU:
                    this.DisplayMainMenu();
                    break;

                case MenuChoices.STUDENTS_MENU:
                    this._studentsMenu.DisplayStudentsMenu();
                    break;

                case MenuChoices.COURSES_MENU:
                    this._coursesMenu.DisplayCoursesMenu();
                    break;

                case MenuChoices.LIST_STUDENTS:
                    Console.Clear();
                    this._studentsMenu.DisplayListStudents(this.GetStudents());
                    Console.ReadKey(false);
                    // come back to the students menu (previous menu)
                    this.ManageMenus(MenuChoices.STUDENTS_MENU);
                    break;

                case MenuChoices.NEW_STUDENT:
                    Console.Clear();
                    this._studentsMenu.CreateNewStudent();
                    this.ManageMenus(MenuChoices.STUDENTS_MENU);
                    break;

                case MenuChoices.DISPLAY_STUDENT:
                    try
                    {
                        Student studentToDisplay = this.ChooseStudent("De quel élève souhaitez-vous consulter les informations (tapez son index) ?");
                        this._studentsMenu.DisplayStudentInfo(studentToDisplay);
                        this.ManageMenus(MenuChoices.STUDENTS_MENU);
                        break;
                    }
                    catch (Exception)
                    {
                        this.ManageMenus(MenuChoices.STUDENTS_MENU);
                        break;
                    }

                case MenuChoices.ADD_GRADE:
                    try
                    {
                        Student studentToAssess = this.ChooseStudent("A quel élève voulez-vous ajouter une note (tapez son index) ?");
                        Course courseToAssess = this.ChooseCourse("A quel cours voulez-vous ajouter une note (tapez son index) ?");
                        this._studentsMenu.ManageAddingGrade(studentToAssess, courseToAssess);
                        this.ManageMenus(MenuChoices.STUDENTS_MENU);
                        break;
                    }
                    catch (Exception)
                    {
                        this.ManageMenus(MenuChoices.STUDENTS_MENU);
                        break;
                    }

                case MenuChoices.LIST_COURSES:
                    Console.Clear();
                    this._coursesMenu.DisplayListCourses(this.GetCourses());
                    Console.ReadKey(false);
                    // come back to the courses menu (previous menu)
                    this.ManageMenus(MenuChoices.COURSES_MENU);
                    break;

                case MenuChoices.ADD_LESSON:
                    this._coursesMenu.ManageAddingCourse();
                    this.ManageMenus(MenuChoices.COURSES_MENU);
                    break;

                case MenuChoices.REMOVE_LESSON:
                    try
                    {
                        Course courseToRemove = this.ChooseCourse("Quel cours voulez-vous supprimer (tapez son index) ?");
                        this._coursesMenu.ManageRemovingCourse(courseToRemove);
                        this.ManageMenus(MenuChoices.COURSES_MENU);
                        break;
                    }
                    catch (Exception)
                    {
                        this.ManageMenus(MenuChoices.COURSES_MENU);
                        break;
                    }

                // L'utilisateur a choisi de quitter l'application == QUIT_APP
                default:
                    Console.WriteLine("Fermeture de l'application...");
                    break;
            }
        }

        private void DisplayMainMenu()
        {
            // Efface ce qui était affiché dans la console
            Console.Clear();

            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("        MENU\n");

            Console.WriteLine("Que voulez-vous consulter ? Tapez 1, 2 ou 3 dans la console.");
            Console.WriteLine("1. Elèves");
            Console.WriteLine("2. Cours");
            Console.WriteLine("3. Quitter l'application");
            Console.WriteLine();

            Console.Write($">{" ",4}");

            int inputChoice = CheckInput.CheckInt(Console.ReadLine(), 1, 3);

            if (inputChoice == 1)
            {
                ManageMenus(MenuChoices.STUDENTS_MENU);
            }
            else if (inputChoice == 2)
            {
                ManageMenus(MenuChoices.COURSES_MENU);
            }
            else
            {
                ManageMenus(MenuChoices.QUIT_APP);
            }
        }

        protected List<Student> GetStudents()
        {
            return this._api.GetStudents();
        }

        protected List<Course> GetCourses()
        {
            return this._api.GetCourses();
        }

        private Student ChooseStudent(string message)
        {
            Console.Clear();
            var students = this.GetStudents();

            // no students added yet
            if (students.Count == 0)
            {
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.WriteLine("Il n'y a pas encore d'élèves dans le campus.\nVous ne pouvez donc consulter les informations d'aucun élève.");
                Console.WriteLine();
                Console.WriteLine(MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);

                Log.Error("Tentative d'accès à un élève inexistant");

                // returns to the try-catch where the function was called
                // before the end of it
                throw new Exception();
            }

            // display list of students
            this._studentsMenu.DisplayListStudents(students, 0);

            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");
            // revoir le message d'erreur
            int choice = CheckInput.CheckInt(Console.ReadLine(), 1, students.Count());

            return students[choice - 1];
        }

        private Course ChooseCourse(string message)
        {
            Console.Clear();

            var courses = this._api.GetCourses();

            // no students added yet
            if (courses.Count == 0)
            {
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.WriteLine("Il n'y a pas encore de cours disponibles sur le campus.");
                Console.WriteLine(MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);

                Log.Error("Tentative d'accès à un cours inexistant");

                // returns to the try-catch where the function was called
                // before the end of it
                throw new Exception();
            }

            // display list of students
            this._coursesMenu.DisplayListCourses(courses, 0);

            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");
            int choice = CheckInput.CheckInt(Console.ReadLine(), 1, courses.Count());

            return courses[choice - 1];
        }
    }
}
