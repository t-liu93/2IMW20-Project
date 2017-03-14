using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2IMW20_Project
{
    class Vertex
    {
        public int id;
        public int vertexDegree;
        public int triangleDegree;

        public float expectedVertexDegree;
        public float expectedTriangleDegree;

        public Vertex(int id)
        {
            this.id = id;

            this.vertexDegree = 0;
            this.triangleDegree = 0;

            this.expectedVertexDegree = 0f;
            this.expectedTriangleDegree = 0f;
        }
    }
}
