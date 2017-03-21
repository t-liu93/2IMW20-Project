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
            //Calculate c1 and c2 representing the amount of vertices adjacent to both u1 and v1, and u2 and v2, respectively.
            float c1 = 0f;
            float c2 = 0f;

            float result = 0f;//(Math.Abs(u1.GetTriangleDiscrepancy() - c1)) - Math.Abs(u1.GetTriangleDiscrepancy()) +
                              //(Math.Abs(v1.GetTriangleDiscrepancy() - c1)) - Math.Abs(v1.GetTriangleDiscrepancy()) +
                              //(Math.Abs(u2.GetTriangleDiscrepancy() + c2)) - Math.Abs(u2.GetTriangleDiscrepancy()) +
                              //(Math.Abs(v2.GetTriangleDiscrepancy() + c2)) - Math.Abs(v2.GetTriangleDiscrepancy()) +
                              // Summation of all vertices 'w' adjacent to both u1 and v1 (Math.Abs(w.GetTriangleDiscrepancy() - 1) - Math.Abs(w.GetTriangleDiscrepancy())) +
                              // Summation of all vertices 'w' adjacent to both u2 and v2 (Math.Abs(w.GetTriangleDiscrepancy() + 1) - Math.Abs(w.GetTriangleDiscrepancy()));
            return result;
        }

        public float EquationThree(Vertex u3, Vertex v3)
        {
            //Calculate c3 representing the amount of vertices adjacent to both u3 and v3.

            float result = 0f;//(Math.Abs(u3.GetTriangleDiscrepancy() + c3)) - Math.Abs(u3.GetTriangleDiscrepancy()) +
                              //(Math.Abs(v3.GetTriangleDiscrepancy() + c3)) - Math.Abs(v3.GetTriangleDiscrepancy()) +
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

            foreach (Edge e in _graph.E)
            {
                mG += e.probability;
            }

            // Sort edges according to their probability (3)
            List<Edge> sortedGraphEdges = _graph.E.OrderBy(e => ((Edge)e).probability).ToList();

            // Loop edges untill we reach the expected amount of edges (4)
            while(_edgeList.Count < Math.Floor(mG))
            {
                _edgeListDifference = sortedGraphEdges.Except(_edgeList).ToList();
                Edge e = _edgeListDifference.First();
                Double r = _random.NextDouble();
                if (r <= e.probability)
                    _edgeList.Add(e);
            }
            _graph.E = _edgeList;
            _graph.CalculateDegrees();


            //Phase 2

            int N = 100; // what is N?
            for (i = 1; i < N; i++) // (9)
            {
                _edgeListDifference = sortedGraphEdges.Except(_edgeList).ToList();

                Edge e1 = _edgeList.ElementAt(_random.Next(0, _edgeList.Count()));
                Edge e2 = _edgeListDifference.ElementAt(_random.Next(0, _edgeListDifference.Count()));
                Double d1 = EquationOne(_graph.V[e1.u], _graph.V[e1.v], _graph.V[e2.u], _graph.V[e2.v]);
                Double d2 = EquationTwo(_graph.V[e1.u], _graph.V[e1.v], _graph.V[e2.u], _graph.V[e2.v]);

                Edge e3 = _edgeListDifference.ElementAt(_random.Next(0, _edgeListDifference.Count()));

                if (d1 + d2 < 0) // (14)
                {
                    _graph.RemoveEdge(e1);
                    _graph.AddEdge(e2);
                }

                Double d3 = EquationThree(_graph.V[e3.u], _graph.V[e3.v]);
                if (d3 < 0) // (18)
                {
                    _graph.AddEdge(e3);
                }
            }
            
            //Return an approximate triangle-based representative possible world of G
        }
    }
}
