using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.parser
{
    /// <summary>
    /// This class stores the raw data parsed from the DBLP XML file.
    /// It create an instance of parser and get all nodes with tag author of articles
    /// It create edges when two authors co-authored the same article. 
    /// </summary>
    class RawDataDblp
    {
        private Dictionary<string, int> nodes;
        private Dictionary<Edge, int> edges;
        private string location;

        public RawDataDblp(string location)
        {
            this.nodes = new Dictionary<string, int>();
            this.edges = new Dictionary<Edge, int>();
            this.location = location;
        }

        public void Parse()
        {
            Parser parser = new Parser(this.location);
            nodes = parser.GetNodesToDictionary("authors");
        }

        public void AddNode(string key, int value)
        {
            if (! nodes.ContainsKey(key))
            {
                nodes.Add(key, value);
            }
        }

        public void AddEdges(Edge edge, int counter)
        {
            if (! edges.ContainsKey(edge))
            {
                edges.Add(edge, 1);
            }
            else
            {
                edges[edge]++;
            }
        }

        public Dictionary<string, int> GetNodes()
        {
            return this.nodes;
        }

        public Dictionary<Edge, int> GetEdges()
        {
            return this.edges;
        }

        public void buildEdges()
        {

        }
    }


}
