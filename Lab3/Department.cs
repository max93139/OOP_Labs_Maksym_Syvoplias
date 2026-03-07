using System;
using System.Collections.Generic;

namespace Lab3
{
    public class Department
    {
        private string name;
        private int studentCount;
        private string educationProgram;
        private List<string> disciplines;
        private int maxStudents;

        // Зв'язок 1 Department ------------------------ * Student (навчає)
        public List<Student> Students { get; private set; }

        public Department(string name, string educationProgram, int maxStudents)
        {
            this.name = name;
            this.educationProgram = educationProgram;
            this.maxStudents = maxStudents;
            this.studentCount = 0;
            this.disciplines = new List<string>();
            this.Students = new List<Student>();
        }

        public void IncreaseStudents()
        {
            throw new NotImplementedException();
        }

        public void DecreaseStudents()
        {
            throw new NotImplementedException();
        }

        public void AddDiscipline(string discipline)
        {
            throw new NotImplementedException();
        }

        public void RemoveDiscipline(string discipline)
        {
            throw new NotImplementedException();
        }

        public void AccreditationCheck()
        {
            throw new NotImplementedException();
        }
    }
}
