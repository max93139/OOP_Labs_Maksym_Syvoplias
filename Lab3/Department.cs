using System;
using System.Collections.Generic;

namespace Lab3
{
    public class Department
    {
        // Закриті поля
        private string name;
        private int studentCount;
        private string educationProgram;
        private List<string> disciplines;
        private int maxStudents;

        // 1. Конструктор за замовчуванням (без параметрів)
        public Department()
        {
            name = "";
            studentCount = 0;
            educationProgram = "";
            disciplines = new List<string>();
            maxStudents = 0;
        }

        // 2. Конструктор з параметрами
        public Department(string name, int studentCount, string educationProgram, List<string> disciplines, int maxStudents)
        {
            this.name = name;
            this.studentCount = studentCount;
            this.educationProgram = educationProgram;
            this.disciplines = new List<string>(disciplines);
            this.maxStudents = maxStudents;
        }

        // 3. Конструктор копії
        public Department(Department other)
        {
            this.name = other.name;
            this.studentCount = other.studentCount;
            this.educationProgram = other.educationProgram;
            this.disciplines = new List<string>(other.disciplines);
            this.maxStudents = other.maxStudents;
        }

        // Відкриті властивості (get, set)
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

        // Відкриті методи

        // Збільшити кількість студентів
        public void IncreaseStudents()
        {
            if (studentCount < maxStudents)
                studentCount++;
        }

        // Зменшити кількість студентів
        public void DecreaseStudents()
        {
            if (studentCount > 0)
                studentCount--;
        }

        // Додати дисципліну
        public void AddDiscipline(string discipline)
        {
            if (!disciplines.Contains(discipline))
                disciplines.Add(discipline);
        }

        // Видалити дисципліну
        public void RemoveDiscipline(string discipline)
        {
            disciplines.Remove(discipline);
        }

        // Перевірка акредитації — повертає true/false (вивід робить Service)
        public bool AccreditationCheck()
        {
            return disciplines.Count >= 3 && studentCount > 0;
        }
    }
}
