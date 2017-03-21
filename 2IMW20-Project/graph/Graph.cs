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

            // Calculate vertex and triangle degrees
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

            // Calculate the expected degree of the vertex
            foreach (Edge e in E)
            {
                V[e.u].vertexDegree++;
                V[e.v].vertexDegree++;

                V[e.u].expectedVertexDegree += e.probability;
                V[e.v].expectedVertexDegree += e.probability;

                // TODO: Calculate triangle degree
            }
        }


        /// <summary>
        /// Add an edge to the graph.
        /// </summary>
        /// <param name="e">The edge that is to be added.</param>
        public void AddEdge(Edge e)
        {
            // Add edge to the list
            E.Add(e);

            // Update vertex degree
            V[e.u].vertexDegree++;
            V[e.v].vertexDegree++;

            // TODO: Update triangle degree

        }

        /// <summary>
        /// Remove an edge from the graph.
        /// </summary>
        /// <param name="e">The edge that is to be removed.</param>
        public void RemoveEdge(Edge e)
        {
            // Add edge to the list
            E.Remove(e);

            // Update vertex degree
            V[e.u].vertexDegree--;
            V[e.v].vertexDegree--;

            // TODO: Update triangle degree

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
            foreach (KeyValuePair<Edge, int> e in rawData.GetEdges())
            {
                edges.Add(e.Key);
                float probability = 1f - (float)Math.Pow(Math.E, (-0.5 * e.Value));
                edges.Last().probability = probability;
            }

            return new Graph(vertices, edges);
        }
    }
}
