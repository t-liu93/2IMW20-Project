using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace _2IMW20_Project.Test
{
    class DatasetTest
    {
        private string location;
        private dataset.RawDataDblp dataset;
        private Stopwatch stopwatch;

        public DatasetTest(string location)
        {
            this.location = location;
            this.dataset = new dataset.RawDataDblp(location);
            this.stopwatch = new Stopwatch();
        }

        public void RunTest()
        {
            stopwatch.Start();
            dataset.BuildDataset();
            stopwatch.Stop();
            Console.WriteLine("Total articles processed: " + dataset.GetArticleNumber());
            Console.WriteLine("Total nodes generated: " + dataset.GetNodes().Count);
            Console.WriteLine("Total edges generated: " + dataset.GetEdges().Count);
            Console.WriteLine("Elapsed time: " + stopwatch.ElapsedMilliseconds / 1000 + " seconds");
        }
    }
}
