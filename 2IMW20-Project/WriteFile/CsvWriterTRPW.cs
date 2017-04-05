using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2IMW20_Project.WriteFile
{
    class CsvWriterTRPW
    {
        private StringBuilder expectedDegree;
        private StringBuilder actualDegree;
        private StringBuilder expectedTriangleDegree;
        private StringBuilder actualTriangleDegree;

        public CsvWriterTRPW()
        {
            this.expectedDegree = new StringBuilder();
            this.actualDegree = new StringBuilder();
            this.expectedTriangleDegree = new StringBuilder();
            this.actualTriangleDegree = new StringBuilder();
        }

        public void AppendExpectedDegree(string newLine)
        {
            this.expectedDegree.AppendLine(newLine);
        }

        public void AppendActualDegree(string newLine)
        {
            this.actualDegree.AppendLine(newLine);
        }

        public void AppendExpectedTriangleDegree(string newLine)
        {
            this.expectedTriangleDegree.AppendLine(newLine);
        }

        public void AppendActualTriangleDegree(string newLine)
        {
            this.actualTriangleDegree.AppendLine(newLine);
        }

        public void WriteFile()
        {
            File.WriteAllText(@"ExpectedDegreeTRPW.csv", this.expectedDegree.ToString());
            File.WriteAllText(@"ActualDegreeTRPW.csv", this.actualDegree.ToString());
            File.WriteAllText(@"ExpectedTriangleDegreeTRPW.csv", this.expectedTriangleDegree.ToString());
            File.WriteAllText(@"ActualTriangleDegreeTRPW.csv", this.actualTriangleDegree.ToString());
        }

    }
}
