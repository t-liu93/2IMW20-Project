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

        public UncertainGraph(List<Vertex> V, List<Edge> E) : base(V, E)
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
                V[i].expectedVertexDegree = 0f;
                V[i].expectedVertexDegree = 0f;
            }

            // Calculate the expected degree of the vertex
            foreach (UncertainEdge e in E)
            {
                V[e.u].expectedVertexDegree += e.probability;
                V[e.v].expectedVertexDegree += e.probability;

                // TODO: Calculate triangle degree
            }
        }
    }
}
