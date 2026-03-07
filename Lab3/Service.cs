using System;

namespace Lab3
{
    public class Service
    {
        private string outputFormat;
        private string filePath;
        private string data;

        public Service(string outputFormat, string filePath)
        {
            this.outputFormat = outputFormat;
            this.filePath = filePath;
            this.data = string.Empty;
        }

        public void ReadFromConsole()
        {
            throw new NotImplementedException();
        }

        public void WriteToConsole(string data)
        {
            throw new NotImplementedException();
        }

        public void ReadFromFile()
        {
            throw new NotImplementedException();
        }

        public void WriteToFile(string data)
        {
            throw new NotImplementedException();
        }
    }
}
