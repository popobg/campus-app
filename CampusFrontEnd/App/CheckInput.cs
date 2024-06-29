using System.Text.RegularExpressions;

namespace CampusFrontEnd.App
{
    /// <summary>
    /// A static class with functions checking user's input
    /// </summary>
    internal static class CheckInput
    {
        internal static int CheckInt(string input, int firstNumber, int LastNumber)
        {
            List<int> acceptedNumbers = Enumerable.Range(firstNumber, LastNumber).ToList();
            int number;

            // Incorrect input if empty or null,
            // if not a number and not in the accepted range
            while (string.IsNullOrEmpty(input) || !int.TryParse(input, out number) || !acceptedNumbers.Contains(number))
            {
                Console.WriteLine("Le chiffre n'est pas reconnu. Entrez une option valide.");
                Console.Write($">{" ",4}");
                input = Console.ReadLine();
            }

            return number;
        }

        internal static string CheckName(string name, int value)
        {
            // numbers not allowed
            if (value == MenuChoices.CHECK_PERSON_NAME)
            {
                while (string.IsNullOrEmpty(name) || !Regex.Match(name, @"(\b[a-zâãäåæçéèêëìíîïñòóôõøùúûü]+\b)+", RegexOptions.IgnoreCase).Success)
                {
                    Console.WriteLine("Le nom ne doit comporter que des lettres ; plusieurs mots peuvent être séparés par un tiret ou un espace.\nLes majuscules et minuscules sont acceptées.");
                    Console.Write($">{" ",4}");
                    name = Console.ReadLine();
                }
            }
            // numbers allowed
            else
            {
                while (string.IsNullOrEmpty(name) || !Regex.Match(name, @"(\b\w+\b)+", RegexOptions.IgnoreCase).Success)
                {
                    Console.WriteLine("Le nom doit comporter au moins une lettre ou un chiffre. Il peut comporter des espaces ou des tirets.\nLes majuscules et minuscules sont acceptées.");
                    Console.Write($">{" ",4}");
                    name = Console.ReadLine();
                }
            }

            return name;
        }

        internal static DateTime CheckDateTime(string input)
        {
            DateTime birthDate;

            while (true)
            {
                try
                {
                    // limitation:
                    // doesn't throw an error if the year is forthcoming
                    birthDate = DateTime.ParseExact(input, "dd/MM/yyyy", null);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("La date de naissance doit être au format DD/MM/YYYY. Les dates entrées doivent exister.");
                    Console.Write($">{" ",4}");
                    input = Console.ReadLine();
                }
            }

            return birthDate;
        }

        internal static int ConfirmationCheck(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");
            string input = Console.ReadLine()?.ToLower();

            while (string.IsNullOrEmpty(input) || input != "y" && input != "n")
            {
                Console.WriteLine(message);
                Console.Write($">{" ",4}");
                input = Console.ReadLine()?.ToLower();
            }

            Console.WriteLine();

            if (input == "y")
            {
                return MenuChoices.YES;
            }
            return MenuChoices.NO;
        }

        internal static double CheckInputGrade(string input)
        {
            double grade;

            while (string.IsNullOrEmpty(input) || !double.TryParse(input, out grade) || grade < 0 || grade > 20)
            {
                Console.WriteLine("Vous devez entrer une note entre 0 et 20.\nLes chiffres à virgule sont acceptés.");
                Console.Write($">{" ",4}");
                input = Console.ReadLine();
            }

            return grade;
        }
    }
}
