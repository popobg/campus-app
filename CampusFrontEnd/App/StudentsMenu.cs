using CampusBackEnd.API;
using CampusBackEnd.DataModels;
using Serilog;

namespace CampusFrontEnd.App
{
    internal class StudentsMenu
    {
        private readonly API _api;

        internal Menu Menu { get; set; }

        internal StudentsMenu(API api)
        {
            this._api = api;
        }

        internal void DisplayStudentsMenu()
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

            int inputChoice = CheckInput.CheckInt(Console.ReadLine(), 1, 5);

            if (inputChoice == 1)
            {
                Menu.ManageMenus(MenuChoices.LIST_STUDENTS);
            }
            else if (inputChoice == 2)
            {
                Menu.ManageMenus(MenuChoices.NEW_STUDENT);
            }
            else if (inputChoice == 3)
            {
                Menu.ManageMenus(MenuChoices.DISPLAY_STUDENT);
            }
            else if (inputChoice == 4)
            {
                Menu.ManageMenus(MenuChoices.ADD_GRADE);
            }
            else
            {
                Menu.ManageMenus();
            }
        }

        internal void DisplayListStudents(List<Student> students, int directCall = 0)
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
            }
        }

        internal void CreateNewStudent()
        {
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.WriteLine("AJOUT D'UN ELEVE A LA LISTE DU CAMPUS :");
            Console.WriteLine();

            Console.WriteLine("Quel est le prénom de l'élève ?");
            Console.Write($">{" ",4}");
            string firstName = CheckInput.CheckName(Console.ReadLine(), MenuChoices.CHECK_PERSON_NAME);

            Console.WriteLine("Quel est le nom de l'élève ?");
            Console.Write($">{" ",4}");
            string lastName = CheckInput.CheckName(Console.ReadLine(), MenuChoices.CHECK_PERSON_NAME);

            Console.WriteLine("Quelle est sa date de naissance (format dd/mm/aaaa) ?");
            Console.Write($">{" ",4}");
            DateTime birthDate = CheckInput.CheckDateTime(Console.ReadLine());

            Console.WriteLine($"Prénom : {firstName}, nom : {lastName}");
            Console.WriteLine($"Date de naissance : {birthDate.ToShortDateString()}\n");

            // Confirmation
            int choice = CheckInput.ConfirmationCheck("Ajouter cet élève au campus ? (y/n)");

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

            this._api.AddStudents(firstName, lastName, birthDate);
            Console.WriteLine();
            Console.WriteLine("L'élève a été ajouté avec succès à la liste des élèves du campus.\nVous pouvez maintenant consulter ses informations et lui ajouter des cours, des notes et des appréciations.");
            Console.WriteLine();
            Console.WriteLine(MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout de l'élève {firstName} {lastName} à la liste du campus");
        }

        internal void DisplayStudentInfo(Student student)
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
                string courseName = this._api.GetCourse(kvp.Key).Name;

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
                Console.WriteLine($"Moyenne pour ce cours : {this._api.CalculateCourseAverage(kvp.Value)}/20");
                Console.WriteLine();
            }

            Console.WriteLine();

            double average = this._api.CalculateGeneralAverage(student);

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

        internal void ManageAddingGrade(Student student, Course course)
        {
            Console.WriteLine($"ELEVE : {student.FirstName} {student.LastName}");

            Console.Write("Note à ajouter (obligatoire) :");
            Console.Write("{0, 4}", " ");

            double grade = CheckInput.CheckInputGrade(Console.ReadLine());

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
            int choice = CheckInput.ConfirmationCheck("Ajouter cette note et cette appréciation à l'élève ? (y/n)");

            // no grade added, go back to previous menu
            if (choice == MenuChoices.NO)
            {
                Console.WriteLine("La note et l'appréciation n'ont pas été ajoutées.");
                Console.WriteLine("\n" + MenuChoices.RETURN);
                Console.WriteLine(MenuChoices.SEPARATION_LINE);
                Console.ReadKey(false);
                return;
            }

            this._api.AddGrade(course.ID, grade, comment, student);
            Console.WriteLine();
            Console.WriteLine("La note et l'appréciation ont été ajoutées avec succès au bulletin de l'élève.");
            Console.WriteLine("\n" + MenuChoices.RETURN);
            Console.WriteLine(MenuChoices.SEPARATION_LINE);
            Console.ReadKey(false);

            Log.Information($"Ajout d'une note pour l'élève {student.FirstName} {student.LastName} pour le cours {course.Name} : {grade}/20, \"{comment}\"");
        }
    }
}

