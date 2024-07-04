namespace CampusFrontEnd.App
{
    /// <summary>
    /// A static class with functions checking user's input
    /// </summary>
    internal static class CheckInput
    {
        internal static bool CheckInt(string input, int firstNumber, int LastNumber)
        {
            List<int> acceptedNumbers = Enumerable.Range(firstNumber, LastNumber).ToList();
            int number;

            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out number) || !acceptedNumbers.Contains(number))
            {
                return false;
            }

            return true;
        }

        internal static bool CheckName(string name, int value)
        {
            if (string.IsNullOrEmpty(name)) return false;

            // numbers not allowed
            if (value == MenuChoices.CHECK_PERSON_NAME)
            {
                foreach (char character in name)
                {
                    if (!Char.IsLetter(character) && !Char.IsWhiteSpace(character) && character != '-') return false;
                }
                return true;
            }
            // numbers allowed
            else
            {
                foreach (char character in name)
                {
                    if (!Char.IsLetterOrDigit(character) && !Char.IsWhiteSpace(character) && character != '-') return false;
                }
                return true;
            }
        }

        internal static bool CheckDateTime(string input)
        {
            DateTime birthDate;

            while (true)
            {
                try
                {
                    // limitation:
                    // doesn't throw an error if the year is forthcoming
                    birthDate = DateTime.ParseExact(input, "dd/MM/yyyy", null);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        internal static bool ConfirmationCheck(string input)
        {
            if (string.IsNullOrEmpty(input) || input != "y" && input != "n")
            {
                return false;
            }

            return true;
        }

        internal static bool CheckInputGrade(string input)
        {
            double grade;

            if (string.IsNullOrEmpty(input) || !double.TryParse(input, out grade) || grade < 0 || grade > 20)
            {
                return false;
            }

            return true;
        }
    }
}
