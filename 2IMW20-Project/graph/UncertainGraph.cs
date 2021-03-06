﻿using System;
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

                // Calculate triangle degrees
                foreach (KeyValuePair<int, Edge> neighbour in V[e.u].neighbours)
                {
                    if (V[e.v].neighbours.ContainsKey(neighbour.Key))
                    {
                        //calculate triangle probability by multiplying all edge probabilities of the triangle
                        float triangleProbability = e.probability * neighbour.Value.probability * V[e.v].neighbours[neighbour.Key].probability;

                        //We find the same triangle multiple times, so we only add the value for the current edge
                        V[e.u].expectedTriangleDegree += (0.5f * triangleProbability);
                        V[e.v].expectedTriangleDegree += (0.5f * triangleProbability);

                    }
                }
            }
        }

        /// <summary>
        /// Add an edge to the graph.
        /// </summary>
        /// <param name="e">The edge that is to be added.</param>
        public override void AddEdge(Edge e)
        {
            // Add edge to the list
            AddToEdges(e);

            // Update vertex degree
            V[e.u].vertexDegree++;
            V[e.v].vertexDegree++;

            V[e.u].AddEdgeToVertex(e.v, e);
            V[e.v].AddEdgeToVertex(e.u, e);

            // Update triangle degree
            foreach (KeyValuePair<int, Edge> neighbour in V[e.u].neighbours)
            {
                if (V[e.v].neighbours.ContainsKey(neighbour.Key))
                {
                    //calculate triangle probability by multiplying all edge probabilities of the triangle
                    float triangleProbability = e.probability * neighbour.Value.probability * V[e.v].neighbours[neighbour.Key].probability;

                    V[e.u].expectedTriangleDegree += triangleProbability;
                    V[neighbour.Key].expectedTriangleDegree += triangleProbability;
                    V[e.v].expectedTriangleDegree += triangleProbability;
                }
            }
        }

        /// <summary>
        /// Remove an edge from the graph.
        /// </summary>
        /// <param name="e">The edge that is to be removed.</param>
        public override void RemoveEdge(Edge e)
        {
            // Add edge to the list
            RemoveFromEdges(e);

            // Update vertex degree
            V[e.u].vertexDegree--;
            V[e.v].vertexDegree--;

            // Update triangle degree
            foreach (KeyValuePair<int, Edge> neighbour in V[e.u].neighbours)
            {
                if (V[e.v].neighbours.ContainsKey(neighbour.Key))
                {
                    //calculate triangle probability by multiplying all edge probabilities of the triangle
                    float triangleProbability = e.probability * neighbour.Value.probability * V[e.v].neighbours[neighbour.Key].probability;

                    V[e.u].expectedTriangleDegree -= triangleProbability;
                    V[neighbour.Key].expectedTriangleDegree -= triangleProbability;
                    V[e.v].expectedTriangleDegree -= triangleProbability;
                }
            }

            V[e.u].RemoveEdge(e.v);
            V[e.v].RemoveEdge(e.u);
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

            Dictionary<long, int> counters = rawData.GetEdgeCounters();
            float probability = 0.0f;
            foreach (KeyValuePair<long, Edge> e in rawData.GetEdges())
            {
                probability = 1f - (float)Math.Pow(Math.E, (-0.5 * counters[e.Key]));
                e.Value.probability = probability;
                edges.Add(e.Value);
            }

            return new UncertainGraph(vertices, edges);
        }
    }
}
