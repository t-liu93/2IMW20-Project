using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project.graph
{
    class UncertainEdge : Edge
    {
        public float probability;       // Edge probability

        public UncertainEdge(int id, int u, int v, float probability) : base(id, u, v)
        {
            this.probability = probability;
        }
    }
}
