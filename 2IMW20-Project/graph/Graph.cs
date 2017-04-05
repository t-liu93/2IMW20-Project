using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.graph
{
    class Graph
    {
        public Dictionary<int, Vertex> V;
        public List<Edge> E;

        public Graph()
        {
            this.V = new Dictionary<int, Vertex>();
            this.E = new List<Edge>();
        }

        public Graph(Dictionary<int, Vertex> V, List<Edge> E)
        {
            this.V = V;
            this.E = E;

            foreach (Vertex v in V.Values)
            {
                v.neighbours = new Dictionary<int, Edge>();
            }

            foreach (Edge e in E)
            {
                V[e.u].AddEdgeToVertex(e.v, e);
                V[e.v].AddEdgeToVertex(e.u, e);
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
        /// Add an edge to the graph.
        /// </summary>
        /// <param name="e">The edge that is to be added.</param>
        public virtual void AddEdge(Edge e)
        {
            // Add edge to the list
            E.Add(e);

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
            E.Remove(e);

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
            foreach (KeyValuePair<long, Edge> e in rawData.GetEdges())
            {
                edges.Add(e.Value);
                float probability = 1f - (float)Math.Pow(Math.E, (-0.5 * counters[e.Key]));
                edges.Last().probability = probability;
            }

            return new Graph(vertices, edges);
        }
    }
}
