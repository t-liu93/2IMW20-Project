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
        private StringBuilder actualDegree;
        private StringBuilder actualTriangleDegree;
        private StringBuilder actualCoefficient;

        public CsvWriterTRPW()
        {
            this.actualDegree = new StringBuilder();
            this.actualTriangleDegree = new StringBuilder();
            this.actualCoefficient = new StringBuilder();
        }

        public void AppendActualDegree(string newLine)
        {
            this.actualDegree.AppendLine(newLine);
        }

        public void AppendActualTriangleDegree(string newLine)
        {
            this.actualTriangleDegree.AppendLine(newLine);
        }

        public void AppendActualCoefficient(string newLine)
        {
            this.actualCoefficient.AppendLine(newLine);
        }

        public void WriteFile()
        {
            File.WriteAllText(@"ActualDegreeTRPW.csv", this.actualDegree.ToString());
            File.WriteAllText(@"ActualTriangleDegreeTRPW.csv", this.actualTriangleDegree.ToString());
            File.WriteAllText(@"ActualCoefficientTRPW.csv", this.actualCoefficient.ToString());
        }

    }
}
