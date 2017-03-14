using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.graph
{
    class Graph
    {
        public List<Vertex> V;
        public List<Edge> E;

        public Graph()
        {
            this.V = new List<Vertex>();
            this.E = new List<Edge>();
        }

        public Graph(List<Vertex> V, List<Edge> E)
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

            // TODO: Update vertex degree
            // TODO: Update triangle degree
        }
        

        /// <summary>
        /// Construct a graph from a dataset
        /// </summary>
        /// <returns>The resulting graph</returns>
        public static Graph constructFromDataset()
        {
            List<Vertex> vertices = new List<Vertex>();
            List<Edge> edges = new List<Edge>();

            return new Graph(vertices, edges);
        }
    }
}
