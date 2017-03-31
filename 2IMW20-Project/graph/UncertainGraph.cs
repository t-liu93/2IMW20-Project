using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.graph
{
    class UncertainGraph : Graph
    {
        public UncertainGraph() : base()
        {

        }

        public UncertainGraph(Dictionary<int ,Vertex> V, List<Edge> E) : base(V, E)
        {

        }


        /// <summary>
        /// Calculates the expected vertex and triangle degrees for all vertices.
        /// </summary>
        public override void CalculateDegrees()
        {
            // Reset vertex and triangle degrees
            for (int i = 0; i < V.Count; i++)
            {
                V[i].expectedTriangleDegree = 0f;
                V[i].expectedVertexDegree = 0f;
            }

            // Calculate the expected degree of the vertex
            foreach (Edge e in E)
            {
                V[e.u].expectedVertexDegree += e.probability;
                V[e.v].expectedVertexDegree += e.probability;

                // TODO: Calculate triangle degree
            }
        }


        /// <summary>
        /// Construct a graph from a dataset
        /// </summary>
        /// <returns>The resulting graph</returns>
        public static new UncertainGraph constructFromDataset(dataset.RawData rawData)
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

            return new UncertainGraph(vertices, edges);
        }
    }
}
