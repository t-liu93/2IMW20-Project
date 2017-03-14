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
        private List<Edge> edgeList; // Our current edge list
        private Random random;

        public TRPW(UncertainGraph G)
        {
            edgeList = new List<Edge>();
            random = new Random();
        }
    }
}
