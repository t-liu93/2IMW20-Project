using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project
{
    class Edge
    {
        public int id;                  // Edge id
        public int u;                   // Vertex index 
        public int v;                   // Vertex index
        public float probability;       // Uncertain edge probability

        public Edge(int id, int u, int v)
        {
            this.id = id;
            this.u = u;
            this.v = v;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
