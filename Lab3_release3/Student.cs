//Для перевірки і виправлення помилок використовувався Antigravity з моделью  Cloude Opus 4.6
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    /// <summary>
    /// Клас "Студент" — зберігає дані про студента та реалізує бізнес-логіку.
    /// Використовує Service для введення/виведення даних.
    /// </summary>
    public class Student
    {
        private string name;             // Ім'я студента
        private string educationProgram; // Спеціальність  підготовки
        private List<int> grades;        // Список оцінок
        private int workloadLevel;       // Обсяг навчального навантаження
        private double rating;           // Рейтинг успішності
        private int studentNumber;       // Номер студента
        private string admissionReason;  // Причина додавання/вступу

        public Student()
        {
            name             = "";
            educationProgram = "";
            grades           = new List<int>();
            workloadLevel    = 0;
            rating           = 0.0;
            studentNumber    = 0;
            admissionReason  = "";
        }
        public Student(string name, string educationProgram,
                       int workloadLevel, int studentNumber = 0)
        {
            this.name             = name;
            this.educationProgram = educationProgram;
            this.workloadLevel    = workloadLevel;
            this.studentNumber    = studentNumber;
            this.grades           = new List<int>();
            this.rating           = 0.0;
            this.admissionReason  = "";
        }
                public string EducationProgram
        {
            get
            {
                return educationProgram;
            }
            set
            {
                educationProgram = value;
            }
        }

        /// <summary>
        /// Отримує або встановлює список оцінок студента.
        /// </summary>
        public List<int> Grades
        {
            get
            {
                return new List<int>(grades);
            }
            set
            {
                grades = new List<int>(value);
            }
        }

        /// <summary>
        /// Отримує або встановлює Обсяг навчального навантаження (у кредитах ЄКТС).
        /// </summary>
        public int WorkloadLevel
        {
            get
            {
                return workloadLevel;
            }
            set
            {
                workloadLevel = value;
            }
        }


        /// <summary>
        /// Отримує поточний рейтинг студента (середній бал).
        /// Зовні можна прочитати, але змінювати — лише через відповідний метод.
        /// </summary>
        public double Rating
        {
            get
            {
                return rating;
            }
        }

        /// <summary>
        /// Отримує або встановлює порядковий номер студента в системі/списку.
        /// </summary>
        public int StudentNumber
        {
            get
            {
                return studentNumber;
            }
            set
            {
                studentNumber = value;
            }
        }

        /// <summary>
        /// Отримує або встановлює причину вступу/додавання студента.
        /// </summary>
        public string AdmissionReason
        {
            get
            {
                return admissionReason;
            }
            set
            {
                admissionReason = value;
            }
        }


    }
}