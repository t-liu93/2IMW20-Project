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
            bool exit = false;
            while (!exit)
            {
                bool hasDataset = false;
                while (!hasDataset)
                {
                    Console.WriteLine("Please specify a dataset:");
                    string datasetPath = Console.ReadLine();

                    if (datasetPath != "") // TODO: Check whether dataset exists & is valid
                        hasDataset = true;
                    else
                        Console.WriteLine("Invalid path!");
                }


                // Parse data

                // Execute algorithms:

                Console.WriteLine("Executing Degree Based...");
                Console.WriteLine("Completed Degree Based");

                Console.WriteLine("Executing TRPW...");
                Console.WriteLine("Completed TRPW");


                // Write results



                Console.WriteLine("Do you wish to try another dataset? (Y/N)");

                if (Console.ReadLine() == "N")
                    exit = true;
            }
        }
    }
}
