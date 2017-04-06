using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.graph
{
    class Graph
    {
        private Dictionary<Edge, int> _edgeListHash; //Edgelist is a combination of a List and Dictionary to be able to do all calls in constant time
        public Dictionary<int, Vertex> V;
        public List<Edge> E;

        public Graph()
        {
            this.V = new Dictionary<int, Vertex>();
            this.E = new List<Edge>();
            this._edgeListHash = new Dictionary<Edge, int>();
        }

        public Graph(Dictionary<int, Vertex> V, List<Edge> E)
        {
            this.V = V;
            this.E = E;
            this._edgeListHash = new Dictionary<Edge, int>();

            foreach (Vertex v in V.Values)
            {
                v.neighbours = new SortedList<int, Edge>();
            }


            for (int i = 0; i < E.Count(); i++)
            {
                _edgeListHash.Add(E[i], i);
                V[E[i].u].AddEdgeToVertex(E[i].v, E[i]);
                V[E[i].v].AddEdgeToVertex(E[i].u, E[i]);
            }
            

            //Calculate vertex and triangle degrees
            CalculateDegrees();
        }

        /// <summary>
        /// Calculates the vertex and triangle degrees for all vertices.
        /// </summary>
        public virtual void CalculateDegrees()
        {
            // Reset vertex and triangle degrees
            for (int i = 0; i < V.Count; i++)
            {
                V[i].vertexDegree = 0;
                V[i].triangleDegree = 0;
            }

            // Calculate the expected degree of both vertices
            
            foreach (Edge e in E)
            {
                V[e.u].vertexDegree++;
                V[e.v].vertexDegree++;

                // Calculate triangle degree of both vertices
                foreach (int neighbour in V[e.u].neighbours.Keys)
                {
                    if (V[e.v].neighbours.ContainsKey(neighbour))
                    {
                        V[e.u].triangleDegree += 0.5f;
                        V[e.v].triangleDegree += 0.5f;
                    }
                }
            }

        }

        /// <summary>
        /// Remove edge from both the edgelist as the edgelisthashing
        /// </summary>
        protected void RemoveFromEdges(Edge e)
        {
            Edge tempEdge = E.Last();
            int tempIndex = _edgeListHash[e];
            E[tempIndex] = tempEdge;
            _edgeListHash[tempEdge] = tempIndex;
            E.RemoveAt(E.Count() - 1);
            _edgeListHash.Remove(e);
        }

        /// <summary>
        /// Add edge to both the edgelist as the edgelisthashing
        /// </summary>
        protected void AddToEdges(Edge e)
        {
            E.Add(e);
            _edgeListHash.Add(e, E.Count() - 1);
        }

        /// <summary>
        /// Add an edge to the graph.
        /// </summary>
        /// <param name="e">The edge that is to be added.</param>
        public virtual void AddEdge(Edge e)
        {
            // Add edge to the list
            AddToEdges(e);

            // Update vertex degree
            V[e.u].vertexDegree++;
            V[e.v].vertexDegree++;

            V[e.u].AddEdgeToVertex(e.v, e);
            V[e.v].AddEdgeToVertex(e.u, e);

            // TODO: Update triangle degree
            foreach (int neighbour in V[e.u].neighbours.Keys)
            {
                if (V[e.v].neighbours.ContainsKey(neighbour))
                {
                    V[e.u].triangleDegree++;
                    V[neighbour].triangleDegree++;
                    V[e.v].triangleDegree++;
                }
            }
        }

        /// <summary>
        /// Remove an edge from the graph.
        /// </summary>
        /// <param name="e">The edge that is to be removed.</param>
        public virtual void RemoveEdge(Edge e)
        {
            // Add edge to the list
            RemoveFromEdges(e);

            // Update vertex degree
            V[e.u].vertexDegree--;
            V[e.v].vertexDegree--;

            // Update triangle degree
            foreach (int neighbour in V[e.u].neighbours.Keys)
            {
                if (V[e.v].neighbours.ContainsKey(neighbour))
                {
                    V[e.u].triangleDegree--;
                    V[neighbour].triangleDegree--;
                    V[e.v].triangleDegree--;
                }
            }

            V[e.u].RemoveEdge(e.v);
            V[e.v].RemoveEdge(e.u);
        }

        /// <summary>
        /// Construct a graph from a dataset
        /// </summary>
        /// <param name="rawData">Raw data parsed from XML file</param>
        /// <returns>The resulting graph</returns>
        public static Graph constructFromDataset(dataset.RawData rawData)
        {
            Dictionary<int, Vertex> vertices = new Dictionary<int, Vertex>();
            foreach (KeyValuePair<string, int> v in rawData.GetNodes())
            {

                vertices.Add(v.Value, new Vertex(v.Value));
            }

            List<Edge> edges = new List<Edge>();

            Dictionary<long, int> counters = rawData.GetEdgeCounters();
            float probability = 0.0f;
            foreach (KeyValuePair<long, Edge> e in rawData.GetEdges())
            {
                probability = 1f - (float)Math.Pow(Math.E, (-0.5 * counters[e.Key]));
                e.Value.probability = probability;
                edges.Add(e.Value);
            }

            return new Graph(vertices, edges);
        }
    }
}
