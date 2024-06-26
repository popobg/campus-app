using System.Text.RegularExpressions;
using CampusApp.School;
using Newtonsoft.Json;
using Serilog;

namespace CampusApp.App
{
    /// <summary>
    /// Manages console UI and coordinates all the classes accordingly.
    /// Manages incorrect input too.
    /// </summary>
    internal class App
    {
        [JsonProperty]
        private Campus _students;
        [JsonProperty]
        private Courses _courses;

        [JsonConstructor]
        internal App()
        {
            _students = new Campus();
            _courses = new Courses();
        }

        private int TryConvertInputToInt(string input, int firstNumber, int LastNumber)
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

        private string CheckName(string name, int value)
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

        private DateTime ConvertToDateTime(string input)
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

        private int ConfirmationCheck(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");
            string input = Console.ReadLine().ToLower();

            while (string.IsNullOrEmpty(input) || input != "y" && input != "n")
            {
                Console.WriteLine(message);
                Console.Write($">{" ",4}");
                input = Console.ReadLine().ToLower();
            }

            Console.WriteLine();

            if (input == "y")
            {
                return MenuChoices.YES;
            }
            return MenuChoices.NO;
        }

        // recursive function
        internal void ManageMenus(int choice = MenuChoices.MAIN_MENU)
        {
            switch (choice)
            {
                case MenuChoices.MAIN_MENU:
                    DisplayMainMenu();
                    break;

                case MenuChoices.STUDENTS_MENU:
                    DisplayStudentsMenu();
                    break;

                case MenuChoices.COURSES_MENU:
                    DisplayCoursesMenu();
                    break;

                case MenuChoices.LIST_STUDENTS:
                    Console.Clear();
                    DisplayListStudents();
                    Console.ReadKey(false);
                    // come back to the students menu (previous menu)
                    ManageMenus(MenuChoices.STUDENTS_MENU);
                    break;

                case MenuChoices.NEW_STUDENT:
                    Console.Clear();
                    CreateNewStudent();
                    ManageMenus(MenuChoices.STUDENTS_MENU);
                    break;

                case MenuChoices.DISPLAY_STUDENT:
                    try
                    {
                        Student studentToDisplay = ChooseStudent("De quel élève souhaitez-vous consulter les informations (tapez son index) ?");
                        DisplayStudentInfo(studentToDisplay);
                        ManageMenus(MenuChoices.STUDENTS_MENU);
                        break;
                    }
                    catch (Exception)
                    {
                        ManageMenus(MenuChoices.STUDENTS_MENU);
                        break;
                    }

                case MenuChoices.ADD_GRADE:
                    try
                    {
                        Student studentToAssess = ChooseStudent("A quel élève voulez-vous ajouter une note (tapez son index) ?");
                        Course courseToAssess = ChooseCourse("A quel cours voulez-vous ajouter une note (tapez son index) ?");
                        ManageAddingGrade(studentToAssess, courseToAssess);
                        ManageMenus(MenuChoices.STUDENTS_MENU);
                        break;
                    }
                    catch (Exception)
                    {
                        ManageMenus(MenuChoices.STUDENTS_MENU);
                        break;
                    }

                case MenuChoices.LIST_COURSES:
                    Console.Clear();
                    DisplayListCourses();
                    Console.ReadKey(false);
                    // come back to the courses menu (previous menu)
                    ManageMenus(MenuChoices.COURSES_MENU);
                    break;

                case MenuChoices.ADD_LESSON:
                    ManageAddingCourse();
                    ManageMenus(MenuChoices.COURSES_MENU);
                    break;

                case MenuChoices.REMOVE_LESSON:
                    try
                    {
                        Course courseToRemove = ChooseCourse("Quel cours voulez-vous supprimer (tapez son index) ?");
                        ManageRemovingCourse(courseToRemove);
                        ManageMenus(MenuChoices.COURSES_MENU);
                        break;
                    }
                    catch (Exception)
                    {
                        ManageMenus(MenuChoices.COURSES_MENU);
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

            int inputChoice = TryConvertInputToInt(Console.ReadLine(), 1, 3);

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

        private void DisplayStudentsMenu()
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

            int inputChoice = TryConvertInputToInt(Console.ReadLine(), 1, 5);

            if (inputChoice == 1)
            {
                ManageMenus(MenuChoices.LIST_STUDENTS);
            }
            else if (inputChoice == 2)
            {
                ManageMenus(MenuChoices.NEW_STUDENT);
            }
            else if (inputChoice == 3)
            {
                ManageMenus(MenuChoices.DISPLAY_STUDENT);
            }
            else if (inputChoice == 4)
            {
                ManageMenus(MenuChoices.ADD_GRADE);
            }
            else
            {
                ManageMenus();
            }
        }

        private void DisplayCoursesMenu()
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

            int inputChoice = TryConvertInputToInt(Console.ReadLine(), 1, 4);

            if (inputChoice == 1)
            {
                ManageMenus(MenuChoices.LIST_COURSES);
            }
            else if (inputChoice == 2)
            {
                ManageMenus(MenuChoices.ADD_LESSON);
            }
            else if (inputChoice == 3)
            {
                ManageMenus(MenuChoices.REMOVE_LESSON);
            }
            else
            {
                ManageMenus();
            }
        }

        private void DisplayListStudents(int directCall = 0)
        {
            Console.Clear();
            var listStudents = _students.StudentsList;
            int listIndex = 1;

            if (listStudents.Count() == 0)
            {
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.WriteLine("Il n'y a pas encore d'élèves saisis.");
                Console.WriteLine(MenuChoices.SEPARATION_LINE);

                Log.Error("Tentative de consultation d'une liste d'élèves vide");

                return;
            }

            Console.WriteLine("Liste des élèves :\n");

            foreach (Student eleve in listStudents)
            {
                Console.WriteLine($"{listIndex}. Eleve: {eleve.FirstName} {eleve.LastName}");
                listIndex++;
            }

            Console.WriteLine();
            Console.WriteLine(MenuChoices.SEPARATION_LINE);

            if (directCall == 1)
            {
                Log.Information("Consultation de la liste des élèves");
            }
        }

        private void CreateNewStudent()
        {
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("AJOUT D'UN ELEVE A LA LISTE DU CAMPUS :");
            Console.WriteLine();

            Console.WriteLine("Quel est le prénom de l'élève ?");
            Console.Write($">{" ",4}");
            string firstName = CheckName(Console.ReadLine(), MenuChoices.CHECK_PERSON_NAME);

            Console.WriteLine("Quel est le nom de l'élève ?");
            Console.Write($">{" ",4}");
            string lastName = CheckName(Console.ReadLine(), MenuChoices.CHECK_PERSON_NAME);

            Console.WriteLine("Quelle est sa date de naissance (format dd/mm/aaaa) ?");
            Console.Write($">{" ",4}");
            DateTime birthDate = ConvertToDateTime(Console.ReadLine());

            Console.WriteLine($"Prénom : {firstName}, nom : {lastName}");
            Console.WriteLine($"Date de naissance : {birthDate.ToShortDateString()}\n");

            // Confirmation
            int choice = ConfirmationCheck("Ajouter cet élève au campus ? (y/n)");

            // student not added, go back to previous menu
            if (choice == MenuChoices.NO)
            {
                Console.WriteLine("L'élève n'a pas été ajouté à la liste des élèves du campus.");
                Console.WriteLine();
                Console.WriteLine(MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            _students.AddStudent(firstName, lastName, birthDate);
            Console.WriteLine();
            Console.WriteLine("L'élève a été ajouté avec succès à la liste des élèves du campus.\nVous pouvez maintenant consulter ses informations et lui ajouter des cours, des notes et des appréciations.");
            Console.WriteLine();
            Console.WriteLine(MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout de l'élève {firstName} {lastName} à la liste du campus");
        }

        private Student ChooseStudent(string message)
        {
            Console.Clear();
            var listStudents = _students.StudentsList;

            // no students added yet
            if (listStudents.Count() == 0)
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
            DisplayListStudents(0);

            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");
            // revoir le message d'erreur
            int choice = TryConvertInputToInt(Console.ReadLine(), 1, listStudents.Count());

            return listStudents[choice - 1];
        }

        private void DisplayStudentInfo(Student student)
        {
            Console.Clear();
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.Write("{0, 4}", " ");
            Console.WriteLine("Informations sur l'élève :");
            Console.WriteLine();
            Console.WriteLine("Nom{0, 15} {1}", ":", student.LastName);
            Console.WriteLine("Prénom{0, 12} {1}", ":", student.FirstName);
            Console.WriteLine("{0, 17} : {1}", "Date de naissance", student.BirthDate.ToShortDateString());
            Console.WriteLine("\n");

            Console.Write("{0, 4}", " ");
            Console.WriteLine("Résultats scolaires :");
            Console.WriteLine();

            foreach (KeyValuePair<int, List<Grade>> kvp in student.SchoolReport)
            {
                string courseName = _courses.GetCourseByID(kvp.Key);

                Console.Write("{0, 8}", " ");
                Console.WriteLine($"Cours : {courseName}");

                foreach (Grade grade in kvp.Value)
                {
                    Console.Write("{0, 12}", " ");
                    Console.WriteLine($"Note : {grade.Mark:0.00}/20");

                    Console.Write("{0, 12}", " ");
                    Console.WriteLine($"Appréciation : {grade.Comment}");
                    Console.WriteLine();
                }

                Console.Write("{0, 8}", " ");
                Console.WriteLine($"Moyenne pour ce cours : {student.CalculateMeanCourse(kvp.Value):00}");
                Console.WriteLine();
            }

            Console.WriteLine();

            double mean = student.GeneralMean;

            if (mean == -1)
            {
                Console.WriteLine();
                Console.WriteLine("Aucune note n'a été rentrée pour cet élève. Il n'a pas encore de moyenne générale.");
            }
            else
            {
                Console.Write("{0, 4}", " ");
                Console.WriteLine($"Moyenne générale : {mean}");
            }

            Console.WriteLine();
            Console.WriteLine(MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Consultation du bulletin de l'élève {student.FirstName} {student.LastName}");
        }

        private void ManageAddingGrade(Student student, Course course)
        {
            Console.WriteLine($"ELEVE : {student.FirstName} {student.LastName}");
            double grade;

            Console.Write("Note à ajouter (obligatoire) :");
            Console.Write("{0, 4}", " ");
            string input = Console.ReadLine();

            while (string.IsNullOrEmpty(input) || !double.TryParse(input, out grade) || grade < 0 || grade > 20)
            {
                Console.WriteLine("Vous devez entrer une note entre 0 et 20.\nLes chiffres à virgule sont acceptés.");
                Console.Write($">{" ",4}");
                input = Console.ReadLine();
            }

            Console.Write("Appréciation (facultative) :");
            Console.Write("{0, 4}", " ");
            string comment = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Etudiant : {student.FirstName} {student.LastName}");
            Console.WriteLine($"* Cours : {course.Name}");
            Console.WriteLine($"Note : {grade}/20");
            Console.WriteLine($"Appréciation : {comment}");

            // Confirmation
            int choice = ConfirmationCheck("Ajouter cette note et cette appréciation à l'élève ? (y/n)");

            // no grade added, go back to previous menu
            if (choice == MenuChoices.NO)
            {
                Console.WriteLine("La note et l'appréciation n'ont pas été ajoutées.");
                Console.WriteLine("\n" + MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            student.AddGrade(course.LessonID, grade, comment);
            Console.WriteLine();
            Console.WriteLine("La note et l'appréciation ont été ajoutées avec succès au bulletin de l'élève.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout d'une note pour l'élève {student.FirstName} {student.LastName} pour le cours {course.Name} : {grade}/20, \"{comment}\"");
        }

        private void ManageAddingCourse()
        {
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("Quelle matière voulez-vous ajouter au programme ?");
            Console.Write($">{" ",4}");
            string fieldName = CheckName(Console.ReadLine(), MenuChoices.CHECK_COURSE_NAME);

            Console.WriteLine($"Nouveau cours : {fieldName}.");

            // Confirmation
            int choice = ConfirmationCheck("Ajouter ce cours au programme ? (y/n)");

            if (choice == MenuChoices.NO)
            {
                Console.WriteLine("Le cours n'a pas été ajouté au programme.");
                Console.WriteLine("\n" + MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            _courses.AddLesson(fieldName);
            Console.WriteLine("Le cours a bien été ajouté au programme.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout du cours {fieldName} au programme");
        }

        private void ManageRemovingCourse(Course course)
        {
            //if (!_courses.CoursesList.Contains(course))
            //{
            //    Console.WriteLine(MenuChoices.SEPARATION_LINE);
            //    Console.WriteLine("Ce cours n'est pas inscrit au programme. Il ne peut pas être supprimé.");
            //    Console.WriteLine(MenuChoices.RETURN);
            //    Console.WriteLine(MenuChoices.SEPARATION_LINE);
            //    Console.ReadKey(false);

            //    // returns to the try-catch where the function was called
            //    // before the end of it
            //    throw new Exception();
            //}

            // Confirmation
            int choice = ConfirmationCheck($"Supprimer le cours {course.Name} du programme ? (y/n)");

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

            _courses.RemoveLesson(course, _students.StudentsList);
            Console.WriteLine($"Le cours {course.Name} a été supprimé avec succès.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Suppression du cours {courseName} au programme");
        }

        private void DisplayListCourses(int directCall = 1)
        {
            List<Course> courses = _courses.CoursesList;
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

        private Course ChooseCourse(string message)
        {
            Console.Clear();

            var listCourses = _courses.CoursesList;

            // no students added yet
            if (listCourses.Count() == 0)
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
            DisplayListCourses(0);

            Console.WriteLine();
            Console.WriteLine(message);
            Console.Write($">{" ",4}");
            int choice = TryConvertInputToInt(Console.ReadLine(), 1, listCourses.Count());

            return listCourses[choice - 1];
        }
    }
}

