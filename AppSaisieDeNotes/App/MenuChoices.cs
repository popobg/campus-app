namespace CampusApp.App
{
    internal struct MenuChoices
    {
        // Represent all the choices possible in the menu interface
        public const int QUIT_APP = -1;
        public const int MAIN_MENU = 0;
        public const int STUDENTS_MENU = 1;
        public const int COURSES_MENU = 2;
        // Come from the students menu
        public const int LIST_STUDENTS = 3;
        public const int NEW_STUDENT = 4;
        public const int DISPLAY_STUDENT = 5;
        public const int ADD_GRADE = 6;
        // Come from the courses menu
        public const int LIST_COURSES = 7;
        public const int ADD_LESSON = 8;
        public const int REMOVE_LESSON = 9;
        // Confirmation choices
        public const int YES = 10;
        public const int NO = 11;
        // Success or failure
        public const int CHECK_PERSON_NAME = 12;
        public const int CHECK_COURSE_NAME = 13;

        public const string RETURN = "Appuyez sur n'importe quelle touche pour revenir au menu précédent.";
        public const string SEPARATION_LINE = "---------------------------------------------";
    }
}
