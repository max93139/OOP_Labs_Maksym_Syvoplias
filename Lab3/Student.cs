using System;
using System.Collections.Generic;

namespace Lab3
{
    public class Student
    {
        private string name;
        private string educationProgram;
        private List<int> grades;
        private int workloadLevel;
        private string recordBookNumber;

        public Student(string name, string educationProgram, int workloadLevel, string recordBookNumber)
        {
            this.name = name;
            this.educationProgram = educationProgram;
            this.workloadLevel = workloadLevel;
            this.recordBookNumber = recordBookNumber;
            this.grades = new List<int>();
        }

        public void AddGrade(int grade)
        {
            throw new NotImplementedException();
        }

        public void ViewGrades()
        {
            throw new NotImplementedException();
        }

        public double CalculateRating()
        {
            throw new NotImplementedException();
        }
    }
}
