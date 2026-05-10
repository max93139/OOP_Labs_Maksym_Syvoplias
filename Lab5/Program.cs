using System;
using System.Collections.Generic;

namespace Lab5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.InputEncoding  = System.Text.Encoding.UTF8;
            Console.Clear();

            Service    service    = new Service("Console", "department_data.txt", "protocol.txt");
            Menu       menu       = new Menu();
            Department department = new Department();

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
