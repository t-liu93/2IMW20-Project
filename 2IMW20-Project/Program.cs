using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2IMW20_Project.graph;

namespace _2IMW20_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //bool exit = false;
            //while (!exit)
            //{
            //    bool hasDataset = false;
            //    while (!hasDataset)
            //    {
            //        Console.WriteLine("Please specify a dataset:");
            //        string datasetPath = Console.ReadLine();

            //        if (datasetPath != "") // TODO: Check whether dataset exists & is valid
            //            hasDataset = true;
            //        else
            //            Console.WriteLine("Invalid path!");
            //    }


            //    // Parse data

            //    // Execute algorithms:

            //    Console.WriteLine("Executing Degree Based...");
            //    Console.WriteLine("Completed Degree Based");

            //    Console.WriteLine("Executing TRPW...");
            //    Console.WriteLine("Completed TRPW");


            //    // Write results



            //    Console.WriteLine("Do you wish to try another dataset? (Y/N)");

            //    if (Console.ReadLine() == "N")
            //        exit = true;
            //}

            //Check parser
            string location = "file:///D:/smallDblp.xml";

            //parser.Parser parser = new parser.Parser(location);
            //parser.Load();
            //Dictionary<string, int> testList = parser.GetNodesToDictionary("author");
            //for (int i = 0; i < testList.Count; i++)
            //{
            //    int value;
            //    testList.TryGetValue(i, out value);
            //    Console.WriteLine(value);
            //}
            //foreach (KeyValuePair<string, int> kvp in testList)
            //{
            //    Console.Write(kvp.Key);
            //    Console.WriteLine(kvp.Value);
            //}
            //Console.ReadKey();

            dataset.RawDataDblp r = new dataset.RawDataDblp(location);
            r.getNodes();
            r.buildEdges();
            Dictionary<Edge, int> test = r.GetEdges();
            Dictionary<string, int> testNodes = r.GetNodes();
            foreach (KeyValuePair<Edge, int> kvp in test)
            {
                Console.WriteLine("ID " + kvp.Key.id + " U " + kvp.Key.u + " V " + kvp.Key.v);
                Console.WriteLine("Counter " + kvp.Value);
            }
            //foreach (KeyValuePair<string, int> kvp in testNodes)
            //{
            //    Console.WriteLine("Author: " + kvp.Key + " id: " + kvp.Value);
            //}
            Console.ReadKey();
        }
    }
}
