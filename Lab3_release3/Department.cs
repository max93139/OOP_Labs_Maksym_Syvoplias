//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lab3
{
    /// <summary>
    /// Клас "Кафедра" — зберігає дані про кафедру та реалізує бізнес-логіку.
    /// Використовує Service для введення/виведення даних.
    /// </summary>
    public class Department
    {
       // Закриті поля 
        private string name;
        private int studentCount;
        private string educationProgram;
        private List<string> disciplines;
        private int maxStudents;
        private List<Student> students;

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Department"/> зі значеннями за замовчуванням.
        /// </summary>
        public Department()
        {
            name             = "";
            studentCount     = 0;
            educationProgram = "";
            disciplines      = new List<string>();
            maxStudents      = 0;
            students         = new List<Student>();
        }

        /// <summary>
        /// Ініціалізує новий екземпляр класу <see cref="Department"/> із заданими параметрами.
        /// </summary>
        public Department(string name, int studentCount, string educationProgram,
                          List<string> disciplines, int maxStudents)
        {
            this.name             = name;
            this.studentCount     = studentCount;
            this.educationProgram = educationProgram;
            this.disciplines      = new List<string>(disciplines);
            this.maxStudents      = maxStudents;
            this.students         = new List<Student>();
        }

        //  Властивості 

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int StudentCount
        {
            get { return studentCount; }
            set { studentCount = value; }
        }

        public string EducationProgram
        {
            get { return educationProgram; }
            set { educationProgram = value; }
        }

        public List<string> Disciplines
        {
            get { return new List<string>(disciplines); }
            set { disciplines = new List<string>(value); }
        }

        public int MaxStudents
        {
            get { return maxStudents; }
            set { maxStudents = value; }
        }

        public IReadOnlyList<Student> Students
        {
            get { return students.AsReadOnly(); }
        }

    
    }
}
