using CampusBackEnd.API;
using CampusBackEnd.DataModels;
using Serilog;

namespace CampusFrontEnd.App
{
    internal class CoursesMenu
    {
        private readonly API _api;

        internal Menu Menu { get; set; }

        internal CoursesMenu(API api)
        {
            this._api = api;
        }

        internal void DisplayCoursesMenu()
        {
            Console.Clear();

            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("        MENU COURS\n");

            Console.WriteLine("Tapez l'option qui vous intéresse (1, 2, 3, 4) :");
            Console.WriteLine("1. Lister les cours existants");
            Console.WriteLine("2. Ajouter un nouveau cours au programme");
            Console.WriteLine("3. Supprimer un cours par son identifiant");
            Console.WriteLine("4. Retour au menu principal");
            Console.WriteLine();

            Console.Write($">{" ",4}");

            int inputChoice = CheckInput.CheckInt(Console.ReadLine(), 1, 4);

            if (inputChoice == 1)
            {
                Menu.ManageMenus(MenuChoices.LIST_COURSES);
            }
            else if (inputChoice == 2)
            {
                Menu.ManageMenus(MenuChoices.ADD_LESSON);
            }
            else if (inputChoice == 3)
            {
                Menu.ManageMenus(MenuChoices.REMOVE_LESSON);
            }
            else
            {
                Menu.ManageMenus();
            }
        }

        internal void DisplayListCourses(List<Course> courses, int directCall = 1)
        {
            int index = 1;

            Console.WriteLine(MenuChoices.SEPARATION_LINE);

            if (courses.Count == 0)
            {
                Console.WriteLine("Aucun cours n'a été ajouté au programme pour le moment.");
                Console.WriteLine(MenuChoices.SEPARATION_LINE);

                Log.Error("Tentative de consultation d'une liste de cours vide");

                return;
            }

            Console.WriteLine("Liste des cours disponibles sur le campus :\n");

            foreach (Course course in courses)
            {
                Console.Write("{0, 8}", "");
                Console.WriteLine($"{index}. Discipline : {course.Name}");
                //Console.Write("{0, 8}", "");
                //Console.WriteLine($"Identifiant : {course.LessonID}");

                index++;
            }

            Console.WriteLine();
            Console.WriteLine(MenuChoices.SEPARATION_LINE);

            if (directCall == 1)
            {
                Log.Information("Consultation de la liste des cours");
            }
        }

        internal void ManageAddingCourse()
        {
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("Quelle matière voulez-vous ajouter au programme ?");
            Console.Write($">{" ",4}");
            string courseName = CheckInput.CheckName(Console.ReadLine(), MenuChoices.CHECK_COURSE_NAME);

            Console.WriteLine($"Nouveau cours : {courseName}.");

            // Confirmation
            int choice = CheckInput.ConfirmationCheck("Ajouter ce cours au programme ? (y/n)");

            if (choice == MenuChoices.NO)
            {
                Console.WriteLine("Le cours n'a pas été ajouté au programme.");
                Console.WriteLine("\n" + MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            this._api.AddCourse(courseName);
            Console.WriteLine("Le cours a bien été ajouté au programme.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout du cours {courseName} au programme");
        }

        internal void ManageRemovingCourse(Course course)
        {
            // Confirmation
            int choice = CheckInput.ConfirmationCheck($"Supprimer le cours {course.Name} du programme ? (y/n)");

            if (choice == MenuChoices.NO)
            {
                Console.WriteLine("Le cours n'a pas été supprimé du programme.");
                Console.WriteLine("\n" + MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            // necessary for the log
            string courseName = course.Name;

            this._api.RemoveCourse(course);
            Console.WriteLine($"Le cours {course.Name} a été supprimé avec succès.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Suppression du cours {courseName} au programme");
        }
    }
}
