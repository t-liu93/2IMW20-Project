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
		protected Dictionary<long, Edge> edges; //Edges, with hashed edge id
		protected Dictionary<long, int> edgeCounters; //Edges, key Edge, value integer counter, a.k.a. times appear
		protected int edgeIdCounter;
		protected int nodeIdCounter;
		protected string location; //Dataset location

		public RawData(string location)
		{
			this.nodes = new Dictionary<string, int>();
			this.edgeCounters = new Dictionary<long, int>();
			this.edges = new Dictionary<long, Edge>();
			this.edgeIdCounter = 0;
			this.nodeIdCounter = 0;
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
				this.nodes.Add(key, nodeIdCounter);
				nodeIdCounter++;
			}
		}

		/// <summary>
		/// Add edge to a dictionary from xml
		/// </summary>
		/// <param name="u">vertix u of the edge</param>
		/// <param name="v">vertix v of the edge</param>
		public void AddEdge(int u, int v)
		{
            if (u == v)
                return;
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
			long edgeId = Hash(u, v);
			if (edges.ContainsKey(edgeId))
			{
				edgeCounters[edgeId]++;
			}
			else
			{
				edges.Add(edgeId, new Edge(edgeIdCounter++, u, v));
				edgeCounters.Add(edgeId, 1);
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
		public Dictionary<long, Edge> GetEdges()
		{
			return this.edges;
		}

		/// <summary>
		/// Get edge counter dictionary
		/// </summary>
		/// <returns>Edge counters dictionary</returns>
		public Dictionary<long, int> GetEdgeCounters()
		{
			return this.edgeCounters;
		}

		/// <summary>
		/// Build dataset including nodes and edges
		/// Be overrided by sub classes
		/// </summary>
		public virtual void BuildDataset()
		{
		}

		
		private long Hash(int u, int v)
		{
			int a = Math.Min(u, v);
			int b = Math.Max(u, v);
			var A = (ulong)(a >= 0 ? 2 * (long)a : -2 * (long)a - 1);
			var B = (ulong)(b >= 0 ? 2 * (long)b : -2 * (long)b - 1);
			var C = (long)((A >= B ? A * A + A + B : A + B * B) / 2);
			return a < 0 && b< 0 || a >= 0 && b >= 0 ? C : -C - 1;
		}
	}
}
