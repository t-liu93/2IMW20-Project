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
            string location = "smallDblp.xml";

            //Following code is used to generate the dataset that will be used by Graph and algorithms

            dataset.RawData dataset = new dataset.RawDataDblp(location); //In the final version, location will be input from console.
            dataset.BuildDataset();

            //Now the dataset can be load by using following code:
            dataset.GetNodes();
            //This will return a dictionary contains all nodes, in form <string, int>
            //The key of the dictionary will be the string from xml
            //The value of each key will be the node ID

            dataset.GetEdges();
            //This will return a dictionary contains all edges, in form <Edge, int>
            //The edges are uncertain, contains an unique id, a vertix u and a vertix v
            //The value represents the time an edge appears
            //Also known as the weight of the edge
        }
    }
}
