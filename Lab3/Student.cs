using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    public class Student
    {
        // Закриті поля
        private string name;
        private string educationProgram;
        private List<int> grades;
        private int workloadLevel;
        private string recordBookNumber;

        // 1. Конструктор за замовчуванням (без параметрів)
        public Student()
        {
            name = "";
            educationProgram = "";
            grades = new List<int>();
            workloadLevel = 0;
            recordBookNumber = "";
        }

        // 2. Конструктор з параметрами
        public Student(string name, string educationProgram, int workloadLevel, string recordBookNumber)
        {
            this.name = name;
            this.educationProgram = educationProgram;
            this.workloadLevel = workloadLevel;
            this.recordBookNumber = recordBookNumber;
            this.grades = new List<int>();
        }

        // 3. Конструктор копії
        public Student(Student other)
        {
            this.name = other.name;
            this.educationProgram = other.educationProgram;
            this.grades = new List<int>(other.grades);
            this.workloadLevel = other.workloadLevel;
            this.recordBookNumber = other.recordBookNumber;
        }

        // Відкриті властивості (get, set)
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string EducationProgram
        {
            get { return educationProgram; }
            set { educationProgram = value; }
        }

        public List<int> Grades
        {
            get { return new List<int>(grades); }
            set { grades = new List<int>(value); }
        }

        public int WorkloadLevel
        {
            get { return workloadLevel; }
            set { workloadLevel = value; }
        }

        public string RecordBookNumber
        {
            get { return recordBookNumber; }
            set { recordBookNumber = value; }
        }

        // Відкриті методи

        // Додати оцінку
        public void AddGrade(int grade)
        {
            grades.Add(grade);
        }

        // Переглянути оцінки — повертає список
        public List<int> ViewGrades()
        {
            return new List<int>(grades);
        }

        // Обчислити рейтинг
        public double CalculateRating()
        {
            if (grades.Count == 0)
            {
                return 0.0;
            }
            else
            {
                return grades.Average();
            }
        }
    }
}
