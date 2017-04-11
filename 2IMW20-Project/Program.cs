using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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


                //Check parser
                string facebook = "..//..//..//Data//facebook_combined.txt";
				string email = "..//..//..//Data//email-Enron.txt";
                string asskitter = "..//..//..//DATA//as-skitter.txt";
                string fullDblp = "..//..//..//Data//FullDBLP.xml";
                string brightkite = "..//..//..//Data//Brightkite_edges.txt";
                string gowalla = "..//..//..//Data//Gowalla_edges.txt";
                //dataset.RawData data = new dataset.RawDataSNAP(snapLocation);
                //data.BuildDataset();
                //foreach (KeyValuePair<Edge, int> kvp in data.GetEdges())
                //{
                //    Console.WriteLine("Edge number: " + kvp.Key.id + "u: " + kvp.Key.u + "v: " + kvp.Key.v + "app: " + kvp.Value);
                //}
                //Console.ReadKey();

                //Run tester and draw logs
                //Test.DatasetTest datasetTest = new Test.DatasetTest(location);
                //datasetTest.RunTest();
                //Console.ReadKey();
                System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
                //Following code is used to generate the dataset that will be used by Graph and algorithms
                Console.WriteLine("Start build dataset...");
                //dataset.RawData data = new dataset.RawDataDblp(fullDblp); //In the final version, location will be input from console.
                dataset.RawData data = new dataset.RawDataSNAP(gowalla);
                data.BuildDataset();

                Console.WriteLine("Dataset build finished.");


                //Now the dataset can be load by using following code:
                //dataset.GetNodes();
                //This will return a dictionary contains all nodes, in form <string, int>
                //The key of the dictionary will be the string from xml
                //The value of each key will be the node ID

                //dataset.GetEdges();
                //This will return a dictionary contains all edges, in form <Edge, int>
                //The edges are uncertain, contains an unique id, a vertix u and a vertix v
                //The value represents the time an edge appears
                //Also known as the weight of the edge


                // Execute algorithms:

                Console.WriteLine("Executing Degree Based...");
                timer.Start();
                ADR ADRAlgorithm = new ADR(UncertainGraph.constructFromDataset(data));
                ADRAlgorithm.Run();
                timer.Stop();
                ADRAlgorithm.PrintResults();
                
                File.WriteAllText(@"ADRElapsedTime.txt", timer.Elapsed.ToString());

                Console.WriteLine("Completed Degree Based");

                Console.WriteLine("Executing TRPW...");
                timer.Reset();
                timer.Start();
                TRPW TRPWAlgorithm = new TRPW(UncertainGraph.constructFromDataset(data));
                TRPWAlgorithm.Run();
                timer.Stop();
                TRPWAlgorithm.PrintResults();
                
                Console.WriteLine("Completed TRPW");
                File.WriteAllText(@"TRPWElapsedTime.txt", timer.Elapsed.ToString());

                // Write results
                
                Console.WriteLine("Do you wish to try another dataset? (Y/N)");

                if (Console.ReadLine() == "N")
                    exit = true;
                }
            }
        }
}
