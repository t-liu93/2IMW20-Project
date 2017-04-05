using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2IMW20_Project.graph;

namespace _2IMW20_Project
{
    class TRPW
    {
        private Graph _graph;
        private Graph _reprGraph;
        private float expectedDegree;
        private List<Edge> _edgeList; // Our current edge list
        private List<Edge> _edgeListDifference; // Constantly changing difference of the current edge list and the complete edge list
        private Random _random;

        public TRPW(Graph G)
        {
            // Initialize algorithm
            _edgeList = new List<Edge>();
            _random = new Random();
            _graph = G;
        }

        public float EquationOne(Vertex u1, Vertex v1, Vertex u2, Vertex v2)
        {
            float result = (Math.Abs(u1.GetVertexDiscrepancy() - 1)) - Math.Abs(u1.GetVertexDiscrepancy()) +
                            (Math.Abs(v1.GetVertexDiscrepancy() - 1)) - Math.Abs(v1.GetVertexDiscrepancy()) +
                            (Math.Abs(u2.GetVertexDiscrepancy() + 1)) - Math.Abs(u2.GetVertexDiscrepancy()) +
                            (Math.Abs(v2.GetVertexDiscrepancy() + 1)) - Math.Abs(v2.GetVertexDiscrepancy()); 
            return result;
        }

        public float EquationTwo(Vertex u1, Vertex v1, Vertex u2, Vertex v2)
        {
            float c1 = 0f;
            float c2 = 0f;
            float errorAdjacent1 = 0f;
            float errorAdjacent2 = 0f;

            foreach (int neighbour in u1.neighbours.Keys)
            {
                if (v1.neighbours.ContainsKey(neighbour))
                {
                    c1++;
                    errorAdjacent1 += (Math.Abs(_reprGraph.V[neighbour].GetTriangleDiscrepancy() - 1) - Math.Abs(_reprGraph.V[neighbour].GetTriangleDiscrepancy()));
                }
            }
            foreach (int neighbour in u2.neighbours.Keys)
            {
                if (v2.neighbours.ContainsKey(neighbour))
                {
                    c2++;
                    errorAdjacent2 += (Math.Abs(_reprGraph.V[neighbour].GetTriangleDiscrepancy() + 1) - Math.Abs(_reprGraph.V[neighbour].GetTriangleDiscrepancy()));
                }
            }

            //Calculate c1 and c2 representing the amount of vertices adjacent to both u1 and v1, and u2 and v2, respectively.

            float result = (Math.Abs(u1.GetTriangleDiscrepancy() - c1)) - Math.Abs(u1.GetTriangleDiscrepancy()) +
                              (Math.Abs(v1.GetTriangleDiscrepancy() - c1)) - Math.Abs(v1.GetTriangleDiscrepancy()) +
                              (Math.Abs(u2.GetTriangleDiscrepancy() + c2)) - Math.Abs(u2.GetTriangleDiscrepancy()) +
                              (Math.Abs(v2.GetTriangleDiscrepancy() + c2)) - Math.Abs(v2.GetTriangleDiscrepancy()) +
                              errorAdjacent1 +
                              errorAdjacent2;
                            // Summation of all vertices 'w' adjacent to both u1 and v1 (Math.Abs(w.GetTriangleDiscrepancy() - 1) - Math.Abs(w.GetTriangleDiscrepancy())) +
                            // Summation of all vertices 'w' adjacent to both u2 and v2 (Math.Abs(w.GetTriangleDiscrepancy() + 1) - Math.Abs(w.GetTriangleDiscrepancy()));
            return result;
        }

        public float EquationThree(Vertex u3, Vertex v3)
        {
            //Calculate c3 representing the amount of vertices adjacent to both u3 and v3.
            float c3 = 0f;
            float errorAdjacent3 = 0f;
            foreach (int neighbour in u3.neighbours.Keys)
            {
                if (v3.neighbours.ContainsKey(neighbour))
                {
                    c3++;
                    errorAdjacent3 += (Math.Abs(_reprGraph.V[neighbour].GetTriangleDiscrepancy() + 1) - Math.Abs(_reprGraph.V[neighbour].GetTriangleDiscrepancy()));
                }
            }

            float result = (Math.Abs(u3.GetTriangleDiscrepancy() + c3)) - Math.Abs(u3.GetTriangleDiscrepancy()) +
                              (Math.Abs(v3.GetTriangleDiscrepancy() + c3)) - Math.Abs(v3.GetTriangleDiscrepancy()) +
                              errorAdjacent3;
                              // Summation of all vertices 'w' adjacent to both u3 and v3 (Math.Abs(w.GetTriangleDiscrepancy() + 1) - Math.Abs(w.GetTriangleDiscrepancy()));
            return result;
        }

