using CampusBackEnd.API;
using CampusBackEnd.DataModels;
using Serilog;

namespace CampusFrontEnd.App
{
    internal static class CoursesMenu
    {
        internal static void DisplayListCourses(List<Course> courses, int directCall = 1)
        {
            Console.Clear();

            Console.WriteLine(MenuChoices.SEPARATION_LINE);

            if (courses.Count == 0)
            {
                Console.WriteLine("Aucun cours n'a été ajouté au programme pour le moment.");
                Console.WriteLine(MenuChoices.SEPARATION_LINE);

                Log.Error("Tentative de consultation d'une liste de cours vide");

                return;
            }

            int index = 1;

            Console.WriteLine("Liste des cours disponibles sur le campus :\n");

            foreach (Course course in courses)
            {
                Console.Write("{0, 8}", "");
                Console.WriteLine($"{index}. Discipline : {course.Name}");

                index++;
            }

            Console.WriteLine();
            Console.WriteLine(MenuChoices.SEPARATION_LINE);

            if (directCall == 1)
            {
                Log.Information("Consultation de la liste des cours");
                Console.ReadKey(false);
            }
        }

        internal static void ManageAddingCourse(API api)
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

            api.AddCourse(courseName);
            Console.WriteLine("Le cours a bien été ajouté au programme.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout du cours {courseName} au programme");
        }

        internal static void ManageRemovingCourse(API api, Course course)
        {
            string message = $"Supprimer le cours {course.Name} du programme ? (y / n)";

            string confirmation = App.ConfirmationLoop(message);

            if (confirmation == "n")
            {
                Console.WriteLine("Le cours n'a pas été supprimé du programme.");
                Console.WriteLine("\n" + MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            // required for the log
            string courseName = course.Name;

            api.RemoveCourse(course);
            Console.WriteLine($"Le cours {courseName} a été supprimé avec succès.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Suppression du cours {courseName} au programme");
        }
    }
}