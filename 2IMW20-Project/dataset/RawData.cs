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
		protected Dictionary<long, Edge> hashedEdges; //Edges, with hashed edge id
        protected Dictionary<Edge, int> edges; //Edges, key Edge, value integer counter, a.k.a. times appear
        protected int edgeIdCounter;
        protected string location; //Dataset location

        public RawData(string location)
        {
            this.nodes = new Dictionary<string, int>();
            this.edges = new Dictionary<Edge, int>();
			this.hashedEdges = new Dictionary<long, Edge>();
            this.edgeIdCounter = 0;
            this.location = location;
        }

        /// <summary>
        /// BuildNodes method, should be overrided in each kind of dataset
        /// </summary>
        public virtual void BuildNodes()
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
                this.nodes.Add(key, GetMaxNodeId(nodes) + 1);
            }
        }

        /// <summary>
        /// Add edge to a dictionary from xml
        /// </summary>
        /// <param name="u">vertix u of the edge</param>
        /// <param name="v">vertix v of the edge</param>
        public void AddEdge(int u, int v)
        {
            //int i = GetEdgeId(u, v);
            //if (i != -1)
            //{
            //    Edge e = GetEdgeById(i);
            //    edges[e]++;
            //}
            //else
            //{
            //    edges.Add(new Edge(GetMaxEdgeId(this.edges) + 1, u, v), 1);
            //}
            long hashedId = Hash(u, v);
            if (hashedEdges.ContainsKey(hashedId))
            {
                edges[hashedEdges[hashedId]]++;
            }
            else
            {
                hashedEdges.Add(hashedId, new Edge(edgeIdCounter++, u, v));
                edges.Add(hashedEdges[hashedId], 1);
            }
        }

        //Get class variables

        /// <summary>
        /// Get nodes dictionary
        /// </summary>
        /// <returns>Nodes dictionary</returns>
        public Dictionary<string, int> GetNodes()
        {
            return this.nodes;
        }
        
        /// <summary>
        /// Get edges dictionary
        /// </summary>
        /// <returns>Edges dictionary</returns>
        public Dictionary<Edge, int> GetEdges()
        {
            return this.edges;
        }

        /// <summary>
        /// Find current max value (a.k.a. node id) in the nodes dictionary
        /// </summary>
        /// <param name="dic">the input node dictionary</param>
        /// <returns></returns>
        private int GetMaxNodeId(Dictionary<string, int> dic)
        {
            int max = -1;
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
        protected int GetMaxEdgeId(Dictionary<Edge, int> edge)
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

        /// <summary>
        /// Build dataset including nodes and edges
        /// Be overrided by sub classes
        /// </summary>
        public virtual void BuildDataset()
        {
        }

        /// <summary>
        /// Check if the two vertices matches an edge in the dictionary
        /// We only check two vertices
        /// If two vertices matches one edge in the dictionary, then the edge exists. 
        /// </summary>
        /// <param name="u">Vertix u</param>
        /// <param name="v">Vertix v</param>
        /// <returns>A boolean variable indicates whether the edge exists</returns>
        //protected bool ContainsEdge(int u, int v)
        //{
        //    foreach(KeyValuePair<Edge, int> kvp in this.edges)
        //    {
        //        if ((u == kvp.Key.u) && (v == kvp.Key.v))
        //        {
        //            return true;
        //        }
        //        else if ((u == kvp.Key.v) && (v == kvp.Key.u))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        /// <summary>
        /// When an edge exisits in this.edges
        /// Then check and return the edge id of it.
        /// </summary>
        /// <param name="e">The input edge to be checked</param>
        /// <returns>The corresponding edge id</returns>
        protected int GetEdgeId(int u, int v)
        {
            foreach(KeyValuePair<Edge, int> kvp in this.edges)
            {
                if ((u == kvp.Key.u) && (v == kvp.Key.v))
                {
                    return kvp.Key.id;
                }
                else if ((u == kvp.Key.v) && (v == kvp.Key.u))
                {
                    return kvp.Key.id; 
                }
            }
            return -1;
        }

        /// <summary>
        /// Get an edge by its corresponding ID
        /// </summary>
        /// <param name="id">The ID to identify an edge</param>
        /// <returns>An edge with corresponding ID</returns>
        protected Edge GetEdgeById(int id)
        {
            Edge e = new Edge(-1, -1, -1);
            foreach(KeyValuePair<Edge, int> kvp in this.edges)
            {
                if (id == kvp.Key.id)
                {
                    e = kvp.Key;
                }
            }
            return e;
        }

		private long Hash(int u, int v)
		{
			var A = (ulong)(u >= 0 ? 2 * (long)u : -2 * (long)u - 1);
			var B = (ulong)(v >= 0 ? 2 * (long)v : -2 * (long)v - 1);
			var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
			return u < 0 && v< 0 || u >= 0 && v >= 0 ? C : -C - 1;
		}
    }
}
