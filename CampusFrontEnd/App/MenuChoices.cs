namespace CampusFrontEnd.App
{
    internal struct MenuChoices
    {
        // Choices from the main menu
        public const int QUIT_APP = -1;
        public const int MAIN_MENU = 0;
        public const int STUDENTS_MENU = 1;
        public const int COURSES_MENU = 2;
        // Choices from students menu
        public const int LIST_STUDENTS = 3;
        public const int ADD_STUDENT = 4;
        public const int DISPLAY_STUDENT = 5;
        public const int ADD_GRADE = 6;
        // Choices from courses menu
        public const int LIST_COURSES = 7;
        public const int ADD_COURSE = 8;
        public const int REMOVE_COURSE = 9;
        // which type of name
        public const int CHECK_PERSON_NAME = 10;
        public const int CHECK_COURSE_NAME = 11;

        public const string RETURN = "Appuyez sur n'importe quelle touche pour revenir au menu précédent.";
        public const string SEPARATION_LINE = "---------------------------------------------";
    }
}
