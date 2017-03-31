using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.parser
{
    class SNAPParser
    {
        private string location; //Location of the snap txt file
        private List<int> nodes; //node ids, in a list
        private List<Edge> edges; //Edges with only u and v, id is always -1, in a list

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="location">SNAP txt file location</param>
        public SNAPParser(string location)
        {
            this.location = location;
            this.nodes = new List<int>();
            this.edges = new List<Edge>();
        }

        /// <summary>
        /// Method to read a snap text dataset, parse it and save it to class variables
        /// </summary>
        public void ReadFile()
        {
            string line;
            string firstInt = "";
            string secondInt = "";
            int onelineNodeCounter = 1;
            System.IO.StreamReader textFile = new System.IO.StreamReader(this.location);
            while ((line = textFile.ReadLine()) != null)
            {
                if (IsNumber(line[0]))
                {
                    for (int i = 0; i < line.Length; i++)
                    {
                        if (IsNumber(line[i]) && (onelineNodeCounter == 1))
                        {
                            firstInt += line[i];
                        }
                        if (!(IsNumber(line[i])) && (onelineNodeCounter == 1))
                        {
                            onelineNodeCounter = 2;
                        }
                        if (IsNumber(line[i]) && (onelineNodeCounter == 2))
                        {
                            secondInt += line[i];
                        }
                    }
                    nodes.Add(int.Parse(firstInt));
                    nodes.Add(int.Parse(secondInt));
                    edges.Add(new Edge(-1, int.Parse(firstInt), int.Parse(secondInt)));
                }
                firstInt = "";
                secondInt = "";
                onelineNodeCounter = 1;
            }
        }

        /// <summary>
        /// Get private variable nodes
        /// </summary>
        /// <returns>The list of all nodes</returns>
        public List<int> GetNodes()
        {
            return this.nodes;
        }

        /// <summary>
        /// Get private variable edges
        /// </summary>
        /// <returns>The list of all edges</returns>
        public List<Edge> GetEdges()
        {
            return this.edges;
        }

        /// <summary>
        /// Check whether a character is a number, i.e. numeric character from 0 to 9
        /// </summary>
        /// <param name="ch">The character to be checked</param>
        /// <returns>Whether the character is a number</returns>
        private bool IsNumber(char ch)
        {
            return (ch >= 48 && ch <= 57);
        }

        /// <summary>
        /// Calculater the power of two integers
        /// </summary>
        /// <param name="number">The base number</param>
        /// <param name="exp">The exponential number</param>
        /// <returns></returns>
        private int PowerInteger(int number, int exp)
        {
            if (exp == 0)
            {
                return 1;
            }
            else
            {
                return number * PowerInteger(number, exp - 1);
            }
        }
    }
}