        public void Run()
        {
            //Phase 1

            // Initialize (1)
            int i = 0;
            _edgeList = new List<Edge>();

            // Calculate total probability (2)
            float mG = 0f;


            foreach (Edge edge in _graph.E)
            {
                mG += edge.probability;
            }
            expectedDegree = mG * 2;

            // Sort edges according to their probability (3)
            List<Edge> sortedGraphEdges = _graph.E.OrderByDescending(edge => ((Edge)edge).probability).ToList();

            // Loop edges untill we reach the expected amount of edges (4)

            _edgeListDifference = sortedGraphEdges.Except(_edgeList).ToList();
            int next = 0;
            Edge e = null;
            Double r = 0.0;

            while (_edgeList.Count < Math.Floor(mG))
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

            //Phase 2
            Edge e1 = null;
            Edge e2 = null;
            Edge e3 = null;
            Double d1 = 0.0;
            Double d2 = 0.0;
            Double d3 = 0.0;

            int N = 1000000; // what is N?
            for (i = 1; i < N; i++) // (9)
            {
                e1 = _reprGraph.E.ElementAt(_random.Next(0, _edgeList.Count()));
                e2 = _edgeListDifference.ElementAt(_random.Next(0, _edgeListDifference.Count()));
                d1 = EquationOne(_reprGraph.V[e1.u], _reprGraph.V[e1.v], _reprGraph.V[e2.u], _reprGraph.V[e2.v]);
                d2 = EquationTwo(_reprGraph.V[e1.u], _reprGraph.V[e1.v], _reprGraph.V[e2.u], _reprGraph.V[e2.v]);

                e3 = _edgeListDifference.ElementAt(_random.Next(0, _edgeListDifference.Count()));

                if (d1 + d2 < 0) // (14)
                {
                    _edgeListDifference.Remove(e2);
                    _edgeListDifference.Add(e1);
                    _reprGraph.RemoveEdge(e1);
                    _reprGraph.AddEdge(e2);
                }

                d3 = EquationThree(_reprGraph.V[e3.u], _reprGraph.V[e3.v]);
                if (d3 < 0) // (18)
                {
                    _edgeListDifference.Remove(e3);
                    _reprGraph.AddEdge(e3);
                }
            }
        }
        public void PrintResults()
        {
            Console.WriteLine("Uncertain Graph : ");
            Console.WriteLine("Amount of Vertices  : " + _graph.V.Count());
            Console.WriteLine("Amount of Edges  : " + _graph.E.Count());
            Console.WriteLine("Expected Degree  : " + ((expectedDegree / _graph.V.Count())) + "\n");
            WriteFile.CsvWriterTRPW csvWriter = new WriteFile.CsvWriterTRPW();

            float triangleDegree = 0f;
            float clusteringCoefficient = 0f;
            foreach (Vertex v in _graph.V.Values)
            {
                triangleDegree += v.expectedTriangleDegree;
                if (v.expectedVertexDegree > 1)
                    clusteringCoefficient += (v.expectedTriangleDegree * 2) / (v.expectedVertexDegree * (v.expectedVertexDegree - 1));

                csvWriter.AppendExpectedDegree(string.Format("{0}, {1}", v.id, v.expectedVertexDegree));
                csvWriter.AppendExpectedTriangleDegree(string.Format("{0}, {1}", v.id, v.expectedTriangleDegree));
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
                    clusteringCoefficient += (v.triangleDegree * 2) / (v.vertexDegree * (v.vertexDegree - 1));

                csvWriter.AppendActualDegree(string.Format("{0}, {1}", v.id, v.vertexDegree));
                csvWriter.AppendActualTriangleDegree(string.Format("{0}, {1}", v.id, v.triangleDegree));
            }
            Console.WriteLine("Actual Triangle Degree  : " + ((triangleDegree / _graph.V.Count())) + "\n");
            Console.WriteLine("Actual Clustering Coefficient  : " + ((clusteringCoefficient / _graph.V.Count())) + "\n");

            csvWriter.WriteFile();
        }
    }
}
