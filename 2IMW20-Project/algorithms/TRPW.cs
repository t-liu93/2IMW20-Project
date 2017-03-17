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
        private UncertainGraph _graph;
        private List<Edge> _edgeList; // Our current edge list
        private Random _random;

        public TRPW(UncertainGraph G)
        {
            // Initialize algorithm
            _edgeList = new List<Edge>();
            _random = new Random();
            _graph = G;

         
        }

        public void Run()
        {
            // Initialize (1)
            int i = 0;
            _edgeList = new List<Edge>();

            // Calculate total probability (2)
            float mG = 0f;

            foreach (UncertainEdge e in _graph.E)
            {
                mG += e.probability;
            }

            // Sort edges according to their probability (3)
           // List<UncertainEdge> sortedGraphEdges = _graph.E.OrderBy(e => ((UncertainEdge)e).probability).ToList();


            int N = 0; // what is N?
            for (i = 1; i < N; i++) // (9)
            {

            }

            
        }
    }
}
