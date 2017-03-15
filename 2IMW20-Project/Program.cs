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
            string location = "file:///C:/smallDblp.xml";

            parser.Parser parser = new parser.Parser(location);
            parser.Load();
            Dictionary<string, int> testList = parser.GetNodesToMap("author");
            //for (int i = 0; i < testlist.count; i ++)
            //{
            //    int value;
            //    testlist.trygetvalue(i, out value);
            //    console.writeline(value);
            //}
            foreach (KeyValuePair<string, int> kvp in testList)
            {
                Console.Write(kvp.Key);
                Console.WriteLine(kvp.Value);
            }
            Console.ReadKey();
        }
    }
}
