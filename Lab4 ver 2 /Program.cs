using System;

namespace Lab4
{
    /// <summary>
    /// Головний клас програми, що містить точку входу.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Головний метод програми. Налаштовує кодування консолі та ініціалізує сервіс і меню.
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding  = System.Text.Encoding.UTF8;
            Console.Clear();

            Service    service    = new Service();
            Menu       menu       = new Menu();
            Department department = new Department();

            // ВЕРСІЯ 2: EducationalEntity є abstract — пряме створення неможливе.
            // Наступний рядок викликає помилку компілятора CS0144:
            // «Cannot create an instance of the abstract type or interface 'EducationalEntity'»
            // EducationalEntity entity = new EducationalEntity(); // ← CS0144

            // Успадкування працює: Department і Student є конкретними реалізаціями.
            // Student studentDemo = new Student("Іваненко І.І.", "Комп'ютерні науки", 5);
            // Department deptDemo = new Department();

            int  nextStudentNumber = 1;
            bool running           = true;

            while (running)
            {
                string choice = menu.GetChoice(service);

                switch (choice.Trim())
                {
                    case "1":
                        department.HandleCreate(service);
                        nextStudentNumber = 1;
                        break;
                    case "2":
                        nextStudentNumber = department.HandleAddStudent(service, nextStudentNumber);
                        break;
                    case "3":
                        department.HandleRemoveStudent(service);
                        break;
                    case "4":
                        department.HandleAddDiscipline(service);
                        break;
                    case "5":
                        department.HandleRemoveDiscipline(service);
                        break;
                    case "6":
                        department.HandleCalculateRating(service);
                        break;
                    case "7":
                        department.HandleViewGrades(service);
                        break;
                    case "8":
                        department.HandleAddGrade(service);
                        break;
                    case "9":
                        department.HandleSaveData(service);
                        break;
                    case "10":
                        department.HandleChangeSpecialty(service);
                        break;
                    case "11":
                        department.HandleChangeWorkload(service);
                        break;
                    case "12":
                        Department.HandleReadData(service);
                        break;
                    case "0":
                        Department.HandleExit(service);
                        running = false;
                        break;
                    default:
                        Department.HandleUnknownOption(service);
                        break;
                }
            }
        }
    }
}
