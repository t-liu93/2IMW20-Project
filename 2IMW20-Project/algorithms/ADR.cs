﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2IMW20_Project.graph;

namespace _2IMW20_Project
{
    class ADR
    {
        private UncertainGraph _graph;
        private Graph _reprGraph;
        private float expectedDegree;
        private List<Edge> _edgeList; // Our current edge list
        private List<Edge> _edgeListDifference; // Constantly changing difference of the current edge list and the complete edge list
        private Random _random;

        public ADR(UncertainGraph G)
        {
            // Initialize algorithm
            _edgeList = new List<Edge>();
            _random = new Random();
            _graph = G;
        }

        public float EquationOne(Vertex u1, Vertex v1)
        {
            float result = (Math.Abs(u1.GetVertexDiscrepancy() - 1)) + (Math.Abs(v1.GetVertexDiscrepancy() - 1)) -
                            (Math.Abs(u1.GetVertexDiscrepancy()) + Math.Abs(v1.GetVertexDiscrepancy()));
            return result;
        }

        public float EquationTwo(Vertex u2, Vertex v2)
        {
            float result = (Math.Abs(u2.GetVertexDiscrepancy() + 1)) + (Math.Abs(v2.GetVertexDiscrepancy() + 1)) -
                            (Math.Abs(u2.GetVertexDiscrepancy()) + Math.Abs(v2.GetVertexDiscrepancy()));
            return result;
        }

        public void Run()
        {
            // Phase 1

            // Initialize (1)
            int i = 0;
            _edgeList = new List<Edge>();

            // Calculate total probability (2)
            float P = 0f;

            foreach (Edge edge in _graph.E)
            {
                P += edge.probability;
            }
            expectedDegree = P;

            // Sort edges according to their probability (3)
            List<Edge> sortedGraphEdges = _graph.E.OrderByDescending(edge => ((Edge)edge).probability).ToList();

            // Loop edges untill we reach the expected amount of edges (4)
            _edgeListDifference = sortedGraphEdges.Except(_edgeList).ToList();
            int next = 0;

            Edge e = null;
            Double r = 0.0;

            while (_edgeList.Count < Math.Round(P))
            {
                e = _edgeListDifference.ElementAt(next);
                r = _random.NextDouble();

                if (r <= e.probability)
                    _edgeList.Add(e);

                next++;
                if (next >= _edgeListDifference.Count())
                {
                    _edgeListDifference = sortedGraphEdges.Except(_edgeList).ToList();
                    next = 0;
                }
            }

            _reprGraph = new Graph(_graph.V, _edgeList);
            _edgeListDifference = sortedGraphEdges.Except(_edgeList).ToList();


            // Phase 2
            Edge e1 = null;
            Edge e2 = null;
            Double d1 = 0.0;
            Double d2 = 0.0;
            int steps = 100; // what is steps?
            for (i = 1; i < steps; i++) // (9)
            {
                for (int j = 0; j < _reprGraph.V.Values.Count(); j++)
                {
                    if (_reprGraph.V[j].neighbours.Count() == 0)
                        continue;

                    //Find all edges with vertex u and take one random edge from this
                    e1 = _reprGraph.V[j].neighbours.ElementAt(_random.Next(_reprGraph.V[j].neighbours.Count())).Value;//_reprGraph.E.Where(edge => edge.u == _reprGraph.V[j].id || edge.v == _reprGraph.V[j].id).ElementAt(_random.Next(_reprGraph.V[j].neighbours.Count() - 1));
                    e2 = _edgeListDifference.ElementAt(_random.Next(0, _edgeListDifference.Count()));

                    d1 = EquationOne(_reprGraph.V[e1.u], _reprGraph.V[e1.v]);
                    d2 = EquationTwo(_reprGraph.V[e2.u], _reprGraph.V[e2.v]);
                    

                    if (d1 + d2 < 0) // (14)
                    {
                        _reprGraph.RemoveEdge(e1);
                        _reprGraph.AddEdge(e2);
                        _edgeListDifference.Remove(e2);
                        _edgeListDifference.Add(e1);
                    }
                }
            }
        }

        public void PrintResults()
        {
            Console.WriteLine("Uncertain Graph : ");
            Console.WriteLine("Amount of Vertices  : " + _graph.V.Count());
            Console.WriteLine("Amount of Edges  : " + _graph.E.Count());
            Console.WriteLine("Expected Degree  : " + ((expectedDegree / _graph.V.Count()) * 2) + "\n");
            WriteFile.CsvWriterADR csvWriter = new WriteFile.CsvWriterADR();

            float triangleDegree = 0f;
            float clusteringCoefficient = 0f;
            foreach (Vertex v in _graph.V.Values)
            {
                triangleDegree += v.expectedTriangleDegree;
                if (v.expectedVertexDegree > 1)
                {
                    clusteringCoefficient += (v.expectedTriangleDegree * 2) / (v.expectedVertexDegree * (v.expectedVertexDegree - 1));
                    csvWriter.AppendExpectedCoefficient(string.Format("{0}", (v.expectedTriangleDegree * 2) / (v.expectedVertexDegree * (v.expectedVertexDegree - 1))));
                }

                csvWriter.AppendExpectedDegree(string.Format("{0}", v.expectedVertexDegree));
                csvWriter.AppendExpectedTriangleDegree(string.Format("{0}", v.expectedTriangleDegree));
                
            }

            Console.WriteLine("Expected Triangle Degree  : " + ((triangleDegree / _graph.V.Count())) + "\n");
            Console.WriteLine("Expected Clustering Coefficient  : " + ((clusteringCoefficient / _graph.V.Count())) + "\n");

            Console.WriteLine("Representative Graph : ");
            Console.WriteLine("Amount of Edges  : " + _reprGraph.E.Count());
            Console.WriteLine("Actual Degree  : " + (((float)_reprGraph.E.Count() / (float)_reprGraph.V.Count()) * 2) + "\n");
            triangleDegree = 0f;
            clusteringCoefficient = 0f;
            foreach (Vertex v in _reprGraph.V.Values)
            {
                triangleDegree += v.triangleDegree;
                if (v.vertexDegree > 1)
                {
                    clusteringCoefficient += (v.triangleDegree * 2) / (v.vertexDegree * (v.vertexDegree - 1));
                    csvWriter.AppendActualCoefficient(string.Format("{0}", (v.triangleDegree * 2) / (v.vertexDegree * (v.vertexDegree - 1))));
                }

                csvWriter.AppendActualDegree(string.Format("{0}", v.vertexDegree));
                csvWriter.AppendActualTriangleDegree(string.Format("{0}", v.triangleDegree));
                
            }
            Console.WriteLine("Actual Triangle Degree  : " + ((triangleDegree / _graph.V.Count())) + "\n");
            Console.WriteLine("Actual Clustering Coefficient  : " + ((clusteringCoefficient / _graph.V.Count())) + "\n");

            csvWriter.WriteFile();
        }
    }
}
