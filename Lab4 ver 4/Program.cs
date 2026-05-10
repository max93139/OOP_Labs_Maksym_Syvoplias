using System;

namespace Lab4
{
    /// <summary>
    /// Головний клас програми, що містить точку входу.
    /// main() — виключно координатор: ініціалізує об'єкти та делегує виклики іншим класам.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Точка входу. Містить лише диспетчеризацію — жодної бізнес-логіки.
        /// </summary>
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding  = System.Text.Encoding.UTF8;
            Console.Clear();

            Service           service     = new Service();
            Menu              menu        = new Menu();
            Department        department  = new Department();
            CatalogDepartment catalogA    = new CatalogDepartment("Каталог A", 10);
            CatalogDepartment catalogB    = new CatalogDepartment("Каталог B", 10);

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
                    case "13":
                        catalogA.HandleAddDepartment(service, department);
                        break;
                    case "14":
                        catalogB.HandleAddDepartment(service, department);
                        break;
                    case "15":
                        catalogA.HandleShowCatalog(service);
                        break;
                    case "16":
                        catalogA.HandleCompareTo(service, catalogB);
                        break;
                    case "17":
                        catalogA.HandleSortByStudentsAndDisciplines(service);
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
