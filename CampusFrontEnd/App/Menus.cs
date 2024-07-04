namespace CampusFrontEnd.App
{
    internal static class Menus
    {
        internal static int DisplayMainMenu()
        {
            Console.Clear();

            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("        MENU\n");

            Console.WriteLine("Que voulez-vous consulter ? Tapez 1, 2 ou 3 dans la console.");
            Console.WriteLine("1. Elèves");
            Console.WriteLine("2. Cours");
            Console.WriteLine("3. Quitter l'application");
            Console.WriteLine();

            Console.Write($">{" ",4}");

            string input = Console.ReadLine();

            while(!CheckInput.CheckInt(input, 1, 3))
            {
                Console.WriteLine("Le chiffre n'est pas reconnu. Vous devez rentrer une option entre 1 et 3.");
                Console.Write($">{" ",4}");
                input = Console.ReadLine();
            }

            int inputChoice = Convert.ToInt32(input);

            if (inputChoice == 1)
            {
                return MenuChoices.STUDENTS_MENU;
            }
            else if (inputChoice == 2)
            {
                return MenuChoices.COURSES_MENU;
            }
            else
            {
                return MenuChoices.QUIT_APP;
            }
        }

        internal static int DisplayStudentsMenu()
        {
            Console.Clear();

            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("        MENU ELEVES\n");

            Console.WriteLine("Tapez l'option qui vous intéresse (1, 2, 3, 4, 5) :");
            Console.WriteLine("1. Lister les élèves");
            Console.WriteLine("2. Créer un nouvel élève");
            Console.WriteLine("3. Consulter un élève existant");
            Console.WriteLine("4. Ajouter une note et une appréciation pour le cours d'un élève");
            Console.WriteLine("5. Retour au menu principal");
            Console.WriteLine();

            Console.Write(">{0, 4}", "");

            string input = Console.ReadLine();

            while (!CheckInput.CheckInt(input, 1, 5))
            {
                Console.WriteLine("Le chiffre n'est pas reconnu. Veuillez saisir une option entre 1 et 5.");
                Console.Write($">{" ",4}");
                input = Console.ReadLine();
            }

            int inputChoice = Convert.ToInt32(input);

            if (inputChoice == 1)
            {
               return MenuChoices.LIST_STUDENTS;
            }
            else if (inputChoice == 2)
            {
                return MenuChoices.ADD_STUDENT;
            }
            else if (inputChoice == 3)
            {
                return MenuChoices.DISPLAY_STUDENT;
            }
            else if (inputChoice == 4)
            {
                return MenuChoices.ADD_GRADE;
            }
            else
            {
                return MenuChoices.MAIN_MENU;
            }
        }

        internal static int DisplayCoursesMenu()
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
                return MenuChoices.LIST_COURSES;
            }
            else if (inputChoice == 2)
            {
                return MenuChoices.ADD_COURSE;
            }
            else if (inputChoice == 3)
            {
                return MenuChoices.REMOVE_COURSE;
            }
            else
            {
                return MenuChoices.MAIN_MENU;
            }
        }
    }
}
