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
            string location = "file:///D:/ebay.xml";

            Parser parser = new Parser(location);
            parser.Load();
            Dictionary<int,string> testList = parser.GetNodesToMap("seller_name");
            for (int i = 0; i < testList.Count; i ++)
            {
                string value = "";
                testList.TryGetValue(i, out value);
                Console.WriteLine(value);
            }
            Console.ReadKey();
        }
    }
}
