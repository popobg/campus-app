using CampusBackEnd.API;
using CampusBackEnd.DataModels;
using Serilog;
using System.Xml.Linq;

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

            string input = Console.ReadLine();

            while (!CheckInput.CheckInt(input, 1, 4))
            {
                Console.WriteLine("Le chiffre n'est pas reconnu. Vous devez rentrer une option entre 1 et 4.");
                Console.Write($">{" ",4}");
                input = Console.ReadLine();
            }

            int inputChoice = Convert.ToInt32(input);

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

            string courseName = Console.ReadLine();

            while (!CheckInput.CheckName(courseName, MenuChoices.CHECK_COURSE_NAME))
            {
                Console.WriteLine("Le nom doit comporter au moins une lettre ou un chiffre. Il peut comporter des espaces ou des tirets.\nLes majuscules et minuscules sont acceptées.");

                Console.Write($">{" ",4}");
                courseName = Console.ReadLine();
            }

            Console.WriteLine($"Nouveau cours : {courseName}.");

            this._api.AddCourse(courseName);
            Console.WriteLine("Le cours a bien été ajouté au programme.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout du cours {courseName} au programme");
        }

        internal void ManageRemovingCourse(Course course)
        {
            string message = $"Supprimer le cours {course.Name} du programme ? (y / n)";
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

            if (confirmation == "n")
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
            Console.WriteLine($"Le cours {courseName} a été supprimé avec succès.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Suppression du cours {courseName} au programme");
        }
    }
}
