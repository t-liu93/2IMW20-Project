using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.dataset
{
    /// <summary>
    /// Raw data class
    /// Stores raw data in two dictionaries indicates vertices (a.k.a. nodes)
    /// and Edges
    /// </summary>
    class RawData
    {
        protected Dictionary<string, int> nodes; //Nodes, key string, value integer ID
        protected Dictionary<Edge, int> edges; //Edges, key Edge, value integer ID
        protected string location; //Dataset location

        public RawData(string location)
        {
            this.nodes = new Dictionary<string, int>();
            this.edges = new Dictionary<Edge, int>();
            this.location = location;
        }

        /// <summary>
        /// getNodes method, should be overrided in each kind of dataset
        /// </summary>
        public virtual void getNodes()
        {
        }

        /// <summary>
        /// Add a node to the dictionary
        /// </summary>
        /// <param name="key">the key string from xml file</param>
        /// <param name="value">the integer value indicates the id</param>
        public void AddNode(string key, int value)
        {
            if (!nodes.ContainsKey(key))
            {
                nodes.Add(key, getMax(nodes) + 1);
            }
        }

        /// <summary>
        /// Add edge to a dictionary from xml
        /// </summary>
        /// <param name="edge">The edge to be input</param>
        public void AddEdge(Edge edge)
        {
            if (!edges.ContainsKey(edge))
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

        /// <summary>
        /// Find current max value (a.k.a. node id) in the nodes dictionary
        /// </summary>
        /// <param name="dic">the input node dictionary</param>
        /// <returns></returns>
        private int getMax(Dictionary<string, int> dic)
        {
            int max = 0;
            foreach(KeyValuePair<string, int> kvp in dic)
            {
                if (kvp.Value >= max)
                {
                    max = kvp.Value;
                }
            }

            return max;
        }

        /// <summary>
        /// Check the maximal ID exists
        /// </summary>
        /// <param name="edge">Edges to be checked</param>
        /// <returns></returns>
        protected int getEdgeId(Dictionary<Edge, int> edge)
        {
            int max = 0;

            foreach(KeyValuePair<Edge, int> kvp in edge)
            {
                if (kvp.Key.id >= max)
                {
                    max = kvp.Key.id;
                }
            }

            return max;
        }
    }
}
