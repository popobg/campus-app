using CampusBackEnd.API;
using CampusBackEnd.DataModels;
using Serilog;

namespace CampusFrontEnd.App
{
    internal static class StudentsMenu
    {
        internal static void DisplayListStudents(List<Student> students, int directCall = 1)
        {
            Console.Clear();
            int listIndex = 1;

            if (students.Count == 0)
            {
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.WriteLine("Il n'y a pas encore d'élèves saisis.");
                Console.WriteLine(MenuChoices.SEPARATION_LINE);

                Log.Error("Tentative de consultation d'une liste d'élèves vide");

                return;
            }

            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("Liste des élèves :\n");

            foreach (Student student in students)
            {
                Console.WriteLine($"{listIndex}. Eleve: {student.FirstName} {student.LastName}");
                listIndex++;
            }

            Console.WriteLine();
            Console.WriteLine(MenuChoices.SEPARATION_LINE);

            if (directCall == 1)
            {
                Log.Information("Consultation de la liste des élèves");
                Console.ReadKey(false);
            }
        }

        private static List<string> GettingUserStudentInfo()
        {
            #region getting first name from user
            Console.WriteLine("Quel est le prénom de l'élève ?");
            Console.Write($">{" ",4}");
            string firstName = Console.ReadLine();

            while (!CheckInput.CheckName(firstName, MenuChoices.CHECK_PERSON_NAME))
            {
                Console.WriteLine("Le nom ne doit comporter que des lettres ; plusieurs mots peuvent être séparés par un tiret ou un espace.\nLes majuscules et minuscules sont acceptées.");

                Console.Write($">{" ",4}");
                firstName = Console.ReadLine();
            }
            #endregion

            #region getting last name from user
            Console.WriteLine("Quel est le nom de l'élève ?");
            Console.Write($">{" ",4}");
            string lastName = Console.ReadLine();

            while (!CheckInput.CheckName(lastName, MenuChoices.CHECK_PERSON_NAME))
            {
                Console.WriteLine("Le nom ne doit comporter que des lettres ; plusieurs mots peuvent être séparés par un tiret ou un espace.\nLes majuscules et minuscules sont acceptées.");

                Console.Write($">{" ",4}");
                lastName = Console.ReadLine();
            }
            #endregion

            #region getting birth date from user
            Console.WriteLine("Quelle est sa date de naissance (format dd/mm/aaaa) ?");
            Console.Write($">{" ",4}");
            string dateInput = Console.ReadLine();

            while (!CheckInput.CheckDateTime(dateInput))
            {
                Console.WriteLine("La date de naissance doit être au format DD/MM/YYYY. Les dates entrées doivent exister.");

                Console.Write($">{" ",4}");
                dateInput = Console.ReadLine();
            }
            #endregion

            return new List<string>() { firstName, lastName, dateInput };
        }

        internal static void CreateNewStudent(API api)
        {
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("AJOUT D'UN ELEVE A LA LISTE DU CAMPUS :");
            Console.WriteLine();

            List<string> infos = GettingUserStudentInfo();

            Console.WriteLine($"Prénom : {infos[0]}, nom : {infos[1]}");
            Console.WriteLine($"Date de naissance : {infos[2]}\n");

            string message = "Ajouter cet élève au campus ? (y/n)";

            string confirmation = App.ConfirmationLoop(message);

            if (confirmation == "n")
            {
                Console.WriteLine("L'élève n'a pas été ajouté à la liste des élèves du campus.");
                Console.WriteLine();
                Console.WriteLine(MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            api.AddStudents(infos[0], infos[1], DateTime.ParseExact(infos[2], "dd/MM/yyyy", null));
            Console.WriteLine();
            Console.WriteLine("L'élève a été ajouté avec succès à la liste des élèves du campus.\nVous pouvez maintenant consulter ses informations et lui ajouter des cours, des notes et des appréciations.");
            Console.WriteLine();
            Console.WriteLine(MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout de l'élève {infos[0]} {infos[1]} à la liste du campus");
        }

        internal static void DisplayStudentInfo(API api, Student student)
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
                string courseName = api.GetCourse(kvp.Key).Name;

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
                Console.WriteLine($"Moyenne pour ce cours : {api.CalculateCourseAverage(kvp.Value)}/20");
                Console.WriteLine();
            }

            Console.WriteLine();

            double average = api.CalculateGeneralAverage(student);

            if (average == -1)
            {
                Console.WriteLine();
                Console.WriteLine("Aucune note n'a été rentrée pour cet élève. Il n'a pas encore de moyenne générale.");
            }
            else
            {
                Console.Write("{0, 4}", " ");
                Console.WriteLine($"Moyenne générale : {average}/20");
            }

            Console.WriteLine();
            Console.WriteLine(MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Consultation du bulletin de l'élève {student.FirstName} {student.LastName}");
        }

        internal static void ManageAddingGrade(API api, Student student, Course course)
        {
            Console.WriteLine($"ELEVE : {student.FirstName} {student.LastName}");

            Console.Write("Note à ajouter (obligatoire) :");
            Console.Write("{0, 4}", " ");

            string gradeInput = Console.ReadLine();

            while (!CheckInput.CheckInputGrade(gradeInput))
            {
                Console.WriteLine("Vous devez entrer une note entre 0 et 20.\nLes chiffres à virgule sont acceptés.");
                Console.Write($">{" ",4}");

                gradeInput = Console.ReadLine();
            }

            double grade = Convert.ToInt32(gradeInput);

            Console.Write("Appréciation (facultative) :");
            Console.Write("{0, 4}", " ");
            string comment = Console.ReadLine();

            Console.WriteLine();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine($"Etudiant : {student.FirstName} {student.LastName}");
            Console.WriteLine($"* Cours : {course.Name}");
            Console.WriteLine($"Note : {grade}/20");
            Console.WriteLine($"Appréciation : {comment}");

            string message = "Ajouter cette note et cette appréciation à l'élève ? (y/n)";

            string confirmation = App.ConfirmationLoop(message);

            if (confirmation == "n")
            {
                Console.WriteLine("La note et l'appréciation n'ont pas été ajoutées.");
                Console.WriteLine("\n" + MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            api.AddGrade(course.ID, grade, comment, student);
            Console.WriteLine();
            Console.WriteLine("La note et l'appréciation ont été ajoutées avec succès au bulletin de l'élève.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout d'une note pour l'élève {student.FirstName} {student.LastName} pour le cours {course.Name} : {grade}/20, \"{comment}\"");
        }
    }
}