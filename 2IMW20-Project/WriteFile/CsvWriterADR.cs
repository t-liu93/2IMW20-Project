using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _2IMW20_Project.WriteFile
{
    class CsvWriterADR
    {
        private StringBuilder expectedDegree;
        private StringBuilder actualDegree;
        private StringBuilder expectedTriangleDegree;
        private StringBuilder actualTriangleDegree;
        private StringBuilder expectedCoefficient;
        private StringBuilder actualCoefficient;

        public CsvWriterADR()
        {
            this.expectedDegree = new StringBuilder();
            this.actualDegree = new StringBuilder();
            this.expectedTriangleDegree = new StringBuilder();
            this.actualTriangleDegree = new StringBuilder();
            this.expectedCoefficient = new StringBuilder();
            this.actualCoefficient = new StringBuilder();
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
        public void AppendExpectedCoefficient(string newLine)
        {
            this.expectedCoefficient.AppendLine(newLine);
        }
        public void AppendActualCoefficient(string newLine)
        {
            this.actualCoefficient.AppendLine(newLine);
        }

        public void WriteFile()
        {
            File.WriteAllText(@"ExpectedDegree.csv", this.expectedDegree.ToString());
            File.WriteAllText(@"ActualDegreeADR.csv", this.actualDegree.ToString());
            File.WriteAllText(@"ExpectedTriangleDegree.csv", this.expectedTriangleDegree.ToString());
            File.WriteAllText(@"ActualTriangleDegreeADR.csv", this.actualTriangleDegree.ToString());
            File.WriteAllText(@"ExpectedCoefficient.csv", this.expectedCoefficient.ToString());
            File.WriteAllText(@"ActualCoefficientADR.csv", this.actualCoefficient.ToString());
        }
    }
}
